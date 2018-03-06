using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterModels
{
    public class StatusEntities
    {
        public object[] hashtags { get; set; }
        public object[] symbols { get; set; }
        public User_Mentions[] user_mentions { get; set; }
        public Url[] urls { get; set; }
        public Media[] media { get; set; }
    }

}
