namespace SKZSoft.SKZTweets.Usercontrols
{
    partial class ctlTweetList
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
            this.lstTweets = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstTweets
            // 
            this.lstTweets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTweets.FormattingEnabled = true;
            this.lstTweets.Location = new System.Drawing.Point(0, 0);
            this.lstTweets.Name = "lstTweets";
            this.lstTweets.Size = new System.Drawing.Size(622, 426);
            this.lstTweets.TabIndex = 1;
            this.lstTweets.SelectedIndexChanged += new System.EventHandler(this.lstTweets_SelectedIndexChanged);
            this.lstTweets.DoubleClick += new System.EventHandler(this.lstTweets_DoubleClick);
            // 
            // ctlTweetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstTweets);
            this.Name = "ctlTweetList";
            this.Size = new System.Drawing.Size(622, 426);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstTweets;
    }
}
