using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Exceptions;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterModels;
using Logging = SKZSoft.Common.Logging;


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


namespace SKZTweets_tiny_harness
{
    public partial class frmMain : Form
    {
        private Controller m_controller;
        private TwitterData m_twitterData;

        public frmMain(Controller controller)
        {
            m_controller = controller;
            m_twitterData = controller.TwitterData;

            InitializeComponent();
        }


        /// <summary>
        /// Button click to post status                                                    l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostStatus_Click(object sender, EventArgs e)
        {
            m_twitterData.PostStatus(m_controller.Credentials, txtTweet.Text, delegate_StatusPosted, delegate_ExceptionHandler);
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

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuTestsRunAll_Click(object sender, EventArgs e)
        {

        }
    }
}
