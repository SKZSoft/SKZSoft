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
    public class JobGetStatus : TwitterJob
    {
        private Status m_status;


        internal JobGetStatus(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong id, bool includeMyRetweet)
            : base(credentials, completionDelegate)
        {
            base.ParameterStrings["id"] = id.ToString();
            base.ParameterStrings["include_my_retweet"] = includeMyRetweet ? "true": "false";

            //base.Parameters["trim_user"] = "true";
            base.ParameterStrings["include_entities"] = "false";
        }


        public ulong StatusID
        {
            get
            {
                string idFromParams = base.ParameterStrings["id"];
                ulong id;
                ulong.TryParse(idFromParams, out id);
                return id;
            }
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
                m_status = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The statuses returned from the API
        /// </summary>
        public Status Status { get { return m_status; } }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_STATUSES_SHOW; } }


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


    }
}
