using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZTweets.TwitterModels;
using SKZTweets.TwitterJobs;

namespace SKZTweets.TwitterData
{
    /// <summary>
    /// Event arguments for ThreadProgress event
    /// </summary>
    public class ThreadProgressUpdateArgs : EventArgs
    {
        public ThreadProgressUpdateArgs(Job lastJobCompleted, int sent, int total)
        {
            Sent = sent;
            Total = total;
            LastJobCompleted = lastJobCompleted;
        }

        /// <summary>
        /// tweet which was just sent
        /// </summary>
        public Job LastJobCompleted { get; set; }

        /// <summary>
        /// Total sent (including most recent)
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        /// Total to send
        /// </summary>
        public int Total { get; set; }
    }
}
