using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterJobs;

namespace SKZSoft.Twitter.TwitterData.Controllers
{
    public class JobFactories
    {

        public JobFactories(TwitterJobs.Factory factory)
        {
            Help = new Jobs.Help(factory);
        }

        public Controllers.Jobs.Help Help { get; set; }
    }
}
