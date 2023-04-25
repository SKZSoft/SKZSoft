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
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.DataModels;

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
        private TwitterAccount m_selectedAccount;

        private DataBase.Persistence m_persistence;

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

                // initialise Consumer defaults with this app's ID
                ConsumerData.ConsumerKey = AppId.ConsumerKey;
                ConsumerData.ConsumerSecret = AppId.ConsumerSecret;

                CreateTwitterData();

                frmSelectAccount selectAccount = new frmSelectAccount();
                List<TwitterAccount> accounts = m_persistence.TwitterAccountGetAllAvailable();
                TwitterAccount account = selectAccount.SelectAccount(accounts, this);

                // No authorisation. Quit.
                if (account==null)
                {
                    Terminate();
                    return false;
                }

                m_selectedAccount = account;

                // Results will come back into the delegate method
                m_twitterData.Initialise(account.Credentials, GetConfigEnd, GetConfigException);
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

            SKZTweetsContext dbContext = DataBase.Utils.EnsureCreated(appName);
            m_persistence = new Persistence(dbContext);


            return true;
        }



        /// <summary>
        /// initialise application
        /// </summary>
        private bool CreateTwitterData()
        {
            try
            {
                theLog.Log.LevelDown();

                // Get credentials from application settings
                Properties.Settings settings = Properties.Settings.Default;
                theLog.Log.WriteDebug("Reading credentials from settings", Logging.LoggingSource.Boot);


                string userAgent = GetUserAgent();
                m_twitterData = new SKZSoft.Twitter.TwitterData.TwitterData(m_httpClient, AppId.oAuthCallbackValue, userAgent);

                return true;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private TwitterAccount GetAccountViaLogin(Credentials appCredentials)
        {
            TwitterAccount account = InitialiseTwitter(appCredentials);

            if (account == null)
            {
                // Cannot get authorised on Twitter. Give up.
                return null;
            }

            return account;
        }


        private string GetUserAgent()
        {
            string version = typeof(AppController).Assembly.GetName().Version.ToString();
            string value = string.Format("{0} {1}", "SKZTweets", version);
            return value;
        }

        private TwitterAccount InitialiseTwitter(Credentials partialCredentials)
        {
            TwitterAccount account = GetTwitterAuth(partialCredentials);
            return account;
        }

        /// <summary>
        /// Get authorisation from Twitter site
        /// </summary>
        private TwitterAccount GetTwitterAuth(Credentials partialCredentials)
        {
            try
            {
                theLog.Log.LevelDown();

                frmAuthorise authorise = new frmAuthorise(partialCredentials, this);
                TwitterAccount account = authorise.AuthoriseTwitter();
                return account;
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
                frmMainWindow mainWindow = new frmMainWindow(this, m_persistence, m_selectedAccount);
                MainWindow = mainWindow;
                mainWindow.Initialise();
                mainWindow.Show();
                m_splash.Close();
            }
            finally { theLog.Log.LevelUp(); }
        }

        public bool AllFormsMayClose { get; set; }

        /// <summary>
        /// Persistence to database functionality
        /// </summary>
        public Persistence Persistence {  get { return m_persistence; } }

    }
}
