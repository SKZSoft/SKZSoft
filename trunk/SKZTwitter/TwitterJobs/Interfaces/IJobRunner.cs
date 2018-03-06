using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterJobs.Interfaces
{
    public interface IJobRunner
    {
        void RunJob(TwitterJob job);
    }
}
