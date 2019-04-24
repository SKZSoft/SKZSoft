using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;

using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Followers
{
    public class List : Followers
    {
        private TwitterModels.Users m_users;

        internal List(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count)
            : base(credentials, completionDelegate, cursor, count)
        { }


        /// <summary>
        /// The URL of the Twitter API
        /// </summary>
        public override string URL
        {
            get
            {
                return URLs.URL_API_FOLLOWERS__LIST;
            }
        }

        public override void Finalize(string JsonResults)
        {
            try
            {
                theLog.Log.LevelDown();
                m_users = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterModels.Users>(JsonResults);
            }
            finally { theLog.Log.LevelUp(); }
        }

        public Users Users { get { return m_users; } }

        public override string JobDescription { get { return TwitterDataStrings.JobDescGetFollowersList; } }

        /// <summary>
        /// ID of the next cursor to pass in to Twitter
        /// </summary>
        public string NextCursor
        {
            get { return m_users.next_cursor_str; }
        }

    }

}
