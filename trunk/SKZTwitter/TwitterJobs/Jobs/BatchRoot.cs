using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class BatchRoot : Batch
    {
        public BatchRoot(string authCallback, Credentials credentials, IJobRunner jobRunner, EventHandler<BatchCompleteArgs> completionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, string httpUserAgent) 
            : base(authCallback, credentials, jobRunner, completionDelegate)
        {
            JobException += exceptionDelegate;
            HttpUserAgent = httpUserAgent;
        }

        /// <summary>
        /// TRUE because this is the root batch job, with no parent
        /// </summary>
        public override bool IsRootBatch { get { return true; } }

        internal string HttpUserAgent { get; private set; }

        /// <summary>
        /// Handle an exceptoin thrown during processing
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="job"></param>
        public override void HandleException(Exception ex, Job job)
        {
            // this message has been passed all the way back up the chain from the offending job.
            // notify the owner.
            OnJobException(ex, job);
        }

        /// <summary>
        /// Raised in the event of an exception at some stage during a batch
        /// </summary>
        public event EventHandler<JobExceptionArgs> JobException;

        /// <summary>
        /// Notify the delegate method of job completion
        /// </summary>
        public virtual void OnJobException(Exception ex, Job job)
        {
            EventHandler<JobExceptionArgs> handler = JobException;
            JobExceptionArgs args = new JobExceptionArgs(ex, job);

            if (handler != null)
            {
                handler(this, args);
            }

            Terminate();
        }

        internal override void ChildCompleted(Job job)
        {
            base.ChildCompleted(job);

            // Now child copleteion has been completely handled, raise cancellation if that's pending
            if(m_cancelled)
            {
                OnCancelled();
                Terminate();
            }

        }

    }
}
