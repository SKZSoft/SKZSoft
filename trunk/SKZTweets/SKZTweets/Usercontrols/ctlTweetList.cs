using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Models;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class ctlTweetList : UserControl
    {
        public ctlTweetList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate the list with the tweets provided
        /// </summary>
        /// <param name="tweets"></param>
        public void SetTweets(Statuses statuses)
        {
            try
            {
                theLog.Log.LevelDown();
                lstTweets.Items.Clear();

                foreach (Status status in statuses.Items)
                {
                    lstTweets.Items.Add(status);
                }

                if (lstTweets.Items.Count > 0)
                {
                    lstTweets.SelectedIndex = 0;
                }

            }
            finally { theLog.Log.LevelUp(); }
        }

        #region events
        /// <summary>
        /// Raised when all tweets sent
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler<TweetSelectedArgs> TweetSelected;
        protected virtual void OnTweetSelected(Status status)
        {
            EventHandler<TweetSelectedArgs> handler = TweetSelected;
            if (handler != null)
            {
                TweetSelectedArgs e = new TweetSelectedArgs(status);
                handler(this, e);
            }
        }


        private void lstTweets_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTweetSelected(SelectedItem);
        }



    #endregion

    
        public void Clear()
        {
            SetTweets(new Statuses());
        }
    /// <summary>
    /// Get selected item
    /// </summary>
    /// <returns>NULL is no selection or the Status which is selected</returns>
    public Status SelectedItem
        {
            get
            {
                try
                {
                    theLog.Log.LevelDown();
                    if (lstTweets.SelectedIndex == -1)
                    {
                        return null;
                    }

                    Status status = (Status)lstTweets.SelectedItem;
                    return status;
                }
                finally { theLog.Log.LevelUp(); }
            }
        }

        private void lstTweets_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.OnDoubleClick(e);
            }
            catch (Exception ex)
            {
                Utils.HandleException(ex);
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
