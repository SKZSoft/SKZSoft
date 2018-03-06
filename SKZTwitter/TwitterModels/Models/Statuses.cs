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
    public class Statuses
    {
        public List<Status> Items { get; set; }

        public Statuses()
        {
            Items = new List<Status>();
        }
    }
}
