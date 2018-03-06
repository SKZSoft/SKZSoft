using SKZTweets.TwitterJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterData
{
    /// <summary>
    /// Event arguments for ThreadComplete event
    /// </summary>
    public class ThreadCompleteArgs : EventArgs
    {
        public ThreadCompleteArgs(JobBatchRoot rootBatch, int tweetCount)
        {
            TweetCount = tweetCount;
            RootBatch = rootBatch;
        }

        /// <summary>
        /// The root batch job of the thread
        /// </summary>
        public JobBatchRoot RootBatch { get; internal set; } 

        /// <summary>
        /// Total number of tweets sent
        /// </summary>
        public int TweetCount { get; set; }
    }
}
