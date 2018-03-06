using SKZSoft.Twitter.TwitterJobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobDestoryRTOfPrevious : JobDestroy
    {

        internal JobDestoryRTOfPrevious(EventHandler<JobCompleteArgs> completionDelegate) : base(completionDelegate, 0) { }

        public override void InitializeFromLastJob(Job previousJob)
        {
            JobGetStatus originalTweet = (JobGetStatus)previousJob;

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
