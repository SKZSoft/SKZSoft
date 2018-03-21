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
        public frmSelectAccount()
        {
            InitializeComponent();
        }

        public Credentials SelectAccount(List<TwitterAccount> availableAccounts)
        {

            PopulateList(availableAccounts);
            this.ShowDialog();


            if (lstAccounts.SelectedIndex < 0)
            {
                return null;
            }

            TwitterAccount account = (TwitterAccount)lstAccounts.SelectedItem;
            Credentials cred = new Credentials(account.OAuthToken, account.OAuthTokenSecret, account.Screenname, account.AccountId);
            return cred;
        }

        private void PopulateList(List<TwitterAccount> accounts)
        {
            lstAccounts.Items.Clear();
            foreach(TwitterAccount acc in accounts)
            {
                lstAccounts.Items.Add(acc);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(lstAccounts.SelectedIndex < 0)
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
    }
}
