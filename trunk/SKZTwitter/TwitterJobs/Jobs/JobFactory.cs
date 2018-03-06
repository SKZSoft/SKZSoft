using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZTweets.TwitterModels;
using SKZTweets.TwitterJobs.Interfaces;

namespace SKZTweets.TwitterJobs
{
    public class JobFactory
    {
        public string AuthConsumerKey { get; set; }
        public string AuthCallBack { get; set; }
        public IJobRunner m_jobRunner;
        private string m_httpUserAgent;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oAuthConsumerKey"></param>
        /// <param name="oAuthCallback"></param>
        public JobFactory(IJobRunner jobRunner, string oAuthConsumerKey, string oAuthCallback, string httpUserAgent)
        {
            AuthCallBack = oAuthCallback;
            AuthConsumerKey = oAuthConsumerKey;
            m_jobRunner = jobRunner;
            m_httpUserAgent = httpUserAgent;
        }


        /// <summary>
        /// Create and return the root batch job, from which all others must stem as descendents
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public JobBatchRoot CreateRootBatch(EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            JobBatchRoot root = new JobBatchRoot(m_jobRunner, batchCompleteDelegate, exceptionDelegate, m_httpUserAgent);
            root.AuthCallBack = AuthCallBack;
            root.AuthConsumerKey = AuthConsumerKey;
            root.RootBatch = root;
            return root;
        }
    }
}
