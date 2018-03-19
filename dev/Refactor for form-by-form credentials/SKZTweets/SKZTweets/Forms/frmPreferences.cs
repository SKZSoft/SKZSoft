using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZSoft.SKZTweets
{
    public partial class frmPreferences : Form
    {
        private List<IPreferenceControl> m_childControls = new List<IPreferenceControl>();

        public frmPreferences()
        {
            InitializeComponent();

            // Add all child controls to the list for group operations
            m_childControls.Add(ctlPrefsSchedules);
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            this.Icon = Utils.GetIconFromBitmap(Properties.Resources.Settings_64x);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // check for dirty data and warn
            if (IsDirty)
            {
                if (!Utils.SKZConfirmationMessageBox(Strings.DiscardChanges))
                {
                    // cancel closing the form
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
        }

        private bool IsDirty
        {
            get
            {
                // check all child controls for dirty flags
                foreach(IPreferenceControl control in m_childControls)
                {
                    if (control.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
