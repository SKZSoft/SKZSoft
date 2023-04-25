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

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Statuses
{
    public class MentionsTimeline : TwitterJob
    {
        private Status[] m_statuses;


        internal MentionsTimeline(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, int count)
            : base(credentials, completionDelegate)
        {
            base.ParameterStrings["count"] = count.ToString();

            //base.Parameters["trim_user"] = "true";
            base.ParameterStrings["include_entities"] = "false";
        }



        public Status[] Statuses {  get { return m_statuses; } }

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
                m_statuses = Newtonsoft.Json.JsonConvert.DeserializeObject<Status[]>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_STATUSES__MENTIONS_TIMELINE; } }


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

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetMentions; } }


    }
}
