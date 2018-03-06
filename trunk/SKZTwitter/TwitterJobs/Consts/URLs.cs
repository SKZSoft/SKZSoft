using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs.Consts
{
    internal class URLs
    {
        public const string URL_API_REQUEST_TOKEN = "https://api.twitter.com/oauth/request_token";
        public const string URL_API_ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";
        public const string URL_API_USER_TIMELINE = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        public const string URL_API_STATUSES_SHOW = "https://api.twitter.com/1.1/statuses/show.json";
        public const string URL_API_HELP_CONFIG = "https://api.twitter.com/1.1/help/configuration.json";
        public const string URL_API_RETWEET = "https://api.twitter.com/1.1/statuses/retweet/";
        public const string URL_API_DESTROY = "https://api.twitter.com/1.1/statuses/destroy/";
        public const string URL_API_UPLOAD_MEDIA = "https://upload.twitter.com/1.1/media/upload.json";
        public const string URL_API_STATUS_UPDATE = "https://api.twitter.com/1.1/statuses/update.json";
        public const string URL_API_DIRECT_MESSAGE_CREATE = "https://api.twitter.com/1.1/direct_messages/events/new.json";

        public const string URL_API_STATUSES_MENTIONS_TIMELINE = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";

    }
}
