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

        private Batch m_batch;

        public Help(Batch batch)
        {
            m_batch = batch;
        }


        /// <summary>
        /// Create a job (as part of this batch) to get the Twitter configuration from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Help.Configuration Configuration(EventHandler<JobCompleteArgs> completionDelegate)
        {
            Jobs.Help.Configuration job = new Jobs.Help.Configuration(m_batch.Credentials, completionDelegate);
            m_batch.InitialiseJob(job);
            return job;
        }


    }
}
