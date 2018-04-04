using SKZSoft.Twitter.TwitterJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Consts;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterData
{
    /// <summary>
    /// Class to get all followers by making repeated calls to the GetFollowersIds method
    /// </summary>
    public class GetAllFollowers
    {
        bool m_inProgress = false;
        TwitterData m_twitterData;
        long m_batchSize;
        EventHandler<FollowerIDArgs> m_gotFollowersDelegate;
        EventHandler<JobExceptionArgs> m_exceptionDelegate;
        EventHandler<JobCompleteArgs> m_completedJobDelegate;
        List<TwitterModels.User> m_followers;

        public GetAllFollowers(TwitterData twitterData, long batchSize, EventHandler<FollowerIDArgs> GotFollowersDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, EventHandler<JobCompleteArgs> completedJobDelegate)
        {
            try
            {
                theLog.Log.LevelDown();
                m_twitterData = twitterData;

                if(batchSize < 1 || batchSize > Twitter.TwitterData.Consts.DataConsts.MAX_BATCH_SIZE_FOLLOWER_IDS)
                {
                    throw new ArgumentOutOfRangeException("Batch size must be between 1 and 5000");
                }

                m_batchSize = batchSize;
                m_gotFollowersDelegate = GotFollowersDelegate;
                m_exceptionDelegate = exceptionDelegate;
                m_completedJobDelegate = completedJobDelegate;
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Start to retrieve the data
        /// </summary>
        public void Begin(Credentials credentials)
        {
            try
            {
                theLog.Log.LevelDown();
                if (m_inProgress)
                {
                    throw new Exception("Batch already in progress");
                }

                m_followers = new List<User>();

                m_twitterData.GetFollowers(credentials, m_batchSize, JobGetFollowers.GetFollowerType.fullData, null, m_exceptionDelegate, GotSomeFollowers);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// A bunch of followers have been returned from Twitter.
        /// Handle them and request more if we have not reached the end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GotSomeFollowers(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                JobGetFollowers job = (JobGetFollowers)e.Job;

                // store these users
                foreach(TwitterModels.User user in job.Users.users)
                {
                    m_followers.Add(user);
                }

                if(job.NextCursor == "0")
                {
                    // end of the road. Nothing more to get.
                    CallBatchComplete();
                    return;
                }

                // we have more to go. Issue another request.
                m_twitterData.GetFollowers(job.Credentials, job.NextCursor, m_batchSize, JobGetFollowers.GetFollowerType.fullData, null, m_exceptionDelegate, GotSomeFollowers);

            }
            finally { theLog.Log.LevelUp(); }

        }


        private void CallBatchComplete()
        {
            try
            {
                theLog.Log.LevelDown();
                
                // Let the caller know we are done.
                EventHandler<FollowerIDArgs> handler = m_gotFollowersDelegate;
                if (handler != null)
                {
                    FollowerIDArgs e = new FollowerIDArgs(m_followers);
                    handler(this, e);
                }
            }
            finally { theLog.Log.LevelUp(); }

        }
    }
}
