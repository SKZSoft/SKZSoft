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



        public void UserAddOrUpdate(ulong accountId, string screenName, string oAuthToken, string oAuthTokenSecret)
        {
            try
            {
                theLog.Log.LevelDown();

                TwitterAccount acc = new TwitterAccount();
                acc.AccountId = accountId;
                acc.Screenname = screenName;
                acc.OAuthToken = oAuthToken;
                acc.OAuthTokenSecret = oAuthTokenSecret;

                if (m_dbContext.TwitterAccounts.Any(a => a.AccountId == acc.AccountId)) 
                {
                    m_dbContext.TwitterAccounts.Update(acc);
                }
                else
                {
                    m_dbContext.TwitterAccounts.Add(acc);
                }
                m_dbContext.SaveChanges();

            }
            finally { theLog.Log.LevelUp(); }
        }

        public List<TwitterAccount> UserGetAllAvailable()
        {
            List<TwitterAccount> results = m_dbContext.TwitterAccounts.ToList<TwitterAccount>();
            return results;
        }

    }
}
