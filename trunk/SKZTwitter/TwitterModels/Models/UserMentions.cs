using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class User_Mentions
    {
        public string screen_name { get; set; }
        public string name { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public int[] indices { get; set; }
    }
}
