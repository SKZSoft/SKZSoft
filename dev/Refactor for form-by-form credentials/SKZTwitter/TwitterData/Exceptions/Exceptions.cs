using SKZSoft.Twitter.TwitterModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterData.Exceptions
{

    /// <summary>
    /// Just exists so that all child excpetions can be identified easily in code.
    /// If it is of this type, it contains its own meaningful error message
    /// </summary>
    public class TwitterException : Exception
    {
        public TwitterException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown if twitter refuses to post a tweet because it is a duplicate
    /// </summary>
    public class TwitterDuplicateStatusException : TwitterException
    {
        public TwitterDuplicateStatusException()
            : base(ExceptionStrings.TwitterErrorDuplicateStatus) { }
    }

    /// <summary>
    /// Thrown when Twitter APi returns 429. MUST notify user to log out of other apps.
    /// </summary>
    public class TwitterTooManyRequestsException : TwitterException
    {
        public TwitterTooManyRequestsException() : base(ExceptionStrings.TwitterErrorTooManyRequests) { }
    }

    /// <summary>
    /// 401 - HttpStatusCode.Unauthorized
    /// </summary>
    public class TwitterUnauthorizedException : TwitterException
    {
        private TwitterRequestResponse m_twitterRequestResponse;
        public TwitterUnauthorizedException(TwitterRequestResponse twitterResponse) : base(ExceptionStrings.TwitterErrorUnauthorized)
        {
            m_twitterRequestResponse = twitterResponse;
        }

        TwitterRequestResponse TwitterRequestResponse {  get { return m_twitterRequestResponse; } }
    }

    /// <summary>
    /// 502 bad gateway - down or being upgraded
    /// </summary>
    public class TwitterBadGateway: TwitterException
    {
        public TwitterBadGateway() : base(ExceptionStrings.TwitterErrorBadGateway) { }
    }

    /// <summary>
    /// 420 - twitter is enforcing rate limits
    /// </summary>
    public class TwitterRateLimits : TwitterException
    {
        public TwitterRateLimits() : base(ExceptionStrings.TwitterErrorRateLimits) { }
    }

    /// <summary>
    /// 429 - Twitter is enforcing rate limits
    /// </summary>
    public class TwitterTooManyRequests : TwitterException
    {
        public TwitterTooManyRequests() : base(ExceptionStrings.TwitterErrorTooManyRequests) { }
    }

    /// <summary>
    /// 422 - Twitter failed to process a picture
    /// </summary>
    public class TwitterUnprocessableEntity : TwitterException
    {
        public TwitterUnprocessableEntity() : base(ExceptionStrings.TwitterErrorUnprocessableEntity) { }
    }

    /// <summary>
    /// 503 - service unavailable (twitter is overwhelmed)
    /// </summary>
    public class TwitterServiceUnavailable : TwitterException
    {
        public TwitterServiceUnavailable() : base(ExceptionStrings.TwitterServiceUnavailable) { }
    }
}
