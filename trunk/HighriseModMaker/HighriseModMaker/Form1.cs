namespace HighriseModMaker
{
    public partial class Form1 : Form
    {

        private const int RoomWidth = 100;
        private const int RoomHeight = 300;
        private Image modImage;
        private Color backGroundColor;
        private Color borderColor = Color.FromArgb(81, 79, 72);
        private int sizePerecent = 7;
        private int resizedWidth = 0;
        private int resizedHeight = 0;
        private bool changing = false;

        float xPosition = 0;
        float yPosition = 0;

        public Form1()
        {
            InitializeComponent();
            txtSize.Text = sizePerecent.ToString();  
            LoadNewImage("C:\\Users\\Desktop\\Desktop\\tmp\\00160006.jpg");
            backGroundColor = Color.White;
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            CheckDrag(e);
        }


        private static void CheckDrag(DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                effects = DragDropEffects.Copy;
            }
            e.Effect = effects;
        }


        private void Form1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void picPicToUse_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            LoadNewImage(path);
        }

        private void LoadNewImage(string path)
        {
            try
            {
                changing = true;
                picPicToUse.Load(path);
                picPicToUse.SizeMode = PictureBoxSizeMode.StretchImage;


                if (modImage != null)
                {
                    modImage.Dispose();
                }

                modImage = new Bitmap(path, false);

                DrawMod();
                vScrollBar1.Height = picMod.Height;
                vScrollBar1.Value = 0;
                vScrollBar1.Maximum = picMod.Height;
                vScrollBar1.Minimum = -picMod.Height;


                hScrollBar1.Width = picMod.Width;
                hScrollBar1.Value = 0;
                hScrollBar1.Maximum = picMod.Width;
                hScrollBar1.Minimum = -picMod.Width;
            }
            finally { changing = false; }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picPicToUse.AllowDrop = true;
            DrawMod();
        }


        private void DrawMod()
        {
            int height = GetTextBoxValue(txtHeight, 1);
            resizedHeight = height * RoomHeight;

            int width = GetTextBoxValue(txtWidth, 1);
            resizedWidth = width * RoomWidth;



            picMod.Width = resizedWidth;
            picMod.Height = resizedHeight;

            picMod.Refresh();

        }

        private int GetTextBoxValue(TextBox txt, int defaultNumber)
        {
            string n = txt.Text;
            int value = defaultNumber;
            int.TryParse(n, out value);
            return value;
        }



        private void picMod_Click(object sender, EventArgs e)
        {

        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            if (changing)
                return;
            try
            {
                changing = true;
                DrawMod();
            }
            finally { changing = false; }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (changing)
                return;
            try
            {
                changing = true;
                DrawMod();
            }
            finally { changing = false; }
        }

        private void picMod_Paint(object sender, PaintEventArgs e)
        {
            xPosition = GetTextBoxValue(txtX, 0);
            yPosition = GetTextBoxValue(txtY, 0);


            if (modImage == null)
            {
                return;
            }
            
            Bitmap bmp = new Bitmap(picMod.Width, picMod.Height);
            using (Graphics g = Graphics.FromImage(bmp))    
            {

                using (Pen p = new Pen(backGroundColor))
                {
                    g.DrawRectangle(p, 0, 0, picMod.Width, picMod.Height);
                }

                float width = (modImage.Width * sizePerecent) / 100;
                float height = (modImage.Height * sizePerecent) / 100;

                g.DrawImage(modImage, xPosition, yPosition, width, height);


                // border
                if(chkBorder.Checked)
                {
                    using (Brush b = new SolidBrush(borderColor))
                    {
                        g.FillRectangle(b, 0, 0, picMod.Width, 2);
                        g.FillRectangle(b, 0, 0, 2, picMod.Height);
                        g.FillRectangle(b, picMod.Width - 4, 0, picMod.Width, picMod.Height);
                        g.FillRectangle(b, 0, picMod.Height -4 , picMod.Width, picMod.Height);

                    }
                }
            }
            picMod.Image = bmp;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (changing)
                return;
            try
            {
                changing = true;
                sizePerecent = GetTextBoxValue(txtSize, 100);
            }
            finally { changing = false; }
        }

        private void txtY_TextChanged(object sender, EventArgs e)
        {
            if (changing)
                return;
            try
            {
                changing = true;
                int y = 0;
                int.TryParse(txtY.Text, out y);
                vScrollBar1.Value = y;
                DrawMod();
            }
            finally { changing = false; }
        }

        private void txtX_TextChanged(object sender, EventArgs e)
        {
            if (changing)
                return;

            try
            {
                changing = true;
                int x = 0;
                int.TryParse(txtX.Text, out x);
                hScrollBar1.Value = x;
                DrawMod();
            }
            finally { changing = false; }

        }

        private void chkBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (changing)
                return;

            try
            {
                changing = true;
                DrawMod();
            }
            finally { changing = false; }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (changing)
                return;
            try
            {
                changing = true;
                txtX.Text = hScrollBar1.Value.ToString();
                DrawMod();
            }
            finally { changing = false; }
        }
    }
}