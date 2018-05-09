using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZStrips
{
    public partial class frmMain : Form
    {
        private Controller m_controller;
        public frmMain()
        {
            InitializeComponent();
        }

        public void Initialise(Controller controller)
        {
            m_controller = controller;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            foreach(ImagePalette palette in m_controller.ImagePalettes)
            {
                palette.Show();
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            foreach (ImagePalette palette in m_controller.ImagePalettes)
            {
                palette.BringToFront();
            }

        }
    }
}
