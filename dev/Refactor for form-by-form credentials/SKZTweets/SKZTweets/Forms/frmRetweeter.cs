using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.SKZTweets.Controllers;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Reflection;
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.SKZTweets.Interfaces;
using SKZSoft.SKZTweets.Models;
using System.Diagnostics;
using SKZSoft.Common.Queueing;

namespace SKZSoft.SKZTweets
{

    public partial class frmRetweeter : SafeForm
    {
        private enum JobTypes
        {
            Retweet,
            UpdateCounts
        }

        private bool m_terminated = false;
        private bool m_scheduleRunning = false;
        private bool m_tweetSelected = false;

        private QueueManager<JobTypes> m_queueManager;

        private Timer m_Timer;
        private Timer m_TimerCounts;
        private Queue<DateTime> m_triggerTimes;
        ToolStripStatusLabel m_tsslRTCount;
        ToolStripStatusLabel m_tsslStatus;

        public override bool Dirty
        { 
            get 
            {
                // not enough data to ever really care. Only if we're busy do we care.
                return false;
            }
        }


        public override bool Busy
        {
            get
            {
                return m_scheduleRunning;
            }
        }

        public frmRetweeter(Credentials credentials, AppController mainController) : base(credentials, mainController)
        {
            try
            {
                theLog.Log.LevelDown();

                m_mainController = mainController;
                InitializeComponent();
                tweetDisplay.Initialize(mainController);
                SetEnabled();

                m_queueManager = new QueueManager<JobTypes>(false);
                m_queueManager.QueueCompleted += M_queueManager_QueueCompleted;
                m_queueManager.ProcessJob += M_queueManager_ProcessJob;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Timer RT fired");
                m_queueManager.AddJob(JobTypes.Retweet);

                Cursor.Current = Cursors.WaitCursor;
                m_queueManager.ProcessNextJob();
                SetNextTriggerTime();
            }
            catch (Exception ex)
            {
                // an error occurred on the timer.
                theLog.Log.WriteError(string.Format("Exception raised in Retweet timer. Tweet ID is: [{0}]", tweetDisplay.Status.id), Logging.LoggingSource.GUI);

                // handle the exception silently
                Utils.HandleException(ex, false);

                // show in log window
                string logWindow = theLog.Log.GetExceptionText(ex);
                AddMessage(logWindow);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnRTNow_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                m_queueManager.AddJob(JobTypes.Retweet);
                Cursor.Current = Cursors.WaitCursor;
                m_queueManager.ProcessNextJob();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
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
                    case JobTypes.Retweet:
                        DoRetweet();
                        break;

                    case JobTypes.UpdateCounts:
                        UpdateCountsBegin();
                        break;
                }
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_queueManager_QueueCompleted(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            SetEnabled();
        }



        /// <summary>
        /// Retweet a tweet. If it has been retweeted previously, then delete the previous RT 
        /// before it is RT'd again. Tweets may only be RT'd once per account.
        /// </summary>
        /// <param name="tweetId">UID of the tweet</param>
        /// <param name="screenName">Screenname of the account (ie "@Screenname")</param>
        private void DoRetweet()
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("RT job begins");
                ulong tweetId = tweetDisplay.Status.id;
                m_mainController.TwitterData.Retweet(m_formCredentials, tweetId, RTBatchCompleted, ExceptionHandler, OnRTDeleted, OnRTCompleted);
            }
            catch (Exception ex)
            {
                theLog.Log.WriteException(ex);
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void OnRTDeleted(object sender, JobCompleteArgs e)
        {
            Debug.WriteLine("RT deleted");
            AddMessage(Strings.RTUndone);
        }

        private void OnRTCompleted(object sender, JobCompleteArgs e)
        {
            Debug.WriteLine("RT sent");
            AddMessage(Strings.RTDone);
        }

        private void RTBatchCompleted(object sender, BatchCompleteArgs e)
        {
            try
            {
                if (m_terminated)
                {
                    return;
                }

                Debug.WriteLine("RT batch complete");
                theLog.Log.LevelDown();

                m_queueManager.JobProcessed(true);

                m_queueManager.AddJob(JobTypes.UpdateCounts);
                m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void AddMessageException(Exception ex, Job job)
        {
            try
            {
                theLog.Log.LevelDown();

                string time = GetTimeNow();
                string errorMessage = Utils.GetExceptionMessage(ex, job);

                string formattedMessage = string.Format(Strings.ErrorOccurredInJob, time, errorMessage);

                ListBoxItem item = new ListBoxItem();
                item.DisplayText = formattedMessage;
                item.Job = job;
                item.Object = ex;

                lstHistory.Items.Add(item);
                theLog.Log.WriteDebug("Added status: " + formattedMessage, Logging.LoggingSource.GUI);

                // select last message
                lstHistory.SelectedIndex = lstHistory.Items.Count - 1;
            }
            finally { theLog.Log.LevelUp(); }
        }

    

        /// <summary>
        /// Add message to listbox
        /// </summary>
        /// <param name="msg"></param>
        private void AddMessage(string msg)
        {
            try
            {
                theLog.Log.LevelDown();

                string formattedMessage = GetTimeNow();
                formattedMessage += " " + msg;

                ListBoxItem item = new ListBoxItem();
                item.DisplayText = formattedMessage;

                lstHistory.Items.Add(item);
                theLog.Log.WriteDebug("Added status: " + formattedMessage, Logging.LoggingSource.GUI);

                // select last message
                lstHistory.SelectedIndex = lstHistory.Items.Count - 1;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private string GetTimeNow()
        {
            try
            {
                theLog.Log.LevelDown();
                string time = DateTime.Now.ToShortTimeString();

                return time;
            }
            finally { theLog.Log.LevelUp(); }
        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                ctlScheduleBasic1.StartAt = DateTime.Parse("04:00"); // TODO make this a setting  DateTime.Now.AddMinutes(1);
                ctlScheduleBasic1.IntervalMinutes = 15;         //TOOD make this a setting
                ctlScheduleBasic1.EndAt = DateTime.Parse("23:00"); // DateTime.Now.AddHours(1);


                m_tsslRTCount = new ToolStripStatusLabel();
                base.StatusStrip.Items.Add(m_tsslRTCount);

                m_tsslStatus = new ToolStripStatusLabel();
                base.StatusStrip.Items.Add(m_tsslStatus);

                m_tsslRTCount.Text = string.Empty;
                m_tsslStatus.Text = string.Empty;

                // create and start timer to monitor tweet counts
                m_TimerCounts = new Timer();
                m_TimerCounts.Tick += m_TimerCounts_Tick;
                txtUpdateInterval.Text = "20";     // TODO - parameterise

                StartAutoUpdating();

                this.Icon = Utils.GetIconFromBitmap(Properties.Resources.retweet);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void m_TimerCounts_Tick(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Counts timer triggered");
                m_queueManager.AddJob(JobTypes.UpdateCounts);
                Cursor.Current = Cursors.WaitCursor;
                m_queueManager.ProcessNextJob();
            }
            catch (Exception ex)
            {
                m_tsslRTCount.Text = Strings.ErrorUpdatingCounts;
                Utils.HandleException(ex, false);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Update the counts of RTs and favourites on the tweet
        /// </summary>
        private void UpdateCountsBegin()
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Update counts start");

                ulong tweetId = tweetDisplay.Status.id;
                
                // get the original tweet
                m_mainController.TwitterData.GetOriginalTweetByIdStart(m_formCredentials, null, UpdateCountsEnd, ExceptionHandlerCounts, tweetId);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void UpdateCountsEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Update counts ends");

                if (m_terminated)
                {
                    return;
                }

                // Cast the job
                JobGetStatus castJob = (JobGetStatus)e.Job;

                Status originalTweet = castJob.Status;
                if (originalTweet == null)
                {
                    string message = string.Format(Strings.TweetWithIdDoesNotExist, tweetDisplay.Status.id);
                    theLog.Log.WriteDebug(message, Logging.LoggingSource.GUI);
                    m_tsslRTCount.Text = message;
                }
                else
                {
                    theLog.Log.WriteDebug(string.Format("Updating counts for tweet id \"{0}\"", tweetDisplay.Status.id), Logging.LoggingSource.GUI);
                    string counts = string.Format(Strings.RTAndFavCount, originalTweet.retweet_count, originalTweet.favorite_count);
                    m_tsslRTCount.Text = counts;
                }

                m_queueManager.JobProcessed(true);
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Start the RT process with the selected times.
        /// </summary>
        private void StartWithTimes()
        {
            try
            {
                theLog.Log.LevelDown();

                // kill the existing timer and its data
                DestroyTimer();
                m_triggerTimes = null;

                SetTimes(ctlScheduleBasic1.StartAt, ctlScheduleBasic1.EndAt, ctlScheduleBasic1.IntervalMinutes);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Accepts a string contianing times.
        /// They may or may not have dates.
        /// Delimiter may be: space, comma, line-return
        /// </summary>
        /// <param name="stimes"></param>
        private bool SetTimes(DateTime startAt, DateTime endAt, int intervalMinutes)
        {
            try
            {
                theLog.Log.LevelDown();

                Scheduler scheduler = new Scheduler();
                m_triggerTimes = scheduler.GetScheduleTimes(startAt, endAt, intervalMinutes);

                // set up timer
                CreateTimer();
                SetNextTriggerTime();
                return true;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void CreateTimer()
        {
            try
            {
                theLog.Log.LevelDown();

                m_Timer = new Timer();
                m_Timer.Tick += m_Timer_Tick;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void SetStatus(string text)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Setting status to: " + text, Logging.LoggingSource.GUI);
                m_tsslStatus.Text = text;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void SetNextTriggerTime()
        {
            try
            {
                theLog.Log.LevelDown();

                if (m_triggerTimes == null)
                {
                    // no times set
                    theLog.Log.WriteDebug("No trigger times defined. Not activating timer.", Logging.LoggingSource.GUI);
                    return;
                }

                if (m_triggerTimes.Count == 0)
                {
                    theLog.Log.WriteDebug("No more trigger times. Tearing down.", Logging.LoggingSource.GUI);
                    // nothing left to trigger.
                    DestroyTimer();
                    SetStatus(Strings.NoMoreWork);

                    m_scheduleRunning = false;
                    SetEnabled();
                    return;
                }

                // get next time to fire
                DateTime nextTrigger = m_triggerTimes.Dequeue();
                int milliseconds = SKZSoft.Twitter.TwitterData.Utils.GetMillisecondsToTrigger(nextTrigger);

                if (milliseconds < 1)
                {
                    theLog.Log.WriteDebug("Next time is in the past - skipping", Logging.LoggingSource.GUI);
                    // Date is in the past - skip it
                    SetNextTriggerTime();
                    return;
                }

                // set in milliseconds
                m_Timer.Interval = milliseconds;
                m_Timer.Start();

                theLog.Log.WriteDebug(string.Format("timer set to {0} ms", milliseconds), Logging.LoggingSource.GUI);

                string newStatus = string.Format(Strings.WillRTAt, nextTrigger.ToShortTimeString());
                SetStatus(newStatus);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DestroyTimer()
        {
            theLog.Log.LevelDown();

            if (m_Timer != null)
            {
                theLog.Log.WriteDebug("Tearing down timer", Logging.LoggingSource.GUI);
                m_Timer.Tick -= m_Timer_Tick;
                m_Timer = null;
            }
            else
            {
                theLog.Log.WriteDebug("Timer already torn down", Logging.LoggingSource.GUI);
            }
            SetStatus(Strings.Stopped);
            theLog.Log.LevelUp();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Validate Schedule and quit if it is bad
                List<string> errors;
                if (!ctlScheduleBasic1.Validate(out errors, false))
                {
                    Utils.SKZMessageBoxErrors(errors, MessageBoxIcon.Error);
                    return;
                }

                // Start running the timer
                StartWithTimes();
                m_scheduleRunning = true;
                SetEnabled();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DestroyTimer();
                m_scheduleRunning = false;
                SetEnabled();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }


        private void ShowTweetBegin(ulong tweetId, string screenName)
        {
            try
            {
                theLog.Log.LevelDown();

                // get the original tweet
                m_mainController.TwitterData.GetOriginalTweetByIdStart(m_formCredentials, null, ShowTweet_GotTweet, ExceptionHandler, tweetId);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void ShowTweet_GotTweet(object sender, JobCompleteArgs e)
        {
            try
            {
                // get and cast job
                JobGetStatus job = (JobGetStatus)e.Job;
                Status originalTweet = job.Status;

                if (originalTweet == null)
                {
                    // no tweet could be found. Inform user and bug out.
                    string message = string.Format(Strings.CannotFindTweet, job.StatusID);
                    theLog.Log.WriteWarning(message, Logging.LoggingSource.GUI);
                    Utils.SKZMessageBox(message, MessageBoxIcon.Error);
                    tweetDisplay.Clear();
                    return;
                }

                // display tweet
                tweetDisplay.Status = originalTweet;

                // update the counts for the tweet as the next job.
                m_queueManager.AddJob(JobTypes.UpdateCounts);
                m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }

        }

        private void btnRefreshCounts_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Debug.WriteLine("Refresh Counts button");
                m_queueManager.AddJob(JobTypes.UpdateCounts);
                m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void DoSelectTweet()
        {
            try
            {
                theLog.Log.LevelDown();

                // allow selector form to do the groundwork
                theLog.Log.WriteDebug("creating selector form", Logging.LoggingSource.GUI);
                frmSelectTweet selecter = new frmSelectTweet(m_formCredentials, m_mainController);

                theLog.Log.WriteDebug("invoking method", Logging.LoggingSource.GUI);
                Status status = selecter.GetTweet();

                // form was cancelled.
                if (status == null)
                {
                    theLog.Log.WriteDebug("Form cancelled - returning null", Logging.LoggingSource.GUI);
                    return;
                }

                // we have a new tweet.
                string tweetId = status.id.ToString();
                theLog.Log.WriteDebug(string.Format("Tweet {0} selected", tweetId), Logging.LoggingSource.GUI);
                tweetDisplay.Status = status;

                m_tweetSelected = true;

                m_queueManager.AddJob(JobTypes.UpdateCounts);
                m_queueManager.ProcessNextJob();
            }
            finally { theLog.Log.LevelUp(); }
        }



        private void SetEnabled()
        {
            btnRefreshCounts.Enabled = m_tweetSelected;
            btnRTNow.Enabled = m_tweetSelected;
            btnStart.Enabled = !m_scheduleRunning && m_tweetSelected;
            btnStop.Enabled = m_scheduleRunning;

            ctlScheduleBasic1.Enabled = !m_scheduleRunning;

        }


        private void ExceptionHandlerCounts(object sender, JobExceptionArgs e)
        {
            // add message to status bar
            m_tsslRTCount.Text = Strings.ErrorUpdatingCounts;

            // do the standard job exception handling
            ExceptionHandler(sender, e);
        }

        private void ExceptionHandler(object sender, JobExceptionArgs e)
        {
            // log but do not display dialog
            Utils.HandleException(e.Exception, false);

            // show in log window
            AddMessageException(e.Exception, e.Job);

            // Whatever this was, it has not completed properly.
            // There could still be jobs in the queue, though.
            // Keep going with them.
            m_queueManager.JobFailed();
            m_queueManager.ProcessNextJob();
        }

        private void btnSelectTweet_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoSelectTweet();
                SetEnabled();
                StartAutoUpdating();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void lstHistory_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoShowHistoryError();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void DoShowHistoryError()
        {
            try
            {
                theLog.Log.LevelDown();
                ListBoxItem item = (ListBoxItem)lstHistory.SelectedItem;

                if (item != null)
                {
                    if (item.Object != null)
                    {
                        Exception ex = (Exception)item.Object;
                        Job job = item.Job;
                        Utils.DisplayException(ex, job);
                    }
                }
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void chkUpdateCountsAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                StartAutoUpdating();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private int GetSeconds()
        {
            int number = -1;
            if(int.TryParse(txtUpdateInterval.Text, out number))
            {
                return number * 1000;
            }

            return -1;
        }


        private void StartAutoUpdating()
        {
            int interval = GetSeconds();
            chkUpdateCountsAutomatically.Enabled = ( interval > 0);

            if (chkUpdateCountsAutomatically.Checked)
            {
                if(interval>0)
                {
                    if(m_tweetSelected)
                    {
                        m_TimerCounts.Interval = interval;
                        m_TimerCounts.Start();
                    }
                }
            }
        }

        private void txtUpdateInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StartAutoUpdating();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
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
                DestroyTimer();

                if (m_TimerCounts != null)
                {
                    m_TimerCounts.Tick -= m_TimerCounts_Tick;
                    m_TimerCounts = null;
                }

                m_mainController = null;
                m_terminated = true;
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}