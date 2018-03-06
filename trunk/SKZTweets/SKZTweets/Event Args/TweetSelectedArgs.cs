using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZTweets.TwitterModels;

namespace SKZTweets
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
