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
    }
}
