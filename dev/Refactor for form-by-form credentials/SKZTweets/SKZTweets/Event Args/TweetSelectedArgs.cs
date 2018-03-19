using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.SKZTweets
{
    public class TweetSelectedArgs : EventArgs
    {
        public Status Status { get; set; }

        public TweetSelectedArgs(Status status)
        {
            Status = status;
        }
    }
}
