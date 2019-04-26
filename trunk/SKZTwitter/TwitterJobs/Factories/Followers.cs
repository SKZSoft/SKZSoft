using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class Followers 
    {

        private Batch m_batch;

        public Followers(Batch batch)
        {
            m_batch = batch;
        }


        /// <summary>
        /// Create a job (as part of this batch) to return a set of {count} follower Ids from {cursor} position.
        /// Initial cursor position should be -1.
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="cursor"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Jobs.Followers.Ids Ids(EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count)
        {
            Jobs.Followers.Ids job = new Jobs.Followers.Ids(m_batch.Credentials, completionDelegate, cursor, count);
            m_batch.InitialiseJob(job);

            return job;
        }

    }
}
