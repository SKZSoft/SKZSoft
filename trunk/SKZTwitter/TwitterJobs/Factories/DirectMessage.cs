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
        private Batch m_batch;

        public DirectMessage(Batch batch)
        {
            m_batch = batch;
        }

        /// <summary>
        /// New Direct Message
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="credentials"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="recipientId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Jobs.DirectMessage.New New(EventHandler<JobCompleteArgs> completionDelegate, ulong recipientId, string text)
        {
            Jobs.DirectMessage.New job = new Jobs.DirectMessage.New(m_batch.Credentials, completionDelegate, recipientId, text);
            m_batch.InitialiseJob(job);
            return job;
        }


    }
}
