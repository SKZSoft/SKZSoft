using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SKZTweets.TwitterModels
{
    public class Status
    {
        public string created_at { get; set; }
        public ulong id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }

        public Status current_user_retweet { get; set; }

        /// <summary>
        /// If set to true, the full text is available under quoted_status.Text.
        /// Largey obsolete behaviour. Source: https://dev.twitter.com/overview/api/tweets
        /// </summary>
        public bool truncated { get; set; }
        public StatusEntities entities { get; set; }
        public ExtendedEntities extended_entities { get; set; }
        public string source { get; set; }
        public ulong? in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long? in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public User user { get; set; }
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public bool is_quote_status { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public bool possibly_sensitive { get; set; }
        public string lang { get; set; }
        public Status retweeted_status { get; set; }
        public long quoted_status_id { get; set; }
        public string quoted_status_id_str { get; set; }
        public Status quoted_status { get; set; }

        public DateTime CreatedAtDateTime
        {
            get
            {
                // the twitter datetime format is very nonstandard. Reformat it so it can be converted.
                // We start with: 
                //  ddd mmm dd hh:mm:ss +UTC yyyy
                string[] components = created_at.Split(' ');

                StringBuilder sb = new StringBuilder(50);
                sb.Append(components[1]);           // month (text) eg "May"
                sb.Append(" ");
                sb.Append(components[2]);           // day eg "06"
                sb.Append(" ");
                sb.Append(components[5]);           // year eg "2017"
                sb.Append(" ");
                sb.Append(components[3]);           // time eg 14:23:39
                sb.Append(" ");
                sb.Append(components[4]);           // UTC offset

                string formattedDT = sb.ToString();

                DateTime dateTime;
                if (DateTime.TryParse(formattedDT, out dateTime))
                {
                    return dateTime;
                }

                // return something obviously fake
                return new DateTime(0);

            }
        }

        public string OriginalURL
        {
            get
            {
                string url = string.Format("https://twitter.com/{0}/status/{1}", user.screen_name, id);
                return url;
            }
        }


        public override string ToString()
        {
            return text;
        }
    }
}
