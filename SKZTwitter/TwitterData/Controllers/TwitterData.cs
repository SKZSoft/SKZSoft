﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Models;
using System.Net.Http;
using System.Net;
using System.Web.Script.Serialization;
using SKZSoft.Twitter.TwitterData.Exceptions;
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Twitter.TwitterJobs;
using System.Diagnostics;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterModels.Enums;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterModels.Models;
using System.Net.Http.Headers;

namespace SKZSoft.Twitter.TwitterData
{
    /// <summary>
    /// Data layer for Twitter
    /// </summary>
    public class TwitterData : IJobRunner
    {
        private HttpClient m_httpClient;
        private const string USER_AGENT = "SKZTweets";
        private string m_userAgent = "";
        private TwitterConfiguration m_twitterConfiguration;

        private Factory m_batchFactory;
        private Controllers.JobFactories m_jobFactories;
        private TwitterConsts m_twitterConsts = new TwitterConsts();


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authCallback"></param>
        /// <param name="userAgent"></param>
        public TwitterData(HttpClient httpClient, string authCallback, string userAgent)
        {
            try
            {
                theLog.Log.LevelDown();

                m_httpClient = httpClient;
                m_userAgent = "SKZTweets/" + typeof(TwitterData).Assembly.GetName().Version;

                // Circular reference
                m_batchFactory = new Factory(this, authCallback, userAgent);

                m_jobFactories = new Controllers.JobFactories(m_batchFactory);
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// For unit testing only. Allow test to set a MOCK configuration which hasn't been fetched from twitter.
        /// </summary>
        public TwitterConfiguration MOCKTwitterConfiguration {  set { m_twitterConfiguration = value; } }


        public void Initialise(Credentials credentials, EventHandler<BatchCompleteArgs> completionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            // This is rather nasty, but the config is unique in that it must block progression.
            // So we have a BATCH completion delegate which is called when EVERYTHING is finished
            // and a JOB (priority) delegate which gets called BEFORE that.
            // When the JOB ends, it inisialises the twitter config. That's necessary before anything else
            // can happen. Only then is the caller notified that initialisation is complete.
            m_jobFactories.Help.ConfigurationGet(credentials, completionDelegate, GetTwitterConfigPriorityEnd, exceptionDelegate);
        }

        private void GetTwitterConfigPriorityEnd(object sender, JobCompleteArgs e)
        {
            TwitterJobs.Jobs.Help.Configuration job = (TwitterJobs.Jobs.Help.Configuration)e.Job;
            
            // keep the configuration
            m_twitterConfiguration = job.TwitterConfiguration;
        }

        /// <summary>
        /// Get mentions for user
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="completionDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        /// <param name="count"></param>
        public void GetMentions(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, int count)
        {
            try
            {
                theLog.Log.LevelDown();
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, null, exceptionDelegate);
                TwitterJobs.Jobs.Statuses.MentionsTimeline job = rootBatch.JobFactories.Statuses.MentionsTimeline(completionDelegate, count);
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }




        /// <summary>
        /// Get list of statuses made by the specified screen name
        /// </summary>
        /// <param name="count">The maximum number of statuses to fetch</param>
        /// <param name="screenName">The screen name</param>
        /// <returns></returns>
        public void GetRecentStatusesForUserStart(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, int count, string screenName, string startId)
        {
            try
            {
                theLog.Log.LevelDown();

                theLog.Log.WriteAPI(string.Format("Calling Twitter API to get {0} statuses for screenname {1}", count, screenName), Logging.LoggingSource.API);

                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, null, exceptionDelegate);
                throw new NotImplementedException(); //refactoring lost the startId ability - needs to be added back in.
                //rootBatch.JobFactories.Statuses.MentionsTimeline(completionDelegate, screenName, count);//, startId);
                //rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Perform a Retweet
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="tweetId"></param>
        /// <param name="batchCompleteDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        /// <param name="onDeleteOldRT"></param>
        /// <param name="onRTCompleted"></param>
        public void Retweet(Credentials credentials, ulong tweetId, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, EventHandler<JobCompleteArgs> onDeleteOldRT, EventHandler<JobCompleteArgs> onRTCompleted)
        {
            try
            {
                theLog.Log.LevelDown();

                // Create root batch and pass in completion and exception delegate methods
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompleteDelegate, exceptionDelegate);

                // Create job to fetch original status based on the ID
                rootBatch.JobFactories.Statuses.Show(null, tweetId, true);

                // Create a job to destroy the existing RT if it exists
                rootBatch.JobFactories.Statuses.DestroyFromPreviousShow(onDeleteOldRT);

                // Create a job to retweet the original tweet
                rootBatch.JobFactories.Statuses.Retweet(onRTCompleted, tweetId);

                // Run the batch
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }



        /// <summary>
        /// Return a list of follower IDs for the logged-in account.
        /// Maximum of {count} IDs will be returned. No cursor specified so will start at the beginning of the follower list.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="batchCompleteDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        /// <param name="completedJobDelegate"></param>
        public void GetFollowers(Credentials credentials, long count, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, EventHandler<JobCompleteArgs> completedJobDelegate)
        {
            GetFollowers(credentials, "-1", count, batchCompleteDelegate, exceptionDelegate, completedJobDelegate);
        }


        /// <summary>
        /// Return a list of follower IDs for the logged-in account.
        /// Maximum of {count} IDs will be returned.
        /// Set will begin at {cursor}. Use -1 to set the cursor at the start of the list.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="count"></param>
        /// <param name="batchCompleteDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        /// <param name="onRTCompleted"></param>
        public void GetFollowers(Credentials credentials, string cursor, long count, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, EventHandler<JobCompleteArgs> completedJobDelegate)
        {
            try
            {
                theLog.Log.LevelDown();

                // Create root batch and pass in completion and exception delegate methods
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompleteDelegate, exceptionDelegate);

                rootBatch.JobFactories.Followers.Ids(completedJobDelegate, cursor, count);

                // Run the batch
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Send a DM
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="recipientId"></param>
        /// <param name="text"></param>
        /// <param name="batchCompleteDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        /// <param name="onCompleted"></param>
        public void SendDM(Credentials credentials, ulong recipientId, string text, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, EventHandler<JobCompleteArgs> onCompleted)
        {
            try
            {
                theLog.Log.LevelDown();

                // Create root batch and pass in completion and exception delegate methods
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompleteDelegate, exceptionDelegate);

                // Create a job to retweet the original tweet
                rootBatch.JobFactories.DirectMessage.New(onCompleted, recipientId, text);

                // Run the batch
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Send a tweet
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="text"></param>
        /// <param name="batchCompleteDelegate"></param>
        /// <param name="exceptionDelegate"></param>
        public void PostStatus(Credentials credentials, string text, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            try
            {
                theLog.Log.LevelDown();

                // Create root batch and pass in completion and exception delegate methods
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompleteDelegate, exceptionDelegate);

                // create job to post simple status
                Status newStatus = new Status();
                newStatus.text = text;
                rootBatch.JobFactories.Statuses.Update(null, newStatus);

                // run the batch
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Get a tweet using the ID
        /// </summary>
        /// <param name="tweetId"></param>
        /// <returns></returns>
        public void GetOriginalTweetByIdStart(Credentials credentials, EventHandler<BatchCompleteArgs> batchCompleteDelegate, EventHandler<JobCompleteArgs> jobCompletionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, ulong tweetId)
        {
            try
            {
                theLog.Log.LevelDown();
                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompleteDelegate, exceptionDelegate);
                rootBatch.JobFactories.Statuses.Show(jobCompletionDelegate, tweetId, true);
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Launch specified browser. 
        /// </summary>
        /// <param name="browserPath"></param>
        public void LaunchTwitterSignin(Credentials credentials, string browserPath)
        {
            string url = GetTwitterSignInURL(credentials);

            if (browserPath.Length > 0)
            {
                ProcessStartInfo info = new ProcessStartInfo(browserPath, url);
                Process.Start(info);
            }
            else
            {
                System.Diagnostics.Process.Start(url);
            }
        }

        /// <summary>
        /// Return the URL which Twitter uses to sign in
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public string GetTwitterSignInURL(Credentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.AuthToken))
            {
                throw new InvalidOperationException("No auth token found. Call GetAuthToken() before this method.");
            }

            string url = Consts.DataConsts.URL_API_AUTHENITCATE;

            // add on the part which identifies this application to Twitter.
            url += string.Format("?oauth_token={0}", credentials.AuthToken);
            return url;
        }

        /// <summary>
        /// Launch a browser to the Twitter authorisation page
        /// </summary>
        /// <param name="credentials"></param>
        public void LaunchTwitterSignin(Credentials credentials)
        {
            LaunchTwitterSignin(credentials, "");
        }

        /// <summary>
        /// Get the authorisation token from Twitter which is required for further communication
        /// if user is not yet authenticated.
        /// </summary>
        /// <returns></returns>
        public void GetRequestTokenStart(Credentials credentials, EventHandler<JobCompleteArgs> jobCompletionDelegate, EventHandler<BatchCompleteArgs> batchCompletionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate)
        {
            try
            {
                theLog.Log.LevelDown();

                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, batchCompletionDelegate, exceptionDelegate);
                TwitterJobs.Jobs.Oauth.RequestToken job = rootBatch.JobFactories.Oauth.RequestToken(jobCompletionDelegate);
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Handle authentication PIN from user
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public void HandlePINStart(Credentials credentials, EventHandler<JobCompleteArgs> jobCompletionDelegate, EventHandler<BatchCompleteArgs> completionDelegate, EventHandler<JobExceptionArgs> exceptionDelegate, string pin)
        {
            try
            {
                theLog.Log.LevelDown();

                Batch rootBatch = m_batchFactory.CreateRootBatch(credentials, completionDelegate, exceptionDelegate);
                TwitterJobs.Jobs.Oauth.AccessToken job = rootBatch.JobFactories.Oauth.AccessToken(jobCompletionDelegate, pin, credentials.AuthToken);
                job.CompletedPriority += Job_AccessTokenCompleted;
                rootBatch.RunBatch();
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void Job_AccessTokenCompleted(object sender, JobCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                TwitterJobs.Jobs.Oauth.AccessToken job = (TwitterJobs.Jobs.Oauth.AccessToken)e.Job;

                // Log results
                theLog.Log.WriteDebug(string.Format("Screenname={0} AccountId = {1}", job.ScreenName, job.AccountId), Logging.LoggingSource.DataLayer);

                if (string.IsNullOrEmpty(job.AuthToken))
                {
                    theLog.Log.WriteError("Auth Token was not returned", Logging.LoggingSource.DataLayer);
                }
                if (string.IsNullOrEmpty(job.AuthTokenSecret))
                {
                    theLog.Log.WriteError("Auth Token Secret was not returned", Logging.LoggingSource.DataLayer);
                }


            }
            finally { theLog.Log.LevelUp(); }
        }


        internal async Task<bool> RunJobAsync(TwitterJob job)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Preparing header", SKZSoft.Common.Logging.LoggingSource.DataLayer);

                // Get data
                HttpRequestMessage req = job.CreateHttpRequest();
                job.AddParameters();

                // XXXSKZ - these need to be queued to prevent re-entrancy from multiple forms using this single instance.
                await DoWebRequest(job, req);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                job.HandleException(ex, job);
                return false;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private async Task<bool> DoWebRequest(TwitterJob job, HttpRequestMessage req)
        {
            try
            {
                theLog.Log.LevelDown();

                theLog.Log.WriteDebug("Sending request", SKZSoft.Common.Logging.LoggingSource.DataLayer);

                // Send HTTP request
                HttpResponseMessage msg = await m_httpClient.SendAsync(req);

                // read response
                string response = await msg.Content.ReadAsStringAsync();


                /// SKZTODO - handle headers:
                /// x-rate-limit-limit
                /// x-rate-limit-remaining
                /// x-rate-limit-reset
                HttpResponseHeaders headers = msg.Headers;
                foreach (KeyValuePair<string, IEnumerable<string>>  header in headers)
                {
                    if(header.Key == "x-rate-limit-remaining")
                    {
                        //TODO - handle rate limiting data.
                        //System.Diagnostics.Debug.WriteLine("LIMIT");
                    }

                    /*int n = 0;
                    foreach (string value in header.Value)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0}({1})={2}", header.Key.ToString(),n, value));
                        n++;
                    }
                    */
                }

                // Throw errors if necessary
                ThrowErrors(msg, response);

                // feed the job the results for it to parse and process
                theLog.Log.WriteDebug("Passing results to job", SKZSoft.Common.Logging.LoggingSource.DataLayer);
                job.Finalize(response);

                // Notify the owner and delegates of completion
                job.OnCompleted();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                job.HandleException(ex, job);
                return false;
            }
            finally { theLog.Log.LevelUp(); }

        }


        /// <summary>
        /// Throw errors if the server response is not as expected
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="response"></param>
        private void ThrowErrors(HttpResponseMessage msg, string response)
        {
            const int TooManyRequests = 429;
            const int EnhanceYourCalm = 420;
            const int UnprocessableEntity = 422;
            const int BadGateway = 502;
            const int ServiceUnavailable = 503;

            // All went well - just leave
            if(msg.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            switch (msg.StatusCode)
            {
                // Specific exception - authorisation failed
                case HttpStatusCode.Unauthorized:
                    TwitterRequestResponse responseObject = new TwitterRequestResponse();
                    try
                    {
                        responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterRequestResponse>(response);
                    }
                    catch
                    {
                        // we are error handling already. Swallow any bad stuff. 
                    }
                    throw new TwitterUnauthorizedException(responseObject);
                
                // Stuff not handled by standard enumerations
                default:
                    switch ((int)msg.StatusCode)
                    {
                        // too many requests. Twitter applications MUST handle this and notify user
                        // that they need to log out of other apps.
                        case TooManyRequests:
                            throw new TwitterTooManyRequestsException();

                        case EnhanceYourCalm:
                            throw new TwitterRateLimits();

                        case UnprocessableEntity:
                            throw new TwitterUnprocessableEntity();

                        case BadGateway:
                            throw new TwitterBadGateway();

                        case ServiceUnavailable:
                            throw new TwitterServiceUnavailable();

                        default:
                            // Nothing http-specific to handle here. Create a generic exception.
                            TwitterGenericException genericEx = new TwitterGenericException(msg.StatusCode, response);
                            HandleTwitterErrorCodes(genericEx);
                            // this line will never be reached
                            break;
                    }
                    break;
            }
        }


        private void HandleTwitterErrorCodes(TwitterGenericException ex)
        {
            switch(ex.FirstTwitterError.CodeAsEnum)
            {
                // Duplicate status
                case TwitterErrorCodes.DuplicateStatus:
                    throw new TwitterDuplicateStatusException();

                // no special handling for this
                default:
                    throw ex;
            }
        }



        /// <summary>
        /// Convert json string into Dictionary key/pairs
        /// </summary>
        /// <param name="jsonResponse"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetJsonResponseAsDictionary(string jsonResponse)
        {
            Dictionary<string, string> values = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            return null;
        }



        /// <summary>
        /// Return the full URL for the specified screen name
        /// </summary>
        /// <param name="screenName"></param>
        /// <returns></returns>
        public string GetURLForScreenName(string screenName)
        {
            string url = string.Format(Consts.DataConsts.URL_USER_TIMELINE, screenName); 
            return url;
        }


        /// <summary>
        /// The configuration returned by Twitter
        /// </summary>
        public TwitterConfiguration TwitterConfiguration {  get { return m_twitterConfiguration;  } }

        /// <summary>
        /// Clean up all references
        /// </summary>
        public void Terminate()
        {
            m_batchFactory = null;
            m_httpClient = null;
            m_twitterConfiguration = null;
            m_userAgent = null;
        }

        void IJobRunner.RunJob(TwitterJob job)
        {
            try
            {
                theLog.Log.LevelDown();
                // This method exists simply to make the code elsewhere look nice.
                // Pragma ONCE here and nowhere else.
#pragma warning disable 4014
                RunJobAsync(job);
#pragma warning restore 4014
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Twitter constants
        /// </summary>
        public TwitterConsts TwitterConsts { get { return m_twitterConsts; } } 

        /// <summary>
        /// Create and initialise a new ThreadPoster class
        /// </summary>
        /// <param name="tweets"></param>
        /// <param name="replyTo"></param>
        /// <returns></returns>
        public ThreadPoster CreateThreadPoster(Queue<Status> tweets, Status replyTo)
        {
            ThreadPoster poster = new ThreadPoster(this, m_batchFactory, tweets, replyTo);
            return poster;
        }

    }
}
