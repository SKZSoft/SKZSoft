using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SKZTweets.TwitterData;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZTweets.TwitterJobs;
using SKZTweets.TwitterData.Models;
using SKZTweets.TwitterModels;

namespace SKZTweets.TwitterData
{
    public class ThreadPoster
    {
        private int m_total = 0;
        private int m_sent = 0;
        private Queue<Status> m_tweets;
        private TwitterData m_twitterData;
        private JobBatchRoot m_rootBatch;
        private JobFactory m_jobFactory;
        private Status m_inReplyTo;
        
        #region Events
        /// <summary>
        /// Raised when the counter increases (ie another tweet has been sent)
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler<ThreadProgressUpdateArgs> ThreadProgressUpdate;
        protected virtual void OnThreadProgressUpdate(ThreadProgressUpdateArgs e)
        {
            EventHandler<ThreadProgressUpdateArgs> handler = ThreadProgressUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Raised when thread is cancelled
        /// </summary>
        public event EventHandler<EventArgs> ThreadCancelled;
        protected virtual void OnThreadCancelled()
        {
            EventHandler<EventArgs> handler = ThreadCancelled;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }


        /// <summary>
        /// Raised when the thread's tweets have been deleted
        /// </summary>
        public event EventHandler<EventArgs> ThreadDeleted;
        protected virtual void OnThreadDeleted()
        {
            EventHandler<EventArgs> handler = ThreadDeleted;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public event EventHandler<JobExceptionArgs> ExceptionRaised;
        protected virtual void OnException(Exception ex, Job job)
        {
            EventHandler<JobExceptionArgs> handler = ExceptionRaised;
            if (handler != null)
            {
                handler(this, new JobExceptionArgs(ex, job));
            }
        }

        /// <summary>
        /// Raised when all tweets sent
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler<ThreadCompleteArgs> ThreadComplete;
        protected virtual void OnThreadComplete(JobBatchRoot rootBatch)
        {
            EventHandler<ThreadCompleteArgs> handler = ThreadComplete;
            if (handler != null)
            {
                ThreadCompleteArgs e = new ThreadCompleteArgs(rootBatch, m_sent);
                handler(this, e);
            }
        }

        #endregion

        internal ThreadPoster(TwitterData twitterData, JobFactory jobFactory, Queue<Status> tweets, Status replyTo)
        {
            try
            {
                theLog.Log.LevelDown();
                m_tweets = tweets;
                m_twitterData = twitterData;
                m_jobFactory = jobFactory;
                m_inReplyTo = replyTo;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="twitterData"></param>
        /// <param name="tweets"></param>
        internal ThreadPoster(TwitterData twitterData, JobFactory jobFactory, Queue<Status> tweets) : this(twitterData, jobFactory, tweets, null)
        {
        }

        /// <summary>
        /// Delete all tweets which have been sent by this instance
        /// </summary>
        /// <returns></returns>
        public void DeleteAll(JobBatchRoot batchToDelete)
        {
            try
            {
                theLog.Log.LevelDown();

                if(batchToDelete == null)
                {
                    throw new NullReferenceException("No batch specified for deletion");
                }

                JobBatch rootBatch = m_jobFactory.CreateRootBatch(DeleteBatchComplete, JobExceptionRaised);
                List<Job> jobs = batchToDelete.GetAllJobs(true);

                foreach (Job job in jobs)
                {
                    if (job is JobStatusUpdate)
                    {
                        JobStatusUpdate castJob = (JobStatusUpdate)job;
                        JobDestroy jobDestroy = rootBatch.CreateDestroy(DeleteJobCompleted, castJob.NewStatus.id);
                    }
                }

                rootBatch.RunBatch();

                return;
            }

            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Number of statuses which were posted
        /// </summary>
        /// <returns></returns>
        public int PostedCount { get { return m_rootBatch.CompletedJobs.Count; } }

        private void DeleteBatchComplete(object sender, BatchCompleteArgs e)
        {
            OnThreadDeleted();
        }


        private void DeleteJobCompleted(object sender, JobCompleteArgs e)
        {
            JobDestroy job = (JobDestroy)e.Job;
        }

        private List<Media> ExtractMediaItems(Status status)
        {
            List<Media> mediaItems = new List<Media>();
            if (status.extended_entities != null & status.extended_entities.media != null)
            {
                foreach (Media media in status.extended_entities.media)
                {
                    mediaItems.Add(media);
                }
            }
            return mediaItems;
        }

        private JobBatchStatusWithImages GetJobWithImages(JobBatch batch, Status status, Status replyTo)
        {
            List<Media> mediaItems = ExtractMediaItems(status);
            JobBatchStatusWithImages job = batch.CreateJobStatusWithImages(null, mediaItems, status.text, replyTo);
            return job;
        }


        private JobBatchStatusWithImages GetJobWithImages(JobBatch batch, Status status, JobBatchStatusWithImages replyTo)
        {
            List<Media> mediaItems = ExtractMediaItems(status);
            JobBatchStatusWithImages job = batch.CreateJobStatusWithImages(null, mediaItems, status.text, replyTo);
            return job;
        }

        public void PostThreadBegin(int millisecondsBetweenTweets)
        {
            try
            {
                theLog.Log.LevelDown();

                System.Diagnostics.Debug.WriteLine("PostThreadBegin");

                m_total = m_tweets.Count;

                MilliSecondsBetweenTweets = millisecondsBetweenTweets;

                m_rootBatch = m_jobFactory.CreateRootBatch(PostBatchComplete, JobExceptionRaised);
                m_rootBatch.BatchProgress += M_BatchProgress;
                m_rootBatch.Cancelled += M_Cancelled;
                Status firstTweet = m_tweets.Dequeue();
                JobBatchStatusWithImages previousJob = GetJobWithImages(m_rootBatch, firstTweet, m_inReplyTo);

                while (m_tweets.Count > 0)
                {
                    Status nextTweet = m_tweets.Dequeue();
                    JobBatchStatusWithImages nextJob = GetJobWithImages(m_rootBatch, nextTweet, previousJob);
                    nextJob.DelayBefore = millisecondsBetweenTweets;
                    previousJob = nextJob;
                }

                System.Diagnostics.Debug.WriteLine("RunBatch");
                m_rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_BatchProgress(object sender, BatchProgressArgs e)
        {
            ThreadProgressUpdateArgs args = new ThreadProgressUpdateArgs(e.CompletedJob, e.TotalJobsCompleted, e.TotalJobs);
            OnThreadProgressUpdate(args);
        }

        private void PostBatchComplete(object sender, BatchCompleteArgs e)
        {
            OnThreadComplete(e.Batch.RootBatch);
        }



        /// <summary>
        /// Terminate everything that is going on.
        /// </summary>
        public void Terminate()
        {
            try
            {
                theLog.Log.LevelDown();
            }
            finally { theLog.Log.LevelUp(); }
        }

        public void Cancel()
        {
            try
            {
                theLog.Log.LevelDown();

                m_rootBatch.Cancel();

                // Cancelling may not happen immediately, so we only raise it back up when we get notified that the root batch has confirmed cancellation
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_Cancelled(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Batch has notified us that it got cancelled. Inform owner.
                OnThreadCancelled();
            }
            finally { theLog.Log.LevelUp(); }

        }

        void JobExceptionRaised(object sender, JobExceptionArgs e)
        {
            OnException(e.Exception, e.Job);
        }


        private int MilliSecondsBetweenTweets { get; set; }
    }
}
