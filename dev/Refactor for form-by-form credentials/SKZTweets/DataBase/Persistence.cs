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


        /// <summary>
        /// Add or update an account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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
                    TwitterAccountAdd(account);
                }
                return account;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Update an existing account. Account ID must match an existing record in the database
        /// but theobject does not have to have been taken from the database.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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
                existingRecord.ScreenName = account.ScreenName;


                m_dbContext.TwitterAccounts.Update(existingRecord);
                m_dbContext.SaveChanges();
                return existingRecord;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Add a new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return all Twitter accounts
        /// </summary>
        /// <returns></returns>
        public List<TwitterAccount> TwitterAccountGetAllAvailable()
        {
            List<TwitterAccount> results = m_dbContext.TwitterAccounts.ToList<TwitterAccount>();
            return results;
        }



        /// <summary>
        /// Raised when an account is updated in the database
        /// </summary>
        public event EventHandler<TwitterAccountChangedArgs> TwitterAccountChanged;
        protected virtual void OnTwitterAccountChanged(TwitterAccount account, DatabaseChangeType databaseChangeType)
        {
            EventHandler<TwitterAccountChangedArgs> handler = TwitterAccountChanged;
            if (handler != null)
            {
                List<TwitterAccount> allAccounts = TwitterAccountGetAllAvailable();
                TwitterAccountChangedArgs e = new TwitterAccountChangedArgs(databaseChangeType, account, allAccounts);
                handler(this, e);
            }
        }
    }
}
