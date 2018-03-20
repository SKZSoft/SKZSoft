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
using Newtonsoft.Json.Linq;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobDMSend : TwitterJob
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="status"></param>
        internal JobDMSend(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong recipientId, string text) 
            : base(credentials, completionDelegate)
        {
            JObject messageDataNode = new JObject();
            JValue textValue = new JValue(text);
            messageDataNode.Add("text", textValue);

            JObject targetNode = new JObject();
            JValue recipientIdValue = new JValue(recipientId);
            targetNode.Add(TwitterParameters.twitter_api_DM_recipient_id, recipientIdValue);

            JObject eventData = new JObject();
            JValue typeValue = new JValue("message_create");
            eventData.Add("type", typeValue);


            JObject messageCreateNode = new JObject();
            messageCreateNode.Add("target", targetNode);
            messageCreateNode.Add("message_data", messageDataNode);

            eventData.Add("message_create", messageCreateNode);

            JObject paramsBase = base.ParmatersJson;
            paramsBase.Add("event", eventData);
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
                //m_status = Newtonsoft.Json.JsonConvert.DeserializeObject<Status>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_DIRECT_MESSAGE_CREATE; } }

        /// <summary>
        /// Response type
        /// </summary>
        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }

        /// <summary>
        /// Parameter Type
        /// </summary>
        public override ParameterType ParameterType { get { return ParameterType.json; } }


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
        public override string JobDescription { get { return TwitterDataStrings.JobDescDMSend; } }

    }
}
