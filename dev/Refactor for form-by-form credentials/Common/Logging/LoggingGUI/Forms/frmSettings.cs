using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.Common.Logging;

namespace SKZSoft.Common.Logging.GUI
{
    internal partial class frmSettings : Form
    {
        private bool m_cancelled = false;

        public frmSettings()
        {
            InitializeComponent();
        }

        public Logging.LogSettings EditSettings(Logging.LogSettings currentSettings)
        {
            ctlLoggingSettings.SetOptions(currentSettings);
            this.ShowDialog();

            if(m_cancelled)
            {
                return null;
            }

            Logging.LogSettings newSettings = ctlLoggingSettings.GetOptions();

            CopyHiddenSettings(currentSettings, newSettings);

            return newSettings;
        }

        /// <summary>
        /// Copy settings which are not displayed on the form.
        /// </summary>
        /// <param name="oldSettings"></param>
        /// <param name="newSettings"></param>
        private void CopyHiddenSettings(Logging.LogSettings oldSettings, Logging.LogSettings newSettings)
        {
            newSettings.LogFileName = oldSettings.LogFileName;
            newSettings.LogFullPath = oldSettings.LogFullPath;
            newSettings.UnhandledLogFileName = oldSettings.UnhandledLogFileName;
            newSettings.UnhandledLogFullPath = oldSettings.UnhandledLogFullPath;
            newSettings.AppName = oldSettings.AppName;
            newSettings.LogFileExtension = oldSettings.LogFileExtension;
            newSettings.UnhandledFileExtension = oldSettings.UnhandledFileExtension;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_cancelled = true;
            this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (!ctlLoggingSettings.ValidateData(errors))
            {
                // validation failed.

                // TODO: make this a reusable GUI object, in its own project, which displays things flexibly and nicely
                StringBuilder sb = new StringBuilder(500);
                sb.AppendLine(Strings.ErrorsFound);
                foreach(string error in errors)
                {
                    sb.AppendLine(error);
                }

                MessageBox.Show(sb.ToString());

                return;
            }

            // validation passed
            m_cancelled = false;
            this.Hide();
        }
    }
}
