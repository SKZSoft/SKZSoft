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

            // this property is not exposed to the IDE. Nobody knows why.
            pictureBox1.AllowDrop = true;
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            foreach (ImagePalette palette in m_controller.ImagePalettes)
            {
                palette.BringToFront();
            }

        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            // TODO checks
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            // TODO handle bitmaps too.
            // even if the images are cast to the parent interface on the drag, they come out here as their specific types.
            Models.PaletteImageVector image = (Models.PaletteImageVector)e.Data.GetData(typeof (Models.PaletteImageVector));

            // get mouse position
            Point mousePos = pictureBox1.PointToClient(new Point(e.X, e.Y));
            int x = mousePos.X;
            int y = mousePos.Y;

            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            image.RenderToBitmap(bitmap, x, y, image.DefaultHeight, image.DefaultWidth);
            pictureBox1.Image = bitmap;


        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
