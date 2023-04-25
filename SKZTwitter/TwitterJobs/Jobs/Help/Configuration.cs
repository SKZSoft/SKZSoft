using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Help
{
    public class Configuration : TwitterJob
    {

        private TwitterConfiguration m_configuration;

        /// <summary>
        /// Constructor. Just calls base class.
        /// </summary>
        /// <param name="parameters"></param>
        internal Configuration(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate) 
            : base(credentials, completionDelegate) { }

        /// <summary>
        /// The twitter configuration
        /// </summary>
        public TwitterConfiguration TwitterConfiguration {  get { return m_configuration; } }

        /// <summary>
        /// Populate from results
        /// </summary>
        /// <param name="results"></param>
        public override void Finalize(string JsonResults)
        {
            m_configuration = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterConfiguration>(JsonResults);
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_HELP__CONFIGURATION; } }


        public override HttpMethod RequestType { get { return HttpMethod.Get; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }
        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }


        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }


        public override string JobDescription { get { return TwitterDataStrings.JobDescGetConfig; } }


    }
}
