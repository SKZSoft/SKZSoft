using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SKZSoft.SKZTweets.DataBase.Models
{
    public class SKZTweets : DataContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connmection"></param>
        public SKZTweets(string connection) : base(connection)
        { }

        public Table<User> Users;
    }
}
