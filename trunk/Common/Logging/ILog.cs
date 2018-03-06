using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    public interface ILog
    {
        void LevelUp();
        void LevelDown();
        void WriteDebug(string text, LoggingSource source);
        void WriteAPI(string text, LoggingSource source);
        void WriteError(string text, LoggingSource source);
        void WriteWarning(string text, LoggingSource source);
        void WriteMandatory(string text, LoggingSource source);
        void WriteLog(string text, LoggingSource source, LoggingLevel priority);
        string GetSourceAsString(LoggingSource source);
        void WriteException(Exception ex);
        string GetExceptionText(Exception ex);
        LogSettings LogSettings { get; }
        string LogDirectory { get; }
        string LogPath { get; }
        string UnhandledLogPath { get; }
    }
}
