using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZBanners
{
    public partial class frmMain : Form
    {
        private Dictionary<string, FontFamily> m_fonts;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadFonts();
            picBackColor.BackColor = Color.AliceBlue;
            picForeColor.BackColor = Color.Blue;

            RefreshPreview();
        }

        private void LoadFonts()
        {
            m_fonts = new Dictionary<string, FontFamily>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                m_fonts.Add(font.Name, font);
            }

            int defaultIndex = -1;
            foreach(KeyValuePair<string, FontFamily> kvp in m_fonts)
            {
                cmbFonts.Items.Add(kvp.Value.Name);
                if(kvp.Value.Name == "Comic Sans MS")
                {
                    defaultIndex = cmbFonts.Items.Count - 1;
                }
            }

            cmbFonts.SelectedIndex = defaultIndex;


        }

        private void RefreshPreview()
        {
            string fontName = GetFontName();
            if (cmbFonts.SelectedIndex > -1)
            {
                FontFamily font = m_fonts[fontName];
                txtText.Font = new Font(font, GetFontSize());
            }

            picPreview.Refresh();
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private string GetFontName()
        {
            string fontName = cmbFonts.Items[cmbFonts.SelectedIndex].ToString();
            return fontName;
        }

        private int GetFontSize()
        {
            // TODO - allow user to change this
            return 10;
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {

            using (SolidBrush myBrush = new SolidBrush(picBackColor.BackColor))
            {
                e.Graphics.FillRectangle(myBrush, new Rectangle(0, 0, picPreview.Width, picPreview.Height));
            }

            string fontName = GetFontName();
            using (Font myFont = new Font(fontName, GetFontSize()))
            {
                using (SolidBrush brush = new SolidBrush(picForeColor.BackColor))
                {
                    e.Graphics.DrawString(txtText.Text, myFont, brush, new Point(2, 2));
                }
            }
        }
    }
}
