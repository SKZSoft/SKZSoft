using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.SKZTweets.Data;
using SKZSoft.Twitter.TwitterData;

namespace SKZSoft.SKZTweets.Interfaces
{
    public interface IMainWindow
    {
        void CredentialsChanged(SKZSoft.Twitter.TwitterData.TwitterData twitterData);
    }
}
