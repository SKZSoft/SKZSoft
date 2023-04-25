using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterData.Exceptions
{
    /// <summary>
    /// Generic class to handle Twitter status codes which are not otherwise handled
    /// </summary>
    public class TwitterGenericException : Exception
    {
        private HttpStatusCode m_status;

        public int Code { get; set; }
        public string TwitterMessage { get; set; }

        private TwitterErrors m_errors;

        /// <summary>
        /// The Http Status which was returned
        /// </summary>
        public HttpStatusCode HttpStatus { get { return m_status; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public TwitterGenericException(HttpStatusCode code, string jsonResponse) : base("Twitter error")
        {
            m_status = code;

            // decode the response from Twitter
            m_errors = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterErrors>(jsonResponse);
        }

        public TwitterError FirstTwitterError { get { return m_errors.errors[0]; } }

        public TwitterErrors TwitterErrors { get { return m_errors; } }

    }
}


