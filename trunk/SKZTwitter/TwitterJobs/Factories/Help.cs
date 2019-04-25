using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class Help
    {

        /// <summary>
        /// Create a job (as part of this batch) to get the Twitter configuration from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Help.Configuration CreateGetTwitterConfig(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate)
        {
            Jobs.Help.Configuration job = new Jobs.Help.Configuration(credentials, completionDelegate);
            batch.InitialiseJob(job);
            return job;
        }


    }
}
