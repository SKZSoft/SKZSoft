using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Settings
    {
        /// <summary>
        /// Constructor with no functionality
        /// </summary>
        public Settings()
        {

        }

        /// <summary>
        /// Populating constructor
        /// </summary>
        /// <param name="level"></param>
        /// <param name="deleteAfterDays"></param>
        public Settings(LoggingLevel level, int deleteAfterDays)
        {
            Level = level;
            DeleteAfterDays = deleteAfterDays;
        }

        /// <summary>
        /// The logging level
        /// </summary>
        public LoggingLevel Level { get; set; }


        /// <summary>
        /// How many days logs should be left before deletion
        /// </summary>
        public int DeleteAfterDays { get; set; }

        /// <summary>
        /// Full path to log file
        /// </summary>
        public string LogFileName { get; set; }

        /// <summary>
        /// Full path to log file to which the app will dump unhandled log messages
        /// </summary>
        public string UnhandledLogFileName { get; set; }

        /// <summary>
        /// Extension of log file
        /// </summary>
        public string LogFileExtension { get; set; }

        /// <summary>
        /// Extension of unhandled log file
        /// </summary>
        public string UnhandledFileExtension { get; set; }

        /// <summary>
        /// The dispay-name of the application using this logging libaray (for dialogs)
        /// </summary>
        public string AppName { get; set; }


        internal string LogFullPath { get; set; }
        internal string UnhandledLogFullPath { get; set; }
    }
}
