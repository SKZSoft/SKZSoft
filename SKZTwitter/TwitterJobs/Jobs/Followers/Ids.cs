using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Followers
{
    public class Ids : Followers
    {

        private SKZSoft.Twitter.TwitterModels.FollowerIds m_followerIDs;

        internal Ids(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count)
            : base(credentials, completionDelegate, cursor, count)
        {
        }

        /// <summary>
        /// Populate from results
        /// </summary>
        /// <param name="results"></param>
        public override void Finalize(string JsonResults)
        {
            try
            {
                theLog.Log.LevelDown();

                m_followerIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<FollowerIds>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL
        {
            get
            {
                return URLs.URL_API_FOLLOWERS__IDS;
            }
        }


        public override string JobDescription { get { return TwitterDataStrings.JobDescGetFollowersId; } }


    }
}
