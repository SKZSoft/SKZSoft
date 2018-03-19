using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.Common.Queueing;
using SKZSoft.SKZTweets.Controllers;
using SKZSoft.Twitter.TwitterJobs;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Consts;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.SKZTweets
{
    public partial class frmDMFollowers : SafeForm
    {
        private bool m_terminated = false;
        private Twitter.TwitterData.GetAllFollowers m_getAllFollowers;
        private DMBroadcaster m_DMBroadcaster;


        private enum JobTypes
        {
            GetFollowers,
            SendDM // XXSXSKZ TODO: this needs to be a class with data of what to DM and where
        }

        private QueueManager<JobTypes> m_queueManager;

        public frmDMFollowers(Credentials credentials, AppController mainController) : base(credentials, mainController)
        {
            m_mainController = mainController;
            InitializeComponent();

            m_queueManager = new QueueManager<JobTypes>(false);
            m_queueManager.QueueCompleted += M_queueManager_QueueCompleted;
            m_queueManager.ProcessJob += M_queueManager_ProcessJob;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                m_queueManager.AddJob(JobTypes.GetFollowers);
                Cursor.Current = Cursors.WaitCursor;
                m_queueManager.ProcessNextJob();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_queueManager_QueueCompleted(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }


        private void M_queueManager_ProcessJob(object sender, RunJobEventArgs<JobTypes> e)
        {
            try
            {
                theLog.Log.LevelDown();
                if (m_terminated)
                {
                    theLog.Log.WriteDebug("object has been terminated; cannot run job. Aborting.", Logging.LoggingSource.GUI);
                    return;
                }

                JobTypes type = e.JobType;

                switch (type)
                {
                    case JobTypes.GetFollowers:
                        DoGetFollowers();
                        break;

                    case JobTypes.SendDM:
                        Data.SendDMData workItem = (Data.SendDMData)e.WorkItem;
                        DoSendDM(workItem);
                        break;
                }

                m_queueManager.JobProcessed(true);
                m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoSendDM(Data.SendDMData workItem)
        {
            try
            {
                theLog.Log.LevelDown();
                m_mainController.TwitterData.SendDM(m_formCredentials, workItem.RecipientId, workItem.Text, null, ExceptionHandler, DMSent);

            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DMSent(object sender, JobCompleteArgs e)
        {
            m_queueManager.ProcessNextJob();
        }

        private void DoGetFollowers()
        {
            try
            {
                theLog.Log.LevelDown();

                m_getAllFollowers = new Twitter.TwitterData.GetAllFollowers(m_mainController.TwitterData, DataConsts.MAX_BATCH_SIZE_FOLLOWER_IDS, FollowerBatchCompleted, ExceptionHandler, null);

                // kick off batch job.
                m_getAllFollowers.Begin(m_formCredentials);

            }
            catch (Exception ex)
            {
                theLog.Log.WriteException(ex);
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }


        private void FollowerBatchCompleted(object sender, FollowerIDArgs e)
        {
            try
            {
                if (m_terminated)
                {
                    return;
                }

                theLog.Log.LevelDown();

                StringBuilder sb = new StringBuilder(10000);
                foreach (ulong id in e.Ids)
                {
                    sb.AppendLine(id.ToString());
                }

                txtFollowerIds.Text = sb.ToString();

                m_queueManager.JobProcessed(true);

                // TODO: run next cursor batch
                //m_queueManager.AddJob(JobTypes.UpdateCounts);
                //m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void ExceptionHandler(object sender, JobExceptionArgs e)
        {
            // handle error
            Utils.HandleException(e.Exception, true);


            // Whatever this was, it has not completed properly.
            // There could still be jobs in the queue, though.
            // Keep going with them.
            m_queueManager.JobFailed();
            m_queueManager.ProcessNextJob();
        }



        public override void Terminate()
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("*** TERMINATING ***", Logging.LoggingSource.GUI);

                if (m_queueManager != null)
                {
                    m_queueManager.Terminate();
                    m_queueManager = null;
                }

                m_mainController = null;
                m_terminated = true;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtFollowerIds.Text);
        }

        private void btnSendDMs_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoSendDMs(txtFollowerIds.Text, txtDMBody.Text);
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void DoSendDMs(string idsWithLinebreaks, string text)
        {
            long totalIds = 0;
            long duplicateIds = 0;
            long badIds = 0;
            Queue<ulong> goodIds = ValidateRecipients(idsWithLinebreaks, text, out totalIds, out duplicateIds, out badIds);

            if (goodIds == null)
            {
                // Messagebox already shown in validation method. Just get out of here.
                return;
            }

            // Show prompt to confirm action
            StringBuilder sb = new StringBuilder(500);
            sb.AppendFormat(Strings.DMWillSendNumber, goodIds.Count);
            sb.AppendLine();
            if (duplicateIds > 0 || badIds > 0)
            {
                sb.AppendFormat(Strings.DMValidationFailures, totalIds, duplicateIds, badIds);
                sb.AppendLine();
            }
            sb.AppendFormat(Strings.DoYouWishToProceed);

            // Confirm with user
            if (!Utils.SKZConfirmationMessageBox(sb.ToString()))
            {
                return;
            }

            // queue up the jobs
            DoStartDMBatch(goodIds, text);

            // Fire them off
            m_queueManager.ProcessNextJob();
        }

        private Queue<ulong> ValidateRecipients(string IdsWithLinebreaks, string text, out long totalIds, out long duplicateIds, out long badIdCount)
        {
            try
            {
                theLog.Log.LevelDown();
                List<string> badIds;
                Queue<ulong> goodIds;

                string[] ids = IdsWithLinebreaks.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                totalIds = ids.Length;
                duplicateIds = -0;

                badIds = new List<string>();
                goodIds = new Queue<ulong>();
                foreach (string id in ids)
                {
                    ulong idAsUlong;
                    if (!ulong.TryParse(id, out idAsUlong))
                    {
                        // invalid ID.
                        theLog.Log.WriteWarning(string.Format("id [{0}] is not a valid ulong", id), Logging.LoggingSource.GUI);
                        badIds.Add(id);
                    }
                    else
                    {
                        // Add ID to list 
                        if (!goodIds.Contains(idAsUlong))
                        {
                            goodIds.Enqueue(idAsUlong);
                        }
                        else
                        {
                            duplicateIds++;
                        }
                    }
                }

                badIdCount = badIds.Count;

                // abort if no good IDs
                if (goodIds.Count == 0)
                {
                    Utils.SKZMessageBox(Strings.NoGoodIDsFound, MessageBoxIcon.Error);
                    return null;
                }

                // display warning if bad IDs were found
                if (badIds.Count > 0)
                {
                    StringBuilder sb = new StringBuilder(1000);
                    foreach (string badId in badIds)
                    {
                        sb.AppendLine(badId);
                    }
                    string msg = string.Format(Strings.BadIDsFound, goodIds.Count, sb.ToString());
                }

                return goodIds;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoStartDMBatch(Queue<ulong> recipients, string text)
        {
            try
            {
                theLog.Log.LevelDown();

                m_DMBroadcaster = m_mainController.TwitterData.CreateDMBroadcaster(recipients, text);
                m_DMBroadcaster.DMBroadcastProgressUpdate += M_DMBroadcaster_DMBroadcastProgressUpdate;
                m_DMBroadcaster.DMBroadcastComplete += M_DMBroadcaster_DMBroadcastComplete;
                m_DMBroadcaster.DMBroadcastCancelled += M_DMBroadcaster_DMBroadcastCancelled;
                m_DMBroadcaster.ExceptionRaised += M_DMBroadcaster_ExceptionRaised;

                m_DMBroadcaster.BroadcastDMsBegin(m_formCredentials, 0);

            }
            finally { theLog.Log.LevelUp(); }

        }

        private void M_DMBroadcaster_ExceptionRaised(object sender, JobExceptionArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // If invoke is required, invoke THIS method with same parameters
                // to get back on main thread. Simpler code thereafter.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => M_DMBroadcaster_ExceptionRaised(sender, e)));
                    return;
                }

                // update the GUI
                Utils.HandleException(e.Exception, true);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_DMBroadcaster_DMBroadcastCancelled(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // If invoke is required, invoke THIS method with same parameters
                // to get back on main thread. Simpler code thereafter.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => M_DMBroadcaster_DMBroadcastCancelled(sender, e)));
                    return;
                }

                //TODO
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_DMBroadcaster_DMBroadcastComplete(object sender, DMBroadcastCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // If invoke is required, invoke THIS method with same parameters
                // to get back on main thread. Simpler code thereafter.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => M_DMBroadcaster_DMBroadcastComplete(sender, e)));
                    return;
                }

                // TODO: reset form
                Utils.SKZMessageBox(Strings.DMBroadCastComplete, MessageBoxIcon.Information);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_DMBroadcaster_DMBroadcastProgressUpdate(object sender, DMBroadcastProgressUpdateArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // If invoke is required, invoke THIS method with same parameters
                // to get back on main thread. Simpler code thereafter.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => M_DMBroadcaster_DMBroadcastProgressUpdate(sender, e)));
                    return;
                }

                // update the GUI
                string msg = string.Format(Strings.ProgressDMSend, e.Sent, e.Total);
                lblProgress.Text = msg;
                lblProgress.Refresh();
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
