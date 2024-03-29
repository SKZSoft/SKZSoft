﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;

using System.Net.Http;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Oauth
{
    public class RequestToken : TwitterJob
    {
        /// <summary>
        /// Constructor. Just calls base class.
        /// </summary>
        /// <param name="parameters"></param>
        internal RequestToken(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate)
            : base(credentials, completionDelegate) { }

        /// <summary>
        /// Auth Token
        /// </summary>
        public string AuthToken
        {
            get
            {
                return ParameterStrings[TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN];
            }
            set
            {
                AddOrUpdateParam(TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN, value);
            }
        }

        /// <summary>
        /// Auth Token Secret
        /// </summary>
        public string AuthTokenSecret
        {
            get
            {
                return ParameterStrings[TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN_SECRET];
            }
            set
            {
                AddOrUpdateParam(TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN_SECRET, value);
            }
        }

        /// <summary>
        /// Populate properties from results of call
        /// </summary>
        /// <param name="results"></param>
        public override void Finalize(string resultsAsString)
        {
            Dictionary<string, string> results = GetHttpResponseAsDictionary(resultsAsString);

            AuthToken = ExtractItemByName(results, TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN);
            AuthTokenSecret = ExtractItemByName(results, TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN_SECRET);

            Credentials.AuthToken = AuthToken;
            Credentials.AuthTokenSecret = AuthTokenSecret;
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL {  get { return URLs.URL_API_OAUTH__REQUEST_TOKEN; } }

        public override ApiResponseType ResponseType { get { return ApiResponseType.http; } }
        public override HttpMethod RequestType { get { return HttpMethod.Get; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }

        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetAuthToken; } }


    }
}
