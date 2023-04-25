using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs.Jobs;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class Oauth
    {
        private Batch m_batch;

        public Oauth(Batch batch)
        {
            m_batch = batch;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the Access Token from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="pin"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public Jobs.Oauth.AccessToken AccessToken(EventHandler<JobCompleteArgs> completionDelegate, string pin, string authToken)
        {
            Jobs.Oauth.AccessToken job = new Jobs.Oauth.AccessToken(m_batch.Credentials, completionDelegate);
            job.AuthVerifier = pin;
            job.AuthToken = authToken;
            m_batch.InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the authoenticationtokens from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Oauth.RequestToken RequestToken(EventHandler<JobCompleteArgs> completionDelegate)
        {
            Jobs.Oauth.RequestToken job = new Jobs.Oauth.RequestToken(m_batch.Credentials, completionDelegate);
            m_batch.InitialiseJob(job);
            return job;
        }


    }
}
