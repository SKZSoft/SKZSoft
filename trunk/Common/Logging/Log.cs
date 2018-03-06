using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;

namespace SKZSoft.Common.Logging
{
    /// <summary>
    /// Logging class.
    /// </summary>
    public class Log : ILog
    {
        private LogWriter m_logWriter;
        private LogSettings m_settings;
        private string m_logDirectory;

        public Log(LogSettings settings)
        {
            Initialise(settings);
        }

        /// <summary>
        /// Initialise logging
        /// </summary>
        /// <param name="logPath">full path to log file</param>
        /// <param name="unhandledLogPath">full path to file which will contain items which tried (but failed) to get logged in main log</param>
        private void Initialise(LogSettings settings)
        {
            // start with defaults
            m_settings = settings;

            DateTime timestamp = DateTime.UtcNow;
            string fileStamp = timestamp.ToString("yyyy-MM-ddTHHmmss");
            string processId = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

            // convert basic file names to full filenames
            m_logDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + string.Format("\\{0}\\Logs\\", settings.AppName);
            m_settings.LogFullPath = GetFilename(m_logDirectory, settings.LogFileName, fileStamp, settings.LogFileExtension, processId);
            m_settings.UnhandledLogFullPath = GetFilename(m_logDirectory, settings.UnhandledLogFileName, fileStamp, settings.UnhandledFileExtension, processId);

            LoggingConfiguration loggingConfiguration = BuildProgrammaticConfig(settings);
            m_logWriter = new LogWriter(loggingConfiguration);

            WriteMandatory("Log created", Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("Assembley Version: {0}", typeof(Log).Assembly.GetName().Version), Logging.LoggingSource.Boot);
            WriteMandatory("Log settings follow:", Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("App Name: {0}", m_settings.AppName), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("DeleteAfterDays: {0}", m_settings.DeleteAfterDays), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("Level: {0}", m_settings.Level), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("LogFileName: {0}", m_settings.LogFileName), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("LogFileExtension: {0}", m_settings.LogFileExtension), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("UnhandledLogFileName: {0}", m_settings.UnhandledLogFileName), Logging.LoggingSource.Boot);
            WriteMandatory(string.Format("UnhandledFileExtension: {0}", m_settings.UnhandledFileExtension), Logging.LoggingSource.Boot);
            WriteMandatory("-----------------------------------------------", Logging.LoggingSource.Boot);


            DeleteOldLogs();
        }

        private void DeleteOldLogs()
        {
            WriteMandatory(string.Format("Checking for old log files in {0}", m_logDirectory), Logging.LoggingSource.Boot);

            // Get the folder
            DirectoryInfo di = new DirectoryInfo(m_logDirectory);
            DateTime now = DateTime.UtcNow;
            DateTime deleteIfOlderThan = now.AddDays(-m_settings.DeleteAfterDays);
            foreach(FileInfo fi in di.GetFiles())
            {
                if(fi.LastAccessTimeUtc < deleteIfOlderThan)
                {
                    WriteMandatory(string.Format("Deleteing {0}", fi.FullName), Logging.LoggingSource.Boot);
                    fi.Delete();
                }
            }

            WriteMandatory("Deleting old logs complete.", Logging.LoggingSource.Boot);
        }

        private string GetFilename(string folder, string filename, string fileStamp, string extension, string processId)
        {
            string name = string.Format("{0}{1} {2} {4}.{3}", folder, filename, fileStamp, extension, processId);
            return name;
        }

        /// <summary>
        /// Full path to the log file
        /// </summary>
        public string LogPath {  get { return m_settings.LogFullPath; } }

        /// <summary>
        /// Full path to the log file which gets any unhandled stuff dumped to it
        /// </summary>
        public string UnhandledLogPath { get { return m_settings.UnhandledLogFullPath; } }

        private int GetThreadId()
        {
            // get thread ID
            Thread thread = Thread.CurrentThread;
            return thread.ManagedThreadId;
        }

