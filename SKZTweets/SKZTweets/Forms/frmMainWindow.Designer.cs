﻿namespace SKZSoft.SKZTweets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainWindow));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRetweeter = new System.Windows.Forms.ToolStripButton();
            this.tsbThreadCreator = new System.Windows.Forms.ToolStripButton();
            this.tsbFollowerMaint = new System.Windows.Forms.ToolStripButton();
            this.menuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tweetsToolStripMenuItem,
            this.schedulesToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1118, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // tweetsToolStripMenuItem
            // 
            this.tweetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retweeterToolStripMenuItem,
            this.threadCreatorToolStripMenuItem});
            this.tweetsToolStripMenuItem.Name = "tweetsToolStripMenuItem";
            this.tweetsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.tweetsToolStripMenuItem.Text = "&Tweets";
            // 
            // retweeterToolStripMenuItem
            // 
            this.retweeterToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.retweet;
            this.retweeterToolStripMenuItem.Name = "retweeterToolStripMenuItem";
            this.retweeterToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.retweeterToolStripMenuItem.Text = "&Retweeter";
            this.retweeterToolStripMenuItem.Click += new System.EventHandler(this.retweeterToolStripMenuItem_Click);
            // 
            // threadCreatorToolStripMenuItem
            // 
            this.threadCreatorToolStripMenuItem.Image = global::SKZSoft.SKZTweets.Properties.Resources.DocumentOutline_16x;
            this.threadCreatorToolStripMenuItem.Name = "threadCreatorToolStripMenuItem";
            this.threadCreatorToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.threadCreatorToolStripMenuItem.Text = "&Thread Creator";
            this.threadCreatorToolStripMenuItem.Click += new System.EventHandler(this.threadCreatorToolStripMenuItem_Click);
            // 
            // schedulesToolStripMenuItem
            // 
            this.schedulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem});
            this.schedulesToolStripMenuItem.Enabled = false;
            this.schedulesToolStripMenuItem.Name = "schedulesToolStripMenuItem";
            this.schedulesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.schedulesToolStripMenuItem.Text = "&Schedules";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
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
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Enabled = false;
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
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
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.logsToolStripMenuItem.Text = "&Logs";
            // 
            // loggingSettingsTtoolStripMenuItem1
            // 
            this.loggingSettingsTtoolStripMenuItem1.Name = "loggingSettingsTtoolStripMenuItem1";
            this.loggingSettingsTtoolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.loggingSettingsTtoolStripMenuItem1.Text = "Logging &settings";
            this.loggingSettingsTtoolStripMenuItem1.Click += new System.EventHandler(this.loggingSettingsTtoolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(203, 6);
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openLogFileToolStripMenuItem.Text = "Open &Log file";
            this.openLogFileToolStripMenuItem.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // openUnhandledLogFileToolStripMenuItem
            // 
            this.openUnhandledLogFileToolStripMenuItem.Name = "openUnhandledLogFileToolStripMenuItem";
            this.openUnhandledLogFileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openUnhandledLogFileToolStripMenuItem.Text = "Open &Unhandled Log file";
            this.openUnhandledLogFileToolStripMenuItem.Click += new System.EventHandler(this.openUnhandledLogFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(203, 6);
            // 
            // openLogDirectoryToolStripMenuItem
            // 
            this.openLogDirectoryToolStripMenuItem.Name = "openLogDirectoryToolStripMenuItem";
            this.openLogDirectoryToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openLogDirectoryToolStripMenuItem.Text = "Open Log &Directory";
            this.openLogDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openLogDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(170, 6);
            // 
            // viewChangelogToolStripMenuItem
            // 
            this.viewChangelogToolStripMenuItem.Name = "viewChangelogToolStripMenuItem";
            this.viewChangelogToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewChangelogToolStripMenuItem.Text = "View &Changelog";
            this.viewChangelogToolStripMenuItem.Click += new System.EventHandler(this.viewChangelogToolStripMenuItem_Click);
            // 
            // viewKnownIssuesToolStripMenuItem
            // 
            this.viewKnownIssuesToolStripMenuItem.Name = "viewKnownIssuesToolStripMenuItem";
            this.viewKnownIssuesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewKnownIssuesToolStripMenuItem.Text = "View &Known Issues";
            this.viewKnownIssuesToolStripMenuItem.Click += new System.EventHandler(this.viewKnownIssuesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscTwitterAccount,
            this.toolStripSeparator2,
            this.tsbRetweeter,
            this.tsbThreadCreator,
            this.tsbFollowerMaint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1118, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscTwitterAccount
            // 
            this.tscTwitterAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscTwitterAccount.Name = "tscTwitterAccount";
            this.tscTwitterAccount.Size = new System.Drawing.Size(121, 25);
            this.tscTwitterAccount.Sorted = true;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRetweeter
            // 
            this.tsbRetweeter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRetweeter.Image = global::SKZSoft.SKZTweets.Properties.Resources.retweet;
            this.tsbRetweeter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetweeter.Name = "tsbRetweeter";
            this.tsbRetweeter.Size = new System.Drawing.Size(23, 22);
            this.tsbRetweeter.Text = "Retweeter";
            this.tsbRetweeter.Click += new System.EventHandler(this.tsbRetweeter_Click);
            // 
            // tsbThreadCreator
            // 
            this.tsbThreadCreator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbThreadCreator.Image = global::SKZSoft.SKZTweets.Properties.Resources.DocumentOutline_16x;
            this.tsbThreadCreator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbThreadCreator.Name = "tsbThreadCreator";
            this.tsbThreadCreator.Size = new System.Drawing.Size(23, 22);
            this.tsbThreadCreator.Text = "Thread creator";
            this.tsbThreadCreator.Click += new System.EventHandler(this.tsbThreadCreator_Click);
            // 
            // tsbFollowerMaint
            // 
            this.tsbFollowerMaint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFollowerMaint.Image = ((System.Drawing.Image)(resources.GetObject("tsbFollowerMaint.Image")));
            this.tsbFollowerMaint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFollowerMaint.Name = "tsbFollowerMaint";
            this.tsbFollowerMaint.Size = new System.Drawing.Size(23, 22);
            this.tsbFollowerMaint.Text = "toolStripButton1";
            this.tsbFollowerMaint.Click += new System.EventHandler(this.tsbFollowerMaint_Click);
            // 
            // frmMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 452);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
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
        private System.Windows.Forms.ToolStripMenuItem tweetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retweeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem viewChangelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewKnownIssuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbFollowerMaint;
        private System.Windows.Forms.ToolStripComboBox tscTwitterAccount;
    }
}