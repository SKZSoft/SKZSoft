using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;

namespace SKZSoft.Twitter.TwitterData.Controllers.Jobs
{
    public class Help
    {
        private Factory m_batchFactory;

        internal Help(Factory batchFactory)
        {
            m_batchFactory = batchFactory;
        }

        /// <summary>
        /// Start the job to get the Twitter configuration.
        /// </summary>
        /// <param name="completionDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        public void ConfigurationGet(Credentials credentials, EventHandler<BatchCompleteArgs> externalCompletionDelegate, EventHandler<JobCompleteArgs> internalCompletionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            try
            {
                theLog.Log.LevelDown();
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, externalCompletionDelegate, exceptionDelegate);
                TwitterJobs.Jobs.Help.Configuration job = rootBatch.JobFactories.Help.Configuration(internalCompletionDelegate);
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
