using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Diagnostics;

namespace SKZCommon.Queueing
{
    /// <summary>
    /// Class for managing queues of objects for processing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueManager<T>
    {
        private bool m_allowDuplicates;
        private Queue<T> m_queue;
        private bool m_jobRunning = false;
        private bool m_terminated = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allowDuplicates"></param>
        public QueueManager(bool allowDuplicates)
        {
            m_allowDuplicates = allowDuplicates;
            Clear();
        }


        /// <summary>
        /// Owner is closing down. Perform no further actions.
        /// </summary>
        public void Terminate()
        {
            m_terminated = true;
            Clear();
        }

        /// <summary>
        /// Clear all queued items not yet processed
        /// </summary>
        public void Clear()
        {
            try
            {
                theLog.Log.LevelDown();

                m_queue = new Queue<T>();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Add a job to the queue for processing
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(T job)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Queueing job for " + job.ToString(), Logging.LoggingSource.GUI);

                if (m_terminated)
                {
                    theLog.Log.WriteWarning("Attempted to queue a job on a termionated Queue manager. Aborting.", Logging.LoggingSource.GUI);
                    return;
                }

                if (m_queue.Contains(job))
                {
                    if (!m_allowDuplicates)
                    {
                        Debug.WriteLine("Job already queued.");
                        theLog.Log.WriteDebug("Job already queued.", Logging.LoggingSource.GUI);
                        return;
                    }
                }

                m_queue.Enqueue(job);
                theLog.Log.WriteDebug("Queued " + job.ToString(), Logging.LoggingSource.GUI);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// The last job which was running failed.
        /// Mark queue manager as no longer busy.
        /// COnsumer must start the next job when appropriate.
        /// </summary>
        public void JobFailed()
        {
            try
            {
                theLog.Log.LevelDown();
                m_jobRunning = false;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The consumer has finished processing the last job.
        /// </summary>
        /// <param name="job"></param>
        public void JobProcessed(bool processNextJob)
        {
            try
            {
                theLog.Log.LevelDown();

                m_jobRunning = false;

                if(m_terminated)
                {
                    theLog.Log.WriteDebug("Queue manager terminated. Will not start any new jobs.", Logging.LoggingSource.GUI);
                    return;
                }

                if (processNextJob)
                {
                    theLog.Log.WriteDebug("Starting next job", Logging.LoggingSource.GUI);
                    ProcessNextJob();
                }
                else
                {
                    theLog.Log.WriteDebug("Not starting next job", Logging.LoggingSource.GUI);
                }
            }
            finally { theLog.Log.LevelUp(); }
        }




        /// <summary>
        /// Process the next job on the queue.
        /// </summary>
        public void ProcessNextJob()
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Process next job");

                if (m_terminated)
                {
                    theLog.Log.WriteDebug("Queue manager terminated. Will not start new job.", Logging.LoggingSource.GUI);
                    return;
                }

                // prevent multiple jobs running concurrently.
                // This is called at the end of any batch, so this job is queued and WILL be processed.
                // Just later.
                if (m_jobRunning)
                {
                    theLog.Log.WriteDebug("A job is already running. Quitting.", Logging.LoggingSource.GUI);
                    return;
                }

                if (m_queue.Count == 0)
                {
                    theLog.Log.WriteDebug("All jobs completed. Nothing further to do.", Logging.LoggingSource.GUI);

                    // nothing more to do.
                    m_jobRunning = false;

                    // Raise event - finished
                    OnQueueCompleted();
                    return;
                }

                m_jobRunning = true;
                
                T job = m_queue.Dequeue();

                theLog.Log.WriteDebug("Dequeued job: " + job.ToString(), Logging.LoggingSource.GUI);

                // raise event - run job
                // The consumer will do the actual running.
                theLog.Log.WriteDebug("Raising job to consumer", Logging.LoggingSource.GUI);
                OnProcessJob(job);
                theLog.Log.WriteDebug("Consumer returned", Logging.LoggingSource.GUI);
            }
            finally { theLog.Log.LevelUp(); }
        }

        protected virtual void OnProcessJob(T job)
        {
            RunJobEventArgs<T> e = new RunJobEventArgs<T>(job);
            EventHandler<RunJobEventArgs<T>> handler = ProcessJob;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler<RunJobEventArgs<T>> ProcessJob;


        protected virtual void OnQueueCompleted()
        {
            EventArgs e = new EventArgs();
            EventHandler<EventArgs> handler = QueueCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler<EventArgs> QueueCompleted;



    }
}
