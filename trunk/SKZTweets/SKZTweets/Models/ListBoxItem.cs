using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.SKZTweets.Models
{
    /// <summary>
    /// Simple wrapper for listbox items
    /// </summary>
    public class ListBoxItem
    {
        /// <summary>
        /// The underlying object
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// The job (if any) that this item refers to
        /// </summary>
        public SKZSoft.Twitter.TwitterJobs.Job Job { get; set; }

        /// <summary>
        /// Text to display
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Return display text for listbox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayText;
        }
    }
}
