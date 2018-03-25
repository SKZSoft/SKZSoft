namespace SKZSoft.SKZTweets
{
    partial class frmRetweeter
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
            this.btnRTNow = new System.Windows.Forms.Button();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRefreshCounts = new System.Windows.Forms.Button();
            this.tweetDisplay = new SKZSoft.SKZTweets.Usercontrols.TweetDisplay();
            this.ctlScheduleBasic1 = new SKZSoft.SKZTweets.Usercontrols.ctlScheduleBasic();
            this.btnSelectTweet = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.chkUpdateCountsAutomatically = new System.Windows.Forms.CheckBox();
            this.txtUpdateInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRTNow
            // 
            this.btnRTNow.Image = global::SKZSoft.SKZTweets.Properties.Resources.retweet16;
            this.btnRTNow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRTNow.Location = new System.Drawing.Point(284, 12);
            this.btnRTNow.Name = "btnRTNow";
            this.btnRTNow.Size = new System.Drawing.Size(88, 23);
            this.btnRTNow.TabIndex = 3;
            this.btnRTNow.Text = "&RT now";
            this.toolTip1.SetToolTip(this.btnRTNow, "Retweet now");
            this.btnRTNow.UseVisualStyleBackColor = true;
            this.btnRTNow.Click += new System.EventHandler(this.btnRTNow_Click);
            // 
            // lstHistory
            // 
            this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.Location = new System.Drawing.Point(12, 305);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(527, 108);
            this.lstHistory.TabIndex = 7;
            this.toolTip1.SetToolTip(this.lstHistory, "The history of retweets is shown here");
            this.lstHistory.DoubleClick += new System.EventHandler(this.lstHistory_DoubleClick);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::SKZSoft.SKZTweets.Properties.Resources.Stop_16x;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(203, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Sto&p";
            this.toolTip1.SetToolTip(this.btnStop, "Stop retweeting automatically");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRefreshCounts
            // 
            this.btnRefreshCounts.Image = global::SKZSoft.SKZTweets.Properties.Resources.Refresh_16x;
            this.btnRefreshCounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshCounts.Location = new System.Drawing.Point(378, 12);
            this.btnRefreshCounts.Name = "btnRefreshCounts";
            this.btnRefreshCounts.Size = new System.Drawing.Size(114, 23);
            this.btnRefreshCounts.TabIndex = 4;
            this.btnRefreshCounts.Text = "Re&fresh counts";
            this.toolTip1.SetToolTip(this.btnRefreshCounts, "Refresh the counts immediately");
            this.btnRefreshCounts.UseVisualStyleBackColor = true;
            this.btnRefreshCounts.Click += new System.EventHandler(this.btnRefreshCounts_Click);
            // 
            // tweetDisplay
            // 
            this.tweetDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tweetDisplay.Location = new System.Drawing.Point(12, 41);
            this.tweetDisplay.Name = "tweetDisplay";
            this.tweetDisplay.Size = new System.Drawing.Size(513, 140);
            this.tweetDisplay.Status = null;
            this.tweetDisplay.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tweetDisplay, "The selected tweet");
            // 
            // ctlScheduleBasic1
            // 
            this.ctlScheduleBasic1.EndAt = new System.DateTime(2018, 3, 20, 11, 5, 0, 0);
            this.ctlScheduleBasic1.IntervalMinutes = 0;
            this.ctlScheduleBasic1.Location = new System.Drawing.Point(14, 195);
            this.ctlScheduleBasic1.Name = "ctlScheduleBasic1";
            this.ctlScheduleBasic1.Size = new System.Drawing.Size(183, 82);
            this.ctlScheduleBasic1.StartAt = new System.DateTime(2018, 3, 20, 3, 5, 0, 0);
            this.ctlScheduleBasic1.TabIndex = 6;
            // 
            // btnSelectTweet
            // 
            this.btnSelectTweet.Image = global::SKZSoft.SKZTweets.Properties.Resources.Select_16x;
            this.btnSelectTweet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectTweet.Location = new System.Drawing.Point(12, 12);
            this.btnSelectTweet.Name = "btnSelectTweet";
            this.btnSelectTweet.Size = new System.Drawing.Size(104, 23);
            this.btnSelectTweet.TabIndex = 0;
            this.btnSelectTweet.Text = "Select &tweet";
            this.toolTip1.SetToolTip(this.btnSelectTweet, "Select a tweet for retweeting");
            this.btnSelectTweet.UseVisualStyleBackColor = true;
            this.btnSelectTweet.Click += new System.EventHandler(this.btnSelectTweet_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::SKZSoft.SKZTweets.Properties.Resources.StartWithoutDebug_16x;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(122, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "&Start";
            this.toolTip1.SetToolTip(this.btnStart, "Start the automatic retweeting");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkUpdateCountsAutomatically
            // 
            this.chkUpdateCountsAutomatically.AutoSize = true;
            this.chkUpdateCountsAutomatically.Checked = true;
            this.chkUpdateCountsAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateCountsAutomatically.Location = new System.Drawing.Point(14, 278);
            this.chkUpdateCountsAutomatically.Name = "chkUpdateCountsAutomatically";
            this.chkUpdateCountsAutomatically.Size = new System.Drawing.Size(125, 17);
            this.chkUpdateCountsAutomatically.TabIndex = 23;
            this.chkUpdateCountsAutomatically.Text = "Update counts every";
            this.chkUpdateCountsAutomatically.UseVisualStyleBackColor = true;
            this.chkUpdateCountsAutomatically.CheckedChanged += new System.EventHandler(this.chkUpdateCountsAutomatically_CheckedChanged);
            // 
            // txtUpdateInterval
            // 
            this.txtUpdateInterval.Location = new System.Drawing.Point(136, 276);
            this.txtUpdateInterval.Name = "txtUpdateInterval";
            this.txtUpdateInterval.Size = new System.Drawing.Size(32, 20);
            this.txtUpdateInterval.TabIndex = 25;
            this.txtUpdateInterval.TextChanged += new System.EventHandler(this.txtUpdateInterval_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "seconds";
            // 
            // frmRetweeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 447);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUpdateInterval);
            this.Controls.Add(this.chkUpdateCountsAutomatically);
            this.Controls.Add(this.btnSelectTweet);
            this.Controls.Add(this.ctlScheduleBasic1);
            this.Controls.Add(this.btnRefreshCounts);
            this.Controls.Add(this.tweetDisplay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.btnRTNow);
            this.Name = "frmRetweeter";
            this.Text = "Retweeter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Controls.SetChildIndex(this.btnRTNow, 0);
            this.Controls.SetChildIndex(this.lstHistory, 0);
            this.Controls.SetChildIndex(this.btnStart, 0);
            this.Controls.SetChildIndex(this.btnStop, 0);
            this.Controls.SetChildIndex(this.tweetDisplay, 0);
            this.Controls.SetChildIndex(this.btnRefreshCounts, 0);
            this.Controls.SetChildIndex(this.ctlScheduleBasic1, 0);
            this.Controls.SetChildIndex(this.btnSelectTweet, 0);
            this.Controls.SetChildIndex(this.chkUpdateCountsAutomatically, 0);
            this.Controls.SetChildIndex(this.txtUpdateInterval, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRTNow;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private Usercontrols.TweetDisplay tweetDisplay;
        private System.Windows.Forms.Button btnRefreshCounts;
        private Usercontrols.ctlScheduleBasic ctlScheduleBasic1;
        private System.Windows.Forms.Button btnSelectTweet;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkUpdateCountsAutomatically;
        private System.Windows.Forms.TextBox txtUpdateInterval;
        private System.Windows.Forms.Label label1;
    }
}

