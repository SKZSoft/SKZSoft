using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterModels;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterJobs.Jobs;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Statuses
{
    public class BatchWithImages : Batch
    {
        private List<MediaUploaded> m_mediaUploaded;
        private Status m_status;
        private Update m_statusJob;
        protected Status m_replyTo;
        private BatchWithImages m_jobReplyTo;

        internal BatchWithImages(Credentials credentials, IJobRunner jobRunner, Batch parent, EventHandler<BatchCompleteArgs> completionDelegate)
            : base(credentials, jobRunner, completionDelegate)
        {
        }

        internal void CreateChildJobs(List<TwitterModels.Media> mediaItems, BatchWithImages replyToJob, string text)
        {
            m_jobReplyTo = replyToJob;
            DoCreateJobs(mediaItems, text);
        }

        internal void CreateChildJobs(List<TwitterModels.Media> mediaItems, Status replyTo, string text)
        {
            m_replyTo = replyTo;
            DoCreateJobs(mediaItems, text);
        }

        /// <summary>
        /// Create all the jobs required in order to upload the media items and the new tweet
        /// </summary>
        /// <param name="mediaItems"></param>
        /// <param name="text"></param>
        internal void DoCreateJobs(List<TwitterModels.Media> mediaItems, string text)
        { 
            m_mediaUploaded = new List<TwitterModels.MediaUploaded>();
            Text = text;

            // check that there actually IS media to upload.
            if (mediaItems != null && mediaItems.Count > 0)
            {
                // Put media items in their own batch so we can be notified when they are all uploaded
                // but before the status goes out.
                Batch batchMedia = CreateBatch(MediaBatchComplete);
                foreach (TwitterModels.Media media in mediaItems)
                {
                    TwitterJobs.Jobs.Media.Upload job = batchMedia.CreateJobPostMedia(MediaUploaded, media.media_url);
                }
            }

            // Create new status with the text to post
            m_status = new Status();
            m_status.text = Text;

            // If we are replying to something, record what.
            if (m_replyTo != null)
            {
                m_status.in_reply_to_status_id = m_replyTo.id;
                m_status.in_reply_to_screen_name = m_replyTo.user.screen_name;
            }

            // Create a new status job, which will be called after the media jobs are complete
            m_statusJob = CreateStatusUpdate(null, m_status);
        }

        private void MediaUploaded(object sender, JobCompleteArgs e)
        {
            // add this media to the list of ones the Status is going to get
            TwitterJobs.Jobs.Media.Upload job = (TwitterJobs.Jobs.Media.Upload)e.Job;
            m_mediaUploaded.Add(job.MediaUploaded);

        }

        /// <summary>
        /// The text of the tweet
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Called when the media items have all been uploaded (ie the media batch is completed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MediaBatchComplete(object sender, BatchCompleteArgs e)
        {
            // Media now uploaded.
            // Add the IDs to the status job
            StringBuilder sb = new StringBuilder(100);
            bool first = true;
            foreach(MediaUploaded img in m_mediaUploaded)
            {
                if(!first)
                {
                    sb.Append(",");
                }
                first = false;
                sb.Append(img.media_id.ToString());
            }
            m_statusJob.ParameterStrings.Add("media_ids", sb.ToString());
        }

        /// <summary>
        /// The Job which will post the new status
        /// </summary>
        public Jobs.Statuses.Update StatusJob {  get { return m_statusJob; } }

        /// <summary>
        /// Get ID of tweet to reply to (if relevant)
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
            base.InitializeFromLastJob(previousJob);

            if(m_jobReplyTo!=null)
            {
                m_statusJob.ReplyToId = m_jobReplyTo.StatusJob.NewStatus.id.ToString();
            }
        }

        internal override void Terminate()
        {
            m_statusJob = null;
            m_replyTo = null;
            m_jobReplyTo = null;
            base.Terminate();
        }
    }
}
