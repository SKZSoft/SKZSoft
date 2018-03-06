using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKZTweets.Usercontrols
{
    public partial class ctlHoverLink : Label
    {
        Font m_originalFont;

        public ctlHoverLink()
        {
            InitializeComponent();

            MouseEnter += CtlHoverLink_MouseEnter;
            MouseLeave += CtlHoverLink_MouseLeave;
            Click += CtlHoverLink_Click;
            m_originalFont = this.Font;
            this.Cursor = Cursors.Hand;
        }

        public string URL { get; set; }

        private void CtlHoverLink_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(URL))
            {
                Utils.SKZMessageBox("Error: no link specified for ctlLinkHover", MessageBoxIcon.Error);
                return;
            }
            System.Diagnostics.Process.Start(URL);
        }

        private void CtlHoverLink_MouseLeave(object sender, EventArgs e)
        {
            base.Font = m_originalFont;
        }

        private void CtlHoverLink_MouseEnter(object sender, EventArgs e)
        {
            Font newFont = new Font(this.Font, this.Font.Style | FontStyle.Underline);
            base.Font = newFont;
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;

                // THIS class MUST call base.Font to set the font.
                // Anything that comes through THIS font will cause the original font to be stored.
                // That's what we need to revert to, later
                m_originalFont = value;
            }
        }
    }
}
