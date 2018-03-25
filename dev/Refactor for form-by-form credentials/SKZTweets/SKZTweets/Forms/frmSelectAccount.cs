using SKZSoft.SKZTweets.Controllers;
using SKZSoft.SKZTweets.DataBase;
using SKZSoft.SKZTweets.DataModels;
using SKZSoft.Twitter.TwitterModels;
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
    public partial class frmSelectAccount : Form
    {
        AppController m_mainController;
        Persistence m_persistence;

        public frmSelectAccount()
        {
            InitializeComponent();
        }

        public TwitterAccount SelectAccount(List<TwitterAccount> availableAccounts, AppController mainController, Persistence persistence)
        {
            m_mainController = mainController;
            m_persistence = persistence;
            PopulateList(availableAccounts);
            this.ShowDialog();


            if (lstAccounts.SelectedIndex < 0)
            {
                return null;
            }

            TwitterAccount account = (TwitterAccount)lstAccounts.SelectedItem;
            return account;
        }

        private void PopulateList(List<TwitterAccount> accounts)
        {
            lstAccounts.Items.Clear();
            foreach (TwitterAccount acc in accounts)
            {
                lstAccounts.Items.Add(acc);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void DoSelect()
        {
            if (lstAccounts.SelectedIndex < 0)
            {
                Utils.SKZMessageBox(Strings.PleaseSelectAnAccount, MessageBoxIcon.Exclamation);
                return;
            }
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lstAccounts.SelectedIndex = -1;
            this.Hide();
        }

        private void lstAccounts_DoubleClick(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            frmAuthorise authoriseNewAccount = new frmAuthorise(new Credentials("", "", "", 0), m_mainController);
            Credentials cred = authoriseNewAccount.AuthoriseTwitter();

            if (cred == null)
            {
                return;
            }


            TwitterAccount accountData = new TwitterAccount(cred.AccountId, cred.ScreenName, cred.AuthToken, cred.AuthTokenSecret, cred.BackColor, cred.ForeColor);
            TwitterAccount savedAccount = m_persistence.TwitterAccountAddOrUpdate(accountData);

            if (lstAccounts.Items.Contains(savedAccount))
            {
                lstAccounts.Items.Remove(savedAccount);
            }

            lstAccounts.Items.Add(savedAccount);
            lstAccounts.SelectedItem = savedAccount;
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedIndex < 0)
            {
                return;
            }

            TwitterAccount account = (TwitterAccount)lstAccounts.SelectedItem;
            lblAccountName.Text = account.ScreenName;
            lblAccountName.BackColor = account.BackColor;
            lblAccountName.ForeColor = account.ForeColor;
        }
    }
}