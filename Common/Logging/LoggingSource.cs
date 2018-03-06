using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    /// <summary>
    /// Sources of logs
    /// </summary>
    public enum LoggingSource
    {
        /// <summary>
        /// Boot process
        /// </summary>
        Boot,

        /// <summary>
        /// GUI actions
        /// </summary>
        GUI,

        /// <summary>
        /// API calls
        /// </summary>
        API,

        /// <summary>
        /// Data layer for Data Layer
        /// </summary>
        DataLayer,

        /// <summary>
        /// Messages from the log itself
        /// </summary>
        Log
        
    }

}
