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
    public partial class frmScheduleEditor : Form
    {
        public frmScheduleEditor()
        {
            InitializeComponent();
        }

        private void schedule1_RequestResize(object sender, ResizedArgs e)
        {
            // child control needs to be resized
            schedule1.Width = e.NewWidth;
            schedule1.Height = e.NewHeight;
        }

        private void frmScheduleEditor_Load(object sender, EventArgs e)
        {
            schedule1.Initialise();
        }

    }
}
