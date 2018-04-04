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
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterData
{
    public class DMBroadcaster
    {
        private int m_total = 0;
        private int m_sent = 0;
        private TwitterData m_twitterData;
        private JobBatchRoot m_rootBatch;
        private JobFactory m_jobFactory;
        private string m_text1;
        private string m_text2;
        private string m_text3;
        private Queue<ulong> m_recipientIds;
        
        #region Events
        /// <summary>
        /// Raised when the counter increases (ie another DM has been sent)
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler<DMBroadcastProgressUpdateArgs> DMBroadcastProgressUpdate;
        protected virtual void OnDMBroadcastProgressUpdate(DMBroadcastProgressUpdateArgs e)
        {
            EventHandler<DMBroadcastProgressUpdateArgs> handler = DMBroadcastProgressUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Raised when DM batch is cancelled
        /// </summary>
        public event EventHandler<EventArgs> DMBroadcastCancelled;
        protected virtual void OnDMBroadcastCancelled()
        {
            EventHandler<EventArgs> handler = DMBroadcastCancelled;
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
        /// Raised when all DMs sent
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler<DMBroadcastCompleteArgs> DMBroadcastComplete;
        protected virtual void OnDMBroadcastComplete(JobBatchRoot rootBatch)
        {
            EventHandler<DMBroadcastCompleteArgs> handler = DMBroadcastComplete;
            if (handler != null)
            {
                DMBroadcastCompleteArgs e = new DMBroadcastCompleteArgs(rootBatch, m_sent);
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
        internal DMBroadcaster(TwitterData twitterData, JobFactory jobFactory, string text1, string text2, string text3, Queue<ulong> recipientIds)
        {
            try
            {
                theLog.Log.LevelDown();
                m_text1 = text1;
                m_text2 = text2;
                m_text3 = text3;
                m_recipientIds = recipientIds;
                m_twitterData = twitterData;
                m_jobFactory = jobFactory;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Number of DMs which were posted
        /// </summary>
        /// <returns></returns>
        public int PostedCount { get { return m_rootBatch.CompletedJobs.Count; } }


        /// <summary>
        /// Start the posting of the thread.
        /// </summary>
        /// <param name="millisecondsBetweenTweets"></param>
        public void BroadcastDMsBegin(EventHandler<JobCompleteArgs> jobCompleteDelegate, Credentials credentials, int millisecondsBetweenDMs)
        {
            try
            {
                theLog.Log.LevelDown();

                m_total = m_recipientIds.Count;

                MilliSecondsBetweenDMs = millisecondsBetweenDMs;

                m_rootBatch = m_jobFactory.CreateRootBatch(credentials, BatchComplete, JobExceptionRaised);
                m_rootBatch.BatchProgress += M_BatchProgress;
                m_rootBatch.Cancelled += M_Cancelled;

                double count = 1;
                while (m_recipientIds.Count > 0)
                {
                    ulong recipientId = m_recipientIds.Dequeue();

                    double textToUse = count % 3;
                    string useText;
                    switch (textToUse)
                    {
                        case 0:
                            useText = m_text1;
                            break;
                        case 1:
                            useText = m_text2;
                            break;
                        case 2:
                            useText = m_text3;
                            break;
                        default:
                            useText = m_text2;
                            break;
                    }
                    count++;

                    JobDMSend job = m_rootBatch.CreateSendDM(jobCompleteDelegate, recipientId, useText);
                    job.DelayBefore = MilliSecondsBetweenDMs;
                }

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
            DMBroadcastProgressUpdateArgs args = new DMBroadcastProgressUpdateArgs(e.CompletedJob, e.TotalJobsCompleted, e.TotalJobs);
            OnDMBroadcastProgressUpdate(args);
        }

        /// <summary>
        /// Notify that the batch to post statuses has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatchComplete(object sender, BatchCompleteArgs e)
        {
            OnDMBroadcastComplete(e.Batch.RootBatch);
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
                OnDMBroadcastCancelled();
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


        private int MilliSecondsBetweenDMs { get; set; }
    }
}
