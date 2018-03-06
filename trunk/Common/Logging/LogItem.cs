using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging
{
    internal class LogItem
    {
        public LogItem(string text, LoggingLevel level, LoggingSource source, int threadId, string methodPath)
        {
            Text = text;
            Level = level;
            Source = source;
            MethodPath = methodPath;
        }

        public int ThreadId { get; set; }
        public string Text { get; set; }
        public LoggingSource Source { get; set; }
        public LoggingLevel Level { get; set; }
        public string MethodPath { get; set; }
        public bool ForcedWrite { get; set; }
    }
}
