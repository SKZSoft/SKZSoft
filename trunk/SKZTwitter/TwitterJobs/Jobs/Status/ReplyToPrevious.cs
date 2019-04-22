using SKZTweets.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterJobs
{
    /// <summary>
    /// Replies to the tweet which was sent in the previous job.
    /// Used for creating threads
    /// </summary>
    public class JobStatusReplyToPrevious : JobStatusWithImages
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="text"></param>
        internal JobStatusReplyToPrevious(JobBatch parent, EventHandler<BatchCompleteArgs> completionDelegate, List<Media> mediaItems, string text) : 
            base(parent, completionDelegate, mediaItems, 0, text) { }

        /// <summary>
        /// Get the tweet which was sent in the previous job
        /// </summary>
        /// <param name="previousJob"></param>
        public override void Initialize(Job previousJob)
        {
            if(!(previousJob is JobStatusReplyToPrevious))
            {
                return;
            }

            JobStatusReplyToPrevious jobCast = (JobStatusReplyToPrevious)previousJob;
            m_replyToId = jobCast.StatusJob.NewStatus.id;
        }

    }
}
