using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    /// <summary>
    /// Logging levels
    /// </summary>
    public enum LoggingLevel
    {
        /// <summary>
        /// Marker for code to be meaningful - used to denote minimum logging level.
        /// Keep as first item minus 1
        /// </summary>
        Minimum = -1,

        /// <summary>
        /// Must go into log, regardless of logging level
        /// </summary>
        Mandatory = 0,

        /// <summary>
        /// Only log errors
        /// </summary>
        Errors = 1,

        /// <summary>
        /// Log warnings and errors
        /// </summary>
        Warnings = 10,

        /// <summary>
        /// Log warnings and API calls
        /// </summary>
        APICalls = 20,

        /// <summary>
        /// Log everything
        /// </summary>
        Debug = 30,

        /// <summary>
        /// Marker for code to be meaningful - used to denote maximum logging level.
        /// Keep as last item plus one
        /// </summary>
        Maximum = 31
    }
}
