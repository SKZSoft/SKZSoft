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

namespace SKZSoft.SKZTweets
{
    public partial class frmDMFollowers : SafeForm
    {
        private bool m_terminated = false;
        Twitter.TwitterData.GetAllFollowers m_getAllFollowers;
        private enum JobTypes
        {
            GetFollowers,
            SendDM // XXSXSKZ TODO: this needs to be a class with data of what to DM and where
        }

        private QueueManager<JobTypes> m_queueManager;

        public frmDMFollowers(AppController mainController)
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
                m_mainController.TwitterData.SendDM(workItem.RecipientId, workItem.Text, null, ExceptionHandler, DMSent);

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
                m_getAllFollowers.Begin();

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
                foreach(ulong id in e.Ids)
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

        private void DoSendDMs(string IdsWithLinebreaks, string text)
        {
            try
            {
                theLog.Log.LevelDown();
                List<string> badIds;
                Dictionary<ulong, ulong> goodIds;

                string[] ids = IdsWithLinebreaks.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


                badIds = new List<string>();
                goodIds = new Dictionary<ulong, ulong>();
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
                        if (!goodIds.ContainsKey(idAsUlong))
                        {
                            goodIds.Add(idAsUlong, idAsUlong);
                        }
                    }
                }

                // abort if no good IDs
                if(goodIds.Count ==0)
                {
                    Utils.SKZMessageBox(Strings.NoGoodIDsFound, MessageBoxIcon.Error);
                    return;
                }

                // display warning if bad IDs were found
                if(badIds.Count > 0)
                {
                    StringBuilder sb = new StringBuilder(1000);
                    foreach(string badId in badIds)
                    {
                        sb.AppendLine(badId);
                    }
                    string msg = string.Format(Strings.BadIDsFound, goodIds.Count, sb.ToString());
                }

                // queue up the jobs
                DoQueueDMs(goodIds, text);

                // Fire them off
                m_queueManager.ProcessNextJob();

            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoQueueDMs(Dictionary<ulong, ulong> ids, string text)
        {
            try
            {
                theLog.Log.LevelDown();
                foreach(KeyValuePair<ulong, ulong> kvp in ids)
                {
                    Data.SendDMData dmJob = new Data.SendDMData(kvp.Key, text);
                    QueueResult qr = m_queueManager.AddJob(JobTypes.SendDM, dmJob);

                    Utils.ShowBadQueueResult(qr);
                }
            }
            finally { theLog.Log.LevelUp(); }

        }
    }
}
