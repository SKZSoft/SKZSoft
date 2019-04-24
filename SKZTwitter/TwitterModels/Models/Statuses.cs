using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    /// <summary>
    /// Not deserializable
    /// </summary>
    public class StatusList
    {
        public List<Status> Items { get; set; }

        public StatusList()
        {
            Items = new List<Status>();
        }
    }
}
