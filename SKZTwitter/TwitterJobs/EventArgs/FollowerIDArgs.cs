using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs
{
    public class FollowerIDArgs : EventArgs
    {
        List<TwitterModels.User> m_ids;

        public FollowerIDArgs(List<TwitterModels.User> data)
        {
            m_ids = data;
        }

        public List<TwitterModels.User> Users
        {
            get { return m_ids; }
        }

    }
}
