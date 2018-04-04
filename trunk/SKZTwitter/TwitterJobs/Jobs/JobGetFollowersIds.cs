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
    public class JobGetFollowers : TwitterJob
    {

        public enum GetFollowerType
        {
            ids,
            fullData
        }

        private GetFollowerType m_getFollowerType;
        private SKZSoft.Twitter.TwitterModels.FollowerIds m_followerIDs;
        private TwitterModels.Users m_users;


        internal JobGetFollowers(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count, GetFollowerType getFollowerType)
            : base(credentials, completionDelegate)
        {
            m_getFollowerType = getFollowerType;
            base.ParameterStrings["count"] = count.ToString();
            base.ParameterStrings["cursor"] = cursor;
        }

        public Users Users { get { return m_users; } }

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
                switch (m_getFollowerType)
                {
                    case GetFollowerType.ids:
                        m_followerIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<FollowerIds>(JsonResults);
                        break;

                    case GetFollowerType.fullData:
                        m_users = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterModels.Users>(JsonResults);
                        break;

                    default:
                        throw new NotImplementedException("Type of request not known");
                }
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
                switch(m_getFollowerType)
                {
                    case GetFollowerType.ids:
                        return URLs.URL_API_GET_FOLLOWERS_IDS;

                    case GetFollowerType.fullData:
                        return URLs.URL_API_GET_FOLLOWERS;

                    default:
                        throw new NotImplementedException("request type not known");
                }
                
            }
        }


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
        /// ID of the next cursor to pass in to Twitter
        /// </summary>
        public string NextCursor
        {
            get { return m_users.next_cursor_str; }
        }

    }
}
