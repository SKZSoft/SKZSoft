using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs;

/*

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Statuses
{
  /// <summary>
  /// Replies to the tweet which was sent in the previous job.
  /// Used for creating threads
  /// </summary>
  public class ReplyToPrevious : Jobs.Statuses.BatchWithImages
  {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="owner"></param>
      /// <param name="completionDelegate"></param>
      /// <param name="text"></param>
      internal ReplyToPrevious(TwitterJobs.Batch parent, EventHandler<BatchCompleteArgs> completionDelegate, List<TwitterModels.Media> mediaItems, string text) : 
          base(parent, completionDelegate, mediaItems, 0, text) { }

      /// <summary>
      /// Get the tweet which was sent in the previous job
      /// </summary>
      /// <param name="previousJob"></param>
      public override void Initialize(Job previousJob)
      {
          if(!(previousJob is ReplyToPrevious))
          {
              return;
          }

          JobStatusReplyToPrevious jobCast = (JobStatusReplyToPrevious)previousJob;
          m_replyToId = jobCast.StatusJob.NewStatus.id;
      }

  }
}
*/
