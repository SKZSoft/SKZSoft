namespace SKZTweets
{
    partial class frmSelectTweet
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.tweetList = new SKZTweets.Usercontrols.ctlTweetList();
            this.tweetDisplay = new SKZTweets.Usercontrols.TweetDisplay();
            this.label1 = new System.Windows.Forms.Label();
            this.txtScreenNameOrUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(507, 33);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "S&elect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tweetList
            // 
            this.tweetList.Location = new System.Drawing.Point(88, 33);
            this.tweetList.Name = "tweetList";
            this.tweetList.Size = new System.Drawing.Size(413, 287);
            this.tweetList.TabIndex = 3;
            this.tweetList.DoubleClick += new System.EventHandler(this.tweetList_DoubleClick);
            this.tweetList.Enter += new System.EventHandler(this.tweetList_Enter);
            // 
            // tweetDisplay
            // 
            this.tweetDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tweetDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tweetDisplay.Location = new System.Drawing.Point(88, 326);
            this.tweetDisplay.Name = "tweetDisplay";
            this.tweetDisplay.Size = new System.Drawing.Size(413, 167);
            this.tweetDisplay.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Screen name or URL";
            // 
            // txtScreenNameOrUrl
            // 
            this.txtScreenNameOrUrl.Location = new System.Drawing.Point(116, 7);
            this.txtScreenNameOrUrl.Name = "txtScreenNameOrUrl";
            this.txtScreenNameOrUrl.Size = new System.Drawing.Size(385, 20);
            this.txtScreenNameOrUrl.TabIndex = 0;
            this.txtScreenNameOrUrl.Enter += new System.EventHandler(this.txtScreenNameOrUrl_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Search Results";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(507, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmSelectTweet
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 505);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScreenNameOrUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tweetList);
            this.Controls.Add(this.tweetDisplay);
            this.Controls.Add(this.btnSelect);
            this.Name = "frmSelectTweet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Tweet";
            this.Load += new System.EventHandler(this.frmSelectTweet_Load);
            this.Shown += new System.EventHandler(this.frmSelectTweet_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSelect;
        private Usercontrols.TweetDisplay tweetDisplay;
        private Usercontrols.ctlTweetList tweetList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScreenNameOrUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
    }
}