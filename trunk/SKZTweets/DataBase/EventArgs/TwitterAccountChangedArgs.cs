using SKZSoft.SKZTweets.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.DataBase
{
    public class TwitterAccountChangedArgs
    {
        public TwitterAccountChangedArgs(DatabaseChangeType databaseChangeType, TwitterAccount account, List<TwitterAccount> allAccounts)
        {
            DatabaseChangeType = databaseChangeType;
            TwitterAccount = account;
            AllAccounts = allAccounts;
        }

        public DatabaseChangeType DatabaseChangeType { get; private set; }
        public TwitterAccount TwitterAccount { get; private set; }
        public List<TwitterAccount> AllAccounts { get; private set; }
    }
}
