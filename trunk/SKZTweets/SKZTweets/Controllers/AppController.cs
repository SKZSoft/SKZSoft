using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using System.Reflection;
using SKZSoft.SKZTweets.Data;
using SKZSoft.Twitter.TwitterData;
using System.Net;
using System.Net.Http;
using SKZSoft.Twitter.TwitterData.Enums;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.SKZTweets.Interfaces;
using SKZSoft.SKZTweets.DataBase;

namespace SKZSoft.SKZTweets.Controllers
{
    public class AppController
    {
        private frmSplash m_splash;
        private IMainWindow m_mainWindow;
        private bool m_terminating;
        private SKZSoft.Twitter.TwitterData.TwitterData m_twitterData;
        private HttpClient m_httpClient;            // single instance for use throughout the app.
        public bool Terminating { get { return m_terminating; } }


        public TwitterData TwitterData { get { return m_twitterData; } }

        /// <summary>
        /// Reference to the main application window
        /// </summary>
        public IMainWindow MainWindow
        {
            get { return m_mainWindow; }
            set { m_mainWindow = value; }
        }

        /// <summary>
        /// Various initialisation and bootstrapping
        /// </summary>
        public bool Initialise()
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug("Creating and showing splash", Logging.LoggingSource.Boot);

                m_splash = new frmSplash();
                m_splash.Show();

                m_splash.SetStatus("Accessing database");

                if(!OpenOrCreateDB())
                {
                    return false;
                }

                m_splash.SetStatus("Logging in to Twitter");

                theLog.Log.WriteDebug("Creating httpClient", SKZSoft.Common.Logging.LoggingSource.Boot);
                HttpClientHandler handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip;
                }
                // TODO: Proxy support?
                //if (Proxy != null && handler.SupportsProxy)
                //handler.Proxy = Proxy;

                m_httpClient = new HttpClient(handler);

                bool result = DoAuthorise();

                // No authorisation. Quit.
                if(!result)
                {
                    Terminate();
                    return false;
                }

