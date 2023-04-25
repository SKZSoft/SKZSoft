using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class JobCompleteArgs: EventArgs
    {
        public Job Job{ get; set; }

        public JobCompleteArgs(Job job)
        {
            Job = job;
        }
    }
}
