using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Auth;
using SKZTweets.TwitterData;
using System.Net.Http;
using System.Collections.Generic;
using SKZSoft.Common.Logging;
using SKZTweets.TwitterData.Exceptions;
using System.Threading.Tasks;
using SKZTweets.TwitterData.Models;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TwitterData m_twitterData;
        private HttpClient m_httpClient;
        private ThreadPoster m_threadPoster;

        private void SaveCredentials(Credentials credentials)
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("SKZThreads", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();
            prefEditor.PutString(Consts.KEY_AUTH_TOKEN, credentials.AuthToken);
            prefEditor.PutString(Consts.KEY_AUTH_TOKEN_SECRET, credentials.AuthTokenSecret);
            prefEditor.PutString(Consts.KEY_SCREEN_NAME, credentials.ScreenName);
            prefEditor.PutString(Consts.KEY_USER_ID, credentials.UserId.ToString());

            prefEditor.Commit();
        }

        private Credentials ReadCredentials()
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("SKZThreads", FileCreationMode.Private);

            string authToken = prefs.GetString(Consts.KEY_AUTH_TOKEN, "");
            string authTokenSecret = prefs.GetString(Consts.KEY_AUTH_TOKEN_SECRET, "");
            string screenName = prefs.GetString(Consts.KEY_SCREEN_NAME, "");
            string userIdAsString = prefs.GetString(Consts.KEY_USER_ID, "0");
            ulong userId = ulong.Parse(userIdAsString);
            Credentials credentials = new Credentials(Consts.CONSUMER_KEY, Consts.CONSUMER_SECRET, authToken, authTokenSecret, screenName, userId );
            return credentials;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // no logging on mobiles, but we need an instance
            // for the data layer
            Logger.InitialiseMOCK();


            Credentials credentials = ReadCredentials();
            m_httpClient = new HttpClient();
            m_twitterData = new TwitterData(m_httpClient, Consts.CONSUMER_KEY, Consts.CONSUMER_SECRET, credentials.AuthToken, credentials.AuthTokenSecret, credentials.ScreenName, credentials.UserId);
            m_twitterData.GetTwitterConfigStart(null, GetTwitterConfigEnd);
        }

        private void GetTwitterConfigEnd(object sender, EventArgs arg)
        { 
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnSignIn);
            button.Click += delegate { LogInTwitter(); };

            button.Enabled = !m_twitterData.Credentials.IsValid;


            button = FindViewById<Button>(Resource.Id.btnTweet);
            button.Click += delegate { Tweet(); };

            button.Enabled = m_twitterData.Credentials.IsValid;

            EditText text = FindViewById<EditText>(Resource.Id.txtMain);
            text.Text = random() + " This is a thread test. It has a bunch of stuff in it. Text, mainly. Need more than 140 characters, to split it, so here is some more text to push us over " + random();
        }

        private string random()
        {
            Random r = new Random();
            return r.Next().ToString();
        }

        private void Test()
        {
            EditText text = FindViewById<EditText>(Resource.Id.txtMain);
            text.Text = random() + " This is a thread test. It has a bunch of stuff in it. Text, mainly. Need more than 140 characters, to split it, so here is some more text to push us over " + random();
        }

        private void Tweet()
        {
            try
            {
                EditText text = FindViewById<EditText>(Resource.Id.txtMain);

                SKZTweets.TwitterData.ThreadController threadController = new ThreadController(m_twitterData);
                SKZTweets.TwitterData.Models.TweetThread threadData = new SKZTweets.TwitterData.Models.TweetThread();
                threadData.IntroText = "";
                threadData.ThreadText = text.Text;

                SKZTweets.TwitterData.Models.ThreadNumberSettings numberSettings = new SKZTweets.TwitterData.Models.ThreadNumberSettings(SKZTweets.TwitterData.Enums.ThreadNumberStyle.XofY, SKZTweets.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart);
                threadData.NumberSettings = numberSettings;
                Queue<string> tweets = GetTweets(ThreadUrlFormat.Full, threadData);

                // tell user there's no text and return
                if (tweets.Count == 0)
                {
                    Toast.MakeText(this, "No tweets to send. Enter some text", ToastLength.Long).Show();
                    return;
                }


                m_threadPoster = new SKZTweets.TwitterData.ThreadPoster(m_twitterData, tweets);
                //m_threadPoster.ThreadProgressUpdate += m_threadPoster_ThreadProgressUpdate;
                m_threadPoster.ThreadComplete += M_threadPoster_ThreadComplete; ;
                //m_threadPoster.ThreadCancelled += M_threadPoster_ThreadCancelled; ;
                //m_threadPoster.ThreadDeleted += M_threadPoster_ThreadDeleted;
                m_threadPoster.ExceptionRaised += M_threadPoster_ExceptionRaised; ;
                int milliSecondsBetweenTweets = 1000;

                //ShowProgress(true);
                //m_status = FormStatus.Posting;
                //SetFormStatuses();

                DoPostThreadBegin(milliSecondsBetweenTweets);
                return;
            }
            catch (TwitterUnauthorizedException ex)
            {
                Toast.MakeText(this, "Authentication error - either a problem with the app or with Twitter", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            return;
        }

        private void M_threadPoster_ExceptionRaised(object sender, ExceptionArgs e)
        {
            Toast.MakeText(this, "error", ToastLength.Long).Show();
        }

        private void M_threadPoster_ThreadComplete(object sender, ThreadCompleteArgs e)
        {
            Toast.MakeText(this, "Thread sent", ToastLength.Short).Show();
        }

        private void DoPostThreadBegin(int millisecondsBetweenTweets)
        {
            m_threadPoster.PostThreadBegin(millisecondsBetweenTweets);
        }


        /// <summary>
        /// Get the tweets which will be generated from data as it stands
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <returns></returns>
        private Queue<string> GetTweets(ThreadUrlFormat urlFormat, TweetThread threadData)
        {
            try
            {
                ThreadController threadController = new ThreadController(m_twitterData);

                Queue<string> tweets = threadController.SplitIntoTweets(threadData, urlFormat);
                return tweets;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            return null;
        }


        private void LogInTwitter()
        {
            var auth = new OAuth1Authenticator(
                consumerKey: Consts.CONSUMER_KEY,
                consumerSecret: Consts.CONSUMER_SECRET,
                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),  
                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),  
                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),  
                callbackUrl: new Uri("https://mobile.twitter.com")  
                );

            auth.Title = "Twitter";

            auth.Completed += twitter_auth_Completed;
            StartActivity(auth.GetUI(this));
        }

        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Toast.MakeText(this, "Authenticated!", ToastLength.Long).Show();

                Dictionary<string, string> values = eventArgs.Account.Properties;
                string authTokenSecret = values[Consts.KEY_AUTH_TOKEN_SECRET];
                string authToken = values[Consts.KEY_AUTH_TOKEN];
                string screenName = values[Consts.KEY_SCREEN_NAME];
                string userIdAsString = values[Consts.KEY_USER_ID];
                ulong userId = ulong.Parse(userIdAsString);

                Credentials credentials = new Credentials(Consts.CONSUMER_KEY, Consts.CONSUMER_SECRET, authToken, authTokenSecret, screenName, userId);

                SaveCredentials(credentials);
                m_twitterData = new TwitterData(m_httpClient, credentials);
                m_twitterData.GetTwitterConfigStart(null, null);
            }
        }
    }
}

