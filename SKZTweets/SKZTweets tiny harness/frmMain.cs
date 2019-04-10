using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Exceptions;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging = SKZSoft.Common.Logging;


namespace SKZTweets_tiny_harness
{
    public partial class frmMain : Form
    {
        private HttpClient m_httpClient;            // single instance for use throughout the app.
        private SKZSoft.Twitter.TwitterData.TwitterData m_twitterData;    // the comms layer
        private Credentials m_credentials;


        public frmMain()
        {
            InitializeComponent();

            ////////////////////////////////
            // Logging initialisation
            ////////////////////////////////

            // Logging MUST be created
            Logging.LogSettings settings = new Logging.LogSettings();
            settings.LogFileName = "SKZTweetsHarness";
            settings.LogFileExtension = "log";
            settings.UnhandledLogFileName = "SKZTweetsHarnessUnprocessed";
            settings.UnhandledFileExtension = ".log";
            settings.AppName = "SKZTweetsHarness";
            settings.DeleteAfterDays = 1;

            Logging.Logger.Initialise(settings);


            ////////////////////////////////
            // Http initialisation
            ////////////////////////////////

            // Create single httpclient for the application
            HttpClientHandler handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip;
            }

            m_httpClient = new HttpClient(handler);
        }

        /// <summary>
        /// Initialise twitter credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogInToTwitter_Click(object sender, EventArgs e)
        {
            frmSecurityDetails security = new frmSecurityDetails();
            m_twitterData = security.GetTwitterData(m_httpClient);
            m_credentials = security.Credentials;
            btnPostStatus.Enabled = true;
        }

        /// <summary>
        /// Button click to post status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostStatus_Click(object sender, EventArgs e)
        {
            m_twitterData.PostStatus(m_credentials, txtTweet.Text, delegate_StatusPosted, delegate_ExceptionHandler);
        }


        /// <summary>
        /// Delegate called when post action completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delegate_StatusPosted(object sender, BatchCompleteArgs e)
        {
            lstProgress.Items.Add("posted status: " + txtTweet.Text);
        }


        /// <summary>
        /// Called by comms layer if an exception has occurred
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delegate_ExceptionHandler(object sender, JobExceptionArgs e)
        {
            MessageBox.Show(e.Exception.ToString());

            if(e.Exception is TwitterUnauthorizedException)
            {
                MessageBox.Show("Did you remember to make your app read and write WITH direct messages?");
            }
        }

    }
}
