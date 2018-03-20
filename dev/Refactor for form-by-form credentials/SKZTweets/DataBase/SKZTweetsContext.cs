using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.EntityFrameworkCore;
using SKZSoft.SKZTweets.DataModels;

namespace SKZSoft.SKZTweets.DataBase
{
    public class SKZTweetsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string DBPath = Utils.GetDBFullPath("SKZTweets", "SKZTweets.db");
            string dataSource = string.Format("Data Source={0}", DBPath);
            optionsBuilder.UseSqlite(dataSource);
        }

        public Table<User> Users;
    }
}
