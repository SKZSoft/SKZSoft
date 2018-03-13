using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Queueing
{
    public class RunJobEventArgs<T> : EventArgs
    {
        public T JobType { get; private set; }
        public object WorkItem { get; private set; }

        public RunJobEventArgs(T jobType, object workItem)
        {
            JobType = jobType;
            WorkItem = workItem;
        }
    }
}
