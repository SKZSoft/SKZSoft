using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.IO;


namespace SKZSoft.SKZTweets.DataBase
{
    public static class Utils
    {

        public static void CreateDatabase(string appName, string filename)
        {
            try
            {
                theLog.Log.LevelDown();

                string fullPath = GetDBFullPath(appName, filename);

                Models.SKZTweetsContext context = new Models.SKZTweetsContext();
                context.Database.EnsureCreated();
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
        public static string GetDBFullPath(string appName, string filename)
        {
            string DBFolder = GetDBPath(appName);
            string fullPath = Path.Combine(DBFolder, filename);
            return fullPath;

        }

        /// <summary>
        /// CHecks if database exists
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Exists(string appName, string filename)
        {
            string fullPath = GetDBFullPath(appName, filename);
            return File.Exists(fullPath);
        }

    }
}
