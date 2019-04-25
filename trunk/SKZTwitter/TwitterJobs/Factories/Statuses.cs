using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class Statuses
    {
        /// <summary>
        /// Create a job for deleting a tweet or a RT event as part of this batch
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public Jobs.Statuses.Destroy Destroy(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong statusId)
        {
            Jobs.Statuses.Destroy job = new Jobs.Statuses.Destroy(credentials, completionDelegate, statusId);
            batch.InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job to delete a RT event (as part of this batch) but taking the ID of the tweet from the job which
        /// executes immediately before this one.
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Statuses.Destroy DestroyFromPreviousShow(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate)
        {
            Jobs.Statuses.Destroy job = new Jobs.Statuses.DestroyFromPreviousShow(credentials, completionDelegate);
            batch.InitialiseJob(job);
            return job;
        }

        public Jobs.Statuses.MentionsTimeline MentionsTimeline(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, int count)
        {
            Jobs.Statuses.MentionsTimeline job = new Jobs.Statuses.MentionsTimeline(credentials, completionDelegate, count);
            batch.InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to return a single tweet details
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="id"></param>
        /// <param name="includeMyRetweet"></param>
        /// <returns></returns>
        public Jobs.Statuses.Show Show(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong id, bool includeMyRetweet)
        {
            Jobs.Statuses.Show job = new Jobs.Statuses.Show(credentials, completionDelegate, id, includeMyRetweet);
            batch.InitialiseJob(job);

            return job;
        }

        public Jobs.Statuses.Update Update(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, Status status)
        {
            Jobs.Statuses.Update job = new Jobs.Statuses.Update(credentials, completionDelegate, status);
            batch.InitialiseJob(job);
            return job;
        }


        public Jobs.Statuses.Retweet Retweet(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong id)
        {
            Jobs.Statuses.Retweet job = new Jobs.Statuses.Retweet(credentials, completionDelegate, id);
            batch.InitialiseJob(job);
            return job;
        }

    }
}