        /// <summary>
        /// Move one level back up the callstack formatting. Call this at the end of a method
        /// </summary>
        public void LevelUp()
        {
            DoWriteLog("Method ends", Logging.LoggingSource.GUI, LoggingLevel.Debug, 2);
            return;
        }

        /// <summary>
        /// Move down a level in the callstack. Call this at the start of a new method.
        /// </summary>
        /// <param name="methodName"></param>
        public void LevelDown()
        {
            DoWriteLog("Method starts", Logging.LoggingSource.GUI, LoggingLevel.Debug, 2);
        }


        private string GetFullPath(int callStackLevel)
        {
            try
            {
                StackTrace st = new StackTrace();
                MethodBase mb = st.GetFrame(callStackLevel + 1).GetMethod();
                MemberInfo rt = mb.ReflectedType;
                string path = string.Format("{0}.{1}.{2}", mb.ReflectedType.Namespace, rt.Name, mb.Name);

                return path;
            }
            catch
            {
                // this is bad.
                return "Could not get callstack";
            }
        }


        private void DoWriteLog(string text, LoggingSource source, LoggingLevel priority, int callStackLevel)
        {
            // get path of whatever called this log action
            string path = GetFullPath(callStackLevel);

            int threadId = GetThreadId();

            // create and save in full stack
            LogItem logItem = new Logging.LogItem(text, priority, source, threadId, path);

            WriteLogItem(logItem);
        }

        private void WriteLogItem(LogItem logItem)
        { 
            // get source enum as a string
            string sourceAsString = GetSourceAsString(logItem.Source);

            // format and write log message
            string logText = string.Format("Thread {0}: {1} {2}", logItem.ThreadId, logItem.MethodPath, logItem.Text);
            m_logWriter.Write(logText, sourceAsString, (int)logItem.Level);
        }


        /// <summary>
        /// Write a line to the log with debug level
        /// </summary>
        /// <param name="text"></param>
        /// <param name="source"></param>
        public void WriteDebug(string text, LoggingSource source)
        {
            DoWriteLog(text, source, LoggingLevel.Debug, 2);
        }


        /// <summary>
        /// Write a line to the log with the API-call level
        /// </summary>
        /// <param name="text"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public void WriteAPI(string text, LoggingSource source)
        {
            DoWriteLog(text, source, LoggingLevel.APICalls, 2);
        }

        /// <summary>
        /// Write a line to the log with error level
        /// </summary>
        /// <param name="text"></param>
        /// <param name="source"></param>
        public void WriteError(string text, LoggingSource source)
        {
            DoWriteLog(text, source, LoggingLevel.Errors, 2);
        }

        /// <summary>
        /// Write a line to the log with warning level
        /// </summary>
        /// <param name="text"></param>
        /// <param name="source"></param>
        public void WriteWarning(string text, LoggingSource source)
        {
            DoWriteLog(text, source, LoggingLevel.Warnings, 2);
        }
        
        /// <summary>
        /// Write a line to the log with mandatory level
        /// </summary>
        /// <param name="text"></param>
        /// <param name="source"></param>
        public void WriteMandatory(string text, LoggingSource source)
        {
            DoWriteLog(text, source, LoggingLevel.Mandatory, 2);
        }

        /// <summary>
        /// Write a line to the log
        /// </summary>
        /// <param name="text"></param>
        /// <param name="source"></param>
        /// <param name="priority"></param>
        public void WriteLog(string text, LoggingSource source, LoggingLevel priority)
        {
            DoWriteLog(text, source, priority, 2);
        }


        /// <summary>
        /// Return text version of the source enumeration, for human-readability
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetSourceAsString(LoggingSource source)
        {
            switch (source)
            {
                case Logging.LoggingSource.Boot:
                    return SourceConsts.SourceBoot;

                case Logging.LoggingSource.GUI:
                    return SourceConsts.SourceGUI;

                case Logging.LoggingSource.API:
                    return SourceConsts.SourceAPI;

                default:
                    return "Unknown source";
            }
        }


