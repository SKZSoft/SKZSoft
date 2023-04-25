using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterData.Exceptions;
using SKZSoft.Twitter.TwitterJobs;
using System.Windows.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZTweets_tiny_harness
{
    public class Utils
    {
        /// <summary>
        /// Application's standard messagebox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        public static void SKZMessageBox(string text, System.Windows.Forms.MessageBoxIcon icon)
        {
            theLog.Log.WriteDebug("Displaying messagebox: " + text, Logging.LoggingSource.GUI);
            System.Windows.Forms.MessageBox.Show(text, "SKZ Tiny harness", System.Windows.Forms.MessageBoxButtons.OK, icon);
        }


        public static void SKZMessageBoxErrors(List<string> errors, System.Windows.Forms.MessageBoxIcon icon)
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendLine(Strings.Problems);
            foreach (string item in errors)
            {
                sb.AppendLine(item);
            }
            string text = sb.ToString();
            theLog.Log.WriteDebug("Displaying messagebox: " + text, Logging.LoggingSource.GUI);

            SKZMessageBox(text, icon);
        }

        public static void HandleException(Exception ex)
        {
            HandleException(ex, true, null);
        }

        public static void HandleException(Exception ex, bool showDialog)
        {
            HandleException(ex, showDialog, null);
        }

        public static void HandleJobException(Exception ex, Job job)
        {
            HandleException(ex, true, job);
        }

        public static string GetExceptionMessage(Exception ex, Job job)
        {
            // Add job description if it exists
            StringBuilder sb = new StringBuilder(1000);
            if (job != null)
            {
                sb.AppendFormat(Strings.JobException, job.JobDescription);
                sb.AppendLine();
            }

            // These exceptions have a meaningful error message - append and return
            if (ex is TwitterException)
            {
                sb.Append(ex.Message);
                return sb.ToString();
            }

            // Twitter exception = append and return
            if (ex is TwitterGenericException)
            {
                TwitterGenericException exCast = (TwitterGenericException)ex;
                string error = string.Format(Strings.TwitterErrorGeneric, exCast.FirstTwitterError.code, exCast.FirstTwitterError.message);
                sb.Append(error);
                return sb.ToString();
            }

            // No idea what this is. Format it as nicely as we can.
            string exceptionText = theLog.Log.GetExceptionText(ex);
            string dialogText = string.Format(Strings.ErrorDialogBody, exceptionText);
            return dialogText;
        }

        public static void HandleException(Exception ex, bool showDialog, Job job)
        {
            // write it to the log
            theLog.Log.WriteException(ex);

            if (showDialog)
            {
                DisplayException(ex, job);
            }
        }

        public static void DisplayException(Exception ex, Job job)
        {
            string message = GetExceptionMessage(ex, job);
            SKZMessageBox(message, MessageBoxIcon.Error);
        }

        public static void HandleAPIError(Job job, int errorNumber, string errorText)
        {
            string message = string.Format(Strings.TwitterError, job.JobDescription, errorNumber, errorText);
        }


    }
}
