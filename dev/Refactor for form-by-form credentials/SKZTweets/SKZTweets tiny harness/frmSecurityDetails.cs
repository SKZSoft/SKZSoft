using SKZTweets.TwitterData;
using SKZTweets.TwitterJobs;
using System;
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
        private SKZTweets.TwitterData.TwitterData m_twitterData;    // the comms layer
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
            m_twitterData = new TwitterData(m_httpClient, txtConsumerKey.Text, txtConsumerSecret.Text, Globals.oAuthCallbackValue, Globals.UserAgent);


            // Get auth token required to launch Twitter in browser.
            // Method stores the token away itself; no need to handle returned job here
            // Will return control to the delegate method, which will launch twitter etc
            m_twitterData.GetAuthTokenStart(delegate_GetAuthTokenEnd, delegate_ExceptionHandler);
        }


        /// <summary>
        /// Called when Twitter has provided a token to authenticate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delegate_GetAuthTokenEnd(object sender, EventArgs e)
        {
            // Launch browser with app authorisation screen - MUST happen AFTER GetAuthToken
            m_twitterData.LaunchTwitterSignin();

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

        private void btnAuthorise_Click(object sender, EventArgs e)
        {
            m_twitterData.HandlePINStart(delegate_HandlePINEnd, delegate_ExceptionHandler, txtAuthCode.Text);
        }


        private void delegate_HandlePINEnd(object sender, EventArgs e)
        {
            // all work is done. The comms layer has initialised itself.
            this.Hide();
        }

    }


}
