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

namespace SKZSoft.SKZTweets
{
    public partial class frmDMFollowers : SafeForm
    {
        private bool m_terminated = false;

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

                JobTypes type = e.Job;

                switch (type)
                {
                    case JobTypes.GetFollowers:
                        DoGetFollowers();
                        break;
                }
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoGetFollowers()
        {
            try
            {
                theLog.Log.LevelDown();
                m_mainController.TwitterData.GetFollowerIds(-1, 100, FollowerBatchCompleted, ExceptionHandler);
            }
            catch (Exception ex)
            {
                theLog.Log.WriteException(ex);
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }


        private void FollowerBatchCompleted(object sender, BatchCompleteArgs e)
        {
            try
            {
                if (m_terminated)
                {
                    return;
                }

                theLog.Log.LevelDown();

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

    }
}
