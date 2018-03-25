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

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobGetFollowersIds : TwitterJob
    {

        SKZSoft.Twitter.TwitterModels.FollowerIds m_followerIDs;

        internal JobGetFollowersIds(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count)
            : base(credentials, completionDelegate)
        {
            base.ParameterStrings["count"] = count.ToString();
            base.ParameterStrings["cursor"] = cursor;
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

                // deserialize Json into matching objects
                m_followerIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<FollowerIds>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_FOLLOWERRS_IDS; } }


        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }
        public override HttpMethod RequestType { get { return HttpMethod.Get; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }

        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetStatus; } }

        /// <summary>
        /// Array of ulong Follower IDs returned by Twitter
        /// </summary>
        public ulong[] FollowerIds
        {
            get { return m_followerIDs.ids; }
        }

        /// <summary>
        /// ID of the next cursor to pass in to Twitter
        /// </summary>
        public string NextCursor
        {
            get { return m_followerIDs.next_cursor_str; }
        }

    }
}