        private LoggingConfiguration BuildProgrammaticConfig(LogSettings settings)
        {
            // Build Configuration
            LoggingConfiguration config = new LoggingConfiguration();

            // Formatter
            TextFormatter briefFormatter = new TextFormatter("{timestamp(local)} {category} {severity} {message}");

            // which priority of messages to catch
            PriorityFilter priorityFilter = new PriorityFilter("Priority Filter", (int)LoggingLevel.Minimum, (int)settings.Level);
            config.Filters.Add(priorityFilter);

            // "Enabled" filter
            LogEnabledFilter logEnabledFilter = new LogEnabledFilter("LogEnabled Filter", true);
            config.Filters.Add(logEnabledFilter);

            // Trace Listeners
            FlatFileTraceListener flatFileTraceListener = new FlatFileTraceListener(settings.LogFullPath, string.Empty, string.Empty, briefFormatter);

            config.AddLogSource(SourceConsts.SourceBoot, SourceLevels.All, true).AddTraceListener(flatFileTraceListener);
            config.AddLogSource(SourceConsts.SourceGUI, SourceLevels.All, true).AddTraceListener(flatFileTraceListener);
            config.AddLogSource(SourceConsts.SourceAPI, SourceLevels.All, true).AddTraceListener(flatFileTraceListener);

            // Special Sources Configuration
            FlatFileTraceListener unprocessedFlatFileTraceListener = new FlatFileTraceListener(settings.UnhandledLogFullPath, "----------------------------------------", string.Empty, briefFormatter);
            config.SpecialSources.Unprocessed.AddTraceListener(unprocessedFlatFileTraceListener);
            config.SpecialSources.LoggingErrorsAndWarnings.AddTraceListener(unprocessedFlatFileTraceListener);

            return config;
        }

        /// <summary>
        /// Write details of exception to the log
        /// </summary>
        /// <param name="ex"></param>
        public void WriteException(Exception ex)
        {
            // write out the whole log queue, item by item.
            DoWriteLog("---------------------------------------------------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
            DoWriteLog("----------------------- EXCEPTION LOGGED ----------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
            DoWriteLog("---------------------------------------------------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
            DoWriteException(ex, 0);
            DoWriteLog("---------------------------------------------------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
            DoWriteLog("----------------------- END OF EXCEPTION ----------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
            DoWriteLog("---------------------------------------------------------------", Logging.LoggingSource.Log, LoggingLevel.Mandatory, 2);
        }

        // Handle exception AND inner exceptions
        private void DoWriteException(Exception ex, int level)
        {
            try
            {
                int threadId = GetThreadId();

                // get text for exception
                string exText = GetExceptionText(ex);
                string errorTitle = string.Empty;

                if (level > 0)
                {
                    errorTitle = string.Format("Inner exception level {0}:", level);
                }
                else
                {
                    errorTitle = "Error:";
                }
                string logText = string.Format("{0} {1}", errorTitle, exText);

                // write it
                m_logWriter.Write(logText, Logging.SourceConsts.SourceGUI, (int)LoggingLevel.Errors);

                if (ex.InnerException != null)
                {
                    level++;
                    DoWriteException(ex.InnerException, level);
                }
            }
            catch (Exception ex2)
            {
                // An exception was thrown when trying to log an exception.
                // All bets are off.
                // just throw it. What else can be done?
                throw ex2;
            }
        }


        /// <summary>
        /// Get text for reporting / logging an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string GetExceptionText(Exception ex)
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.Source);
            sb.AppendLine(ex.StackTrace);
            return sb.ToString();
        }


        /// <summary>
        /// The log settings
        /// </summary>
        public LogSettings LogSettings {  get { return m_settings; } }


        /// <summary>
        /// The folder in which logs are stored
        /// </summary>
        public string LogDirectory { get { return m_logDirectory; } } 
    }
}
