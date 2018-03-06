using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterData.Exceptions
{
    /// <summary>
    /// A job as part of a batch tried to get data from the previous job, but the previous job was of an unexpected type
    /// </summary>
    public class UnexpectedJobException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        public UnexpectedJobException(string text) : base(text) { }
    }
}
