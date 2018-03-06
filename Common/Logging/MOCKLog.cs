using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    public class MOCKLog : ILog
    {
        public void LevelUp() { }
        public void LevelDown() { }
        public void WriteDebug(string text, LoggingSource source) { }
        public void WriteAPI(string text, LoggingSource source) { }
        public void WriteError(string text, LoggingSource source) { }
        public void WriteWarning(string text, LoggingSource source) { }
        public void WriteMandatory(string text, LoggingSource source) { }
        public void WriteLog(string text, LoggingSource source, LoggingLevel priority) { }
        public string GetSourceAsString(LoggingSource source) { return ""; }
        public void WriteException(Exception ex) { }
        public string GetExceptionText(Exception ex) { return ""; }
        public LogSettings LogSettings { get { return new LogSettings(); } }
        public string LogDirectory { get { return ""; } }
        public string LogPath { get { return ""; } }
        public string UnhandledLogPath { get { return ""; } }
        public LogSettings EditSettings(LogSettings currentSettings) { return new LogSettings(); }

    }
}
