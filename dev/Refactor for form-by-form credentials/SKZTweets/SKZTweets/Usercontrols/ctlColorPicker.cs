using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZSoft.SKZTweets.Usercontrols
{
    public partial class ctlColorPicker : UserControl
    {
        public ctlColorPicker()
        {
            InitializeComponent();
        }

        private void btnSelectBack_Click(object sender, EventArgs e)
        {
            colorDialogBack.Color = lblSample.BackColor;
            if (colorDialogBack.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            lblSample.BackColor = colorDialogBack.Color;
        }

        private void btnSelectFore_Click(object sender, EventArgs e)
        {
            colorDialogFore.Color = lblSample.ForeColor;
            if (colorDialogFore.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            lblSample.ForeColor = colorDialogFore.Color;
        }


        public Color SelectedBackColor {  get { return lblSample.BackColor; } }
        public Color SelectedForeColor {  get { return lblSample.ForeColor; } }
    }
}
