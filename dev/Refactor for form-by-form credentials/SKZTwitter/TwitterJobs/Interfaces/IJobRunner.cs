using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs;

namespace SKZSoft.Twitter.TwitterJobs.Interfaces
{
    public interface IJobRunner
    {
        void RunJob(TwitterJob job);
    }
}
