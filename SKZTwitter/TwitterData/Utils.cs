using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;


namespace SKZSoft.Twitter.TwitterData
{
    public static class Utils
    {
        /// <summary>
        /// Work out how many milliseconds there are between now and next trigger time
        /// </summary>
        /// <param name="nextTrigger"></param>
        /// <returns></returns>
        public static int GetMillisecondsToTrigger(DateTime nextTrigger)
        {
            theLog.Log.LevelDown();

            DateTime now = DateTime.Now;
            theLog.Log.WriteDebug(string.Format("Time now is {0}", now), Logging.LoggingSource.GUI);
            theLog.Log.WriteDebug(string.Format("Next trigger is {0}", nextTrigger), Logging.LoggingSource.GUI);

            double milliseconds = (nextTrigger - now).TotalMilliseconds;

            theLog.Log.WriteDebug(string.Format("Milliseconds to wait = {0}", milliseconds), Logging.LoggingSource.GUI);

            theLog.Log.LevelUp();
            return (int)milliseconds;
        }

    }
}
