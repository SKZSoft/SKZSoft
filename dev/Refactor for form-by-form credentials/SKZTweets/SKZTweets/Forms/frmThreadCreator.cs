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
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Common.ListEnum;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.SKZTweets.Interfaces;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs;

namespace SKZSoft.SKZTweets
{

    /// <summary>
    /// Form to create threads
    /// </summary>
    public partial class frmThreadCreator : SafeForm
    {
        private enum FormStatus
        {
            Dirty,
            Clean,
            Posting,
            Cancelled,
            Complete,
            Deleting
        }

        private ThreadPoster m_threadPoster;
        private FormStatus m_status;
        private JobBatchRoot m_lastBatch;
        private bool m_systemChangingText = false;        // flag for text change events to not trigger becauase system is making changes.


        /// <summary>
        /// TRUE if the data on this form is dirty
        /// </summary>
        public override bool Dirty
        {
            get
            {
                return (m_status == FormStatus.Dirty || m_status == FormStatus.Cancelled);
            }
        }

        /// <summary>
        /// TRUE if this form is currently performing an operation
        /// </summary>
        public override bool Busy
        {
            get
            {
                return (m_status == FormStatus.Deleting || m_status == FormStatus.Posting);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="twitterContext"></param>
        public frmThreadCreator(Credentials credentials, AppController controller) : base(credentials, controller)
        {
            try
            {
                theLog.Log.LevelDown();
                m_mainController = controller;
                InitializeComponent();

                // TODO: get these defaults from user options or configs
                ctlTweetNumbering.PopulateControls(ThreadNumberPosition.NumbersAtStart, ThreadNumberStyle.XofY);
                ctlThreadPreview.TwitterData = m_mainController.TwitterData;
                SetMaxIntroLength();
                sp1MainArea_Progress.Panel2Collapsed = true;


                int secondsBetweenTweets = Consts.DefaultMillisecondsBetweenTweets / 1000;
                txtSecondsBetweenTweets.Text = secondsBetweenTweets.ToString();

                lblProgress.Text = string.Empty;

            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Initialise form on loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmThreadCreator_Load(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                UpdateStatusAndPreview();

                SetFormStatuses(FormStatus.Clean);

                this.Icon = Utils.GetIconFromBitmap(Properties.Resources.DocumentOutline_16x);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Generate a preview of what the text would look like.
        /// </summary>
        private void UpdateStatusAndPreview()
        {
            try
            {
                theLog.Log.LevelDown();

                SKZSoft.Twitter.TwitterData.Models.TweetThread threadData = GetSettings();
                int characterCount = 0;
                Queue<string> tweets = GetTweets(SKZSoft.Twitter.TwitterData.ThreadUrlFormat.Display, threadData, out characterCount);

                int originalChars = txtIntro.Text.Length;
                originalChars += txtMain.Text.Length;

                lblTweetCountStatus.Text = string.Format(Strings.CharacterCount, originalChars, characterCount, tweets.Count);

                ctlThreadPreview.ShowTweets(threadData);

                // Reset buttons to allow tweeting
                SetFormStatuses(FormStatus.Dirty);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Get the settings for the thread
        /// </summary>
        /// <returns></returns>
        private SKZSoft.Twitter.TwitterData.Models.TweetThread GetSettings()
        {
            try
            {
                theLog.Log.LevelDown();

                SKZSoft.Twitter.TwitterData.Models.TweetThread threadData = new TweetThread();
                threadData.NumberSettings = ctlTweetNumbering.GetSettings();
                threadData.IntroText = txtIntro.Text;
                threadData.ThreadText = txtMain.Text;
                threadData.StartNewLineAfterIntro = (chkStartNewLineAfterIntro.Checked);
                threadData.InReplyTo = ctlTweetText.Status;

                return threadData;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private Queue<Status> GetTweetsAsStatusObjects(SKZSoft.Twitter.TwitterData.ThreadUrlFormat urlFormat, TweetThread threadData)
        {
            try
            {
                theLog.Log.LevelDown();
                SKZSoft.Twitter.TwitterData.ThreadController threadController = new ThreadController(m_mainController.TwitterData);

                Queue<Status> tweets = threadController.SplitIntoStatuses(threadData, urlFormat);
                return tweets;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private Queue<string> GetTweets(SKZSoft.Twitter.TwitterData.ThreadUrlFormat urlFormat, TweetThread threadData, out int characterCount)
        {
            try
            {
                theLog.Log.LevelDown();

                Queue<string> tweets = GetTweets(urlFormat, threadData);

                characterCount = 0;
                foreach (string tweet in tweets)
                {
                    characterCount += tweet.Length;
                }

                return tweets;
            }
            finally { theLog.Log.LevelUp(); }

        }


        /// <summary>
        /// Get the tweets which will be generated from data as it stands
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <returns></returns>
        private Queue<string> GetTweets(SKZSoft.Twitter.TwitterData.ThreadUrlFormat urlFormat, TweetThread threadData)
        {
            try
            {
                theLog.Log.LevelDown();
                SKZSoft.Twitter.TwitterData.ThreadController threadController = new SKZSoft.Twitter.TwitterData.ThreadController(m_mainController.TwitterData);

                Queue<string> tweets = threadController.SplitIntoTexts(threadData, urlFormat);
                return tweets;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Update the preview whenever text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThreadText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelUp();
                UpdateStatusAndPreview();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }

        }

        private void ShowProgress(bool show)
        {
            try
            {
                theLog.Log.LevelDown();
                this.SuspendLayout();
                if (show)
                {
                    // keep current height
                    int originalHeight = sp1MainArea_Progress.Panel1.Height;

                    // display bottom panel
                    sp1MainArea_Progress.Panel2Collapsed = false;

                    // work out how much higher the form needs to be
                    int extraHeightNeeded = originalHeight - sp1MainArea_Progress.Panel1.Height;

                    // set form to be ciorrect height
                    this.Height += extraHeightNeeded;

                    // force panel to be correct height
                    sp1MainArea_Progress.Height = originalHeight;
                }
                else
                {
                    this.Height -= sp1MainArea_Progress.Panel2.Height;
                    sp1MainArea_Progress.Panel2Collapsed = true;
                }
            }
            finally
            {
                this.ResumeLayout();
                theLog.Log.LevelUp();
            }
        }

        /// <summary>
        /// Invoke the form which will do the actual tweeting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTweetThread_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Validate the seconds between tweets
                int secondsBetweenTweets;
                bool valid = false; // pessimism
                if (int.TryParse(txtSecondsBetweenTweets.Text, out secondsBetweenTweets))
                {
                    if (secondsBetweenTweets >= 1)
                    {
                        valid = true;
                    }
                }

                // tell user that interval is invalid and return
                if (!valid)
                {
                    Utils.SKZMessageBox(Strings.SecondsBetweenTweetsValidation, MessageBoxIcon.Error);
                    return;
                }

                // clear GUI and get settings
                lstTweets.Items.Clear();
                SKZSoft.Twitter.TwitterData.Models.TweetThread threadData = GetSettings();

                Queue<Status> tweets = GetTweetsAsStatusObjects(SKZSoft.Twitter.TwitterData.ThreadUrlFormat.Full, threadData);

                // tell user there's no text and return
                if (tweets.Count == 0)
                {
                    Utils.SKZMessageBox(Strings.NoText, MessageBoxIcon.Exclamation);
                    return;
                }

                // decorate with images
                Queue<Status> decoratedTweets = ctlThreadPreview.DecorateStatusesWithImages(tweets);

                // get "reply to" ID if it exists
                Status replyTo = ctlTweetText.Status;

                // Create new thread poster class, pass it the data, and set it off.
                // It will notify us of completion via a callback method.
                m_threadPoster = m_mainController.TwitterData.CreateThreadPoster(decoratedTweets, replyTo);
                m_threadPoster.ThreadProgressUpdate += m_threadPoster_ThreadProgressUpdate;
                m_threadPoster.ThreadComplete += m_threadPoster_ThreadComplete;
                m_threadPoster.ThreadCancelled += M_threadPoster_ThreadCancelled;
                m_threadPoster.ThreadDeleted += M_threadPoster_ThreadDeleted;
                m_threadPoster.ExceptionRaised += M_threadPoster_ExceptionRaised;
                int milliSecondsBetweenTweets = secondsBetweenTweets * 1000;

                ShowProgress(true);
                SetFormStatuses(FormStatus.Posting);

                m_threadPoster.PostThreadBegin(m_formCredentials, milliSecondsBetweenTweets);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
                Cursor.Current = Cursors.Default;
                SetFormStatuses(FormStatus.Dirty);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_threadPoster_ExceptionRaised(object sender, JobExceptionArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Exception raised by thread poster", SKZSoft.Common.Logging.LoggingSource.GUI);
                Utils.HandleException(e.Exception, true, e.Job);
                SetFormStatuses(FormStatus.Dirty);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_threadPoster_ThreadDeleted(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // if not on the GUI thread, invoke self with same parameters and then quit.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => M_threadPoster_ThreadDeleted(sender, e)));
                    return;
                }

                // Clear progress and add deleted message
                lstTweets.Items.Clear();
                AddProgress(Strings.TweetsDeleted);

                // Back to dirty because there is text which hasn't been persisted anywhere
                SetFormStatuses(FormStatus.Dirty);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void M_threadPoster_ThreadCancelled(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                SetProgressLabel(Strings.Cancelled);

                AddProgress(Strings.Cancelled);

                if (m_threadPoster.PostedCount > 0)
                {
                    // set to cancelled if any status got posted
                    SetFormStatuses(FormStatus.Cancelled);
                }
                else
                {
                    // nothing got posted - set back to "Dirty" (which is all it can have been before posting began)
                    SetFormStatuses(FormStatus.Dirty);
                }

            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Callback when all work done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_threadPoster_ThreadComplete(object sender, ThreadCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => m_threadPoster_ThreadComplete(sender, e)));
                    return;
                }

                m_lastBatch = e.RootBatch;

                string text = string.Format(Strings.ThreadComplete, e.TweetCount);
                SetProgressLabel(text);

                SetFormStatuses(FormStatus.Complete);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Callback when progress is reported
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_threadPoster_ThreadProgressUpdate(object sender, ThreadProgressUpdateArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // If invoke is required, invoke THIS method with same parameters
                // to get back on main thread. Simpler code thereafter.
                if (this.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking this method again to get onto GUI thread", Logging.LoggingSource.GUI);
                    this.Invoke(new Action(() => m_threadPoster_ThreadProgressUpdate(sender, e)));
                    return;
                }

                string progress = string.Format(Strings.ThreadProgress, e.Sent, e.Total);
                SetProgressLabel(progress);

                // counts of zero have no tweets yet. Take care.
                if (e.LastJobCompleted != null)
                {
                    AddJobText(e.LastJobCompleted);
                }
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Safely set the label; protecting against timer threads
        /// </summary>
        /// <param name="text"></param>
        private void SetProgressLabel(string text)
        {
            try
            {
                theLog.Log.LevelDown();
                this.lblProgress.Text = text;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void AddJobText(Job job)
        {
            if (job is JobStatusUpdate)
            {
                AddTweetText((JobStatusUpdate)job);
                return;
            }

            if (job is JobPostMedia)
            {
                AddMediaText((JobPostMedia)job);
                return;
            }
        }

        private void AddMediaText(JobPostMedia job)
        {
            AddProgress(Strings.MediaUploaded);
        }

        /// <summary>
        /// Safely add item to list of tweeted things, protecting against timer threads
        /// </summary>
        /// <param name="text"></param>
        private void AddTweetText(JobStatusUpdate job)
        {
            try
            {
                theLog.Log.LevelDown();
                AddProgress(job.NewStatus.text);
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void AddProgress(string text)
        {
            lstTweets.Items.Add(text);
            lstTweets.SelectedIndex = lstTweets.Items.Count - 1;
        }

        /// <summary>
        /// Numbering settings have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTweetNumbering_SettingsChanged(object sender, NumberingChangedArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                SetMaxIntroLength();
                UpdateStatusAndPreview();

            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Set maximum length for Intro and trim if it is too long after settings have changed
        /// </summary>
        private void SetMaxIntroLength()
        {
            try
            {
                theLog.Log.LevelDown();
                SKZSoft.Twitter.TwitterData.ThreadController threadController = new ThreadController(m_mainController.TwitterData);

                TweetThread threadData = GetSettings();

                int maxIntroLength = threadController.GetMaxIntroLength(threadData);

                // if text is too long for new numbering style, warn user and trim the text.
                if (txtIntro.Text.Length > maxIntroLength)
                {
                    txtIntro.Text = txtIntro.Text.Substring(0, maxIntroLength);
                    Utils.SKZMessageBox(Strings.IntroTooLongForSettings, MessageBoxIcon.Exclamation);
                }
                txtIntro.MaxLength = maxIntroLength;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Intro text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIntro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                if (m_systemChangingText)
                {
                    return;
                }
                UpdateStatusAndPreview();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void txtMain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                if (m_systemChangingText)
                {
                    return;
                }
                UpdateStatusAndPreview();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                DoCancelTweeting();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void DoCancelTweeting()
        {
            m_threadPoster.Cancel();
        }


        /// <summary>
        /// Set form GUI controls enabled/disabled, depending on what current status is
        /// </summary>
        /// <param name="status"></param>
        private void SetFormStatuses(FormStatus status)
        {
            m_status = status;

            // strat optimistic for everything
            bool cancelEnabled = true;
            bool deleteTweetsEnabled = true;
            bool tweetThreadEnabled = true;
            bool controlsEnabled = true;
            bool cursorIsBusy = false;

            switch (m_status)
            {
                case FormStatus.Clean:
                case FormStatus.Dirty:
                    cancelEnabled = false;
                    deleteTweetsEnabled = false;
                    break;

                case FormStatus.Complete:
                    cancelEnabled = false;
                    tweetThreadEnabled = false;
                    break;

                case FormStatus.Posting:
                    controlsEnabled = false;
                    deleteTweetsEnabled = false;
                    tweetThreadEnabled = false;
                    cursorIsBusy = true;
                    break;

                case FormStatus.Cancelled:
                    cancelEnabled = false;
                    tweetThreadEnabled = false;
                    break;

                case FormStatus.Deleting:
                    controlsEnabled = false;
                    cancelEnabled = false;
                    deleteTweetsEnabled = false;
                    tweetThreadEnabled = false;
                    cursorIsBusy = true;
                    break;
                default:
                    throw new Exception("Invalid status");
            }

            // set buttons
            btnCancel.Enabled = cancelEnabled;
            btnDeleteTweets.Enabled = deleteTweetsEnabled;
            btnTweetThread.Enabled = tweetThreadEnabled;

            // set text and usercontrols
            txtIntro.Enabled = controlsEnabled;
            txtMain.Enabled = controlsEnabled;
            txtSecondsBetweenTweets.Enabled = controlsEnabled;
            ctlThreadPreview.Enabled = controlsEnabled;
            ctlTweetNumbering.Enabled = controlsEnabled;

            this.UseWaitCursor = cursorIsBusy;
        }

        private void btnDeleteTweets_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                if (!Utils.SKZConfirmationMessageBox(Strings.DeleteTweetsConfirm))
                {
                    theLog.Log.WriteDebug("User cancelled", SKZSoft.Common.Logging.LoggingSource.GUI);
                    return;
                }

                SetFormStatuses(FormStatus.Deleting);

                m_threadPoster.DeleteAll(m_lastBatch);

            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Tear down this object and stop any actions
        /// </summary>
        public override void Terminate()
        {
            try
            {
                theLog.Log.LevelDown();

                if (m_threadPoster != null)
                {
                    m_threadPoster.Terminate();
                }

                m_threadPoster = null;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void chkStartNewLineAfterIntro_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                SetMaxIntroLength();
                UpdateStatusAndPreview();
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Select tweet to reply to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTweet_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                DoSelectTweet();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
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
                theLog.Log.WriteDebug(string.Format("Tweet {0} selected", status.id), Logging.LoggingSource.GUI);
                ctlTweetText.Status = status;

                // Necessary because twitter insists the name of the other acount is included in the text.
                // First tweet just became shorter.
                UpdateStatusAndPreview();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Clear "reply to" tweet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearTweet_Click(object sender, EventArgs e)
        {
            ctlTweetText.Status = null;
            UpdateStatusAndPreview();
        }


        private string[] GetDroppedFiles(IDataObject data)
        {
            string[] files = (string[])data.GetData(DataFormats.FileDrop);
            return files;
        }

        private void lblImageDropper_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lblImageDropper_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                m_systemChangingText = true;
                this.SuspendLayout();

                // Prompt user
                if (!Utils.SKZConfirmationMessageBox(Strings.DroppingFilesNewThreadConf))
                {
                    return;
                }

                // Get and sort files
                string[] files = GetDroppedFiles(e.Data);
                Array.Sort(files);

                // Create tweet thread using file names
                StringBuilder sb = new StringBuilder();
                foreach (string f in files)
                {
                    string name = System.IO.Path.GetFileNameWithoutExtension(f);
                    sb.AppendLine("\"" + name + "\"");
                }

                // Clear old text and tweet objects
                txtMain.Text = String.Empty;
                ctlThreadPreview.Clear();

                // add new text and attach images to new tweets
                txtMain.Text = sb.ToString();
                UpdateStatusAndPreview();
                ctlThreadPreview.AddImages(files);
            }
            catch(Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally
            {
                m_systemChangingText = false;
                this.ResumeLayout();
            }

        }
    }
}
