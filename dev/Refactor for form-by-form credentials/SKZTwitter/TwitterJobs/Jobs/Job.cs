    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using Newtonsoft.Json.Linq;

namespace SKZSoft.Twitter.TwitterJobs
{


    /// <summary>
    /// Base Job class. Represents a Job on Twitter.
    /// Inherited by all other Job classes.
    /// </summary>
    public abstract class Job
    {
        private string m_jobId;

        /// <summary>
        /// Constructor with default parameters
        /// </summary>
        /// <param name="parameters"></param>
        internal Job(EventHandler<JobCompleteArgs> completionDelegate)
        {
            ParameterStrings = new Dictionary<string, string>(); ;
            ParmatersJson = new JObject();
            ParametersBinary = new Dictionary<string, object>();
            if (completionDelegate != null)
            {
                Completed += completionDelegate;
            }

            m_jobId = Guid.NewGuid().ToString();
        }

        public JobBatchRoot RootBatch { get; set; }

        /// <summary>
        /// The parent job.
        /// </summary>
        internal Job Parent { get; set; }

        /// <summary>
        /// Allow job to get data from the job which preceeded it in a queue
        /// </summary>
        /// <param name="previousJob"></param>
        public abstract void InitializeFromLastJob(Job previousJob);

        /// <summary>
        /// The human-readable job description
        /// </summary>
        public abstract string JobDescription { get; }

        /// <summary>
        /// Delay (in milliseconds) before executing this job as part of a batch.
        /// 0 means immediate execution
        /// </summary>
        public int DelayBefore { get; set; }


        /// <summary>
        /// Job must get itself tidied up, extract results etc
        /// </summary>
        /// <param name="results"></param>
        public abstract void Finalize(string results);


        /// <summary>
        /// Raised when job is completed.
        /// Raised AFTER the "CompletedPriority" event.
        /// </summary>
        public event EventHandler<JobCompleteArgs> Completed;

        /// <summary>
        /// Raised when a job has finished EXECUTING but BEFORE
        /// the batch processing system recognises it as complete
        /// </summary>
        //public event EventHandler<JobCompleteArgs> CompletedNotFInished;

        internal virtual void ChildCompleted(Job job)
        {
            // notify all the way back up the chain.
            if(Parent!=null)
            {
                Parent.ChildCompleted(job);
            }
        }

        /// <summary>
        /// Notify the delegate method of job completion
        /// </summary>
        public virtual void OnCompleted()
        {
            // now call non-priority events.
            EventHandler<JobCompleteArgs> handler = Completed;
            JobCompleteArgs args = new JobCompleteArgs(this);
            if (handler != null)
            {
                handler(this, args);
            }

            // This MUST happen AFTER.
            // The Root Batch will raise the overall cancellation notice.
            ChildCompleted(this);
        }

        /// <summary>
        /// Unique ID for this job
        /// </summary>
        public string JobId {  get { return m_jobId;  } }


        /// <summary>
        /// Set to TRUE if this job should be skipped when in a batch
        /// </summary>
        public bool Skipped { get; set; }


        /// <summary>
        /// Parameters as strings. 
        /// </summary>
        public Dictionary<string, string> ParameterStrings { get; set; }

        /// <summary>
        /// Parameters as Json objects. Only for parameters to be passed in as Json to the API.
        /// </summary>
        public JObject ParmatersJson { get; set; }


        /// <summary>
        /// Binary parameters
        /// </summary>
        public Dictionary<string, object> ParametersBinary { get; set; }

        /// <summary>
        /// Get a copy of the parameters
        /// </summary>
        internal Dictionary<string, string> ParameterStringsCopy
        {
            get
            {
                try
                {
                    theLog.Log.LevelDown();
                    Dictionary<string, string> copy = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> kvp in ParameterStrings)
                    {
                        copy.Add(kvp.Key, kvp.Value);
                    }
                    return copy;
                }
                finally { theLog.Log.LevelUp(); }
            }
        }
        internal void AddOrUpdateParam(string name, string value)
        {
            ParameterStrings[name] = value;
        }

        internal void AddOrUpdateParam(string name, int value)
        {
            AddOrUpdateParam(name, value.ToString());
        }

        internal string GetParamByName(string name)
        {
            if (ParameterStrings.ContainsKey(name))
            {
                return ParameterStrings[name];
            }
            return null;
        }

        /// <summary>
        /// Get item by name (or empty string if it does not exist)
        /// </summary>
        /// <param name="results"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal string ExtractItemByName(Dictionary<string, string> results, string name)
        {
            string value = string.Empty;
            if (results.ContainsKey(name))
            {
                value = results[name];
            }

            return value;
        }


        /// <summary>
        /// Clean up this job
        /// </summary>
        internal virtual void Terminate()
        {

        }

        /// <summary>
        /// Handle an exception.
        /// Root batch class overrides this to actually do the handling.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="job"></param>
        public virtual void HandleException(Exception ex, Job job)
        {
            // the batch needs to know, as does everything on the way up.
            Parent.HandleException(ex, job);
        }

        /// <summary>
        /// Returns TRUE if this is the root batch object
        /// </summary>
        public virtual bool IsRootBatch { get { return false; } }

    }
}
