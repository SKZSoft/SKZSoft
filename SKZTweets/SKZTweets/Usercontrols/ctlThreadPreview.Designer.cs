﻿namespace SKZSoft.SKZTweets.Usercontrols
{
    partial class ctlThreadPreview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTweets = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlTweets
            // 
            this.pnlTweets.AutoScroll = true;
            this.pnlTweets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTweets.Location = new System.Drawing.Point(0, 0);
            this.pnlTweets.Name = "pnlTweets";
            this.pnlTweets.Size = new System.Drawing.Size(455, 317);
            this.pnlTweets.TabIndex = 0;
            this.pnlTweets.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // ctlThreadPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTweets);
            this.Name = "ctlThreadPreview";
            this.Size = new System.Drawing.Size(455, 317);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTweets;
    }
}
