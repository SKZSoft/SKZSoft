using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZTweets.Controllers;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZTweets.TwitterData.Models;
using SKZTweets.TwitterModels;
using SKZTweets.TwitterJobs;
using SKZTweets.TwitterData;


namespace SKZTweets
{
    public partial class frmSelectTweet : Form
    {
        private AppController m_mainController;
        private Status m_selectedTweet;

        public frmSelectTweet(AppController mainController, string screenName)
        {
            m_mainController = mainController;
            InitializeComponent();
            tweetDisplay.Initialize(mainController);
            txtScreenNameOrUrl.Text = screenName;
        }

        private void frmSelectTweet_Load(object sender, EventArgs e)
        {
            tweetList.TweetSelected += TweetList_TweetSelected;
        }

        private void TweetList_TweetSelected(object sender, TweetSelectedArgs e)
        {
            tweetDisplay.Status = e.Status;
        }

        private void frmSelectTweet_Shown(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                DoSearch();

                tweetList.Focus();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally
            {
                theLog.Log.LevelUp();
            }
        }

        private void ExceptionHandler(object sender, JobExceptionArgs e)
        {
            Utils.HandleException(e.Exception);
        }



        private void GetRecentStatusesCurrentUserEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                JobGetUserTimeline castJob = (JobGetUserTimeline)e.Job;

                // Populate listbox
                tweetList.SetTweets(castJob.Statuses);
            }

            finally
            {
                theLog.Log.LevelUp();
                Cursor.Current = System.Windows.Forms.Cursors.Default;
            }


        }


        public Status GetTweet()
        {
            this.ShowDialog();

            return m_selectedTweet;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DoSelected();
        }


        private void DoSelected()
        {
            // get selected tweet
            Status status = (Status)tweetList.SelectedItem;
            if (status == null)
            {
                // nothing selected
                Utils.SKZMessageBox(Strings.PleaseSelectATweetFromTheList, MessageBoxIcon.Information);
                return;
            }

            m_selectedTweet = status;
            this.Hide();
        }

        private void tweetList_DoubleClick(object sender, EventArgs e)
        {
            // this check is also done in the method.
            // But if the user double-clicks on an empty space
            // we don't want to throw up a messagebox.
            if (tweetList.SelectedItem != null)
            {
                DoSelected();
            }
        }



        private void MakeSearchDefault()
        {
            this.AcceptButton = btnSearch;
        }


        private void MakeSelectDefault()
        {
            this.AcceptButton = btnSelect;
        }

        private void tweetList_Enter(object sender, EventArgs e)
        {
            MakeSelectDefault();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            tweetList.Clear();
            tweetDisplay.Clear();

            string parameter = txtScreenNameOrUrl.Text;
            if(parameter.StartsWith("@"))
            {
                // remove @ from screen name
                parameter = parameter.Replace("@", "");
            }

            if(parameter.StartsWith("http"))
            {
                // it's a URL.
                // get the tweet ID from it
                ulong tweetId = GetTweetIDFromURL(txtScreenNameOrUrl.Text);
                if(tweetId==0)
                {
                    Utils.SKZMessageBox(Strings.NotaTweetURL, MessageBoxIcon.Error);
                    return;
                }
                m_mainController.TwitterData.GetOriginalTweetByIdStart(null, GetOriginalTweetByIdEnd, ExceptionHandler, tweetId);
            }
            else
            {
                // screen name
                // Results will come back into the delegate method
                m_mainController.TwitterData.GetRecentStatusesForUserStart(GetRecentStatusesCurrentUserEnd, ExceptionHandler, Consts.TweetsToFetchForSelection, txtScreenNameOrUrl.Text);
            }

            tweetList.Focus();
        }


        private void GetOriginalTweetByIdEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                JobGetStatus castJob = (JobGetStatus)e.Job;

                Status originalTweet = castJob.Status;

                if (originalTweet == null)
                {
                    Utils.SKZMessageBox(Strings.NoTweetAtURL, MessageBoxIcon.Error);
                }
                else
                {
                    Statuses statuses = new Statuses();
                    statuses.Items.Add(originalTweet);
                    tweetList.SetTweets(statuses);
                }
            }
            finally { theLog.Log.LevelUp(); }

        }



        private ulong GetTweetIDFromURL(string url)
        {
            // remove any trailing slash
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            int index = url.LastIndexOf("/");

            // this last section SHOULD be the ID
            string number = url.Substring(index + 1);

            ulong result;
            if(!ulong.TryParse(number, out result))
            {
                // bad URL
                result = 0;
            }

            return result;
        }

        private void txtScreenNameOrUrl_Enter(object sender, EventArgs e)
        {
            MakeSearchDefault();
        }
    }
}
