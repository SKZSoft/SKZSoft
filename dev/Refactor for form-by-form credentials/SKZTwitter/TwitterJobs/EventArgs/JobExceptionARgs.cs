using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobExceptionArgs: EventArgs
    {
        public Exception Exception { get; internal set; }
        public Job Job { get; internal set; }

        public JobExceptionArgs(Exception ex, Job job)
        {
            Exception = ex;
            Job = job;
        }
    }
}
