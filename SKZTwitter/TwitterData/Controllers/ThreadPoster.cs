using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SKZSoft.Twitter.TwitterData;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterData
{
    public class ThreadPoster
    {
        private int m_total = 0;
        private int m_sent = 0;
        private Queue<Status> m_tweets;
        private TwitterData m_twitterData;
        private BatchRoot m_rootBatch;
        private BatchFactory m_batchFactory;
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
        protected virtual void OnThreadComplete(BatchRoot rootBatch)
        {
            EventHandler<ThreadCompleteArgs> handler = ThreadComplete;
            if (handler != null)
            {
                ThreadCompleteArgs e = new ThreadCompleteArgs(rootBatch, m_sent);
                handler(this, e);
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="twitterData"></param>
        /// <param name="jobFactory"></param>
        /// <param name="tweets"></param>
        /// <param name="replyTo"></param>
        internal ThreadPoster(TwitterData twitterData, BatchFactory batchFactory, Queue<Status> tweets, Status replyTo)
        {
            try
            {
                theLog.Log.LevelDown();
                m_tweets = tweets;
                m_twitterData = twitterData;
                m_batchFactory = batchFactory;
                m_inReplyTo = replyTo;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="twitterData"></param>
        /// <param name="tweets"></param>
        internal ThreadPoster(TwitterData twitterData, BatchFactory batchFactory, Queue<Status> tweets) : this(twitterData, batchFactory, tweets, null)
        {
        }

        /// <summary>
        /// Delete all tweets which have been sent by this instance
        /// </summary>
        /// <returns></returns>
        public void DeleteAll(BatchRoot batchToDelete)
        {
            try
            {
                theLog.Log.LevelDown();

                if(batchToDelete == null)
                {
                    throw new NullReferenceException("No batch specified for deletion");
                }

                Batch rootBatch = m_batchFactory.CreateRootBatch(batchToDelete.Credentials, DeleteBatchComplete, JobExceptionRaised);
                List<Job> jobs = batchToDelete.GetAllJobs(true);

                foreach (Job job in jobs)
                {
                    if (job is TwitterJobs.Jobs.Statuses.Update)
                    {
                        TwitterJobs.Jobs.Statuses.Update castJob = (TwitterJobs.Jobs.Statuses.Update)job;
                        TwitterJobs.Jobs.Statuses.Destroy jobDestroy = rootBatch.CreateDestroy(DeleteJobCompleted, castJob.NewStatus.id);
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

        /// <summary>
        /// Notify that Delete Batch has completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBatchComplete(object sender, BatchCompleteArgs e)
        {
            OnThreadDeleted();
        }


        /// <summary>
        /// Notify that a single tweet has been deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteJobCompleted(object sender, JobCompleteArgs e)
        {
            TwitterJobs.Jobs.Statuses.Destroy job = (TwitterJobs.Jobs.Statuses.Destroy)e.Job;
        }

        /// <summary>
        /// Get the media items from a Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a batch of status posts which include images
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="status"></param>
        /// <param name="replyTo"></param>
        /// <returns></returns>
        private TwitterJobs.Jobs.Statuses.BatchWithImages CreateJobWithImages(Batch batch, Status status, Status replyTo)
        {
            List<Media> mediaItems = ExtractMediaItems(status);
            TwitterJobs.Jobs.Statuses.BatchWithImages job = batch.CreateJobStatusWithImages(null, mediaItems, status.text, replyTo);
            return job;
        }


        /// <summary>
        /// Create a single job with images
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="status"></param>
        /// <param name="replyTo"></param>
        /// <returns></returns>
        private TwitterJobs.Jobs.Statuses.BatchWithImages CreateJobWithImages(Batch batch, Status status, TwitterJobs.Jobs.Statuses.BatchWithImages replyTo)
        {
            List<Media> mediaItems = ExtractMediaItems(status);
            TwitterJobs.Jobs.Statuses.BatchWithImages job = batch.CreateJobStatusWithImages(null, mediaItems, status.text, replyTo);
            return job;
        }

        /// <summary>
        /// Start the posting of the thread.
        /// </summary>
        /// <param name="millisecondsBetweenTweets"></param>
        public void PostThreadBegin(Credentials credentials, int millisecondsBetweenTweets)
        {
            try
            {
                theLog.Log.LevelDown();

                System.Diagnostics.Debug.WriteLine("PostThreadBegin");

                m_total = m_tweets.Count;

                MilliSecondsBetweenTweets = millisecondsBetweenTweets;

                m_rootBatch = m_batchFactory.CreateRootBatch(credentials, PostBatchComplete, JobExceptionRaised);
                m_rootBatch.BatchProgress += M_BatchProgress;
                m_rootBatch.Cancelled += M_Cancelled;
                Status firstTweet = m_tweets.Dequeue();

                // prep the first job as the "previous job"
                TwitterJobs.Jobs.Statuses.BatchWithImages previousJob = CreateJobWithImages(m_rootBatch, firstTweet, m_inReplyTo);

                while (m_tweets.Count > 0)
                {
                    // create next job, passing in its previous job to link them together
                    Status nextTweet = m_tweets.Dequeue();
                    TwitterJobs.Jobs.Statuses.BatchWithImages nextJob = CreateJobWithImages(m_rootBatch, nextTweet, previousJob);
                    nextJob.DelayBefore = millisecondsBetweenTweets;

                    previousJob = nextJob;
                };

                System.Diagnostics.Debug.WriteLine("RunBatch");
                m_rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Notify of progress within the batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_BatchProgress(object sender, BatchProgressArgs e)
        {
            ThreadProgressUpdateArgs args = new ThreadProgressUpdateArgs(e.CompletedJob, e.TotalJobsCompleted, e.TotalJobs);
            OnThreadProgressUpdate(args);
        }

        /// <summary>
        /// Notify that the batch to post statuses has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Cancel any posting activity
        /// </summary>
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

        /// <summary>
        /// Notify of cancellation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handle exceptions by passing back to the owner. Let it deal with it at the GUI level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void JobExceptionRaised(object sender, JobExceptionArgs e)
        {
            OnException(e.Exception, e.Job);
        }


        private int MilliSecondsBetweenTweets { get; set; }
    }
}
