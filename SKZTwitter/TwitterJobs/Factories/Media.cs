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
        /// <summary>
        /// Create a job to post a media image as part of this batch
        /// </summary>
        /// <param name="completeionDelegate"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Jobs.Media.Upload CreateJobPostMedia(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completeionDelegate, string filePath)
        {
            Jobs.Media.Upload job = new Jobs.Media.Upload(credentials, completeionDelegate, filePath);
            batch.InitialiseJob(job);
            return job;
        }


    }
}
