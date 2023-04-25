using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs.Consts
{
    /// <summary>
    /// This class holds all API URLs
    /// Design decision to have them centralised rather than in each "Job" class because:
    ///  - some URLs may be used in more than one place
    ///  - naming convention is immediately clearer
    /// </summary>
    internal class URLs
    {
        public const string URL_API_DIRECTMESSAGE__NEW = "https://api.twitter.com/1.1/direct_messages/events/new.json";
        public const string URL_API_FOLLOWERS__IDS = "https://api.twitter.com/1.1/followers/ids.json";
        public const string URL_API_FOLLOWERS__LIST = "https://api.twitter.com/1.1/followers/list.json";
        public const string URL_API_MEDIA__UPLOAD = "https://upload.twitter.com/1.1/media/upload.json";
        public const string URL_API_OAUTH__REQUEST_TOKEN = "https://api.twitter.com/oauth/request_token";
        public const string URL_API_OAUTH__ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";
        public const string URL_API_HELP__CONFIGURATION = "https://api.twitter.com/1.1/help/configuration.json";
        public const string URL_API_STATUSES__DESTROY = "https://api.twitter.com/1.1/statuses/destroy/";
        public const string URL_API_STATUSES__MENTIONS_TIMELINE = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
        public const string URL_API_STATUSES__RETWEET = "https://api.twitter.com/1.1/statuses/retweet/";
        public const string URL_API_STATUSES__SHOW = "https://api.twitter.com/1.1/statuses/show.json";
        public const string URL_API_STATUSES__USER_TIMELINE = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        public const string URL_API_STATUSES__UPDATE = "https://api.twitter.com/1.1/statuses/update.json";

    }
}
