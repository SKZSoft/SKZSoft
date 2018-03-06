using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using SKZTweets.TwitterModels;
using SKZTweets.TwitterJobs.Interfaces;

namespace SKZTweets.TwitterJobs
{
    public class JobBatch : Job
    {
        private int m_totalJobs;
        protected int m_jobCompletedCount;
        private Dictionary<string, object> m_globals;
        protected bool m_cancelled;
        private bool m_idling;
        private Queue<Job> m_jobsQueue;
        protected List<Job> m_completeJobs;
        private List<Job> m_allJobs;
        private Job m_lastJob;
        private Job m_nextJob;
        private Timer m_timer;
        protected JobFactory m_jobFactory;
        protected IJobRunner m_jobRunner;


        internal JobFactory JobFactory { get { return m_jobFactory; } }
        public string AuthConsumerKey { get; set; }
        public string AuthCallBack { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="completionDelegate"></param>
        internal JobBatch(IJobRunner jobRunner, EventHandler<BatchCompleteArgs> completionDelegate) : base(null)
        {
            m_jobRunner = jobRunner;
            m_jobsQueue = new Queue<Job>();
            m_allJobs = new List<Job>();
            m_idling = true;
            m_globals = new Dictionary<string, object>();
            BatchCompleted += completionDelegate;
        }

        /// <summary>
        /// Raised when batch is completed.
        /// </summary>
        public event EventHandler<BatchCompleteArgs> BatchCompleted;

        /// <summary>
        /// Notify the delegate method of job completion
        /// </summary>
        internal virtual void OnBatchCompleted()
        {
            EventHandler<BatchCompleteArgs> handler = BatchCompleted;
            BatchCompleteArgs args = new BatchCompleteArgs(this);

            if (handler != null)
            {
                handler(this, args);
            }

            // ideally this ought to go in that class, but that makes for messy overrides.
            if(IsRootBatch)
            {
                Terminate();
            }
        }



        /// <summary>
        /// The jobs which were completed as part of a batch
        /// </summary>
        public List<Job> CompletedJobs { get { return m_completeJobs; } }


        /// <summary>
        /// Request batch is cancelled.
        /// Caller must wait for cancelled event to be raised, because some jobs may still be in progress.
        /// </summary>
        public void Cancel()
        {
            try
            {
                theLog.Log.LevelDown();
                KillTimer();
                m_cancelled = true;

                // cancel the current job (if it's a batch)
                if (m_nextJob != null && m_nextJob is JobBatch)
                {
                    JobBatch jobBatch = (JobBatch)m_nextJob;
                    jobBatch.Cancel();
                }

                // feed cancellations down to all child batches
                foreach (Job job in m_jobsQueue)
                {
                    if (job is JobBatch)
                    {
                        JobBatch jobBatch = (JobBatch)job;
                        jobBatch.Cancel();
                    }
                }

                // if we were idling, go straight to raising the cancellation event
                if (m_idling)
                {
                    KillTimer();
                    OnCancelled();
                    return;
                }

                // a job is in progress. Wait for delegate to fire with the job results, so that the caller can roll it back if necessary
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Raised if the Batch is cancelled.
        /// </summary>
        public EventHandler<EventArgs> Cancelled;
        protected virtual void OnCancelled()
        {
            EventHandler<EventArgs> handler = Cancelled;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }


        private void Job_BatchCompleted(object sender, BatchCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                // a child batch has completed its work
                // That's just the same as a normal-level job completing.
                JobCompleteArgs newArgs = new JobCompleteArgs((Job)e.Batch);
                JobCompleted(this, newArgs);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Job has completed. This method is called as the first priority, before anything else is notified of completion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobCompleted(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                // no jobs currently pending
                m_idling = true;

                // previous job has completed.
                // Firstly, notify the owner of the batch.
                // Then, kick off the next job or notify of overall completion
                m_lastJob = e.Job;
                m_completeJobs.Add(m_lastJob);

                // If we got cancelled, just bug out now.
                if (m_cancelled)
                {
                    // ChildCompleted in the root batch will take care of raising the cancellation event.
                    // For now, just bug out and do not start another job.
                    return;
                }

                // finished?
                if (m_jobsQueue.Count == 0)
                {
                    OnBatchCompleted();
                    return;
                }

                // not finished. Kick off the next job, if there is one.
                m_nextJob = m_jobsQueue.Dequeue();

                // feed the next job any data it might need from the last job.
                m_nextJob.InitializeFromLastJob(m_lastJob);

                DoRun();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Run all the jobs in this batch
        /// </summary>
        public void RunBatch()
        {
            try
            {
                theLog.Log.LevelDown();
                if (m_jobsQueue.Count == 0)
                {
                    throw new InvalidOperationException("No jobs in this batch");
                }

                m_completeJobs = new List<Job>();
                m_totalJobs = GetNoOfJobs;          // this won't change during the course of the run. Safe to get it once, up-front.

                // get first job from the queue.
                m_nextJob = m_jobsQueue.Dequeue();
                DoRun();
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Run next job (without initialisation for batch)
        /// </summary>
        private void DoRun()
        {
            try
            {
                theLog.Log.LevelDown();
                System.Diagnostics.Debug.WriteLine("DoRun");
                if (m_nextJob.Skipped)
                {
                    // this job has set itself to be skipped.
                    // Proceed right along to the next one.
                    // Let's just pretend it completed.
                    JobCompleted(this, new JobCompleteArgs(m_nextJob));
                    return;
                }

                // is it to be run immediately?
                if (m_nextJob.DelayBefore == 0)
                {
                    RunNextJob();
                    return;
                }

                // needs a delay
                StartTimer(m_nextJob.DelayBefore);
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Timer has expired. Time to run next job, which was delayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                KillTimer();

                // next job is due.
                RunNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        ///  This job is really, actually kicking off NOW.
        /// </summary>
        private void RunNextJob()
        {
            try
            {
                theLog.Log.LevelDown();
                // refuse to run any more if cancelled
                if (m_cancelled)
                {
                    return;
                }

                m_idling = false;
                if (m_nextJob is JobBatch)
                {
                    System.Diagnostics.Debug.WriteLine("Run a sub-batch");
                    JobBatch batch = (JobBatch)m_nextJob;
                    batch.RunBatch();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Run a job");
                    TwitterJob castJob = (TwitterJob)m_nextJob;
                    m_jobRunner.RunJob(castJob);
                }
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Start timer for next job
        /// </summary>
        /// <param name="interval"></param>
        private void StartTimer(int interval)
        {
            try
            {
                theLog.Log.LevelDown();
                m_timer = new Timer();
                m_timer.Interval = interval;
                m_timer.Elapsed += M_timer_Elapsed;
                m_timer.Start();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Kill the timer for the next job
        /// </summary>
        private void KillTimer()
        {
            try
            {
                theLog.Log.LevelDown();
                if (m_timer != null)
                {
                    m_timer.Elapsed -= M_timer_Elapsed;
                    m_timer = null;
                }
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Not implemented. Required by Job interface.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {

        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="results"></param>
        public override void Finalize(string results)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Human-readable description
        /// </summary>
        public override string JobDescription { get { return TwitterDataStrings.JobDescBatch; } }


        #region Factory methods
        /// <summary>
        /// Create a job for deleting a tweet or a RT event as part of this batch
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public JobDestroy CreateDestroy(EventHandler<JobCompleteArgs> completionDelegate, ulong statusId)
        {
            JobDestroy job = new JobDestroy(completionDelegate, statusId);
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a child batch as part of this batch
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public JobBatch CreateJobBatch(EventHandler<BatchCompleteArgs> completionDelegate)
        {
            JobBatch job = new JobBatch(m_jobRunner, completionDelegate);
            job.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job to post a media image as part of this batch
        /// </summary>
        /// <param name="completeionDelegate"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public JobPostMedia CreateJobPostMedia(EventHandler<JobCompleteArgs> completeionDelegate, string filePath)
        {
            JobPostMedia job = new JobPostMedia(completeionDelegate, filePath);
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job to delete a RT event (as part of this batch) but taking the ID of the tweet from the job which
        /// executes immediately before this one.
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public JobDestroy CreateDestoryRTOfPrevious(EventHandler<JobCompleteArgs> completionDelegate)
        {
            JobDestoryRTOfPrevious job = new JobDestoryRTOfPrevious(completionDelegate);
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the Access Token from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="pin"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public JobGetAccessToken CreateGetAccessToken(EventHandler<JobCompleteArgs> completionDelegate, string pin, string authToken)
        {
            JobGetAccessToken job = new JobGetAccessToken(completionDelegate);
            job.AuthVerifier = pin;
            job.AuthToken = authToken;
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the authoenticationtokens from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public JobGetAuthToken CreateGetAuthToken(EventHandler<JobCompleteArgs> completionDelegate)
        {
            JobGetAuthToken job = new JobGetAuthToken(completionDelegate);
            InitialiseJob(job);
            return job;
        }


        public JobGetMentions CreateGetMentions(EventHandler<JobCompleteArgs> completionDelegate, int count)
        {
            JobGetMentions job = new JobGetMentions(completionDelegate, count);
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to return a single tweet details
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="id"></param>
        /// <param name="includeMyRetweet"></param>
        /// <returns></returns>
        public JobGetStatus CreateGetStatus(EventHandler<JobCompleteArgs> completionDelegate, ulong id, bool includeMyRetweet)
        {
            JobGetStatus job = new JobGetStatus(completionDelegate, id, includeMyRetweet);
            InitialiseJob(job);

            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the Twitter configuration from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public JobGetTwitterConfig CreateGetTwitterConfig(EventHandler<JobCompleteArgs> completionDelegate)
        {
            JobGetTwitterConfig job = new JobGetTwitterConfig(completionDelegate);
            InitialiseJob(job);
            return job;
        }


        /// <summary>
        /// Create a job (as part of this batch) to return latest tweets from a specified user
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="screenname"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public JobGetUserTimeline CreateGetUserTimeline(EventHandler<JobCompleteArgs> completionDelegate, string screenname, int count)
        {
            JobGetUserTimeline job = new JobGetUserTimeline(completionDelegate, screenname, count);
            InitialiseJob(job);
            return job;
        }


        /// <summary>
        /// Create a job (as part of this batch) to retweet a tweet
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobRetweet CreateRetweet(EventHandler<JobCompleteArgs> completionDelegate, ulong id)
        {
            JobRetweet job = new JobRetweet(completionDelegate, id);
            InitialiseJob(job);
            return job;
        }


        /// <summary>
        /// Create a job (as part of this batch) to send a DM
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobDMSend CreateSendDM(EventHandler<JobCompleteArgs> completionDelegate, ulong recipientId, string text)
        {
            JobDMSend job = new JobDMSend(completionDelegate, recipientId, text);
            InitialiseJob(job);
            return job;
        }




        /// <summary>
        /// Create a job (as part of this batch) to post a new tweet
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JobStatusUpdate CreateStatusUpdate(EventHandler<JobCompleteArgs> completionDelegate, Status status)
        {
            JobStatusUpdate job = new JobStatusUpdate(completionDelegate, status);
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a batch job (as part of this batch) to post a new tweet with media images
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="mediaItems"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public JobBatchStatusWithImages CreateJobStatusWithImages(EventHandler<BatchCompleteArgs> completionDelegate, List<Media> mediaItems, string text)
        {
            JobBatchStatusWithImages job = new JobBatchStatusWithImages(m_jobRunner, this, completionDelegate);
            InitialiseJob(job);
            job.CreateChildJobs(mediaItems, (Status)null, text);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to post a new tweet with media images, in reply to another tweet
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="mediaItems"></param>
        /// <param name="text"></param>
        /// <param name="replyTo"></param>
        /// <returns></returns>
        public JobBatchStatusWithImages CreateJobStatusWithImages(EventHandler<BatchCompleteArgs> completionDelegate, List<Media> mediaItems, string text, Status replyTo)
        {
            JobBatchStatusWithImages job = new JobBatchStatusWithImages(m_jobRunner, this, completionDelegate);
            job.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(job);
            job.CreateChildJobs(mediaItems, replyTo, text);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to post a new tweet with media images, in reply to a tweet created by a previous job
        /// </summary>
        /// <param name="completionDelegate">Delegate to call on completion</param>
        /// <param name="mediaItems">The images to post</param>
        /// <param name="text">Text for the tweet</param>
        /// <param name="replyTo">The Job which created the status being replied to (ie this is a thread)</param>
        /// <returns></returns>
        public JobBatchStatusWithImages CreateJobStatusWithImages(EventHandler<BatchCompleteArgs> completionDelegate, List<Media> mediaItems, string text, JobBatchStatusWithImages replyTo)
        {
            JobBatchStatusWithImages job = new JobBatchStatusWithImages(m_jobRunner, this, completionDelegate);
            job.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(job);
            job.CreateChildJobs(mediaItems, replyTo, text);
            return job;

        }

        /// <summary>
        /// All jobs require some initialisation. They must all call this method after creation.
        /// This is why the jobs only have internal constructors - consumers cannot be trusted to do all this.
        /// </summary>
        /// <param name="job"></param>
        private void InitialiseJob(Job job)
        {
            job.Parent = this;
            job.RootBatch = this.RootBatch;
            job.ParameterStrings.Add(TwitterParameters.oAuthConsumerKey, AuthConsumerKey);
            job.ParameterStrings.Add(TwitterParameters.oAuthCallback, AuthCallBack);
            job.Completed += JobCompleted;

            // Sanity check that the code which created a child batch has set notification to parent
            // or parent batch will hang forever.
            if (!job.IsRootBatch)
            {
                if (job is JobBatch)
                {
                    JobBatch batch = (JobBatch)job;
                    if (batch.BatchCompleted == null)
                    {
                        throw new ArgumentNullException("Batches MUST notify of their completion");
                    }
                }
            }

            m_jobsQueue.Enqueue(job);
            m_allJobs.Add(job);
        }

        #endregion


        /// <summary>
        /// Handle an exception
        /// </summary>
        /// <param name="ex"></param>
        public override void HandleException(Exception ex, Job job)
        {
            // Prvent this batch from going any further after this
            m_cancelled = true;
            KillTimer();

            // the batch needs to know, as does everything on the way up.
            base.HandleException(ex, job);
        }

        /// <summary>
        /// The jobs left in the queue
        /// </summary>
        public Queue<Job> Jobs { get { return m_jobsQueue; } }

        /// <summary>
        /// Returns all jobs. 
        /// </summary>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public List<Job> GetAllJobs(bool recursive)
        {
            if (!recursive)
            {
                return m_allJobs;
            }

            List<Job> allJobs = new List<Job>();
            GetAllJobsInternal(allJobs);
            return allJobs;
        }

        internal void GetAllJobsInternal(List<Job> allJobs)
        { 
            foreach(Job job in m_allJobs)
            {
                // recurse batches
                if(job is JobBatch)
                {
                    JobBatch jobAsBatch = (JobBatch)job;
                    jobAsBatch.GetAllJobsInternal(allJobs);
                }
                else
                {
                    // just add normal jobs
                    allJobs.Add(job);
                }
            }
        }
            

        /// <summary>
        /// Returns the number of jobs to do for THIS batch AND its child batches
        /// </summary>
        public int GetNoOfJobs
        {
            get
            {
                int count = 0;
                foreach (Job job in m_jobsQueue)
                {
                    if (job is JobBatch)
                    {
                        JobBatch childBatch = (JobBatch)job;
                        int childJobs = childBatch.GetNoOfJobs;
                        count += childJobs;
                    }
                    else
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Notify of batch progress
        /// </summary>
        public EventHandler<BatchProgressArgs> BatchProgress;
        protected virtual void OnBatchProgress(Job job)
        {
            EventHandler<BatchProgressArgs> handler = BatchProgress;
            BatchProgressArgs args = new BatchProgressArgs(job, m_totalJobs, m_jobCompletedCount);

            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// A child job has completed.
        /// </summary>
        /// <param name="job"></param>
        internal override void ChildCompleted(Job job)
        {
            try
            {
                theLog.Log.LevelDown();

                // update count
                m_jobCompletedCount++;

                // let the bas class do its thing
                base.ChildCompleted(job);

                // notify of progress.
                OnBatchProgress(job);
            }
            finally { theLog.Log.LevelUp(); }
        }


        internal override void Terminate()
        {
            // terminate jobs which were completed (ie allow them to clean up references etc)
            if (m_completeJobs != null)
            {
                foreach (Job job in m_completeJobs)
                {
                    job.Terminate();
                }
            }
            m_completeJobs = null;

            // terminate jobs which were never started
            if(m_jobsQueue != null)
            {
                while(m_jobsQueue.Count>0)
                {
                    Job job = m_jobsQueue.Dequeue();
                    job.Terminate();
                }
            }
            m_jobsQueue = null;

            // the last job which was started (NOTE - may still be running - that job must handle this cleanly)
            if(m_lastJob != null)
            {
                m_lastJob.Terminate();
            }
            m_lastJob = null;

            m_timer = null;
            m_jobFactory = null;

            base.Terminate();
        }

    }
}