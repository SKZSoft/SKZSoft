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
    public class JobStatusUpdate : TwitterJob
    {
        private Status m_status;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="status"></param>
        internal JobStatusUpdate(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, Status status) 
            : base(credentials, completionDelegate)
        {
            // If creating a thread, Twitter REQUIRES that the screenname be the included.
            // UNLESS the original tweet was by the same user.
            // In any case, chuck it in if it's needed. No harm and it doesn't count against characters
            string fullStatus = status.text;

            if(status.in_reply_to_screen_name != null)
            {
                fullStatus = "@" + status.in_reply_to_screen_name + " " + fullStatus;
            }

            if (status.in_reply_to_status_id != null && status.in_reply_to_status_id > 0)
            {
                ReplyToId = status.in_reply_to_status_id.ToString();
            }

            base.ParameterStrings[TwitterParameters.twitter_api_status] = fullStatus;
        }

        /// <summary>
        /// The ID of the tweet to reply to
        /// </summary>
        public string ReplyToId { set { base.ParameterStrings[TwitterParameters.twitter_api_in_reply_to_status_id] = value; } }


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
        public Status NewStatus { get { return m_status; } }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_STATUS_UPDATE; } }

        /// <summary>
        /// Response type
        /// </summary>
        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }

        /// <summary>
        /// Parameter Type
        /// </summary>
        public override ParameterType ParameterType { get { return ParameterType.http; } }


        /// <summary>
        /// Request type
        /// </summary>
        public override HttpMethod RequestType { get { return HttpMethod.Post; } }

        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }

        /// <summary>
        /// Human-readable job description
        /// </summary>
        public override string JobDescription { get { return TwitterDataStrings.JobDescStatusUpdate; } }

    }
}
