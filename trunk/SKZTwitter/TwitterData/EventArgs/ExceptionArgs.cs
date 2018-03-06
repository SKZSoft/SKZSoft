using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets.TwitterData
{
    /// <summary>
    /// Arguments for reporting exceptions
    /// </summary>
    public class ExceptionArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ex"></param>
        public ExceptionArgs(Exception ex)
        {
            Exception = ex;
        }

        /// <summary>
        /// The exception which was raised
        /// </summary>
        public Exception Exception { get; set; }
    }
}
