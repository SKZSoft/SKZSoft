using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKZSoft.Twitter.TwitterData.Enums;

namespace SKZSoft.Twitter.TwitterData.Models
{
    /// <summary>
    /// Settings for thread numbering
    /// </summary>
    public class ThreadNumberSettings
    {
        /// <summary>
        /// The style of the tweet numbering
        /// </summary>
        public ThreadNumberStyle Style { get; set; }

        /// <summary>
        /// The position of the number
        /// </summary>
        public ThreadNumberPosition Position { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="style"></param>
        /// <param name="position"></param>
        /// <param name="textAfterNumber"></param>
        public ThreadNumberSettings(ThreadNumberStyle style, ThreadNumberPosition position)
        {
            Style = style;
            Position = position;
        }
    }
}
