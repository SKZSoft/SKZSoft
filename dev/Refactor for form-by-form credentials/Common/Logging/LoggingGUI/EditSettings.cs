using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Logging.GUI
{
    public class EditSettings
    {

        /// <summary>
        /// Display the settings.
        /// </summary>
        /// <param name="originalSettings"></param>
        /// <returns>New settings or NULL if cancelled</returns>
        public LogSettings ShowForm(LogSettings originalSettings)
        {
            frmSettings form = new frmSettings();
            LogSettings newSettings = form.EditSettings(originalSettings);
            return newSettings;
        }

    }
}
