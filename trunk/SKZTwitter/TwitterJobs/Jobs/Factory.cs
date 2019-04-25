using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class Factory
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
        public Factory(IJobRunner jobRunner, string oAuthCallback, string httpUserAgent)
        {
            AuthCallBack = oAuthCallback;
            m_jobRunner = jobRunner;
            m_httpUserAgent = httpUserAgent;
        }


        /// <summary>
        /// Create and return the root batch job, from which all others must stem as descendents
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <returns></returns>
        public BatchRoot CreateRootBatch(Credentials credentials, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            BatchRoot root = new BatchRoot(AuthCallBack, credentials, m_jobRunner, batchCompleteDelegate, exceptionDelegate, m_httpUserAgent);
            return root;
        }
    }
}
