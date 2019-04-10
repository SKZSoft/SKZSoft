using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Models;
using System.Collections.Generic;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Common.Logging;
using System.Text;
using System.Net.Http;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.Twitter.TwitterJobs.Signing;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const int MAX_CHARS_FIRST_TWEET_X = 137;
        private const int MAX_CHARS_FIRST_TWEET_XOFY = 134;
        private const string chars134 = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234";
        private const string chars139 = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
        private const string chars140 = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
        private const string chars139WithSpaces = "123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789";
        private const string chars280 = chars140 + chars140;

        [TestMethod]
        public void TestOAuth()
        {
            string method = "GET";
            string url = " http://test.com";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("param1", "value");
            parameters.Add("param2", "another value");
            string consumerSecret = "CONSUMER_SECRET";
            string oAuthTokenSecret = "AUTH_SECRET_TOKEN";

            OAuth auth = new OAuth();
            auth.MOCKing = true;
            string result = auth.GetAuthorizationString(method, url, parameters, consumerSecret, oAuthTokenSecret, false);
            string expected = "OAuth oauth_nonce=\"759804569087459568y489067y7y689243790847\", oauth_signature=\"zN6Wxz3vcs5ahoIaa6Pob72ESTY%3D\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"1234567890\", oauth_version=\"1.0\"";
            Assert.AreEqual(result, expected);

            
        }

        [TestMethod]
        public void TestHash()
        {
            string key = "signingKey";
            string msg = "signatureBaseString";

            HmacSigner hmac = new HmacSigner();
            byte[] hash = hmac.Sign(key, msg);

            string result = Convert.ToBase64String(hash);
            string expected = "U8qse4tI3xY3rdRlLO5qTbwX0YA=";
            Assert.AreEqual(result, expected);


            // Exactly 64 characters in key
            key = "1234567890123456789012345678901234567890123456789012345678901234";
            msg = "signatureBaseString";

            hash = hmac.Sign(key, msg);

            result = Convert.ToBase64String(hash);
            expected = "3+KWTXkYsgmA2FjLXb6ObW0jlHk=";
            Assert.AreEqual(result, expected);


            // Exactly 64 characters in message
            key = "signingKey";
            msg = "1234567890123456789012345678901234567890123456789012345678901234";

            hash = hmac.Sign(key, msg);

            result = Convert.ToBase64String(hash);
            expected = "KreTvBXCZBrt46ZBiVuzExmoiFU=";
            Assert.AreEqual(result, expected);


            // More than 64 characters in key and message
            key = "1234567890123456789012345678901234567890123456789012345678901234abc";
            msg = "1234567890123456789012345678901234567890123456789012345678901234abc";

            hash = hmac.Sign(key, msg);

            result = Convert.ToBase64String(hash);
            expected = "Yt33QUj9llq+C/28bNe2YyXsI4s=";
            Assert.AreEqual(result, expected);
        }


        [TestMethod]
        public void TestURLs()
        {
            theLog.InitialiseMOCK();

            // General initialisation
            SKZSoft.Twitter.TwitterData.Models.TweetThread data = new TweetThread();
            data.IntroText = "";
            data.ThreadText = "";
            SKZSoft.Twitter.TwitterData.Models.ThreadNumberSettings numberSettings = new ThreadNumberSettings(SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.NoNumbers, SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart);
            data.NumberSettings = numberSettings;

            // URL in intro
            data.IntroText = "http://www.yahoo.com";
            string expected = "http://0000000000000001";
            DoTestSingle(data, expected);

            // 2 URLs in intro
            data.IntroText += " http://google.com";
            expected += " http://0000000000000002";
            DoTestSingle(data, expected);

            // URL in body
            data.ThreadText = " This is a URL too: http://www.bbc.co.uk";
            expected += " This is a URL too: http://0000000000000003";
            DoTestSingle(data, expected);

            // repeat 2st URL in body
            data.ThreadText += " first URL again: http://www.yahoo.com";
            expected += " first URL again: http://0000000000000001";
            DoTestSingle(data, expected);

            // test https
            data.ThreadText += " secure: https://go.com";
            expected += " secure:";

            List<string> expectedResults = new List<string>();
            expectedResults.Add(expected);
            string expectedLine2 = "https://000000000000004";
            expectedResults.Add(expectedLine2);
            DoTestMultiple(data, expectedResults);

            // another http
            data.ThreadText += " http://another.com";
            expectedResults = new List<string>();
            expectedResults.Add(expected);
            expectedLine2 = "https://000000000000005 http://0000000000000004"; // the http tweets get picked up FIRST, so the numbering gets into an odd order.
            expectedResults.Add(expectedLine2);
            DoTestMultiple(data, expectedResults);


            // another https
            data.ThreadText += " https://final.com end text";
            expectedResults = new List<string>();
            expectedResults.Add(expected);
            expectedLine2 += " https://000000000000006 end text";
            expectedResults.Add(expectedLine2);
            DoTestMultiple(data, expectedResults);

        }

        [TestMethod]
        public void TestThreadNumbersComplex()
        {
            theLog.InitialiseMOCK();

            // General initialisation
            SKZSoft.Twitter.TwitterData.Models.TweetThread data = new TweetThread();
            data.IntroText = "";
            data.ThreadText = "";
            // these settings get overridden every test.
            SKZSoft.Twitter.TwitterData.Models.ThreadNumberSettings numberSettings = new ThreadNumberSettings(SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.X, SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart);
            data.NumberSettings = numberSettings;

            // Style: X, at start, edge condition
            string testChars = GetCharacters(MAX_CHARS_FIRST_TWEET_X, "A");
            data.ThreadText = testChars;
            string expected = "1 " + testChars;
            DoTestSingle(data, expected);

            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtEnd;
            expected = testChars + " 1";
            DoTestSingle(data, expected);

            // now add a character and check the boundary is hit. We should get two tweets.
            data.ThreadText = testChars + "B";
            List<string> expectedResults = new List<string>();
            expectedResults.Add(testChars + " 1");
            expectedResults.Add("B 2");
            DoTestMultiple(data, expectedResults);

            // Style: X/Y, at start, edge condition
            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart;
            data.NumberSettings.Style = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.XofY;
            testChars = GetCharacters(MAX_CHARS_FIRST_TWEET_XOFY, "A");
            data.ThreadText = testChars;
            expected = "1/1 " + testChars;
            DoTestSingle(data, expected);

            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtEnd;
            expected = testChars + " 1/1";
            DoTestSingle(data, expected);

            // now add a character and check the boundary is hit. We should get two tweets.
            data.ThreadText = testChars + "B";
            expectedResults = new List<string>();
            expectedResults.Add(testChars + " 1/2");
            expectedResults.Add("B 2/2");
            DoTestMultiple(data, expectedResults);


        }

        private string GetCharacters(int length, string character)
        {
            string result = string.Empty;
            result = result.PadLeft(length);
            result = result.Replace(" ", character);
            return result;

        }

        [TestMethod]
        public void TestThreadNumbersSingles()
        {
            theLog.InitialiseMOCK();

            // General initialisation
            SKZSoft.Twitter.TwitterData.Models.TweetThread data = new TweetThread();
            data.IntroText = "";
            data.ThreadText = "";
            // these settings get overridden every test.
            SKZSoft.Twitter.TwitterData.Models.ThreadNumberSettings numberSettings = new ThreadNumberSettings(SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.X, SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart);
            data.NumberSettings = numberSettings;

            // Test intro and body
            data.IntroText = "Intro";
            data.ThreadText = "Body";
            DoTestNumberStylesSingle(data);

            // Test just body
            data.IntroText = "";
            DoTestNumberStylesSingle(data);

            // Test just intro
            data.IntroText = "Intro";
            data.ThreadText = "";
            DoTestNumberStylesSingle(data);


            // Test tweet which should NOT split for ANY numbers
            // (intro only)
            data.IntroText = chars134;
            data.ThreadText = "";
            DoTestNumberStylesSingle(data);

            // Test tweet which should NOT split for ANY numbers
            // (body only)
            data.IntroText = "";
            data.ThreadText = chars134;
            DoTestNumberStylesSingle(data);

        }

        private void DoTestNumberStylesSingle(TweetThread data)
        {
            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart;
            data.NumberSettings.Style = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.X;
            DoTestNumberSingle(data);

            data.NumberSettings.Style = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.XofY;
            DoTestNumberSingle(data);

            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtEnd;
            DoTestNumberSingle(data);

            data.NumberSettings.Position = SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart;
            DoTestNumberSingle(data);
        }

        private void DoTestNumberSingle(TweetThread data)
        {
            // get number string
            string number = "1";
            if(data.NumberSettings.Style == SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.XofY)
            {
                number = "1/1";
            }

            // add space in appropriate place
            string body;
            if (data.NumberSettings.Position == SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtEnd)
            {
                body =  data.ThreadText + " " + number;
            }
            else
            {
                body = number + " " + data.ThreadText;
            }


            string expectedResult = data.IntroText + body;
            DoTestSingle(data, expectedResult);
        }

        [TestMethod]
        public void TestThreadNoNumbers()
        {
            // Initialise mock log
            theLog.InitialiseMOCK();

            // General initialisation
            SKZSoft.Twitter.TwitterData.Models.TweetThread data = new TweetThread();
            data.IntroText = "";
            data.ThreadText = "";
            SKZSoft.Twitter.TwitterData.Models.ThreadNumberSettings numberSettings = new ThreadNumberSettings(SKZSoft.Twitter.TwitterData.Enums.ThreadNumberStyle.NoNumbers, SKZSoft.Twitter.TwitterData.Enums.ThreadNumberPosition.NumbersAtStart);
            data.NumberSettings = numberSettings;

            // test empty thread
/*            TwitterData td = new TwitterData(null,"", "", "", "", "", 0, "", "");
            td.MOCKTwitterConfiguration = GetMOCKConfig();
            ThreadController controller = new ThreadController(td);
            Queue<string> tweets = controller.SplitIntoTexts(data, ThreadUrlFormat.Tokenized);
            if (tweets.Count > 0)
            {
                Assert.Fail("No tweets should be generated from empty strings with no numbers");
            }

            // Test intro and body
            data.IntroText = "Intro";
            data.ThreadText = "Body";
            DoTestSingle(data, "IntroBody");

            // Test just body
            data.IntroText = "";
            DoTestSingle(data, "Body");

            // Test just into
            data.IntroText = "Intro";
            data.ThreadText = "";
            DoTestSingle(data, "Intro");

            // Test intro of 139 characters and body of 1
            data.IntroText = chars139;
            data.ThreadText = "A";
            DoTestSingle(data, chars139 + "A");

            // test intro of 139 characters and a body of 3
            data.ThreadText = "ABC";
            List<string> testTweets = new List<string>();
            testTweets.Add(chars139);
            testTweets.Add("ABC");
            DoTestMultiple(data, testTweets);

            // test body of 140 characters
            data.IntroText = string.Empty;
            data.ThreadText = chars140;
            DoTestSingle(data, chars140);

            // test body of over 140 characters, no spaces
            data.ThreadText = chars140 + "Hello";
            testTweets.Clear();
            testTweets.Add(chars140);
            testTweets.Add("Hello");
            DoTestMultiple(data, testTweets);

            // test body of 280 chars, no spaces
            data.ThreadText = chars280;
            testTweets.Clear();
            testTweets.Add(chars140);
            testTweets.Add(chars140);
            DoTestMultiple(data, testTweets);

            // test of lots of little words.
            data.ThreadText = chars139WithSpaces + " " + chars139WithSpaces;
            testTweets.Clear();
            testTweets.Add(chars139WithSpaces);
            testTweets.Add(chars139WithSpaces);
            DoTestMultiple(data, testTweets);
            */
        }

        private void DoTestMultiple(TweetThread threadData, List<string> expectedTweets)
        {
            // do split
            TwitterData td = new TwitterData(null, "", "");
            td.MOCKTwitterConfiguration = GetMOCKConfig();

            ThreadController controller = new ThreadController(td);
            Queue<string> tweets = controller.SplitIntoTexts(threadData, ThreadUrlFormat.Tokenized);

            // verify count
            if(tweets.Count != expectedTweets.Count)
            {
                Assert.Fail(string.Format("Expected {0} tweets but found {1}", expectedTweets.Count, tweets.Count));
            }

            // verify each tweet
            foreach(string testTweet in expectedTweets)
            {
                string outputTweet = tweets.Dequeue();
                if(outputTweet != testTweet)
                {
                    Assert.Fail(string.Format("Expected [{0}] found [{1}]", testTweet, outputTweet));
                }
            }
        }


        private void DoTestSingle(TweetThread threadData, string firstTweet)
        {
            TwitterData td = new TwitterData(null, "", "");
            td.MOCKTwitterConfiguration = GetMOCKConfig();
            ThreadController controller = new ThreadController(td);
            Queue<string> tweets = controller.SplitIntoTexts(threadData, ThreadUrlFormat.Tokenized);

            if (tweets.Count !=1)
            {
                Assert.Fail("Expected 1 tweet. Found " + tweets.Count);
            }

            string tweet = tweets.Dequeue();
            if (tweet != firstTweet)
            {
                Assert.Fail(string.Format("Expected [{0}] found [{1}]", firstTweet, tweet));
            }
        }

        private TwitterConfiguration GetMOCKConfig()
        {
            TwitterConfiguration tc = new TwitterConfiguration();
            tc.short_url_length = 23; // current value returned by twitter
            return tc;
        }

        // XXXSKZ TODO: unit tests for twitter jobs and internal methods
    }
}
