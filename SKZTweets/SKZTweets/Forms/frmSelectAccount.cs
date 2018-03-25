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
        private bool m_ok = false;


        public frmSelectAccount()
        {
            InitializeComponent();
        }

        public TwitterAccount SelectAccount(List<TwitterAccount> availableAccounts, AppController mainController)
        {
            m_mainController = mainController;
            PopulateList(availableAccounts);
            this.ShowDialog();


            if (!m_ok)
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

            m_ok = true;
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
            TwitterAccount newAccount = authoriseNewAccount.AuthoriseTwitter();

            if (newAccount== null)
            {
                return;
            }

            TwitterAccount accountData = m_mainController.Persistence.TwitterAccountGetById(newAccount.AccountId);
            TwitterAccount savedAccount;
            if (accountData == null)
            {
                // Account doesn't exist - add it
                savedAccount = m_mainController.Persistence.TwitterAccountAdd(newAccount);
            }
            else
            {
                // Account exists. Copy over new data (screen name, color etc may have changed)
                accountData.BackColor = newAccount.BackColor;
                accountData.ForeColor = newAccount.ForeColor;
                accountData.ScreenName = newAccount.ScreenName;

                savedAccount = m_mainController.Persistence.TwitterAccountUpdate(accountData);
            }

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