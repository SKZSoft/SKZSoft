using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZSoft.SKZTweets.Forms
{
    public partial class frmCloseWindowsConfirm : Form
    {
        private FormCloseAction m_action;

        public frmCloseWindowsConfirm()
        {
            InitializeComponent();
        }

        public FormCloseAction GetConfirmation()
        {
            this.ShowDialog();

            return m_action;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            m_action = FormCloseAction.CloseOK;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            m_action = FormCloseAction.CancelClose;
            this.Close();
        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            m_action = FormCloseAction.CloseAllWindows;
            this.Close();
        }
    }
}
