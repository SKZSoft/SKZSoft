using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Oauth
{
    public class AccessToken : Jobs.Oauth.RequestToken
    {
        /// <summary>
        /// Constructor. Just calls base class.
        /// </summary>
        /// <param name="parameters"></param>
        internal AccessToken(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate)
            : base(credentials, completionDelegate) { }

        /// <summary>
        /// The Screen Name of the authenticated user
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public ulong AccountId { get; set; }

        public string AuthVerifier { set { AddOrUpdateParam(TwitterParameters.TWITTER_PARAM_OAUTH_VERIFIER, value); } }


        /// <summary>
        /// Populate from results
        /// </summary>
        /// <param name="results"></param>
        public override void Finalize(string httpResults)
        {
            base.Finalize(httpResults);
            Dictionary<string, string> results = GetHttpResponseAsDictionary(httpResults);

            ScreenName = ExtractItemByName(results, "screen_name");
            string accountId = ExtractItemByName(results, "user_id");
            ulong accountIdAsLong = ulong.Parse(accountId);
            AccountId = accountIdAsLong;
                
            Credentials.ScreenName = ScreenName;
            Credentials.AccountId = accountIdAsLong;
            Credentials.AuthToken = AuthToken;
            Credentials.AuthTokenSecret = AuthTokenSecret;
        }

        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL { get { return URLs.URL_API_OAUTH__ACCESS_TOKEN; } }
        public override HttpMethod RequestType { get { return HttpMethod.Get; } }

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetAccessToken; } }

    }
}
