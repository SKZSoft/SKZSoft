using SKZSoft.Common.Logging;
using SKZSoft.Common.IniFile;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterData;
using Logging = SKZSoft.Common.Logging;


using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZTweets_tiny_harness
{
    public class Controller
    {

        private const string m_INI_FILE_NAME = "config.ini";

        private HttpClient m_httpClient;
        private SKZSoft.Twitter.TwitterData.TwitterData m_twitterData;    // the comms layer
        private Credentials m_credentials;

        /// <summary>
        /// Twitter Data
        /// </summary>
        public TwitterData TwitterData { get { return m_twitterData; } }

        /// <summary>
        /// Twitter Credentials
        /// </summary>
        public Credentials Credentials { get { return m_credentials; } }

        /// <summary>
        /// Initialise the system
        /// </summary>
        public void Initialise()
        {
            InitialiseLogs();
            InitialiseHttp();
            InitialiseTwitterData();
            InitialiseCredentials();
        }

        /// <summary>
        /// Initialise logs.
        /// These must be initialised for the Twitter layer to work
        /// </summary>
        private void InitialiseLogs()
        {

            Logging.LogSettings settings = new Logging.LogSettings();
            settings.LogFileName = "SKZTweetsHarness";
            settings.LogFileExtension = "log";
            settings.UnhandledLogFileName = "SKZTweetsHarnessUnprocessed";
            settings.UnhandledFileExtension = ".log";
            settings.AppName = "SKZTweetsHarness";
            settings.DeleteAfterDays = 1;

            Logging.Logger.Initialise(settings);
        }

        private void InitialiseTwitterData()
        {
            // create initial data layer with just minimal data since user credentials are not available
            m_twitterData = new TwitterData(m_httpClient, "oob", "Test harness");
        }

        private void InitialiseHttp()
        {
            // Create single httpclient for the application
            HttpClientHandler handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip;
            }

            m_httpClient = new HttpClient(handler);

        }

        private void InitialiseCredentials()
        {
            // look for config file
            string currentPath = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(new string[] { currentPath, m_INI_FILE_NAME });

            // get entries from config file if it exists
            if (File.Exists(fullPath))
            {
                IniFile ini = new IniFile(fullPath);

                // initialise Consumer defaults with this app's ID
                ConsumerData.ConsumerKey = ini.GetEntry("ConsumerKey");
                ConsumerData.ConsumerSecret = ini.GetEntry("ConsumerSecret");
                string accessToken = ini.GetEntry("AccessToken");
                string accessTokenSecret = ini.GetEntry("AccessTokenSecret");
                string screenName = ini.GetEntry("ScreenName");
                ulong userId = ini.GetEntryAsUlong("UserId");
                m_credentials = new Credentials(accessToken, accessTokenSecret, screenName, userId);

                if (m_credentials.IsValid)
                {
                    return;
                }

                // Controller doing GUI stuff but it is needed.
                MessageBox.Show(Strings.IniFileBad);
            }

            // get credentials manually
            frmSecurityDetails security = new frmSecurityDetails();
            m_twitterData = security.GetTwitterData(m_httpClient);
            m_credentials = security.Credentials;
        }

    }
}
