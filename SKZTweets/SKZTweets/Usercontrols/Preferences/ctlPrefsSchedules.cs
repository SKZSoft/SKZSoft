using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.SKZTweets.Data_Classes.Preferences;

namespace SKZSoft.SKZTweets.Usercontrols.Preferences
{
    public partial class ctlPrefsSchedules : UserControl, IPreferenceControl
    {
        private bool m_dirty = false;

        public ctlPrefsSchedules()
        {
            InitializeComponent();
        }

        bool IPreferenceControl.Validate(List<string> errors, bool setFocusToFirstError)
        {
            bool valid = true;      // optimism
            string error;

            // validate minutes
            if (!Utils.ValidateMinutes(txtMinutes.Text, out error))
            {
                errors.Add(error);
                valid = false;
            }

            return valid;
        }

        bool IPreferenceControl.IsDirty
        {
            get { return m_dirty; }
        }

        void IPreferenceControl.ReadPreferences(SKZTweets.Data_Classes.Preferences.Preferences prefs)
        {
            dtpStartTime.Value = prefs.Schedule.StartTime;
            txtMinutes.Text = prefs.Schedule.Interval.ToString();
        }

        void IPreferenceControl.WritePreferences(SKZTweets.Data_Classes.Preferences.Preferences prefs)
        {

        }


        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            m_dirty = true;
        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {
            m_dirty = true;
        }
    }
}
