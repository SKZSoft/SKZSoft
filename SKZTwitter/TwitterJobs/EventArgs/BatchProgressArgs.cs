using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterJobs
{
    /// <summary>
    /// Progress report for a batch
    /// </summary>
    public class BatchProgressArgs: EventArgs
    {
        /// <summary>
        /// Total of all jobs to do
        /// </summary>
        public int TotalJobs { get; internal set; }

        /// <summary>
        /// Jobs completed so far
        /// </summary>
        public int TotalJobsCompleted { get; internal set; }

        /// <summary>
        /// The job which was most recently completed
        /// </summary>
        public Job CompletedJob { get; internal set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="totalJobs"></param>
        /// <param name="jobsCompleted"></param>
        public BatchProgressArgs(Job completedJob, int totalJobs, int totalJobsCompleted)
        {
            TotalJobs = totalJobs;
            TotalJobsCompleted = totalJobsCompleted;
            CompletedJob = completedJob;
        }
    }
}
