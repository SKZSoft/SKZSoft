using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    /// <summary>
    /// Twitter Credentials for a single account.
    /// Application MUST populate the static Consumer properties before creating an instance.
    /// </summary>
    public class Credentials
    {
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public string ScreenName { get; set; }
        public ulong UserId { get; set; }

        public string ConsumerKey { get { return ConsumerData.ConsumerKey; } }
        public string ConsumerSecret { get { return ConsumerData.ConsumerSecret; } }

        public Credentials(string authToken, string authTokenSecret, string screenName, ulong userId)
        {

            if(string.IsNullOrEmpty(ConsumerData.ConsumerKey) || string.IsNullOrEmpty(ConsumerData.ConsumerSecret))
            {
                throw new ArgumentNullException("Must populate ConsumerKey and ConsumerSecret before instantiation");
            }

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


        /// <summary>
        /// Return a copy of this instance
        /// </summary>
        /// <returns></returns>
        public Credentials Clone()
        {
            Credentials newCopy = new Credentials(AuthToken, AuthTokenSecret, ScreenName, UserId);
            return newCopy;
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
