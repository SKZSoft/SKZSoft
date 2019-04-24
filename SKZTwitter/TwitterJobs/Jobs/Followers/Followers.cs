using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Net.Http;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Interfaces;

namespace SKZSoft.Twitter.TwitterJobs.Jobs.Followers
{
    public abstract class Followers : TwitterJob
    {
        internal Followers(Credentials credentials, EventHandler<JobCompleteArgs> completionDelegate, string cursor, long count)
            : base(credentials, completionDelegate)
        {
            base.ParameterStrings["count"] = count.ToString();
            base.ParameterStrings["cursor"] = cursor;
        }


        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }
        public override HttpMethod RequestType { get { return HttpMethod.Get; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }

        /// <summary>
        /// Empty. No data is required from previous jobs.
        /// </summary>
        /// <param name="previousJob"></param>
        public override void InitializeFromLastJob(Job previousJob)
        {
        }

    }
}
