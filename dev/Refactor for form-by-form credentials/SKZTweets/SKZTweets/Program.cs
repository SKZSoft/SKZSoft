using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.SKZTweets.Controllers;

namespace SKZSoft.SKZTweets
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Upgrade settings from earlier versions
                if (SKZTweets.Properties.Settings.Default.UpgradeRequired)
                {
                    SKZTweets.Properties.Settings.Default.Upgrade();
                    SKZTweets.Properties.Settings.Default.UpgradeRequired = false;
                    SKZTweets.Properties.Settings.Default.Save();
                }

                Logging.LogSettings settings = new Logging.LogSettings();
                settings.LogFileName = "SKZTweets";
                settings.LogFileExtension = "log";
                settings.UnhandledLogFileName = "SKZTweetsUnprocessed";
                settings.UnhandledFileExtension = ".log";
                settings.AppName = "SKZTweets";

                // Get logging settings from app configs
                Properties.Settings appSettings = Properties.Settings.Default;
                settings.DeleteAfterDays = appSettings.LogDeleteAfterDays;
                settings.Level = (Logging.LoggingLevel)appSettings.LogLevel;

                Logging.Logger.Initialise(settings);

                // create main controller and initialise it
                theLog.Log.WriteDebug("Initialising main controller", Logging.LoggingSource.GUI);
                AppController mainController = new Controllers.AppController();

                if(mainController.Initialise())
                {
                    Application.Run();
                }

            }
            catch(Exception ex)
            {
                Utils.HandleException(ex);
            }
        }
    }
}
