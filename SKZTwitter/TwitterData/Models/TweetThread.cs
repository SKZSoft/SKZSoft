using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Twitter.TwitterJobs.Consts;
using SKZSoft.Twitter.TwitterData.Consts;
using SKZSoft.Twitter.TwitterModels;

namespace SKZSoft.Twitter.TwitterData.Models
{
    /// <summary>
    /// A thread
    /// </summary>
    public class TweetThread
    {

        private const string TOKEN_HTTP = "http://";
        private const string TOKEN_HTTPS = "https://";

        private Dictionary<string, SKZSoft.Twitter.TwitterModels.Url> m_urls;
        private Dictionary<string, SKZSoft.Twitter.TwitterModels.Url> m_urlsByToken;

        private string m_tokenizedIntro;
        private string m_tokenizedThreadText;

        /// <summary>
        /// The numbering settings
        /// </summary>
        public ThreadNumberSettings NumberSettings { get; set; }

        /// <summary>
        /// The status being replied to
        /// </summary>
        public Status InReplyTo { get; set; }


        /// <summary>
        /// Text before the thread begins
        /// </summary>
        public string IntroText { get; set; }

        /// <summary>
        /// The text of the thread body
        /// </summary>
        public string ThreadText { get; set; }


        /// <summary>
        /// Prepare tokenized version of the text which can be used to judge tweet length.
        /// Replaces URLs with tokenised versions which can be looked up in the "Tokens" dictionary.
        /// For example: Http://00000000001 (length of URL will match maxCharsPerLink)
        /// </summary>
        /// <param name="maxCharsPerLink"></param>
        public void Tokenize(int maxCharsPerLink, TweetThread threadData)
        {
            try
            {
                theLog.Log.LevelDown();

                // replace URLs with tokens of the correct length
                m_urls = new Dictionary<string, SKZSoft.Twitter.TwitterModels.Url>();
                m_urlsByToken = new Dictionary<string, SKZSoft.Twitter.TwitterModels.Url>();

                m_tokenizedIntro = DoTokenize(IntroText, maxCharsPerLink);
                m_tokenizedThreadText = DoTokenize(ThreadText, maxCharsPerLink);

                // finally, add a newline to the intro if it is mandatory and doesn't already end with one and there IS some intro text
                if(threadData.StartNewLineAfterIntro && m_tokenizedIntro.Length > 0)
                {
                    if(!m_tokenizedIntro.EndsWith(Environment.NewLine))
                    {
                        m_tokenizedIntro += Environment.NewLine;
                    }
                }

            }
            finally { theLog.Log.LevelUp(); }
        }


        private string DoTokenize(string text, int maxCharsPerLink)
        {
            try
            {
                theLog.Log.LevelDown();

                // tokenize "http"
                string tokenized = DoTokenizeType(text, maxCharsPerLink, TOKEN_HTTP);

                // tokenize "https"
                tokenized = DoTokenizeType(text, maxCharsPerLink, TOKEN_HTTPS);

                return tokenized;

            }
            finally { theLog.Log.LevelUp(); }
        }

        private string DoTokenizeType(string text, int maxCharsPerLink, string header)
        {
            int nextIndex = text.IndexOf(header, 0, StringComparison.CurrentCultureIgnoreCase);
            int count = m_urls.Count;
            while (nextIndex > -1)
            {
                // find END of URL
                int endOfUrl = GetEndOfUrl(text, nextIndex);
                int urlLength = endOfUrl - nextIndex;
                string url = text.Substring(nextIndex, urlLength);

                if (!m_urls.ContainsKey(url))
                {
                    count++;
                    int numberLength = maxCharsPerLink - header.Length;
                    string token = header + count.ToString("D" + numberLength.ToString());

                    SKZSoft.Twitter.TwitterModels.Url twitterUrl = new SKZSoft.Twitter.TwitterModels.Url();
                    twitterUrl.display_url = GetDisplayUrl(url, maxCharsPerLink);
                    twitterUrl.url = url;
                    twitterUrl.expanded_url = token;

                    m_urls.Add(url, twitterUrl);
                    m_urlsByToken.Add(token, twitterUrl);
                }
                nextIndex = text.IndexOf(header, endOfUrl, StringComparison.CurrentCultureIgnoreCase);
            }

            // Sort the urls by length (descending).
            // Otherwise, "http://bbc.co.uk" will replace part of (and hence corrupt) "http://bbc.co.uk/news" if it preceeds it in the list.
            List<KeyValuePair<string, SKZSoft.Twitter.TwitterModels.Url>> urlsByLength = m_urls.ToList();
            urlsByLength.Sort((firstPair, nextPair) =>
            {
                return nextPair.Key.Length.CompareTo(firstPair.Key.Length);
            });
            
            // Now replace the URLs, one by one, longest first.
            string newText = text;
            foreach (KeyValuePair<string, SKZSoft.Twitter.TwitterModels.Url> kvp in urlsByLength)
            {
                newText = newText.Replace(kvp.Key, kvp.Value.expanded_url);
            }

            return newText;
        }


        private string GetDisplayUrl(string url, int maxCharsPerLink)
        {
            if(url.Length < maxCharsPerLink)
            {
                return url;
            }

            string shortenedUrl = url.Substring(0, maxCharsPerLink - 3);
            shortenedUrl += "...";
            return shortenedUrl;
        }

        private int GetEndOfUrl(string text, int startIndex)
        {
            int endIndex = startIndex;
            text = text.ToLower();
            // scan for first character which is not valid in a URL
            while (endIndex < text.Length)
            {
                string character = text.Substring(endIndex, 1);

                if (DataConsts.URL_VALID_CHARS.IndexOf(character) == -1)
                {
                    return endIndex;
                }
                endIndex++;
            }

            // have gone past the end of the text
            return text.Length;
        }


        public string TokenizedIntroText {  get { return m_tokenizedIntro; } }
        public string TokenizedThreadText {  get { return m_tokenizedThreadText; } }

        /// <summary>
        /// If TRUE, main body text and numbering will start on a new line after the intro
        /// without the user having to manually end the intro text with a new line.
        /// </summary>
        public bool StartNewLineAfterIntro { get; set; }

        public Dictionary<string, SKZSoft.Twitter.TwitterModels.Url> UrlsByToken {  get { return m_urlsByToken; } }
    }
}
