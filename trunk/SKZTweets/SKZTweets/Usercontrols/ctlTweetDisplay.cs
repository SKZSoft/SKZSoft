using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.Controllers;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class TweetDisplay : UserControl
    {
        private Status m_tweet;
        private AppController m_mainController;

        public TweetDisplay()
        {
            InitializeComponent();
        }

        public void Initialize(AppController mainController)
        {
            m_mainController = mainController;
            Clear();
        }

        /// <summary>
        /// Clear the text
        /// </summary>
        public void Clear()
        {
            lblName.Text = string.Empty;
            lblScreenName.Text = string.Empty;
            lblHowLongAgo.Text = string.Empty;
            tweetText.Text = string.Empty;
        }

        /// <summary>
        /// Display tweet text
        /// </summary>
        /// <param name="tweet"></param>
        private void DisplayTweet(Status tweet)
        {
            const int CONTROL_SPACING = 2;

            m_tweet = tweet;

            if(tweet==null)
            {
                Clear();
                return;
            }


            string formatted = tweet.text.Replace("\n", Environment.NewLine);
            string screenName = tweet.user.screen_name;
            lblScreenName.Text = "@" + screenName;
            lblName.Text = tweet.user.name;
            lblHowLongAgo.Text = Utils.FormatDateTime(tweet.CreatedAtDateTime);

            lblScreenName.Top = lblName.Top;
            lblScreenName.Left = lblName.Left + lblName.Width + CONTROL_SPACING;
            lblHowLongAgo.Top = lblScreenName.Top;
            lblHowLongAgo.Left = lblScreenName.Left + lblScreenName.Width + CONTROL_SPACING;
            lblHowLongAgo.URL = tweet.OriginalURL;

            string link = m_mainController.TwitterData.GetURLForScreenName(screenName);
            lblName.URL = link;

            tweetText.Text = formatted;
        }

        /// <summary>
        /// The status for display
        /// </summary>
        public Status Status
        {
            get { return m_tweet; }
            set { DisplayTweet(value); }
        }

    }
}
