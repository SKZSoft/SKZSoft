namespace SKZTweets_tiny_harness
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogInToTwitter = new System.Windows.Forms.Button();
            this.txtTweet = new System.Windows.Forms.TextBox();
            this.btnPostStatus = new System.Windows.Forms.Button();
            this.lstProgress = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnLogInToTwitter
            // 
            this.btnLogInToTwitter.Location = new System.Drawing.Point(12, 12);
            this.btnLogInToTwitter.Name = "btnLogInToTwitter";
            this.btnLogInToTwitter.Size = new System.Drawing.Size(103, 23);
            this.btnLogInToTwitter.TabIndex = 0;
            this.btnLogInToTwitter.Text = "&Log in to Twitter";
            this.btnLogInToTwitter.UseVisualStyleBackColor = true;
            this.btnLogInToTwitter.Click += new System.EventHandler(this.btnLogInToTwitter_Click);
            // 
            // txtTweet
            // 
            this.txtTweet.Location = new System.Drawing.Point(12, 41);
            this.txtTweet.Multiline = true;
            this.txtTweet.Name = "txtTweet";
            this.txtTweet.Size = new System.Drawing.Size(198, 86);
            this.txtTweet.TabIndex = 1;
            // 
            // btnPostStatus
            // 
            this.btnPostStatus.Enabled = false;
            this.btnPostStatus.Location = new System.Drawing.Point(216, 104);
            this.btnPostStatus.Name = "btnPostStatus";
            this.btnPostStatus.Size = new System.Drawing.Size(75, 23);
            this.btnPostStatus.TabIndex = 2;
            this.btnPostStatus.Text = "&Post status";
            this.btnPostStatus.UseVisualStyleBackColor = true;
            this.btnPostStatus.Click += new System.EventHandler(this.btnPostStatus_Click);
            // 
            // lstProgress
            // 
            this.lstProgress.FormattingEnabled = true;
            this.lstProgress.Location = new System.Drawing.Point(12, 133);
            this.lstProgress.Name = "lstProgress";
            this.lstProgress.Size = new System.Drawing.Size(198, 95);
            this.lstProgress.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 242);
            this.Controls.Add(this.lstProgress);
            this.Controls.Add(this.btnPostStatus);
            this.Controls.Add(this.txtTweet);
            this.Controls.Add(this.btnLogInToTwitter);
            this.Name = "frmMain";
            this.Text = "SKZ Tweets tiny harness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogInToTwitter;
        private System.Windows.Forms.TextBox txtTweet;
        private System.Windows.Forms.Button btnPostStatus;
        private System.Windows.Forms.ListBox lstProgress;
    }
}

