using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterData.Consts
{
    public class DataConsts
    {
        /// <summary>
        /// Maximum characters allowed per tweet
        /// </summary>
        public const int MaxCharsPerTweet = 280;


        public const string ThreadCounterTemplateXofY = "xx/xx";       // allow for two two-digit numbers.
        public const string ThreadCounterTemplateX = "xx";             // allow for one two-digit numbers.

        public const string TextAfterNumbers = " ";                     // Text after the numbers (or before, if numbers at end)

        //public const int THUMBNAIL_WIDTH = 50;
        public const int THUMBNAIL_HEIGHT = 50;

        public const string URL_VALID_CHARS = @"abcdefghijklmnopqrstuvwxyz0123456789://?\%.-=_()~[]@!$&'*+,;";
        public const string URL_USER_TIMELINE = "https://twitter.com/{0}";
        public const string URL_API_AUTHENITCATE = "https://api.twitter.com/oauth/authenticate";

        public const long MAX_BATCH_SIZE_FOLLOWER_IDS = 5000;
    }
}
