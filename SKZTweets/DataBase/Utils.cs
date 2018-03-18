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
                Models.SKZTweets db = new Models.SKZTweets(fullPath);

            }
            finally { theLog.Log.LevelUp(); }

        }

        public static string GetDBPath(string appName)
        {
            string DBFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + string.Format("\\{0}\\Database\\", appName);
            return DBFolder;
        }

        public static string GetDBFullPath(string appName, string filename)
        {
            string DBFolder = GetDBPath(appName);
            string fullPath = Path.Combine(DBFolder, filename);
            return fullPath;

        }

        public static bool Exists(string appName, string filename)
        {
            string fullPath = GetDBFullPath(appName, filename);
            return File.Exists(fullPath);
        }

    }
}
