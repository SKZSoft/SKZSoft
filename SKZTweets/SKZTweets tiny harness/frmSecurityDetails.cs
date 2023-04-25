using SKZSoft.Common.IniFile;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZTweets_tiny_harness
{
    public partial class frmSecurityDetails : Form
    {
        private Credentials m_credentials;
        private SKZSoft.Twitter.TwitterData.TwitterData m_twitterData;    // the comms layer
        private HttpClient m_httpClient;

        public frmSecurityDetails()
        {
            InitializeComponent();
        }

        private void frmSecurityDetails_Load(object sender, EventArgs e)
        {
            linkLabel.Links.Add(6, 35, "https://apps.twitter.com/");
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        /// <summary>
        /// Launch Twitter to authorise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLaunchTwitter_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtConsumerKey.Text))
            {
                MessageBox.Show("Enter a consumer key");
            }
            if (string.IsNullOrEmpty(txtConsumerSecret.Text))
            {
                MessageBox.Show("Enter a consumer secret");
            }

            // create initial data layer with just minimal data since user credentials are not available
            m_twitterData = new TwitterData(m_httpClient, "oob", "Test harness");

            // initialise Consumer defaults with this app's ID
            ConsumerData.ConsumerKey = txtConsumerKey.Text;
            ConsumerData.ConsumerSecret = txtConsumerSecret.Text;

            m_credentials = new Credentials("", "", "", 0);

            // Get auth token required to launch Twitter in browser.
            // Method stores the token away itself; no need to handle returned job here
            // Will return control to the delegate method, which will launch twitter etc
            m_twitterData.GetRequestTokenStart(m_credentials, delegate_GetRequestTokenJobEnd, delegate_GetAuthTokenEnd, delegate_ExceptionHandler);
        }


        private void delegate_GetRequestTokenJobEnd(object sender, JobCompleteArgs e)
        {
            try
            {
                SKZSoft.Twitter.TwitterJobs.Jobs.Oauth.RequestToken job = (SKZSoft.Twitter.TwitterJobs.Jobs.Oauth.RequestToken)e.Job;

                // Update credentials with result
                m_credentials = job.Credentials;
            }
            finally {  }
        }



        /// <summary>
        /// Called when Twitter has provided a token to authenticate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delegate_GetAuthTokenEnd(object sender, EventArgs e)
        {
            // Launch browser with app authorisation screen - MUST happen AFTER GetAuthToken
            m_twitterData.LaunchTwitterSignin(m_credentials);

            // enable controls
            txtAuthCode.Enabled = true;
            btnAuthorise.Enabled = true;
        }


        /// <summary>
        /// Called by comms layer if an exception has occurred
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delegate_ExceptionHandler(object sender, JobExceptionArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }


        /// <summary>
        /// Called by form requesting authorisation. Returns initialised comms layer.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public TwitterData GetTwitterData(HttpClient httpClient)
        {
            m_httpClient = httpClient;


            this.ShowDialog();

            return m_twitterData;
        }

        public Credentials Credentials { get { return m_credentials; } }

        private void btnAuthorise_Click(object sender, EventArgs e)
        {
            m_twitterData.HandlePINStart(m_credentials, delegate_JobCredentialsEnd, delegate_HandlePINEnd, delegate_ExceptionHandler, txtAuthCode.Text);
        }

        private void delegate_JobCredentialsEnd(object sender, JobCompleteArgs e)
        {
            SKZSoft.Twitter.TwitterJobs.Jobs.Oauth.RequestToken job = (SKZSoft.Twitter.TwitterJobs.Jobs.Oauth.RequestToken)e.Job;
            m_credentials = job.Credentials;
        }


        private void delegate_HandlePINEnd(object sender, EventArgs e)
        {
            // all work is done. The comms layer has initialised itself.
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // initialise Consumer defaults with this app's ID
            ConsumerData.ConsumerKey = txtConsumerKey.Text;
            ConsumerData.ConsumerSecret = txtConsumerSecret.Text;

            m_credentials = new Credentials(txtAccessToken.Text, txtAccessTokenSecret.Text, "", 0);
            this.Hide();
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            string fullPath = string.Empty;
            string userPath = txtConfigFilePath.Text;
            if (!Path.IsPathRooted(userPath))
            {
                // relative path is given.
                // append to current exe path
                string currentPath = Directory.GetCurrentDirectory();
                fullPath = Path.Combine(new string[] { currentPath, userPath });
            }
            else
            {
                fullPath = userPath;
            }


            IniFile ini = new IniFile(fullPath);

            // initialise Consumer defaults with this app's ID
            ConsumerData.ConsumerKey = ini.GetEntry("ConsumerKey");
            ConsumerData.ConsumerSecret = ini.GetEntry("ConsumerSecret");
            string accessToken = ini.GetEntry("AccessToken");
            string accessTokenSecret = ini.GetEntry("AccessTokenSecret");
            string screenName = ini.GetEntry("ScreenName");
            ulong userId = ini.GetEntryAsUlong("UserId");
            m_credentials = new Credentials(accessToken, accessTokenSecret, screenName, userId);

            this.Hide();
        }
    }


}
