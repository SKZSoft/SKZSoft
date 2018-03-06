namespace SKZTweets.Usercontrols
{
    partial class TweetDisplay
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
            this.tweetText = new SKZTweets.Usercontrols.ctlTweetText();
            this.lblScreenName = new System.Windows.Forms.Label();
            this.lblName = new SKZTweets.Usercontrols.ctlHoverLink();
            this.lblHowLongAgo = new SKZTweets.Usercontrols.ctlHoverLink();
            this.SuspendLayout();
            // 
            // tweetText
            // 
            this.tweetText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tweetText.Font = new System.Drawing.Font("Cambria Math", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tweetText.Location = new System.Drawing.Point(7, 31);
            this.tweetText.Name = "tweetText";
            this.tweetText.Size = new System.Drawing.Size(499, 149);
            this.tweetText.TabIndex = 3;
            // 
            // lblScreenName
            // 
            this.lblScreenName.AutoSize = true;
            this.lblScreenName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreenName.Location = new System.Drawing.Point(123, 9);
            this.lblScreenName.Name = "lblScreenName";
            this.lblScreenName.Size = new System.Drawing.Size(96, 16);
            this.lblScreenName.TabIndex = 5;
            this.lblScreenName.Text = "lblScreenName";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(115, 19);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "ctlHoverLink1";
            // 
            // lblHowLongAgo
            // 
            this.lblHowLongAgo.AutoSize = true;
            this.lblHowLongAgo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHowLongAgo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHowLongAgo.Location = new System.Drawing.Point(345, 12);
            this.lblHowLongAgo.Name = "lblHowLongAgo";
            this.lblHowLongAgo.Size = new System.Drawing.Size(85, 16);
            this.lblHowLongAgo.TabIndex = 8;
            this.lblHowLongAgo.Text = "ctlHoverLink1";
            this.lblHowLongAgo.URL = null;
            // 
            // TweetDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblHowLongAgo);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblScreenName);
            this.Controls.Add(this.tweetText);
            this.Name = "TweetDisplay";
            this.Size = new System.Drawing.Size(509, 183);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Usercontrols.ctlTweetText tweetText;
        private System.Windows.Forms.Label lblScreenName;
        private ctlHoverLink lblName;
        private ctlHoverLink lblHowLongAgo;
    }
}
