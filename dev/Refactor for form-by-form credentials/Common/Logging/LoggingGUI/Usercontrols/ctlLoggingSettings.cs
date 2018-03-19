using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Common.Logging;

namespace SKZSoft.Common.Logging.GUI
{
    public partial class ctlLoggingSettings : UserControl
    {
        public ctlLoggingSettings()
        {
            InitializeComponent();
        }

        internal void SetOptions(LogSettings settings)
        {
            // set level option button
            switch (settings.Level)
            {
                case LoggingLevel.Debug:
                    optDebug.Checked = true;
                    break;

                case LoggingLevel.Errors:
                    optErrorsOnly.Checked = true;
                    break;

                case LoggingLevel.APICalls:
                    optAPIActions.Checked = true;
                    break;

                case LoggingLevel.Warnings:
                    optWarningsAndErrors.Checked = true;
                    break;

                default:
                    theLog.Log.WriteWarning(string.Format("Level {0} not found", settings.Level), Logging.LoggingSource.Log);
                    break;
            }

            txtDeleteAfter.Text = settings.DeleteAfterDays.ToString();
        }

        internal LogSettings GetOptions()
        {
            try
            {
                theLog.Log.LevelDown();
                LogSettings settings = new LogSettings();

                settings.Level = GetLevel();

                // assume number is valid because .validated method ought to have been called first and handled by the GUI
                settings.DeleteAfterDays = int.Parse(txtDeleteAfter.Text);

                return settings;
            }
            finally { theLog.Log.LevelUp(); }

        }

        public bool ValidateData(List<string> errors)
        {
            bool errorsFound = false;

            if (GetLevel() == LoggingLevel.Maximum)
            {
                errors.Add(Strings.NoLevel);
                errorsFound = true;
            }

            int tmp;
            if(!int.TryParse(txtDeleteAfter.Text, out tmp))
            {
                errors.Add(Strings.NoOfDaysValidate);
                errorsFound = true;
            }

            return (!errorsFound);
        }


        private Logging.LoggingLevel GetLevel()
        {
            try
            {
                theLog.Log.LevelUp();
                if (optDebug.Checked)
                {
                    return Logging.LoggingLevel.Debug;
                }
                else
                {
                    if (optErrorsOnly.Checked)
                    {
                        return Logging.LoggingLevel.Errors;
                    }
                    else
                    {
                        if (optAPIActions.Checked)
                        {
                            return Logging.LoggingLevel.APICalls;
                        }
                        else
                        {
                            if (optWarningsAndErrors.Checked)
                            {
                                return Logging.LoggingLevel.Warnings;
                            }
                            else
                            {
                                theLog.Log.WriteError("No level checkboxes checked. Defaulting to maximum", Logging.LoggingSource.Log);
                                return Logging.LoggingLevel.Maximum;
                            }
                        }
                    }
                }
            }
            finally { theLog.Log.LevelUp();  }

        }
    }
}