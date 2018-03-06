using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZTweets.Data;
using SKZTweets.TwitterData;

namespace SKZTweets.Interfaces
{
    public interface IMainWindow
    {
        void CredentialsChanged(TwitterData.TwitterData twitterData);
    }
}
