using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class BatchCompleteArgs : EventArgs
    {
        public Batch Batch { get; internal set; }

        public BatchCompleteArgs(Batch batch)
        {
            Batch = batch;
        }
    }
}
