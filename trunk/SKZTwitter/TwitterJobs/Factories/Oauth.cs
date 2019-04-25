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

        /// <summary>
        /// Create a job (as part of this batch) to get the Access Token from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="pin"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public Jobs.Oauth.AccessToken CreateGetAccessToken(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string pin, string authToken)
        {
            Jobs.Oauth.AccessToken job = new Jobs.Oauth.AccessToken(credentials, completionDelegate);
            job.AuthVerifier = pin;
            job.AuthToken = authToken;
            batch.InitialiseJob(job);
            return job;
        }

        /// <summary>
        /// Create a job (as part of this batch) to get the authoenticationtokens from Twitter
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public Jobs.Oauth.RequestToken CreateGetAuthToken(Batch batch, Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate)
        {
            Jobs.Oauth.RequestToken job = new Jobs.Oauth.RequestToken(credentials, completionDelegate);
            batch.InitialiseJob(job);
            return job;
        }


    }
}
