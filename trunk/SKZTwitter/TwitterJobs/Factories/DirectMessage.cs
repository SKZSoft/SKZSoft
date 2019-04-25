using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class DirectMessage
    {
        /// <summary>
        /// New Direct Message
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="credentials"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="recipientId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Jobs.DirectMessage.New New(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, ulong recipientId, string text)
        {
            Jobs.DirectMessage.New job = new Jobs.DirectMessage.New(credentials, completionDelegate, recipientId, text);
            batch.InitialiseJob(job);
            return job;
        }


    }
}
