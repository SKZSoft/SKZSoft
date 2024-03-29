﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Diagnostics;

namespace SKZSoft.Common.Queueing
{

    public enum QueueResult
    {
        OK,
        ManagerTerminating,
        DuplicateJob
    }

    /// <summary>
    /// Class for managing queues of objects for processing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueManager<T>
    {
        private bool m_allowDuplicates;
        private Queue<QueueItem<T>> m_queue;
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

                m_queue = new Queue<QueueItem<T>>();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Add a job to the queue for processing
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(T jobType)
        {
            try
            {
                theLog.Log.LevelDown();
                AddJob(jobType, null);
            }

            finally { theLog.Log.LevelUp(); }
        }

        public QueueResult AddJob(T jobType, object jobItem)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Queueing job for " + jobType.ToString(), Logging.LoggingSource.GUI);

                if (m_terminated)
                {
                    theLog.Log.WriteWarning("Attempted to queue a job on a terminated Queue manager. Aborting.", Logging.LoggingSource.GUI);
                    return QueueResult.ManagerTerminating;
                }

                QueueItem<T> queueItem = new QueueItem<T>(jobType, jobItem);

                // Note - only TYPEs are checked.
                if (m_queue.Contains(queueItem))
                {
                    if (!m_allowDuplicates)
                    {
                        string report = string.Format("Job of type {0} already queued.", jobType.ToString());
                        Debug.WriteLine(report);
                        theLog.Log.WriteDebug(report, Logging.LoggingSource.GUI);
                        return QueueResult.DuplicateJob;
                    }
                }


                m_queue.Enqueue(queueItem);
                theLog.Log.WriteDebug("Queued " + jobType.ToString(), Logging.LoggingSource.GUI);
                return QueueResult.OK;
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
                    theLog.Log.WriteError("A job is already running. Quitting.", Logging.LoggingSource.GUI);
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
                
                QueueItem<T> queueItem = m_queue.Dequeue();
                T jobType = queueItem.Type;

                theLog.Log.WriteDebug("Dequeued job: " + jobType.ToString(), Logging.LoggingSource.GUI);

                // raise event - run job
                // The consumer will do the actual running.
                theLog.Log.WriteDebug("Raising job to consumer", Logging.LoggingSource.GUI);
                OnProcessJob(jobType, queueItem.Item);
                theLog.Log.WriteDebug("Consumer returned", Logging.LoggingSource.GUI);
            }
            finally { theLog.Log.LevelUp(); }
        }

        protected virtual void OnProcessJob(T jobType, object workItem)
        {
            RunJobEventArgs<T> e = new RunJobEventArgs<T>(jobType, workItem);
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