                // Results will come back into the delegate method
                m_twitterData.GetTwitterConfigStart(GetConfigEnd, GetConfigException);
                return true;
            }
            finally
            {
                theLog.Log.LevelUp();
            }
        }

        private bool OpenOrCreateDB()
        {
            string appName = Strings.AppName;
            string filename = string.Format("{0}.mdf", appName);
            if(!DataBase.Utils.Exists(appName, filename))
            {
                if(!Utils.SKZConfirmationMessageBox(Strings.DBDoesNotExist))
                {
                    return false;
                }
                DataBase.Utils.CreateDatabase(appName, appName + ".mdf");
            }

            return true;
        }


        /// <summary>
        /// initialise application
        /// </summary>
        private bool DoAuthorise()
        {
            try
            {
                theLog.Log.LevelDown();

                // Get credentials from application settings
                Properties.Settings settings = Properties.Settings.Default;
                theLog.Log.WriteDebug("Reading credentials from settings", Logging.LoggingSource.Boot);
                string oAuthToken = settings.OAuthToken;
                string oAuthTokenSecret = settings.OAuthTokenSecret;
                string screenName = settings.ScreenName;
                ulong userId = settings.UserId;

                string userAgent = GetUserAgent();

                m_twitterData = new SKZSoft.Twitter.TwitterData.TwitterData(m_httpClient, AppId.ConsumerKey, AppId.ConsumerSecret, oAuthToken, oAuthTokenSecret, screenName, userId, AppId.oAuthCallbackValue, userAgent);
                if (!m_twitterData.Credentials.IsValid)
                {
                    return InitialiseTwitter();
                }

                // tell main form about the change
                if (m_mainWindow != null)
                {
                    theLog.Log.WriteDebug("Broadcasting credientials change", Logging.LoggingSource.GUI);
                    m_mainWindow.CredentialsChanged(m_twitterData);
                }

                return true;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private string GetUserAgent()
        {
            string version = typeof(AppController).Assembly.GetName().Version.ToString();
            string value = string.Format("{0} {1}", "SKZTweets", version);
            return value;
        }

        private bool InitialiseTwitter()
        {
            bool authorized = GetTwitterAuth();

            if (!authorized)
            {
                // Cannot get authorised on Twitter. Give up.
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get authorisation from Twitter site
        /// </summary>
        private bool GetTwitterAuth()
        {
            try
            {
                theLog.Log.LevelDown();

                frmAuthorise authorise = new frmAuthorise(this);
                bool result = authorise.AuthoriseTwitter();
                return result;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Allow the user to switch credentials on twitter
        /// </summary>
        public bool SwitchCredentials()
        {
            try
            {
                theLog.Log.LevelDown();
                DeleteCredentials();
                if (InitialiseTwitter())
                {
                    m_mainWindow.CredentialsChanged(m_twitterData);
                    return true;
                }
                return false;
            }
            finally { theLog.Log.LevelUp(); }
        }

        public void DeleteCredentials()
        {
            try
            {
                theLog.Log.LevelDown();

                // get application settings
                theLog.Log.WriteDebug("Wiping existing credentials", Logging.LoggingSource.GUI);
                Properties.Settings settings = Properties.Settings.Default;

                // wipe settings
                settings.OAuthToken = string.Empty;
                settings.OAuthTokenSecret = string.Empty;
                settings.ScreenName = string.Empty;
                settings.Save();

                m_twitterData.Credentials.Clear();
                m_mainWindow.CredentialsChanged(m_twitterData);
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Called to REQUEST termination. 
        /// Controller may abort the process if data would be lost and user does not confirm.
        /// If Termination may proceed, will also then terminate the application.
        /// </summary>
        /// <returns>FALSE if the process was aborted.</returns>
        public bool Terminate()
        {
            theLog.Log.LevelDown();

            m_terminating = true;

            // Cast to a form
            System.Windows.Forms.Form mainForm = (System.Windows.Forms.Form)MainWindow;

            theLog.Log.WriteDebug("Unloading main window", Logging.LoggingSource.GUI);

            // kill the form and lose the reference to it.
            MainWindow = null;

            // Kill the data layer
            if (m_twitterData != null)
            {
                m_twitterData.Terminate();
            }

            System.Windows.Forms.Application.Exit();

            return true;
        }

        public void ShowChangeLog()
        {
            try
            {
                theLog.Log.LevelDown();
                string path = System.Windows.Forms.Application.StartupPath;
                path += @"\docs\ChangeLog.txt";
                System.Diagnostics.Process.Start(path);
            }
            finally { theLog.Log.LevelUp(); }
        }

        public void ShowKnownIssues()
        {
            try
            {
                theLog.Log.LevelDown();
                string path = System.Windows.Forms.Application.StartupPath;
                path += @"\docs\KnownIssues.txt";
                System.Diagnostics.Process.Start(path);
            }
            finally { theLog.Log.LevelUp(); }

        }

        
        void GetConfigException(object sender, JobExceptionArgs e)
        {
            Utils.HandleException(e.Exception, true, e.Job);
            Terminate();
        }

        void GetConfigEnd(object sender, BatchCompleteArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                if(m_splash.InvokeRequired)
                {
                    theLog.Log.WriteDebug("Invoking self", Logging.LoggingSource.Boot);
                    m_splash.Invoke((System.Windows.Forms.MethodInvoker) delegate { GetConfigEnd(sender, e); });
                    return;
                }

                theLog.Log.WriteDebug("Config job has completed. Closing splash, starting main window", Logging.LoggingSource.Boot);
                frmMainWindow mainWindow = new frmMainWindow(this);
                MainWindow = mainWindow;
                mainWindow.Initialise();
                mainWindow.Show();
                m_splash.Close();
            }
            finally { theLog.Log.LevelUp(); }
        }

        public bool AllFormsMayClose { get; set; }
    }
}
