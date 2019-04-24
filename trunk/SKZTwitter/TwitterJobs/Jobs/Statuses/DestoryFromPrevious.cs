using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Statuses
{
    public class DestoryRTOfPrevious : Destroy
    {

        internal DestoryRTOfPrevious(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate) 
            : base(credentials, completionDelegate, 0) { }

        public override void InitializeFromLastJob(Job previousJob)
        {
            Show originalTweet = (Show)previousJob;

            if (originalTweet.Status.current_user_retweet != null)
            {
                // populate the ID from the tweet in the previous job
                m_id = originalTweet.Status.current_user_retweet.id;
                Skipped = false;
            }
            else
            {
                // no valid tweet found. Do not delete.
                Skipped = true;
            }
        }

        public override string JobDescription { get { return TwitterDataStrings.JobDescDestroy; } }

    }
}
