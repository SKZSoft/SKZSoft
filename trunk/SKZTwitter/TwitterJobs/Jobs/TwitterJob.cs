using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using System.Net;
using SKZSoft.Twitter.TwitterJobs.Signing;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;
using SKZSoft.Twitter.TwitterJobs.Consts;

namespace SKZSoft.Twitter.TwitterJobs
{
    public enum ApiResponseType
    {
        http,
        json
    }

    public enum ParameterType
    {
        http,
        json
    }


    public abstract class TwitterJob: Job
    {

        protected HttpRequestMessage m_httpRequest;
        protected Credentials m_jobCredentials;

        /// <summary>
        /// Constructor
        /// </summary>
        public TwitterJob(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate) : base(completionDelegate)
        {
            m_jobCredentials = credentials;
        }

        /// <summary>
        /// Return TRUE if only oauth parameters are used for twitter authorisation for this job.
        /// </summary>
        public virtual bool AuthParametersOnly {  get { return false;  } }

        /// <summary>
        /// Class must specify the URL it needs to use on the Twitter API
        /// </summary>
        public abstract string URL { get; }


        /// <summary>
        /// The credentials to use for this batch
        /// </summary>
        public Credentials Credentials { get { return m_jobCredentials; } }

        /// <summary>
        /// Get URL with parameters
        /// </summary>
        public string URLWithParameters
        {
            get
            {
                try
                {
                    theLog.Log.LevelDown();
                    if (this.RequestType == HttpMethod.Get)
                    {
                        // Include parameters in the URL
                        string parameters = GetParametersAsUrl();
                        string fullUrl = string.Format("{0}?{1}", URL, parameters);
                        return fullUrl;
                    }

                    if (this.RequestType == HttpMethod.Post)
                    {
                        // Parameters are added as part of the body
                        return URL;
                    }

                    throw new Exception("Http type not handled");
                }
                finally { theLog.Log.LevelUp(); }
            }
        }

        /// <summary>
        /// Format the parameters in  a way which can simply be appended to a URL:
        /// key=value&key2=value2
        /// </summary>
        /// <returns></returns>
        private string GetParametersAsUrl()
        {
            try
            {
                theLog.Log.LevelDown();

                StringBuilder sb = new StringBuilder(500);
                bool first = true;
                foreach (KeyValuePair<string, string> kvp in ParameterStrings)
                {
                    theLog.Log.WriteDebug(string.Format("Adding {0}={1}", kvp.Key, kvp.Value), SKZSoft.Common.Logging.LoggingSource.DataLayer);
                    if (!first)
                    {
                        sb.Append("&");
                    }
                    string encodedKey = WebUtility.UrlEncode(kvp.Key);
                    string encodedValue = WebUtility.UrlEncode(kvp.Value);
                    sb.AppendFormat("{0}={1}", encodedKey, encodedValue);
                    first = false;
                }

                string fullParams = sb.ToString();
                theLog.Log.WriteDebug("encoding finished: " + fullParams, SKZSoft.Common.Logging.LoggingSource.DataLayer);
                return fullParams;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// The type of response the job will receive from the Twitter API
        /// </summary>
        public abstract ApiResponseType ResponseType { get; }

        /// <summary>
        /// The HTTP method that this job uses (GET/POST etc)
        /// </summary>
        public abstract HttpMethod RequestType { get; }

        /// <summary>
        /// The type of parameters to submit - depends on API definition of the method invoked.
        /// </summary>
        public abstract ParameterType ParameterType { get; }



        /// <summary>
        /// Return a dictionary of values from a http response.
        /// Response format = key=value&key=value
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetHttpResponseAsDictionary(string httpResponse)
        {
            try
            {
                theLog.Log.LevelDown();

                theLog.Log.WriteDebug(string.Format("Processing response: {0}", httpResponse), SKZSoft.Common.Logging.LoggingSource.DataLayer);

                Dictionary<string, string> values = new Dictionary<string, string>();
                string[] elements = httpResponse.Split('&');
                foreach (string element in elements)
                {
                    string[] keyValue = element.Split('=');
                    string key = keyValue[0];

                    // get value (safely)
                    string value = string.Empty;
                    if (keyValue.Length > 1)
                    {
                        value = keyValue[1];
                    }
                    theLog.Log.WriteDebug(string.Format("key={0}, value={1}", key, value), SKZSoft.Common.Logging.LoggingSource.DataLayer);
                    values.Add(key, value);

                }
                return values;
            }
            finally { theLog.Log.LevelUp(); }
        }


        public HttpRequestMessage CreateHttpRequest()
        {
            m_httpRequest = new HttpRequestMessage(RequestType, URLWithParameters);

            // prepare message, headers, authorisation etc
            string authString = GetAuthorizationString(Credentials);

            m_httpRequest.Headers.Add(TwitterParameters.HttpAuthorization, authString);
            if(string.IsNullOrEmpty(RootBatch.HttpUserAgent))
            {
                throw new Exception("Consumer must specify the HttpUserAgent string for root batch");
            }

            m_httpRequest.Headers.Add(TwitterParameters.HttpUserAgent, RootBatch.HttpUserAgent);
            m_httpRequest.Headers.ExpectContinue = false;

            return m_httpRequest;
        }


        public virtual void AddParameters()
        {
            if (RequestType == HttpMethod.Post)
            {
                if (ParameterType == ParameterType.http)
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(ParameterStrings);
                    m_httpRequest.Content = content;
                }
                else
                {
                    StringContent content = new StringContent(ParmatersJson.ToString(), Encoding.UTF8, @"application/json");
                    m_httpRequest.Content = content;
                }
            }
        }

        private string GetAuthorizationString(Credentials credentials)
        {
            Dictionary<string, string> parameters = ParameterStringsCopy;

            // Add in essential authorisation parameters
            if (!string.IsNullOrEmpty(credentials.ConsumerKey))
            {
                parameters[TwitterParameters.oAuthConsumerKey] = credentials.ConsumerKey;
            }

            if (!string.IsNullOrEmpty(credentials.AuthToken))
            {
                parameters[TwitterParameters.TWITTER_PARAM_OAUTH_TOKEN] = credentials.AuthToken;
            }

            // grab the secrets too, for passing in.
            string consumerSecret = credentials.ConsumerSecret ?? "";
            string oAuthTokenSecret = credentials.AuthTokenSecret ?? "";
            OAuth auth = new OAuth();
            string method = RequestType.Method.ToString();
            string oauthUrl = URL;
            string authorizationString = auth.GetAuthorizationString(method, oauthUrl, parameters, consumerSecret, oAuthTokenSecret, AuthParametersOnly);
            return authorizationString;
        }

    }
}
