using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    /// <summary>
    /// Quick and easy global access to the instance of the logging object
    /// </summary>
    public static class Logger
    {
        private static ILog m_Log;

        /// <summary>
        /// Must call this method before using class
        /// </summary>
        public static void Initialise(LogSettings settings)
        {
            m_Log = new Logging.Log(settings);
        }

        public static void InitialiseMOCK()
        {
            m_Log = new MOCKLog(); 
        }

        /// <summary>
        /// Return the logging object to us for logging
        /// </summary>
        public static ILog Log {  get { return m_Log;  } }
    }
}
