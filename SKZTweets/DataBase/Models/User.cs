using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SKZSoft.SKZTweets.DataBase.Models
{
    [Table(Name="Users")]
    public class User
    {
        [Column(IsPrimaryKey =true)]
        public ulong UserId;

        [Column(DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string Screenname;

        [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string OAuthToken;

        [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string OAuthTokenSecret;
    }

}
