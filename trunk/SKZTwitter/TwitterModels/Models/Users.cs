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
    public class Users
    {
        public List<User> users { get; set; }
        public string next_cursor_str { get; set; }

        public Users()
        {
            users = new List<User>();
        }
    }
}
