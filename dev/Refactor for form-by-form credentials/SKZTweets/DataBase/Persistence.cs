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



        public TwitterAccount TwitterAccountAddOrUpdate(TwitterAccount account)
        {
            try
            {
                theLog.Log.LevelDown();

                if (m_dbContext.TwitterAccounts.Any(a => a.AccountId == account.AccountId))
                {
                    TwitterAccountUpdate(account);
                }
                else
                {
                    TwitterAccountAddOrUpdate(account);
                }
                return account;
            }
            finally { theLog.Log.LevelUp(); }
        }


        public TwitterAccount TwitterAccountUpdate(TwitterAccount account)
        {
            try
            {
                theLog.Log.LevelDown();

                // this account might not be from the database.
                // Grab the one we actually want to update and copy the values over.
                TwitterAccount existingRecord = m_dbContext.TwitterAccounts.Find(account.AccountId);

                existingRecord.OAuthToken = account.OAuthToken;
                existingRecord.OAuthTokenSecret = account.OAuthTokenSecret;
                existingRecord.Screenname = account.Screenname;


                m_dbContext.TwitterAccounts.Update(existingRecord);
                m_dbContext.SaveChanges();
                return existingRecord;
            }
            finally { theLog.Log.LevelUp(); }
        }


        public TwitterAccount TwitterAccountAdd(TwitterAccount account)
        {
            try
            {
                theLog.Log.LevelDown();
                m_dbContext.TwitterAccounts.Add(account);
                m_dbContext.SaveChanges();
                return account;
            }
            finally { theLog.Log.LevelUp(); }
        }

        public List<TwitterAccount> TwitterAccountGetAllAvailable()
        {
            List<TwitterAccount> results = m_dbContext.TwitterAccounts.ToList<TwitterAccount>();
            return results;
        }

    }
}
