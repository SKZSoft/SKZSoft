using SKZSoft.Twitter.TwitterJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterData
{
    /// <summary>
    /// Event arguments for ThreadComplete event
    /// </summary>
    public class ThreadCompleteArgs : EventArgs
    {
        public ThreadCompleteArgs(BatchRoot rootBatch, int tweetCount)
        {
            TweetCount = tweetCount;
            RootBatch = rootBatch;
        }

        /// <summary>
        /// The root batch job of the thread
        /// </summary>
        public BatchRoot RootBatch { get; internal set; } 

        /// <summary>
        /// Total number of tweets sent
        /// </summary>
        public int TweetCount { get; set; }
    }
}
