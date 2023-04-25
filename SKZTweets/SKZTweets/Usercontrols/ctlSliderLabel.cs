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
    public partial class ctlSliderLabel : Label
    {
        private Label m_slider;
        private Timer m_timer;

        public ctlSliderLabel()
        {
            InitializeComponent();
            Speed = 10;
        }

        /// <summary>
        /// Scrolling speed (pixels per millisecond)
        /// </summary>
        public int Speed { get; set; }


        /// <summary>
        /// Slide text into label
        /// </summary>
        /// <param name="text">The new label text</param>
        /// <param name="forceSlidIfNoChange">if TRUE, text will slide into view even if no text has changed. If FALSE, unchanged text will not cause a slide.</param>
        public void TextSlide(string text, bool forceSlidIfNoChange)
        {
            if(this.Text == text && !forceSlidIfNoChange)
            {
                // text is the same and we are not forcing a slide.
                // Nothing to do.
                return;
            }

            // tear down any existing stuff
            TearDown();

            // create new slider label
            m_slider = new Label();

            // copy main properties from THIS label so it looks the same
            m_slider.Font = this.Font;
            m_slider.Width = this.Width;
            m_slider.Left = 0;
            m_slider.Padding = this.Padding;
            m_slider.Margin = this.Margin;
            m_slider.Width = this.Width;
            m_slider.BackColor = this.BackColor;
            m_slider.ForeColor = this.ForeColor;

            // set text
            m_slider.Text = text;

            // position it above current text
            m_slider.Top = -this.Height;

            this.Controls.Add(m_slider);
            m_slider.Visible = true;

            // create timer to slide it into view
            m_timer = new Timer();
            m_timer.Interval = Speed;
            m_timer.Tick += m_timer_Tick;
            m_timer.Start();
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            m_slider.Top++;
            if(m_slider.Top==0)
            {
                // done. 

                // Update underling text
                this.Text = m_slider.Text;

                TearDown();
            }
        }

        private void TearDown()
        {
            // Pull everything down
            if (m_timer != null)
            {
                m_timer.Tick -= m_timer_Tick;
                m_timer = null;
            }

            if (m_slider != null)
            {
                this.Controls.Remove(m_slider);
                m_slider = null;
            }

        }
    }
}
