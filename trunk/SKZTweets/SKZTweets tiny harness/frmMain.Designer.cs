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
            this.txtTweet = new System.Windows.Forms.TextBox();
            this.btnPostStatus = new System.Windows.Forms.Button();
            this.lstProgress = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestsRunAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTweet
            // 
            this.txtTweet.Location = new System.Drawing.Point(13, 75);
            this.txtTweet.Margin = new System.Windows.Forms.Padding(4);
            this.txtTweet.Multiline = true;
            this.txtTweet.Name = "txtTweet";
            this.txtTweet.Size = new System.Drawing.Size(263, 105);
            this.txtTweet.TabIndex = 1;
            // 
            // btnPostStatus
            // 
            this.btnPostStatus.Location = new System.Drawing.Point(288, 128);
            this.btnPostStatus.Margin = new System.Windows.Forms.Padding(4);
            this.btnPostStatus.Name = "btnPostStatus";
            this.btnPostStatus.Size = new System.Drawing.Size(100, 28);
            this.btnPostStatus.TabIndex = 2;
            this.btnPostStatus.Text = "&Post status";
            this.btnPostStatus.UseVisualStyleBackColor = true;
            this.btnPostStatus.Click += new System.EventHandler(this.btnPostStatus_Click);
            // 
            // lstProgress
            // 
            this.lstProgress.FormattingEnabled = true;
            this.lstProgress.ItemHeight = 16;
            this.lstProgress.Location = new System.Drawing.Point(13, 188);
            this.lstProgress.Margin = new System.Windows.Forms.Padding(4);
            this.lstProgress.Name = "lstProgress";
            this.lstProgress.Size = new System.Drawing.Size(263, 116);
            this.lstProgress.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(479, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "mnuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(108, 26);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTestsRunAll});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.testsToolStripMenuItem.Text = "&Tests";
            // 
            // mnuTestsRunAll
            // 
            this.mnuTestsRunAll.Name = "mnuTestsRunAll";
            this.mnuTestsRunAll.Size = new System.Drawing.Size(129, 26);
            this.mnuTestsRunAll.Text = "Run &all";
            this.mnuTestsRunAll.Click += new System.EventHandler(this.mnuTestsRunAll_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 354);
            this.Controls.Add(this.lstProgress);
            this.Controls.Add(this.btnPostStatus);
            this.Controls.Add(this.txtTweet);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "SKZ Tweets tiny harness";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTweet;
        private System.Windows.Forms.Button btnPostStatus;
        private System.Windows.Forms.ListBox lstProgress;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTestsRunAll;
    }
}

