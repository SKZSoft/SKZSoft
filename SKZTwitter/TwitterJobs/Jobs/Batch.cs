using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterJobs.Jobs;


namespace SKZSoft.Twitter.TwitterJobs
{
    public class Batch : Job
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
        protected Factory m_batchFactory;
        protected IJobRunner m_jobRunner;
        protected Credentials m_batchCredentials;
        private string m_authCallBack { get; set; }
        internal Factories.JobFactories m_jobFactories;
        internal Factory BatchFactory { get { return m_batchFactory; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="completionDelegate"></param>
        internal Batch(string authCallback, Credentials credentials, IJobRunner jobRunner, EventHandler<BatchCompleteArgs> completionDelegate) : base(null)
        {
            m_authCallBack = authCallback; // XXX is this correct? What of the application ID? 

            m_jobFactories = new Factories.JobFactories();

            m_jobRunner = jobRunner;
            m_jobsQueue = new Queue<Job>();
            m_allJobs = new List<Job>();
            m_idling = true;
            m_globals = new Dictionary<string, object>();
            BatchCompleted += completionDelegate;

            m_batchCredentials = credentials.Clone();
        }

        /// <summary>
        /// The factories for adding jobs to a batch
        /// </summary>
        public Factories.JobFactories JobFactories { get { return m_jobFactories; } }

        /// <summary>
        /// The credentials to use for this batch
        /// </summary>
        public Credentials Credentials { get { return m_batchCredentials; } }



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
                if (m_nextJob != null && m_nextJob is Batch)
                {
                    Batch Batch = (Batch)m_nextJob;
                    Batch.Cancel();
                }

                // feed cancellations down to all child batches
                foreach (Job job in m_jobsQueue)
                {
                    if (job is Batch)
                    {
                        Batch Batch = (Batch)job;
                        Batch.Cancel();
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
        internal void JobCompleted(object sender, JobCompleteArgs e)
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
                if (m_nextJob is Batch)
                {
                    System.Diagnostics.Debug.WriteLine("Run a sub-batch");
                    Batch batch = (Batch)m_nextJob;
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



        /// <summary>
        /// Create a child batch as part of this batch
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Batch CreateBatch(EventHandler<BatchCompleteArgs> completionDelegate)
        {
            Batch job = new Batch(m_authCallBack, m_batchCredentials, m_jobRunner, completionDelegate);
            job.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// All jobs require some initialisation. They must all call this method after creation.
        /// This is why the jobs only have internal constructors - consumers cannot be trusted to do all this.
        /// </summary>
        /// <param name="job"></param>
        internal void InitialiseJob(Job job)
        {
            job.Parent = this;
            job.RootBatch = this.RootBatch;
            job.ParameterStrings.Add(TwitterParameters.oAuthConsumerKey, m_batchCredentials.ConsumerKey);
            job.ParameterStrings.Add(TwitterParameters.oAuthCallback, m_authCallBack);
            job.Completed += JobCompleted;

            // Sanity check that the code which created a child batch has set notification to parent
            // or parent batch will hang forever.
            if (job is Batch && !(job is BatchRoot))
            {
                Batch batchJob = (Batch)job;
                if (batchJob.BatchCompleted == null)
                {
                    throw new Exception("Child batches MUST notify ther parents of completion.");
                }

            }
            EnqueueJob(job);
        }



        public void EnqueueJob(Job job)
        {
            m_jobsQueue.Enqueue(job);
            m_allJobs.Add(job);
        }


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
                if(job is Batch)
                {
                    Batch jobAsBatch = (Batch)job;
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
                    if (job is Batch)
                    {
                        Batch childBatch = (Batch)job;
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

            base.Terminate();
        }


        /// <summary>
        /// Only batches can create other batches. They need data which shouldn't be exposed.
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Statuses.BatchWithImages CreateBatchWithImages(EventHandler<BatchCompleteArgs> completionDelegate, List<TwitterModels.Media> mediaItems, string text, Status replyTo)
        {
            Jobs.Statuses.BatchWithImages batch = new Jobs.Statuses.BatchWithImages(m_authCallBack, m_batchCredentials, m_jobRunner, this, completionDelegate);
            batch.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(batch);
            batch.CreateChildJobs(mediaItems, replyTo, text);
            return batch;
        }

        public Jobs.Statuses.BatchWithImages CreateBatchWithImages(EventHandler<BatchCompleteArgs> completionDelegate, List<TwitterModels.Media> mediaItems, string text, Jobs.Statuses.BatchWithImages replyTo)
        {
            Jobs.Statuses.BatchWithImages batch = new Jobs.Statuses.BatchWithImages(m_authCallBack, m_batchCredentials, m_jobRunner, this, completionDelegate);
            batch.BatchCompleted += Job_BatchCompleted;
            InitialiseJob(batch);
            batch.CreateChildJobs(mediaItems, replyTo, text);
            return batch;
        }


    }
}