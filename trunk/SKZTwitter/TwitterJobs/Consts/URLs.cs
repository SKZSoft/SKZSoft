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
        public const string URL_API_DIRECT_MESSAGE_NEW = "https://api.twitter.com/1.1/direct_messages/events/new.json";
        public const string URL_API_FOLLOWERS_IDS = "https://api.twitter.com/1.1/followers/ids.json";
        public const string URL_API_FOLLOWERS_LIST = "https://api.twitter.com/1.1/followers/list.json";
        public const string URL_API_MEDIA_UPLOAD = "https://upload.twitter.com/1.1/media/upload.json";
        public const string URL_API_OAUTH_REQUEST_TOKEN = "https://api.twitter.com/oauth/request_token";
        public const string URL_API_OAUTH_ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";
        public const string URL_API_HELP_CONFIGURATION = "https://api.twitter.com/1.1/help/configuration.json";
        public const string URL_API_STATUSES_DESTROY = "https://api.twitter.com/1.1/statuses/destroy/";
        public const string URL_API_STATUSES_MENTIONS_TIMELINE = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";
        public const string URL_API_STATUSES_RETWEET = "https://api.twitter.com/1.1/statuses/retweet/";
        public const string URL_API_STATUSES_SHOW = "https://api.twitter.com/1.1/statuses/show.json";
        public const string URL_API_STATUSES_USER_TIMELINE = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        public const string URL_API_STATUSES_UPDATE = "https://api.twitter.com/1.1/statuses/update.json";
        public const string URL_API_USER_TIMELINE = "https://api.twitter.com/1.1/statuses/user_timeline.json";

    }
}
