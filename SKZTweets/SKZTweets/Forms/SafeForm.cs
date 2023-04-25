using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZSoft.SKZTweets.Controllers;
using SKZSoft.SKZTweets.Interfaces;
using Logging = SKZSoft.Common.Logging;
using theLog = SKZSoft.Common.Logging.Logger;
using SKZSoft.Twitter.TwitterData;
using SKZSoft.Twitter.TwitterModels;
using SKZSoft.SKZTweets.DataModels;

namespace SKZSoft.SKZTweets
{
    /// <summary>
    /// Form from which other forms inherit.
    /// Implements the functionality to allow Dirty, Busy, and prompting user to close without saving changes
    /// </summary>
    public partial class SafeForm : Form
    {
        protected AppController m_mainController;
        protected TwitterAccount m_twitterAccount;
        protected ToolStripComboBox m_cmbTwitterAccounts;

        /// <summary>
        /// Constructor which exists only to ensure that forms can open in the IDE
        /// </summary>
        public SafeForm() : this(new List<TwitterAccount>(), new TwitterAccount(0, "DEV_MODE", "", "", Color.AliceBlue, Color.Black), null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SafeForm(List<TwitterAccount> twitterAccounts, TwitterAccount selectedAccount, AppController mainController)
        {
            InitializeComponent();
            m_mainController = mainController;

            // Buttons are not available in the IDE, for some reason.
            // Adding in manually.
            ToolStripButton btnChangeAccount = new ToolStripButton();
            btnChangeAccount.Text="Change account";
            m_cmbTwitterAccounts = new ToolStripComboBox();
            statusStrip.Items.Add(btnChangeAccount);
            btnChangeAccount.Click += BtnChangeAccount_Click;

            UpdateTwitterAccount(selectedAccount);
        }

        private void BtnChangeAccount_Click(object sender, EventArgs e)
        {
            frmSelectAccount accountSelect = new frmSelectAccount();
            List<TwitterAccount> accountsAvailable = m_mainController.Persistence.TwitterAccountGetAllAvailable();
            TwitterAccount newAccount = accountSelect.SelectAccount(accountsAvailable, m_mainController);

            if(newAccount==null)
            {
                return;
            }

            UpdateTwitterAccount(newAccount);
        }

        private void UpdateTwitterAccount(TwitterAccount selectedAccount)
        {
            tsScreenName.Text = selectedAccount.ScreenName;
            tsScreenName.BackColor = selectedAccount.BackColor;
            tsScreenName.ForeColor = selectedAccount.ForeColor;
            m_twitterAccount = selectedAccount;
        }

        /// <summary>
        /// Set to TRUE if the user has made changes which should flag a warning
        /// </summary>
        public virtual bool Dirty { get { return false; } }

        /// <summary>
        /// Set to TRUE if the form is currently doing something
        /// </summary>
        public virtual bool Busy { get { return false; } }


        /// <summary>
        /// Handle all actions allowing form to terminate.
        /// Asks user if they wish to close, close all, or cancel whatever operation started the query
        /// </summary>
        /// <returns></returns>
        public FormCloseAction QueryTerminate()
        {
            return Utils.QueryFormClose(this, m_mainController);
        }


        /// <summary>
        /// Force this form to close, no questions asked.
        /// </summary>
        public void ForceClose()
        {
            this.Close();
        }


        private void SafeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                theLog.Log.LevelDown();

                // Tidy up nicely. Child forms override Terminate.
                Terminate();
            }
            finally { theLog.Log.LevelUp(); }
        }

        /// <summary>
        /// Clean up the form.
        /// Inheriting forms should override this.
        /// This form should ideally be abstract but that causes errors in the IDE.
        /// </summary>
        public virtual void Terminate() { }

        /// <summary>
        /// Change the credentials used by this form
        /// </summary>
        /// <param name="selectedAccount"/>
        public void ChangeAccount(TwitterAccount selectedAccount)
        {
            m_twitterAccount = selectedAccount;
            if (selectedAccount != null)
            {
                //tsslScreenName.Text = credentials.ScreenName;
            }
        }

        public ToolStrip StatusStrip {  get { return statusStrip; } }

    }
}
