﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Reflection;
using SKZSoft.SKZTweets.Controllers;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Common.BrowserDetector;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.DataModels;

namespace SKZSoft.SKZTweets
{
    public partial class frmAuthorise : Form
    {
        private bool m_OK = false;
        private AppController m_controller;
        private Credentials m_partialCredentials;
        private TwitterAccount m_twitterAccount;

        public frmAuthorise(Credentials partialCredentials, AppController controller)
        {
            try
            {
                theLog.Log.LevelDown();
                m_controller = controller;
                m_partialCredentials = partialCredentials;
                InitializeComponent();

                string appName = string.Format(" {0} v{1}", Strings.AppName, typeof(frmAuthorise).Assembly.GetName().Version);
                this.Text += appName;

                BrowserDetector browsers = new BrowserDetector(true, Strings.BrowserUseDefault);
                foreach(Browser browser in browsers.Items.Values)
                {
                    cmbBrowser.Items.Add(browser);
                    if(cmbBrowser.Items.Count>0)
                    {
                        cmbBrowser.SelectedIndex = 0;
                    }
                }
            }
            finally { theLog.Log.LevelUp(); }

        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                // Get auth token required to launch Twitter in browser.
                // Method stores the token away itself; no need to handle returned job here
                // Will return control to the delegate method, which will launch twitter etc
                m_controller.TwitterData.GetAuthTokenStart(m_partialCredentials, GetAuthTokenJobEnd, GetAuthTokenEnd, ExceptionHandler);

                // error if the button is clicked twice.
                // For now, just don't let that happen.
                btnLaunch.Enabled = false;
           }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void ExceptionHandler(object sender, JobExceptionArgs e)
        {
            Utils.HandleException(e.Exception);
        }

        private void GetAuthTokenJobEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                JobGetAuthToken job = (JobGetAuthToken)e.Job;

                // Update credentials with result
                m_partialCredentials = job.Credentials;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void GetAuthTokenEnd(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Launch browser with app authorisation screen - MUST happen AFTER GetAuthToken

                Browser selectedBrowser = (Browser)cmbBrowser.SelectedItem;
                m_controller.TwitterData.LaunchTwitterSignin(m_partialCredentials, selectedBrowser.ShellCommand);

                // enable controls
                txtCode.Enabled = true;
                btnAuthorise.Enabled = true;
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void btnAuthorise_Click(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                HandlePIN(txtCode.Text);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void HandlePIN(string pin)
        {
            try
            {
                theLog.Log.LevelDown();
                m_controller.TwitterData.HandlePINStart(m_partialCredentials, JobCredentialsEnd, HandlePINEnd, ExceptionHandler, pin);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void HandlePINEnd(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                m_OK = true;

                m_twitterAccount = new TwitterAccount(m_partialCredentials.AccountId, m_partialCredentials.ScreenName, m_partialCredentials.AuthToken, m_partialCredentials.AuthTokenSecret, ctlColorPicker.SelectedBackColor, ctlColorPicker.SelectedForeColor);
                this.Hide();
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void JobCredentialsEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                JobGetAccessToken job = (JobGetAccessToken)e.Job;
                m_partialCredentials = job.Credentials;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Method which is used to perform the authorisation process
        /// </summary>
        /// <returns></returns>
        public TwitterAccount AuthoriseTwitter()
        {
            try
            {
                theLog.Log.LevelDown();
                this.ShowDialog();
                if (m_OK)
                {
                    return m_twitterAccount;
                }

                return null;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private void frmAuthorise_Load(object sender, EventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();
                this.Icon = Utils.GetIconFromBitmap(Properties.Resources.Connect_16x);
            }
            finally { theLog.Log.LevelUp(); }
        }

    }
}