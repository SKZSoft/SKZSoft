using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Queueing
{
    public class RunJobEventArgs<T> : EventArgs
    {
        public T Job { get; private set; }

        public RunJobEventArgs(T job)
        {
            Job = job;
        }
    }
}
