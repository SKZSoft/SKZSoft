using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData.Exceptions;
using SKZSoft.Twitter.TwitterJobs;
using SKZSoft.Common.Queueing;

namespace SKZSoft.SKZTweets
{
    public static class Utils
    {

        /// <summary>
        /// Application's standard messagebox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        public static void SKZMessageBox(string text, System.Windows.Forms.MessageBoxIcon icon)
        {
            theLog.Log.WriteDebug("Displaying messagebox: " + text, Logging.LoggingSource.GUI);
            System.Windows.Forms.MessageBox.Show(text, Strings.AppName, System.Windows.Forms.MessageBoxButtons.OK, icon);
        }


        public static void SKZMessageBoxErrors(List<string> errors, System.Windows.Forms.MessageBoxIcon icon)
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendLine(Strings.Problems);
            foreach(string item in errors)
            {
                sb.AppendLine(item);
            }
            string text = sb.ToString();
            theLog.Log.WriteDebug("Displaying messagebox: " + text, Logging.LoggingSource.GUI);

            SKZMessageBox(text, icon);
        }

        /// <summary>
        /// Application's standard confirmation doalog
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool SKZConfirmationMessageBox(string text)
        {
            theLog.Log.WriteDebug("Displaying confirmation: " + text, Logging.LoggingSource.GUI);
            DialogResult result = MessageBox.Show(text, Strings.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            theLog.Log.WriteDebug(string.Format("Result = {0}", result), Logging.LoggingSource.GUI);
            return (result == DialogResult.OK);
        }


        private static FormCloseAction SKZConfirmWindowClose(Form form)
        {
            try
            {
                theLog.Log.LevelDown();
                form.Activate();
                Forms.frmCloseWindowsConfirm frm = new Forms.frmCloseWindowsConfirm();
                FormCloseAction action = frm.GetConfirmation();
                theLog.Log.WriteDebug(string.Format("Result = {0}", action), Logging.LoggingSource.GUI);
                return action;
            }
            finally { theLog.Log.LevelUp(); }
        }


        public static FormCloseAction QueryFormClose(SafeForm form, Controllers.AppController mainController)
        {
            try
            {
                Form iForm = (Form)form;

                // Fast out: pre-confirmed by another form
                if (mainController.AllFormsMayClose)
                {
                    return FormCloseAction.CloseOK;
                }

                // If form is busy or dirty, check with user
                if (form.Busy || form.Dirty)
                {
                    // this would interrupt an action
                    iForm.Show();
                    FormCloseAction action = Utils.SKZConfirmWindowClose(iForm);
                    if (action == FormCloseAction.CloseAllWindows)
                    {
                        // flag to other forms that they too may close
                        mainController.AllFormsMayClose = true;

                        // return an OK code (for simplicity, so calling code only handles one case)
                        return FormCloseAction.CloseOK;
                    }
                    return action;
                }
                return FormCloseAction.CloseOK;
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Get an icon froma bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Icon GetIconFromBitmap(Bitmap bitmap)
        {
            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            return icon;
        }

        /// <summary>
        /// Validate that an integer is a vliad number of minutes
        /// </summary>
        /// <param name="text"></param>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        public static bool ValidateMinutes(string text, out string validationMessage)
        {
            int numericMinutes;
            if (!int.TryParse(text, out numericMinutes))
            {
                validationMessage = Strings.MinutesMustBeNumeric;
                return false;
            }

            if(numericMinutes < 0)
            {
                validationMessage = Strings.MinutesOutOfRange;
                return false;
            }

            validationMessage = string.Empty;
            return true;
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

        /// <summary>
        /// Format datetime in a universal manner for the app.
        /// At present, just a shell method. Use it everywhere, and if it ever changes,
        /// it'll be a doddle to just change this and have all dates change.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime value)
        {
            return value.ToString();
        }


        public static void ShowBadQueueResult(QueueResult qr)
        {
            switch(qr)
            {
                case QueueResult.OK:
                    return;

                case QueueResult.DuplicateJob:
                    Utils.SKZMessageBox(Strings.QueueManagerDuplicate, MessageBoxIcon.Stop);
                    break;

                case QueueResult.ManagerTerminating:
                    Utils.SKZMessageBox(Strings.QueueManagerTerminating, MessageBoxIcon.Error);
                    break;

                default:
                    throw new ArgumentException("Queue status not recognised");
            }
        }

    }
}
