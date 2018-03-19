using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class FollowerIDArgs : EventArgs
    {
        List<ulong> m_ids;

        public FollowerIDArgs(List<ulong> ids)
        {
            m_ids = ids;
        }

        public List<ulong> Ids
        {
            get { return m_ids; }
        }

    }
}
