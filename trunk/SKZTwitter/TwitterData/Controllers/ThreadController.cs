using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Common.Logging;
using SKZTweets.TwitterData.Enums;
using SKZTweets.TwitterData.Models;
using SKZTweets.TwitterModels;
using SKZTweets.TwitterData.Consts;
using System.Diagnostics;

namespace SKZTweets.TwitterData
{

    public enum ThreadUrlFormat
    {
        Tokenized,
        Display,
        Full
    }

    /// <summary>
    /// Methods for processing thread. Splitting into tweets, etc.
    /// For comms, see ThreadPoster class.
    /// </summary>
    public class ThreadController
    {
        private TwitterData m_titterData;
        public ThreadController(TwitterData twitterData)
        {
            m_titterData = twitterData;
        }

        private string ReturnSanitisedText(string fullText)
        {
            try
            {
                theLog.Log.LevelDown();
                
                // remove any whitespace at start or end
                //fullText = fullText.Trim();

                return fullText;
            }
            finally { theLog.Log.LevelUp(); }
        }


        /// <summary>
        /// Get the length of the string required to display numbering in a tweet
        /// </summary>
        /// <param name="numberSettings"></param>
        /// <returns></returns>
        public int GetNumberLength(Models.ThreadNumberSettings numberSettings)
        {
            try
            {
                theLog.Log.LevelDown();
                return GetTemplate(numberSettings.Style).Length;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private string GetTemplate(ThreadNumberStyle style)
        {
            try
            {
                theLog.Log.LevelDown();
                switch (style)
                {
                    case ThreadNumberStyle.X:
                        return DataConsts.ThreadCounterTemplateX;

                    case ThreadNumberStyle.XofY:
                        return DataConsts.ThreadCounterTemplateXofY;

                    case ThreadNumberStyle.NoNumbers:
                        return string.Empty;

                    default:
                        throw new Exception("Numbering style not supported");
                }
            }
            finally { theLog.Log.LevelUp(); }
        }


        public int GetMaxIntroLength(TweetThread threadData)
        {
            try
            {
                theLog.Log.LevelDown();

                string template = GetTemplate(threadData.NumberSettings.Style);
                int templateLength = template.Length;
                int textAfterLength = DataConsts.TextAfterNumbers.Length;

                // the max into length is the max tweet length minus the template length (to ensure at least one number is visible in 1st tweet)
                int maxIntroLen = DataConsts.MaxCharsPerTweet - templateLength - textAfterLength;

                // if we're forcing new lines, knock off a couple more characters
                if(threadData.StartNewLineAfterIntro)
                {
                    maxIntroLen -= 2;
                }

                theLog.Log.WriteDebug(string.Format("Max into length is {0} for template \"{1}\"", maxIntroLen, template), LoggingSource.DataLayer);
                return maxIntroLen;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private void GetMaxLengths(TweetThread threadData, out int maxCharsPerThreadTweet, out int maxCharsForFirstTweet)
        {
            string template = GetTemplate(threadData.NumberSettings.Style);
            string textAfterNumber = DataConsts.TextAfterNumbers;

            Debug.WriteLine("GetMaxLengths");

            // max chars per THREAD tweet needs to allow for thread count text and other stuff
            maxCharsPerThreadTweet = DataConsts.MaxCharsPerTweet - template.Length;

            Debug.WriteLine("Template = [" + template + "]");
            Debug.WriteLine("Max chars less template = " + maxCharsPerThreadTweet.ToString());

            // allow space for text after numbers (if they exist)
            if (threadData.NumberSettings.Style != ThreadNumberStyle.NoNumbers)
            {
                maxCharsPerThreadTweet -= textAfterNumber.Length;
                Debug.WriteLine("Removing " + textAfterNumber.Length.ToString() + " spacing");
                Debug.WriteLine("Max chars per normal tweet = " + maxCharsPerThreadTweet.ToString());
            }

            // the first tweet has reduced characters if there is an intro
            maxCharsForFirstTweet = maxCharsPerThreadTweet - threadData.TokenizedIntroText.Length;
            Debug.WriteLine("Tokenised Intro length = " + threadData.TokenizedIntroText.Length.ToString());
            Debug.WriteLine("Max chars for first tweet = " + maxCharsForFirstTweet.ToString());


            // also if this is a reply. Reduced length will be "@ScreenName ", as this has to be included in sent text.
            // This appears to be a bug in the API.
            if (threadData.InReplyTo != null)
            {
                string screenName = threadData.InReplyTo.user.screen_name;
                maxCharsForFirstTweet -= screenName.Length;
                maxCharsForFirstTweet -= 2;     // for the "@" and the " "

                Debug.WriteLine("Removing space for screen name reply text");
                Debug.WriteLine("Screen name = " + screenName.Length.ToString() + " + 2 for @ and spacing.");
                Debug.WriteLine("Max chars for first tweet = " + maxCharsForFirstTweet.ToString());
            }

        }

        private string GetActualNextChars(int maxCharsPerThreadTweet, int maxCharsForFirstTweet, bool firstTweet, string fullText, string introText, out int indexToEndAt, out bool entirelyIntro)
        {
            try
            {
                //TODO put some specialised logging in here which can be turned on and off from options
                theLog.Log.LevelDown();
                int maxCharsThisTweet = maxCharsPerThreadTweet;

                if (firstTweet)
                {
                    maxCharsThisTweet = maxCharsForFirstTweet;
                }

                // possible for first tweet to have zero characters if the intro and numbering consume
                // all available space.
                string nextMaxChars = string.Empty;
                entirelyIntro = false;
                if (maxCharsThisTweet != 0)
                {
                    if (maxCharsThisTweet >= fullText.Length)
                    {
                        // we can get the whole thing.
                        nextMaxChars = fullText;
                    }
                    else
                    {
                        nextMaxChars = GetNextMaxChars(maxCharsThisTweet, fullText);

                        // test to make sure there is a whole word present so as not to chop 1st word
                        if (!MoreThanOneWord(nextMaxChars))
                        {
                            // if this is the first tweet AND we have an intro,
                            // get nothing and hope that the next tweet can accomodate the text.
                            if (firstTweet && introText.Length > 0)
                            {
                                // we can't get anything meaningful after the intro. Set it to empty.
                                nextMaxChars = string.Empty;
                            }
                            else
                            {
                                // Problem. there are too many characters to fit into one tweet.
                                // Just lop off as much as we CAN do.
                                nextMaxChars = nextMaxChars.Substring(0, maxCharsThisTweet);
                            }
                        }
                    }
                }

                // if we have room for nothing but intro.. it's just intro
                if (nextMaxChars.Length == 0 && firstTweet)
                {
                    entirelyIntro = true;
                }

                // skip past any new lines at the start. Except the first tweet. That might be padding after the intro
                //TODO fix this. Not simple, because if we skip at start we have to extend at the end.
                int indexToStartAt = 0;
                //if (!firstTweet)
                //{
                    //indexToStartAt = IndexOfFirstTextAfterStart(nextMaxChars, Environment.NewLine, 0);
                //}

                // Get index to end at
                indexToEndAt = GetIndexToEndAt(nextMaxChars, maxCharsThisTweet);

                // grab tweet text
                string actualNextChars = nextMaxChars.Substring(indexToStartAt, indexToEndAt);

                return actualNextChars;
            }
            finally { theLog.Log.LevelUp(); }

        }

        /// <summary>
        /// Split a big long string into a queue of Status objects
        /// </summary>
        /// <param name="threadData"></param>
        /// <param name="urlFormat"></param>
        /// <returns></returns>
        public Queue<Status> SplitIntoStatuses(TweetThread threadData, ThreadUrlFormat urlFormat)
        {
            try
            {
                theLog.Log.LevelDown();
                Queue<string> asText = SplitIntoTexts(threadData, urlFormat);
                Queue<Status> asStatus = new Queue<Status>();

                while (asText.Count > 0)
                {
                    string text = asText.Dequeue();
                    Status status = new Status();
                    status.text = text;
                    asStatus.Enqueue(status);
                }

                return asStatus;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// split the text into individual, numbered tweets
        /// </summary>
        /// <param name="fullText"></param>
        /// <returns></returns>
        public Queue<string> SplitIntoTexts(TweetThread threadData, ThreadUrlFormat urlFormat)
        {
            try
            {
                theLog.Log.LevelDown();
                Queue<string> tweets = new Queue<string>();

                // Sanity: get out if there is no text
                if (threadData.IntroText.Trim().Length ==0 && threadData.ThreadText.Trim().Length == 0)
                {
                    Debug.WriteLine("No text");
                    return tweets;
                }

                threadData.Tokenize(m_titterData.TwitterConfiguration.short_url_length, threadData);

                // max chars per THREAD tweet needs to allow for thread count text and other stuff
                int maxCharsPerThreadTweet;
                int maxCharsForFirstTweet;
                GetMaxLengths(threadData, out maxCharsPerThreadTweet, out maxCharsForFirstTweet);

                bool firstTweet = true;

                Queue<string> tweetsWithoutNumbers = new Queue<string>();
                int noOfTweets = 0;

                string fullText = ReturnSanitisedText(threadData.TokenizedThreadText);
                string introText = threadData.TokenizedIntroText;

                // keep going only while there is text left.
                // Or force a run on the first tweet (since intro text needs processing)
                while (fullText.Length > 0 || firstTweet)
                {
                    int indexToEndAt;
                    bool entirelyIntro;
                    string actualNextChars = GetActualNextChars(maxCharsPerThreadTweet, maxCharsForFirstTweet, firstTweet, fullText, introText, out indexToEndAt, out entirelyIntro);

                    // remove tweet text from the original text (INCLUDING newlines at start)
                    fullText = fullText.Substring(indexToEndAt);

                    // sanity: trim it
                    if (firstTweet)
                    {
                        // do not trim text from the start of the first tweet. Might be space after intro.
                        actualNextChars = actualNextChars.TrimEnd();
                    }
                    else
                    {
                        actualNextChars = actualNextChars.Trim();
                    }

                    // only queue it if it is not empty.
                    // OR if it is a special first tweet (entirely intro)
                    if (actualNextChars.Length > 0 || entirelyIntro)
                    {
                        theLog.Log.WriteDebug(string.Format("Next tweet: [{0}]", actualNextChars), LoggingSource.GUI);
                        tweetsWithoutNumbers.Enqueue(actualNextChars);
                        noOfTweets++;
                    }
                    firstTweet = false;

                    // trim any whitespace at the start of the next tweet
                    fullText = fullText.TrimStart();
                }

                tweets = GetNumberedTweets(tweetsWithoutNumbers, noOfTweets, threadData);

                // Tokens are good for measuring string lengths
                // but we don't necessarily want them back on the tweets
                if (urlFormat != ThreadUrlFormat.Tokenized)
                {
                    tweets = ReplaceTokens(threadData, tweets, urlFormat);
                }

                return tweets;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private Queue<string> ReplaceTokens(TweetThread threadData, Queue<string> tweets, ThreadUrlFormat urlFormat)
        {
            Queue<string> newTweets = new Queue<string>();
            while(tweets.Count > 0)
            {
                string tweet = tweets.Dequeue();

                foreach(KeyValuePair<string, SKZTweets.TwitterModels.Url> kvp in threadData.UrlsByToken)
                {
                    string replaceWith;
                    switch(urlFormat)
                    {
                        case ThreadUrlFormat.Display:
                            replaceWith = kvp.Value.display_url;
                            break;

                        case ThreadUrlFormat.Full:
                            replaceWith = kvp.Value.url;
                            break;

                        default:
                            throw new Exception("Url format not supported");
                    }
                    tweet = tweet.Replace(kvp.Key, replaceWith);
                }
                newTweets.Enqueue(tweet);
            }

            return newTweets;
        }

        private bool MoreThanOneWord(string text)
        {
            try
            {
                theLog.Log.LevelDown();
                theLog.Log.WriteDebug(string.Format("Testing text to see if there is more than one word: {0}", text), LoggingSource.DataLayer);

                if (text.IndexOf(" ") > -1)
                {
                    theLog.Log.WriteDebug("Space detected: true", LoggingSource.DataLayer);
                    return true;
                }

                if (text.IndexOf(Environment.NewLine) > -1)
                {
                    theLog.Log.WriteDebug("New line detected: true", LoggingSource.DataLayer);
                    return true;
                }

                theLog.Log.WriteDebug("One word only", LoggingSource.DataLayer);
                return false;
            }
            finally { theLog.Log.LevelUp(); }
        }

        private string GetNextMaxChars(int maxCharsPerThreadTweet, string fullText)
        {
            try
            {
                theLog.Log.LevelUp();

                // grab the next max characters + 1 (it might be a whitespace and thus choppable)
                int lengthToGrab = maxCharsPerThreadTweet + 1;

                // unless there's not that much, then just get as much as we can.
                if (fullText.Length < lengthToGrab)
                {
                    lengthToGrab = fullText.Length;
                }

                // grab text to work with.
                string nextMaxChars = fullText.Substring(0, lengthToGrab);

                theLog.Log.WriteDebug(string.Format("Grabbed next segment: [{0}]", nextMaxChars), LoggingSource.GUI);
                return nextMaxChars;
            }
            finally { theLog.Log.LevelDown(); }
        }

        private int GetIndexToEndAt(string nextMaxChars, int maxCharsPerThreadTweet)
        {
            try
            {
                theLog.Log.LevelDown();

                int indexNewLine = nextMaxChars.IndexOf(Environment.NewLine);

                // If there's a new line, that's our cut-off
                if (indexNewLine > 0)
                {
                    theLog.Log.WriteDebug(string.Format("New line marks next tweet at {0}.", indexNewLine), LoggingSource.GUI);
                    return indexNewLine;
                }
                else
                {
                    // no new line. can we do the whole thing?
                    if (nextMaxChars.Length <= maxCharsPerThreadTweet)
                    {
                        theLog.Log.WriteDebug(string.Format("Only {0} more characters and max is {1}. Using rest of text", nextMaxChars.Length, maxCharsPerThreadTweet), LoggingSource.GUI);
                        return nextMaxChars.Length;
                    }
                    else
                    {
                        // just grab up to the final space.
                        int indexToEndAt = nextMaxChars.LastIndexOf(" ");

                        // if there are no spaces, we have just a LOT of characters.
                        // Grab the maximum
                        if (indexToEndAt < 1)
                        {
                            theLog.Log.WriteDebug(string.Format("No spaces - using maximum of {0} characters", maxCharsPerThreadTweet), LoggingSource.GUI);
                            indexToEndAt = maxCharsPerThreadTweet;
                        }
                        else
                        {
                            theLog.Log.WriteDebug(string.Format("Using up to next space at {0}", indexToEndAt), LoggingSource.GUI);
                        }
                        return indexToEndAt;
                    }
                }
            }
            finally { theLog.Log.LevelUp(); }
        }


        private Queue<string> GetNumberedTweets(Queue<string> tweetsWithoutNumbers, int noOfTweets, TweetThread threadData)
        {
            try
            {
                theLog.Log.LevelDown();

                // If no numbering to be done, we still need to put the intro in place. Cannot get out fast.

                // we now know the number of tweets.
                int count = 1;
                Queue<string> tweets = new Queue<string>();
                bool firstTweet = true;
                while (tweetsWithoutNumbers.Count > 0)
                {
                    string tweet = tweetsWithoutNumbers.Dequeue();

                    if (threadData.NumberSettings.Style != ThreadNumberStyle.NoNumbers)
                    {
                        string numbers = GetFormmattedNumber(count, noOfTweets, threadData.NumberSettings);

                        if (threadData.NumberSettings.Position == ThreadNumberPosition.NumbersAtStart)
                        {
                            tweet = string.Format("{0} {1}", numbers, tweet);
                        }
                        else
                        {
                            tweet = string.Format("{0} {1}", tweet, numbers);
                        }
                    }

                    // if this is the first tweet, it might have intro text. Add it.
                    if(firstTweet)
                    {
                        firstTweet = false;
                        tweet = string.Format("{0}{1}", threadData.TokenizedIntroText, tweet);
                    }

                    tweets.Enqueue(tweet);
                    count++;
                }
                return tweets;
            }
            finally { theLog.Log.LevelUp(); }
        }


        private string GetFormmattedNumber(int tweetNo, int totalTweets, ThreadNumberSettings numberSettings)
        {
            string numbers;
            switch (numberSettings.Style)
            {
                case ThreadNumberStyle.X:
                    numbers = string.Format("{0}", tweetNo);
                    break;
                case ThreadNumberStyle.XofY:
                    numbers = string.Format("{0}/{1}", tweetNo, totalTweets);
                    break;

                case ThreadNumberStyle.NoNumbers:
                    return string.Empty;

                default:
                    throw new Exception("Numbering style not supported");
            }
            return numbers;
        }

        /// <summary>
        /// Find first index of string AFTER start of text.
        /// Repetitions at the start will also be ignored.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ignoreAtStart"></param>
        /// <returns></returns>
        private int IndexOfFirstTextAfterStart(string text, string ignoreAtStart, int startAt)
        {
            try
            {
                theLog.Log.LevelDown();

                theLog.Log.WriteDebug(string.Format("text to analyse: [{0}", text), LoggingSource.GUI);
                theLog.Log.WriteDebug(string.Format("Ignoring at start: [{0}]", ignoreAtStart), LoggingSource.GUI);

                if(startAt < 0)
                {
                    startAt = 0;
                }

                // if text isn't long enough to contain an instance, we're good
                if(text.Length < ignoreAtStart.Length)
                {
                    return 0;
                }

                // If text doesn't occur immediately, we're good.
                if(text.Substring(startAt, ignoreAtStart.Length) != ignoreAtStart)
                {
                    return 0;
                }

                // ignore any items at the start.
                string tempText = text.Substring(startAt);
                while (tempText.Length >= ignoreAtStart.Length && tempText.Substring(0, ignoreAtStart.Length) == ignoreAtStart)
                {
                    tempText = tempText.Substring(ignoreAtStart.Length);
                    theLog.Log.WriteDebug(string.Format("Found text to ignore. Cropping. New Text: [{0}]", tempText), LoggingSource.GUI);
                }

                // now we have trimmed all items from the start. The length of the original minus the new is the length of the repeated strings.
                int endOfRepetitions = text.Length - tempText.Length;

                // return the first index AFTER this.
                int result = text.IndexOf(ignoreAtStart, endOfRepetitions);
                theLog.Log.WriteDebug(string.Format("Result = {0}", result), LoggingSource.GUI);
                return result;
            }
            finally { theLog.Log.LevelUp(); }
        }
    }
}
