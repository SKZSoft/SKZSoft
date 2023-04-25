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
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            // Ensure splash logo and form size match.
            // save manually changing design-tim size if logo size changes.
            // Sheer laziness in advance.
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            this.Width = pictureBox1.Width;
            this.Height = pictureBox1.Height + lblProgress.Height;
            lblProgress.Top = pictureBox1.Height;

            lblProgress.Text = string.Empty;
        }

        public void SetStatus(string text)
        {
            lblProgress.Text = text;
            lblProgress.Refresh();
        }
    }
}
