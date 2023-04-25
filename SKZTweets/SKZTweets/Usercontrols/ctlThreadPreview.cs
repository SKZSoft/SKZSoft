using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Models;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.Usercontrols.Interfaces;
using SKZSoft.SKZTweets.Models;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class ctlThreadPreview : UserControl, ILocalImageContainer 
    {
        private const int TWEET_SPACING = 2;
        private SKZSoft.Twitter.TwitterData.TwitterData m_twitterData;
        private List<ctlTweetTextAndPictures> m_tweetTextControls;

        public ctlThreadPreview()
        {
            InitializeComponent();

            try
            {
                m_tweetTextControls = new List<ctlTweetTextAndPictures>();
            }
            catch { }
        }

        /// <summary>
        /// The Twitter Data object
        /// </summary>
        public SKZSoft.Twitter.TwitterData.TwitterData TwitterData {  set { m_twitterData = value; } }


        public void Clear()
        {
            TweetThread data = new TweetThread();
            data.IntroText = string.Empty;
            data.ThreadText = string.Empty;
            ShowTweets(data);
        }

        /// <summary>
        /// Show tweets as they will be handled by Twitter
        /// </summary>
        /// <param name="threadData"></param>
        public void ShowTweets(SKZSoft.Twitter.TwitterData.Models.TweetThread threadData)
        {
            try
            {
                // remember this. It's where we are scrolled to.
                Point autoScrollPos = pnlTweets.AutoScrollPosition;

                theLog.Log.LevelDown();
                pnlTweets.SuspendLayout();

                SKZSoft.Twitter.TwitterData.ThreadController threadController = new ThreadController(m_twitterData);
                Queue<string> tweets = threadController.SplitIntoTexts(threadData, ThreadUrlFormat.Full);

                int top = 0;
                int tweetIndex = 0;
                foreach (string tweet in tweets)
                {
                    ctlTweetTextAndPictures text;
                    if (m_tweetTextControls.Count <= tweetIndex)
                    {
                        // create a new tweet control and add to form and collection
                        text = new ctlTweetTextAndPictures(this);
                        text.ResizeRequired += M_ResizeRequired;
                        m_tweetTextControls.Add(text);
                        pnlTweets.Controls.Add(text);

                        // do not use "ClientWidth", as it decreases when a scrollbar appears.
                        // That makes this complex. Let's just leave room for the scrollbar.
                        text.Width = pnlTweets.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

                        // bug in c#: cannot use anchor in an AutoScroll control
                        // https://msdn.microsoft.com/en-us/library/system.windows.forms.scrollablecontrol.autoscroll(v=vs.110).aspx
                        //text.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    }
                    else
                    {
                        // re-use existing control
                        text = m_tweetTextControls[tweetIndex];
                    }

                    // set text BEFORE determining size
                    text.Text = tweet;
                    text.ResizeToTextAndThumbnailHeight();

                    // set tweet to new top, adjusted for autoscroll position (0 means "top of VIEWABLE area")
                    text.Top = top + autoScrollPos.Y;

                    top += text.Height + TWEET_SPACING;

                    tweetIndex++;
                }
                
                // finished ADDing and AMENDING existing controls.
                // All tweets are done. Any further controls are 
                // no longer required.
                while(tweetIndex < m_tweetTextControls.Count)
                {
                    ctlTweetTextAndPictures oldControl = m_tweetTextControls[tweetIndex];
                    oldControl.ResizeRequired -= M_ResizeRequired;
                    m_tweetTextControls.RemoveAt(tweetIndex);
                    pnlTweets.Controls.Remove(oldControl);
                    oldControl.Dispose();
                }
            }
            finally
            {
                pnlTweets.AutoScroll = true;
                pnlTweets.VerticalScroll.Visible = true;
                pnlTweets.ResumeLayout();
                theLog.Log.LevelUp();
            }
        }


        private void M_ResizeRequired(object sender, StatusResizeRequiredArgs args)
        {
            DoResize();
        }

        private void DoResize()
        {
            try
            {
                this.SuspendLayout();
                
                // remember this. It's where we are scrolled to.
                Point autoScrollPos = pnlTweets.AutoScrollPosition;

                // a status has changed height.
                // let's just do the simple thing, and resize EVERYTHING.
                int top = 0;
                foreach (ctlTweetTextAndPictures ctl in m_tweetTextControls)
                {
                    ctl.Top = top + autoScrollPos.Y;
                    ctl.Width = pnlTweets.Width;
                    int height = ctl.ResizeToTextAndThumbnailHeight();
                    top += height + TWEET_SPACING;
                }
            }
            finally { this.ResumeLayout(); }

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            try
            {
                DoResize();
            }
            catch
            {
                //Utils.HandleException(ex);
            }
        }


        /// <summary>
        /// Accept a list of statuses which MUST correspond, verbatim, with the text shown in the preview area.
        /// Adds images to the tweets
        /// </summary>
        /// <param name="statuses"></param>
        public Queue<Status> DecorateStatusesWithImages(Queue<Status> statuses)
        {
            Queue<Status> results = new Queue<Status>();

            int indexStatus = 0;
            while(statuses.Count>0)
            {
                Status status = statuses.Dequeue();
                results.Enqueue(status);

                // get control associated with this tweet
                ctlTweetTextAndPictures control = m_tweetTextControls[indexStatus];

                // get local images
                List<LocalImage> imageList = control.LocalImages;

                // create extended entities if not already existing
                if(status.extended_entities==null)
                {
                    status.extended_entities = new ExtendedEntities();
                }

                // create array of media items
                Media[] mediaItems = new Media[imageList.Count];

                // populate array with local images.
                status.extended_entities.media = mediaItems;
                int indexMedia = 0;
                foreach(LocalImage img in imageList)
                {
                    Media item = new Media();
                    mediaItems[indexMedia] = item;
                    item.media_url = img.Path;
                    indexMedia++;
                }

                indexStatus++;
            }
            return results;
        }

        void ILocalImageContainer.SwapImages(LocalImageDragData draggedImage, LocalImageDragData droppedOn)
        {
            draggedImage.RemovedFromParent();

            int destIndex = droppedOn.ParentIndex;

            // if the image dropped ON was AFTER the image which was being dropped, AND they are on the same control...
            if(draggedImage.ParentControl == droppedOn.ParentControl)
            {
                if(destIndex > draggedImage.ParentIndex)
                {
                    // by REMOVING the control, we affected the indexes. Compensate.
                    destIndex--;
                }
            }

            droppedOn.ParentControl.InsertImage(destIndex, draggedImage.LocalImage);
        }

        public void AddImages(string[] pathnames)
        {
            int tweet = 0;
            foreach(string file in pathnames)
            {
                ctlTweetTextAndPictures ctl = m_tweetTextControls[tweet];
                ctl.AddFile(file, 0);
                ctl.ShowImages();
                tweet++;
            }
        }

    }
}
