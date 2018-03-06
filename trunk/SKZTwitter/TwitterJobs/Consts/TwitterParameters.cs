using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs.Consts
{
    internal class TwitterParameters
    {

        /// <summary>
        /// The Consumer Key parameter
        /// </summary>
        public const string oAuthConsumerKey = "oauth_consumer_key";

        /// <summary>
        /// URL to redirect to when user finishes on Twitter website in browser.
        /// Always set to "oob" for a desktop app.
        /// </summary>
        public const string oAuthCallback = "oauth_callback";

        /// <summary>
        /// Standard Http field
        /// </summary>
        public const string HttpAuthorization = "Authorization";

        /// <summary>
        /// Standard Http field
        /// </summary>
        public const string HttpUserAgent = "User-Agent";


        public const string TWITTER_PARAM_OAUTH_VERIFIER = "oauth_verifier";
        public const string TWITTER_PARAM_OAUTH_TOKEN = "oauth_token";
        public const string TWITTER_PARAM_OAUTH_TOKEN_SECRET = "oauth_token_secret";

        public const string twitter_api_id = "id";
        //public const string twitter_api_trim_user = "trim_user";
        public const string twitter_api_status = "status";
        public const string twitter_api_in_reply_to_status_id = "in_reply_to_status_id";

        public const string twitter_api_value_true = "true";

        // DMs
        public const string twitter_api_DM_recipient_id = "recipient_id";
        public const string twitter_api_DM_message_data = "message_data";

        // DM parameter group names
        public const string twitter_api_DM_paramgroup_target = "target";


    }
}
