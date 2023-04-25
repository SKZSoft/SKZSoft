using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets
{
    public class Consts
    {
        /// <summary>
        /// Default number of tweets to fetch when selecting from a timeline
        /// </summary>
        public const int TweetsToFetchForSelection = 100;

        /// <summary>
        /// Minimum wait between tweets.
        /// So that the system does not spam twitter.
        /// </summary>
        public const int DefaultMillisecondsBetweenTweets = 1000;

        /// <summary>
        /// The number of recent tweets to display when picking from a list
        /// </summary>
        public const int RecentTweetsToSelectFrom = 200;

        /// <summary>
        /// Standard space between controls
        /// </summary>
        public const int ControlSpacing = 5;
    }
}
