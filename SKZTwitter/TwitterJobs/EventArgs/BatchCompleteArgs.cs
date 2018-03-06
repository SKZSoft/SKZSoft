using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterJobs
{
    public class BatchCompleteArgs : EventArgs
    {
        public JobBatch Batch { get; internal set; }

        public BatchCompleteArgs(JobBatch batch)
        {
            Batch = batch;
        }
    }
}
