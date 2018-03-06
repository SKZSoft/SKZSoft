using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKZTweets.TwitterData.Models;
using SKZTweets.TwitterModels;

namespace SKZTweets.Usercontrols
{
    public partial class ctlTweetText : UserControl
    {
        private Status m_status;

        public ctlTweetText()
        {
            InitializeComponent();
        }


        public Status Status
        {
            get { return m_status; }
            set
            {
                m_status = value;
                if (value == null)
                {
                    Text = string.Empty;
                }
                else
                {
                    Text = m_status.text;
                }
            }
        }

        public override string Text
        {
            set
            {
                txtText.Text = value;
            }
            get
            {
                return txtText.Text;
            }
        }

        /// <summary>
        /// Get height of text currently displayed in textbox
        /// </summary>
        public int TextHeight
        {
            get
            {
                int lines = txtText.GetLineFromCharIndex(txtText.Text.Length - 1)  + 1;
                int fontHeight = TextRenderer.MeasureText("X", txtText.Font).Height;
                int height = lines * fontHeight;
                return height;
            }
        }


    }
}
