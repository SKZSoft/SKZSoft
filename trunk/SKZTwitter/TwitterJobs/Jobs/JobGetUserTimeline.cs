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
    public class JobGetUserTimeline : TwitterJob
    {

        private Statuses m_statuses;

        /// <summary>
        /// Constructor. Just calls base class.
        /// </summary>
        /// <param name="parameters"></param>
        internal JobGetUserTimeline(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string screenname, int count, string start_id)
            : base(credentials, completionDelegate)
        {
            if(start_id.Length > 0)
            base.ParameterStrings["since_id"] = start_id;
            ScreenName = screenname;
            Count = count;
        }

        public string ScreenName
        {
            get
            {
                return GetParamByName("screen_name");
            }

            set
            {
                AddOrUpdateParam("screen_name", value);
            }
        }


        public int Count
        {
            get
            {
                string countAsString = GetParamByName("count");
                int count = int.Parse(countAsString);
                return count;
            }

            set
            {
                AddOrUpdateParam("count", value);
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
                m_statuses = new Statuses();
                m_statuses.Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Status>>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The statuses returned from the API
        /// </summary>
        public Statuses Statuses {  get { return m_statuses; } }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_USER_TIMELINE; } }


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

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetUserTimeline; } }

    }
}
