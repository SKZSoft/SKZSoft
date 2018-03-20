using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobRetweet : TwitterJob
    {
        private Status m_retweetedStatus;
        private ulong m_id;
        internal JobRetweet(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong id)
            : base(credentials, completionDelegate)
        {
            m_id = id;
            base.ParameterStrings[TwitterParameters.twitter_api_id] = id.ToString();

            //base.Parameters[Consts.TwitterParameters.twitter_api_trim_user] = Consts.TwitterParameters.twitter_api_value_true;
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
                m_retweetedStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }


        public ulong Id { get { return m_id; } }

        /// <summary>
        /// The statuses returned from the API
        /// </summary>
        public Status RetweetedStatus { get { return m_retweetedStatus; } }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return string.Format("{0}{1}.json", URLs.URL_API_RETWEET, Id); } }


        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }
        public override HttpMethod RequestType { get { return HttpMethod.Post; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }

        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }

        public override string JobDescription { get { return TwitterDataStrings.JobDescRetweet; } }

    }
}
