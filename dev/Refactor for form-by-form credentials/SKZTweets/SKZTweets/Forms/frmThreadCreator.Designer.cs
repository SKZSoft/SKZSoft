namespace SKZSoft.SKZTweets
{
    partial class frmThreadCreator
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblImageDropper = new System.Windows.Forms.Label();
            this.btnClearTweet = new System.Windows.Forms.Button();
            this.ctlTweetText = new SKZSoft.SKZTweets.Usercontrols.ctlTweetText();
            this.btnSelectTweet = new System.Windows.Forms.Button();
            this.ctlTweetNumbering = new SKZSoft.SKZTweets.Usercontrols.ctlTweetNumbering();
            this.label6 = new System.Windows.Forms.Label();
            this.sp1MainArea_Progress = new SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber();
            this.sp2MainLeft_Right = new SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber();
            this.chkStartNewLineAfterIntro = new System.Windows.Forms.CheckBox();
            this.txtSecondsBetweenTweets = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIntro = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.btnTweetThread = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctlThreadPreview = new SKZSoft.SKZTweets.Usercontrols.ctlThreadPreview();
            this.lblTweetCountStatus = new System.Windows.Forms.Label();
            this.btnDeleteTweets = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lstTweets = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp1MainArea_Progress)).BeginInit();
            this.sp1MainArea_Progress.Panel1.SuspendLayout();
            this.sp1MainArea_Progress.Panel2.SuspendLayout();
            this.sp1MainArea_Progress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp2MainLeft_Right)).BeginInit();
            this.sp2MainLeft_Right.Panel1.SuspendLayout();
            this.sp2MainLeft_Right.Panel2.SuspendLayout();
            this.sp2MainLeft_Right.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblImageDropper);
            this.panel1.Controls.Add(this.btnClearTweet);
            this.panel1.Controls.Add(this.ctlTweetText);
            this.panel1.Controls.Add(this.btnSelectTweet);
            this.panel1.Controls.Add(this.ctlTweetNumbering);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 86);
            this.panel1.TabIndex = 23;
            // 
            // lblImageDropper
            // 
            this.lblImageDropper.AllowDrop = true;
            this.lblImageDropper.AutoSize = true;
            this.lblImageDropper.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblImageDropper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImageDropper.Location = new System.Drawing.Point(493, 40);
            this.lblImageDropper.Name = "lblImageDropper";
            this.lblImageDropper.Size = new System.Drawing.Size(221, 15);
            this.lblImageDropper.TabIndex = 31;
            this.lblImageDropper.Text = "Drop pictures here to create a picture thread.";
            this.lblImageDropper.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblImageDropper_DragDrop);
            this.lblImageDropper.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblImageDropper_DragEnter);
            // 
            // btnClearTweet
            // 
            this.btnClearTweet.Location = new System.Drawing.Point(413, 33);
            this.btnClearTweet.Name = "btnClearTweet";
            this.btnClearTweet.Size = new System.Drawing.Size(33, 20);
            this.btnClearTweet.TabIndex = 28;
            this.btnClearTweet.Text = "X";
            this.toolTip1.SetToolTip(this.btnClearTweet, "Clear any selected tweet");
            this.btnClearTweet.UseVisualStyleBackColor = true;
            this.btnClearTweet.Click += new System.EventHandler(this.btnClearTweet_Click);
            // 
            // ctlTweetText
            // 
            this.ctlTweetText.Location = new System.Drawing.Point(82, 7);
            this.ctlTweetText.Name = "ctlTweetText";
            this.ctlTweetText.Size = new System.Drawing.Size(325, 73);
            this.ctlTweetText.Status = null;
            this.ctlTweetText.TabIndex = 27;
            this.toolTip1.SetToolTip(this.ctlTweetText, "The tweet being replied to (if this is blank then \r\nthe thread is not a reply but" +
        " stand-alone).");
            // 
            // btnSelectTweet
            // 
            this.btnSelectTweet.Location = new System.Drawing.Point(413, 7);
            this.btnSelectTweet.Name = "btnSelectTweet";
            this.btnSelectTweet.Size = new System.Drawing.Size(33, 20);
            this.btnSelectTweet.TabIndex = 25;
            this.btnSelectTweet.Text = "...";
            this.toolTip1.SetToolTip(this.btnSelectTweet, "Search for a tweet to reply to");
            this.btnSelectTweet.UseVisualStyleBackColor = true;
            this.btnSelectTweet.Click += new System.EventHandler(this.btnSelectTweet_Click);
            // 
            // ctlTweetNumbering
            // 
            this.ctlTweetNumbering.Location = new System.Drawing.Point(458, 4);
            this.ctlTweetNumbering.Name = "ctlTweetNumbering";
            this.ctlTweetNumbering.Size = new System.Drawing.Size(372, 26);
            this.ctlTweetNumbering.TabIndex = 0;
            this.ctlTweetNumbering.SettingsChanged += new System.EventHandler<SKZSoft.SKZTweets.NumberingChangedArgs>(this.ctlTweetNumbering_SettingsChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "In Reply To";
            // 
            // sp1MainArea_Progress
            // 
            this.sp1MainArea_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp1MainArea_Progress.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp1MainArea_Progress.GrabberDots = false;
            this.sp1MainArea_Progress.GrabberLine = true;
            this.sp1MainArea_Progress.Location = new System.Drawing.Point(0, 86);
            this.sp1MainArea_Progress.Name = "sp1MainArea_Progress";
            this.sp1MainArea_Progress.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp1MainArea_Progress.Panel1
            // 
            this.sp1MainArea_Progress.Panel1.Controls.Add(this.sp2MainLeft_Right);
            this.sp1MainArea_Progress.Panel1MinSize = 200;
            // 
            // sp1MainArea_Progress.Panel2
            // 
            this.sp1MainArea_Progress.Panel2.Controls.Add(this.btnDeleteTweets);
            this.sp1MainArea_Progress.Panel2.Controls.Add(this.btnCancel);
            this.sp1MainArea_Progress.Panel2.Controls.Add(this.lblProgress);
            this.sp1MainArea_Progress.Panel2.Controls.Add(this.lstTweets);
            this.sp1MainArea_Progress.Panel2MinSize = 100;
            this.sp1MainArea_Progress.Size = new System.Drawing.Size(892, 406);
            this.sp1MainArea_Progress.SplitterDistance = 250;
            this.sp1MainArea_Progress.TabIndex = 24;
            // 
            // sp2MainLeft_Right
            // 
            this.sp2MainLeft_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp2MainLeft_Right.GrabberDots = false;
            this.sp2MainLeft_Right.GrabberLine = true;
            this.sp2MainLeft_Right.Location = new System.Drawing.Point(0, 0);
            this.sp2MainLeft_Right.Name = "sp2MainLeft_Right";
            // 
            // sp2MainLeft_Right.Panel1
            // 
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.chkStartNewLineAfterIntro);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.txtSecondsBetweenTweets);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.label2);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.txtIntro);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.label5);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.txtMain);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.btnTweetThread);
            this.sp2MainLeft_Right.Panel1.Controls.Add(this.label1);
            // 
            // sp2MainLeft_Right.Panel2
            // 
            this.sp2MainLeft_Right.Panel2.Controls.Add(this.panel2);
            this.sp2MainLeft_Right.Panel2.Controls.Add(this.lblTweetCountStatus);
            this.sp2MainLeft_Right.Size = new System.Drawing.Size(892, 250);
            this.sp2MainLeft_Right.SplitterDistance = 475;
            this.sp2MainLeft_Right.TabIndex = 1;
            // 
            // chkStartNewLineAfterIntro
            // 
            this.chkStartNewLineAfterIntro.AutoSize = true;
            this.chkStartNewLineAfterIntro.Checked = true;
            this.chkStartNewLineAfterIntro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartNewLineAfterIntro.Location = new System.Drawing.Point(82, 72);
            this.chkStartNewLineAfterIntro.Name = "chkStartNewLineAfterIntro";
            this.chkStartNewLineAfterIntro.Size = new System.Drawing.Size(137, 17);
            this.chkStartNewLineAfterIntro.TabIndex = 2;
            this.chkStartNewLineAfterIntro.Text = "Start new line after intro";
            this.toolTip1.SetToolTip(this.chkStartNewLineAfterIntro, "If checked, a new line will be forced after any intro text");
            this.chkStartNewLineAfterIntro.UseVisualStyleBackColor = true;
            this.chkStartNewLineAfterIntro.CheckedChanged += new System.EventHandler(this.chkStartNewLineAfterIntro_CheckedChanged);
            // 
            // txtSecondsBetweenTweets
            // 
            this.txtSecondsBetweenTweets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSecondsBetweenTweets.Location = new System.Drawing.Point(184, 226);
            this.txtSecondsBetweenTweets.Name = "txtSecondsBetweenTweets";
            this.txtSecondsBetweenTweets.Size = new System.Drawing.Size(27, 20);
            this.txtSecondsBetweenTweets.TabIndex = 5;
            this.txtSecondsBetweenTweets.Text = "100";
            this.toolTip1.SetToolTip(this.txtSecondsBetweenTweets, "The number of seconds to pause between each tweet in a thread.\r\nSending too many " +
        "tweets too quickly may result in Twitter throttling your feed.");
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "seconds between tweets";
            // 
            // txtIntro
            // 
            this.txtIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIntro.Location = new System.Drawing.Point(82, 4);
            this.txtIntro.MaxLength = 140;
            this.txtIntro.Multiline = true;
            this.txtIntro.Name = "txtIntro";
            this.txtIntro.Size = new System.Drawing.Size(390, 62);
            this.txtIntro.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtIntro, "Text that you wish to appear before the first number");
            this.txtIntro.TextChanged += new System.EventHandler(this.txtIntro_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Intro text";
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.Location = new System.Drawing.Point(82, 95);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(390, 123);
            this.txtMain.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtMain, "This is the text which will be turned into threaded tweets");
            this.txtMain.TextChanged += new System.EventHandler(this.txtMain_TextChanged);
            // 
            // btnTweetThread
            // 
            this.btnTweetThread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTweetThread.Location = new System.Drawing.Point(82, 224);
            this.btnTweetThread.Name = "btnTweetThread";
            this.btnTweetThread.Size = new System.Drawing.Size(96, 23);
            this.btnTweetThread.TabIndex = 4;
            this.btnTweetThread.Text = "&Tweet thread";
            this.toolTip1.SetToolTip(this.btnTweetThread, "Send the thread to Twitter");
            this.btnTweetThread.UseVisualStyleBackColor = true;
            this.btnTweetThread.Click += new System.EventHandler(this.btnTweetThread_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Main Text";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.ctlThreadPreview);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 212);
            this.panel2.TabIndex = 17;
            // 
            // ctlThreadPreview
            // 
            this.ctlThreadPreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ctlThreadPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlThreadPreview.Location = new System.Drawing.Point(0, 0);
            this.ctlThreadPreview.Name = "ctlThreadPreview";
            this.ctlThreadPreview.Size = new System.Drawing.Size(395, 212);
            this.ctlThreadPreview.TabIndex = 10;
            this.ctlThreadPreview.TabStop = false;
            this.toolTip1.SetToolTip(this.ctlThreadPreview, "This area shows a preview of what your thread will look like.\r\nDrag images here t" +
        "o attach them to specific tweets.");
            // 
            // lblTweetCountStatus
            // 
            this.lblTweetCountStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTweetCountStatus.AutoSize = true;
            this.lblTweetCountStatus.Location = new System.Drawing.Point(3, 224);
            this.lblTweetCountStatus.Name = "lblTweetCountStatus";
            this.lblTweetCountStatus.Size = new System.Drawing.Size(98, 13);
            this.lblTweetCountStatus.TabIndex = 16;
            this.lblTweetCountStatus.Text = "Tweet count status";
            // 
            // btnDeleteTweets
            // 
            this.btnDeleteTweets.Enabled = false;
            this.btnDeleteTweets.Location = new System.Drawing.Point(669, 126);
            this.btnDeleteTweets.Name = "btnDeleteTweets";
            this.btnDeleteTweets.Size = new System.Drawing.Size(89, 23);
            this.btnDeleteTweets.TabIndex = 7;
            this.btnDeleteTweets.Text = "&Delete tweets";
            this.toolTip1.SetToolTip(this.btnDeleteTweets, "Delete the tweets which were previously created as part of a thread.");
            this.btnDeleteTweets.UseVisualStyleBackColor = true;
            this.btnDeleteTweets.Click += new System.EventHandler(this.btnDeleteTweets_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(764, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel the thread creation. Tweets which have already been created will not be de" +
        "leted.");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(3, 126);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(48, 13);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "Progress";
            // 
            // lstTweets
            // 
            this.lstTweets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTweets.FormattingEnabled = true;
            this.lstTweets.Location = new System.Drawing.Point(3, 2);
            this.lstTweets.Name = "lstTweets";
            this.lstTweets.Size = new System.Drawing.Size(886, 121);
            this.lstTweets.TabIndex = 6;
            // 
            // frmThreadCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 514);
            this.Controls.Add(this.sp1MainArea_Progress);
            this.Controls.Add(this.panel1);
            this.Name = "frmThreadCreator";
            this.Text = "Thread Creator";
            this.Load += new System.EventHandler(this.frmThreadCreator_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.sp1MainArea_Progress, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.sp1MainArea_Progress.Panel1.ResumeLayout(false);
            this.sp1MainArea_Progress.Panel2.ResumeLayout(false);
            this.sp1MainArea_Progress.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp1MainArea_Progress)).EndInit();
            this.sp1MainArea_Progress.ResumeLayout(false);
            this.sp2MainLeft_Right.Panel1.ResumeLayout(false);
            this.sp2MainLeft_Right.Panel1.PerformLayout();
            this.sp2MainLeft_Right.Panel2.ResumeLayout(false);
            this.sp2MainLeft_Right.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp2MainLeft_Right)).EndInit();
            this.sp2MainLeft_Right.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Usercontrols.ctlTweetNumbering ctlTweetNumbering;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIntro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTweetCountStatus;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTweetThread;
        private SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber sp1MainArea_Progress;
        private SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber sp2MainLeft_Right;
        private System.Windows.Forms.ListBox lstTweets;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteTweets;
        private System.Windows.Forms.TextBox txtSecondsBetweenTweets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Usercontrols.ctlThreadPreview ctlThreadPreview;
        private System.Windows.Forms.CheckBox chkStartNewLineAfterIntro;
        private System.Windows.Forms.Button btnSelectTweet;
        private Usercontrols.ctlTweetText ctlTweetText;
        private System.Windows.Forms.Button btnClearTweet;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label lblImageDropper;
    }
}