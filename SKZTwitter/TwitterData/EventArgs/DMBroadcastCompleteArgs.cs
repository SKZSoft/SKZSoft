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
    public class DMBroadcastCompleteArgs : EventArgs
    {
        public DMBroadcastCompleteArgs(BatchRoot rootBatch, int dmCount)
        {
            DMCount = dmCount;
            RootBatch = rootBatch;
        }

        /// <summary>
        /// The root batch job of the thread
        /// </summary>
        public BatchRoot RootBatch { get; internal set; } 

        /// <summary>
        /// Total number of tweets sent
        /// </summary>
        public int DMCount { get; set; }
    }
}
