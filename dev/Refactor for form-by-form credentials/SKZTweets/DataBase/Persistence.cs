using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.SKZTweets.DataModels;

namespace SKZSoft.SKZTweets.DataBase
{
    public class Persistence
    {
        private SKZTweetsContext m_dbContext;

        public Persistence(SKZTweetsContext dbContext)
        {
            m_dbContext = dbContext;
        }



        public void UserAddOrUpdate(ulong userId, string screenName, string oAuthToken, string oAuthTokenSecret)
        {
            try
            {
                theLog.Log.LevelDown();

                User user = new User();
                user.UserId = userId;
                user.Screenname = screenName;
                user.OAuthToken = oAuthToken;
                user.OAuthTokenSecret = oAuthTokenSecret;

                if (m_dbContext.Users.Any(u => u.UserId == user.UserId)) 
                {
                    m_dbContext.Users.Update(user);
                }
                else
                {
                    m_dbContext.Users.Add(user);
                }
                m_dbContext.SaveChanges();

            }
            finally { theLog.Log.LevelUp(); }
        }

    }
}
