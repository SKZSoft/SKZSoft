using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class Credentials
    {

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public string ScreenName { get; set; }
        public ulong UserId { get; set; }

        public Credentials(string consumerKey, string consumerSecret, string authToken, string authTokenSecret, string screenName, ulong userId)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AuthToken = authToken;
            AuthTokenSecret = authTokenSecret;
            ScreenName = screenName;
            UserId = userId;

            // if any are invalid, wipe the lot of them
            if(!IsValid)
            {
                Clear();
            }
        }

        public void Clear()
        {
            AuthToken = string.Empty;
            AuthTokenSecret = string.Empty;
            ScreenName = string.Empty;
            UserId = 0;
        }

        public bool IsValid
        {
            get
            {
                // invalid if ANYTHING is missing
                if (string.IsNullOrEmpty(ConsumerKey))
                    return false;
                if (string.IsNullOrEmpty(ConsumerSecret))
                    return false;
                if (string.IsNullOrEmpty(AuthToken))
                    return false;
                if (string.IsNullOrEmpty(AuthTokenSecret))
                    return false;
                if (string.IsNullOrEmpty(ScreenName))
                    return false;
                if (UserId == 0)
                    return false;

                return true;

            }
        }


    }
}
