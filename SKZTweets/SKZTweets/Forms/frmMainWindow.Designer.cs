namespace SKZSoft.SKZTweets
{
    partial class frmMainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectSignIn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.switchAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tweetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retweeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schedulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingSettingsTtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openUnhandledLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.openLogDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.viewChangelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewKnownIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscTwitterAccount = new System.Windows.Forms.ToolStripComboBox();
            this.tsbSignIn = new System.Windows.Forms.ToolStripButton();
            this.tsbSwitchAccount = new System.Windows.Forms.ToolStripButton();
            this.tsbSignout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRetweeter = new System.Windows.Forms.ToolStripButton();
            this.tsbThreadCreator = new System.Windows.Forms.ToolStripButton();
            this.menuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.tweetsToolStripMenuItem,
            this.schedulesToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(1491, 28);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
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
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnectSignIn,
            this.toolStripSeparator1,
            this.switchAccountToolStripMenuItem,
            this.signOutToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.connectToolStripMenuItem.Text = "&Connect";
            // 
            // mnuConnectSignIn
            // 
            this.mnuConnectSignIn.Image = global::SKZSoft.SKZTweets.Properties.Resources.MyWork_24x;
            this.mnuConnectSignIn.Name = "mnuConnectSignIn";
            this.mnuConnectSignIn.Size = new System.Drawing.Size(183, 26);
            this.mnuConnectSignIn.Text = "&Sign in";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // switchAccountToolStripMenuItem
            // 
            this.switchAccountToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.Disconnect_16x;
            this.switchAccountToolStripMenuItem.Name = "switchAccountToolStripMenuItem";
            this.switchAccountToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.switchAccountToolStripMenuItem.Text = "S&witch account";
            this.switchAccountToolStripMenuItem.Click += new System.EventHandler(this.switchAccountToolStripMenuItem_Click);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.UserError_16x;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.signOutToolStripMenuItem.Text = "Sign &Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // tweetsToolStripMenuItem
            // 
            this.tweetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retweeterToolStripMenuItem,
            this.threadCreatorToolStripMenuItem});
            this.tweetsToolStripMenuItem.Name = "tweetsToolStripMenuItem";
            this.tweetsToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.tweetsToolStripMenuItem.Text = "&Tweets";
            // 
            // retweeterToolStripMenuItem
            // 
            this.retweeterToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.retweet;
            this.retweeterToolStripMenuItem.Name = "retweeterToolStripMenuItem";
            this.retweeterToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.retweeterToolStripMenuItem.Text = "&Retweeter";
            this.retweeterToolStripMenuItem.Click += new System.EventHandler(this.retweeterToolStripMenuItem_Click);
            // 
            // threadCreatorToolStripMenuItem
            // 
            this.threadCreatorToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.DocumentOutline_16x;
            this.threadCreatorToolStripMenuItem.Name = "threadCreatorToolStripMenuItem";
            this.threadCreatorToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.threadCreatorToolStripMenuItem.Text = "&Thread Creator";
            this.threadCreatorToolStripMenuItem.Click += new System.EventHandler(this.threadCreatorToolStripMenuItem_Click);
            // 
            // schedulesToolStripMenuItem
            // 
            this.schedulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem});
            this.schedulesToolStripMenuItem.Enabled = false;
            this.schedulesToolStripMenuItem.Name = "schedulesToolStripMenuItem";
            this.schedulesToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            this.schedulesToolStripMenuItem.Text = "&Schedules";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.createNewToolStripMenuItem.Text = "&Create new";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.toolStripSeparator4,
            this.viewChangelogToolStripMenuItem,
            this.viewKnownIssuesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Enabled = false;
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingSettingsTtoolStripMenuItem1,
            this.toolStripSeparator3,
            this.openLogFileToolStripMenuItem,
            this.openUnhandledLogFileToolStripMenuItem,
            this.toolStripSeparator5,
            this.openLogDirectoryToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.logsToolStripMenuItem.Text = "&Logs";
            // 
            // loggingSettingsTtoolStripMenuItem1
            // 
            this.loggingSettingsTtoolStripMenuItem1.Name = "loggingSettingsTtoolStripMenuItem1";
            this.loggingSettingsTtoolStripMenuItem1.Size = new System.Drawing.Size(250, 26);
            this.loggingSettingsTtoolStripMenuItem1.Text = "Logging &settings";
            this.loggingSettingsTtoolStripMenuItem1.Click += new System.EventHandler(this.loggingSettingsTtoolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(247, 6);
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.openLogFileToolStripMenuItem.Text = "Open &Log file";
            this.openLogFileToolStripMenuItem.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // openUnhandledLogFileToolStripMenuItem
            // 
            this.openUnhandledLogFileToolStripMenuItem.Name = "openUnhandledLogFileToolStripMenuItem";
            this.openUnhandledLogFileToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.openUnhandledLogFileToolStripMenuItem.Text = "Open &Unhandled Log file";
            this.openUnhandledLogFileToolStripMenuItem.Click += new System.EventHandler(this.openUnhandledLogFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(247, 6);
            // 
            // openLogDirectoryToolStripMenuItem
            // 
            this.openLogDirectoryToolStripMenuItem.Name = "openLogDirectoryToolStripMenuItem";
            this.openLogDirectoryToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.openLogDirectoryToolStripMenuItem.Text = "Open Log &Directory";
            this.openLogDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openLogDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(204, 6);
            // 
            // viewChangelogToolStripMenuItem
            // 
            this.viewChangelogToolStripMenuItem.Name = "viewChangelogToolStripMenuItem";
            this.viewChangelogToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.viewChangelogToolStripMenuItem.Text = "View &Changelog";
            this.viewChangelogToolStripMenuItem.Click += new System.EventHandler(this.viewChangelogToolStripMenuItem_Click);
            // 
            // viewKnownIssuesToolStripMenuItem
            // 
            this.viewKnownIssuesToolStripMenuItem.Name = "viewKnownIssuesToolStripMenuItem";
            this.viewKnownIssuesToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.viewKnownIssuesToolStripMenuItem.Text = "View &Known Issues";
            this.viewKnownIssuesToolStripMenuItem.Click += new System.EventHandler(this.viewKnownIssuesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscTwitterAccount,
            this.tsbSignIn,
            this.tsbSwitchAccount,
            this.tsbSignout,
            this.toolStripSeparator2,
            this.tsbRetweeter,
            this.tsbThreadCreator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1491, 28);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscTwitterAccount
            // 
            this.tscTwitterAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscTwitterAccount.Name = "tscTwitterAccount";
            this.tscTwitterAccount.Size = new System.Drawing.Size(160, 28);
            this.tscTwitterAccount.Sorted = true;
            // 
            // tsbSignIn
            // 
            this.tsbSignIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSignIn.Image = global::SKZSoft.SKZTweets.Properties.Resources.MyWork_24x;
            this.tsbSignIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSignIn.Name = "tsbSignIn";
            this.tsbSignIn.Size = new System.Drawing.Size(24, 25);
            this.tsbSignIn.Text = "Sign in";
            // 
            // tsbSwitchAccount
            // 
            this.tsbSwitchAccount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSwitchAccount.Image = global::SKZSoft.SKZTweets.Properties.Resources.Disconnect_16x;
            this.tsbSwitchAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwitchAccount.Name = "tsbSwitchAccount";
            this.tsbSwitchAccount.Size = new System.Drawing.Size(24, 25);
            this.tsbSwitchAccount.Text = "toolStripButton2";
            this.tsbSwitchAccount.ToolTipText = "Switch Account";
            this.tsbSwitchAccount.Click += new System.EventHandler(this.tsbSwitchAccount_Click);
            // 
            // tsbSignout
            // 
            this.tsbSignout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSignout.Image = global::SKZSoft.SKZTweets.Properties.Resources.UserError_16x;
            this.tsbSignout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSignout.Name = "tsbSignout";
            this.tsbSignout.Size = new System.Drawing.Size(24, 25);
            this.tsbSignout.ToolTipText = "Sign out";
            this.tsbSignout.Click += new System.EventHandler(this.tsbSignout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbRetweeter
            // 
            this.tsbRetweeter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRetweeter.Image = global::SKZSoft.SKZTweets.Properties.Resources.retweet;
            this.tsbRetweeter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetweeter.Name = "tsbRetweeter";
            this.tsbRetweeter.Size = new System.Drawing.Size(24, 25);
            this.tsbRetweeter.Text = "Retweeter";
            this.tsbRetweeter.Click += new System.EventHandler(this.tsbRetweeter_Click);
            // 
            // tsbThreadCreator
            // 
            this.tsbThreadCreator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbThreadCreator.Image = global::SKZSoft.SKZTweets.Properties.Resources.DocumentOutline_16x;
            this.tsbThreadCreator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbThreadCreator.Name = "tsbThreadCreator";
            this.tsbThreadCreator.Size = new System.Drawing.Size(24, 25);
            this.tsbThreadCreator.Text = "Thread creator";
            this.tsbThreadCreator.Click += new System.EventHandler(this.tsbThreadCreator_Click);
            // 
            // frmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 556);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMainWindow";
            this.Text = "SKZTweets";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainWindow_FormClosed);
            this.Load += new System.EventHandler(this.frmMainWindow_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectSignIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tweetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retweeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSignIn;
        private System.Windows.Forms.ToolStripButton tsbSwitchAccount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRetweeter;
        private System.Windows.Forms.ToolStripButton tsbThreadCreator;
        private System.Windows.Forms.ToolStripMenuItem schedulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingSettingsTtoolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openUnhandledLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem openLogDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbSignout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem viewChangelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewKnownIssuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox tscTwitterAccount;
    }
}