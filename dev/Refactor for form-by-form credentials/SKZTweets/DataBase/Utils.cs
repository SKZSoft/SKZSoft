using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.IO;
using SKZSoft.SKZTweets.DataModels;


namespace SKZSoft.SKZTweets.DataBase
{
    public static class Utils
    {

        public static SKZTweetsContext EnsureCreated(string appName)
        {
            try
            {
                theLog.Log.LevelDown();

                string fullPath = GetDBFullPath(appName);
                string directory = GetDBPath(appName);

                Directory.CreateDirectory(directory);

                SKZTweetsContext context = new SKZTweetsContext();
                context.Database.EnsureCreated();
                return context;
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Get the path in which the database should be stored. In the %appData% Directory.
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string GetDBPath(string appName)
        {
            string DBFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + string.Format("\\{0}\\Database\\", appName);
            return DBFolder;
        }

        /// <summary>
        /// Return the full URL of the database file
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetDBFullPath(string appName)
        {
            string DBFolder = GetDBPath(appName);
            string filename = string.Format("{0}.db", appName);
            string fullPath = Path.Combine(DBFolder, filename);
            return fullPath;

        }

        /// <summary>
        /// CHecks if database exists
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Exists(string appName)
        {
            string fullPath = GetDBFullPath(appName);
            return File.Exists(fullPath);
        }

    }
}
