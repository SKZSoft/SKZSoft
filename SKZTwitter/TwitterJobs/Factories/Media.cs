using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class Media
    {

        private Batch m_batch;

        public Media(Batch batch)
        {
            m_batch = batch;
        }


        /// <summary>
        /// Create a job to post a media image as part of this batch
        /// </summary>
        /// <param name="completeionDelegate"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Jobs.Media.Upload Upload(EventHandler<JobCompleteArgs> completionDelegate, string filePath)
        {
            Jobs.Media.Upload job = new Jobs.Media.Upload(m_batch.Credentials, completionDelegate, filePath);
            m_batch.InitialiseJob(job);
            return job;
        }


    }
}
