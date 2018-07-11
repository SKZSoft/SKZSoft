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

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFonts.SelectedIndex > -1)
            {
                string fontName = cmbFonts.Items[cmbFonts.SelectedIndex].ToString();
                FontFamily font = m_fonts[fontName];
                txtText.Font = new Font(font, 10);
            }
        }
    }
}
