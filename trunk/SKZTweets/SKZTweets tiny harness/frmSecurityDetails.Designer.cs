namespace SKZTweets_tiny_harness
{
    partial class frmSecurityDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecurityDetails));
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConsumerKey = new System.Windows.Forms.TextBox();
            this.txtConsumerSecret = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLaunchTwitter = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.btnAuthorise = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAccessTokenSecret = new System.Windows.Forms.TextBox();
            this.txtAccessToken = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtAccountId = new System.Windows.Forms.TextBox();
            this.txtScreenName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtConfigFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel.Location = new System.Drawing.Point(16, 11);
            this.linkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(196, 34);
            this.linkLabel.TabIndex = 0;
            this.linkLabel.Text = "Go to https://apps.twitter.com/\r\n\r\n";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Click \"Create new app\".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(589, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fill in the details (you can use \"http://www.twitter.com\" for website and leave \"" +
    "callback\" blank)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(424, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Check the \"agree checkbox\" and click the button to create an app.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 224);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Select \"Keys and access tokens\"";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 256);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Copy and paste the values here.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 287);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Consumer Key";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 319);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Consumer Secret";
            // 
            // txtConsumerKey
            // 
            this.txtConsumerKey.Location = new System.Drawing.Point(203, 283);
            this.txtConsumerKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtConsumerKey.Name = "txtConsumerKey";
            this.txtConsumerKey.Size = new System.Drawing.Size(399, 22);
            this.txtConsumerKey.TabIndex = 8;
            // 
            // txtConsumerSecret
            // 
            this.txtConsumerSecret.Location = new System.Drawing.Point(203, 315);
            this.txtConsumerSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtConsumerSecret.Name = "txtConsumerSecret";
            this.txtConsumerSecret.Size = new System.Drawing.Size(399, 22);
            this.txtConsumerSecret.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 361);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(463, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Click button to launch Twitter, and sign in there. Then authorise the app.";
            // 
            // btnLaunchTwitter
            // 
            this.btnLaunchTwitter.Location = new System.Drawing.Point(77, 380);
            this.btnLaunchTwitter.Margin = new System.Windows.Forms.Padding(4);
            this.btnLaunchTwitter.Name = "btnLaunchTwitter";
            this.btnLaunchTwitter.Size = new System.Drawing.Size(144, 28);
            this.btnLaunchTwitter.TabIndex = 11;
            this.btnLaunchTwitter.Text = "Launch twitter";
            this.btnLaunchTwitter.UseVisualStyleBackColor = true;
            this.btnLaunchTwitter.Click += new System.EventHandler(this.btnLaunchTwitter_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 422);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(328, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Then copy and paste the code you are given here:";
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Location = new System.Drawing.Point(72, 442);
            this.txtAuthCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(399, 22);
            this.txtAuthCode.TabIndex = 13;
            // 
            // btnAuthorise
            // 
            this.btnAuthorise.Location = new System.Drawing.Point(72, 490);
            this.btnAuthorise.Margin = new System.Windows.Forms.Padding(4);
            this.btnAuthorise.Name = "btnAuthorise";
            this.btnAuthorise.Size = new System.Drawing.Size(144, 28);
            this.btnAuthorise.TabIndex = 14;
            this.btnAuthorise.Text = "&Authorise";
            this.btnAuthorise.UseVisualStyleBackColor = true;
            this.btnAuthorise.Click += new System.EventHandler(this.btnAuthorise_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 470);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "and click this button";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 134);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "Select \"Permissions\"";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 161);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(631, 51);
            this.label12.TabIndex = 17;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(508, 361);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 17);
            this.label13.TabIndex = 18;
            this.label13.Text = "OR";
            // 
            // txtAccessTokenSecret
            // 
            this.txtAccessTokenSecret.Location = new System.Drawing.Point(727, 454);
            this.txtAccessTokenSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessTokenSecret.Name = "txtAccessTokenSecret";
            this.txtAccessTokenSecret.Size = new System.Drawing.Size(399, 22);
            this.txtAccessTokenSecret.TabIndex = 23;
            // 
            // txtAccessToken
            // 
            this.txtAccessToken.Location = new System.Drawing.Point(727, 422);
            this.txtAccessToken.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessToken.Name = "txtAccessToken";
            this.txtAccessToken.Size = new System.Drawing.Size(399, 22);
            this.txtAccessToken.TabIndex = 22;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(577, 454);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 17);
            this.label14.TabIndex = 21;
            this.label14.Text = "Access Token Secret";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(577, 422);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 17);
            this.label15.TabIndex = 20;
            this.label15.Text = "Access Token";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(520, 391);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(283, 17);
            this.label16.TabIndex = 19;
            this.label16.Text = "Copy and paste existing (valid) values here.";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(727, 555);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 28);
            this.btnLogin.TabIndex = 24;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtAccountId
            // 
            this.txtAccountId.Location = new System.Drawing.Point(727, 528);
            this.txtAccountId.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccountId.Name = "txtAccountId";
            this.txtAccountId.Size = new System.Drawing.Size(399, 22);
            this.txtAccountId.TabIndex = 28;
            // 
            // txtScreenName
            // 
            this.txtScreenName.Location = new System.Drawing.Point(727, 496);
            this.txtScreenName.Margin = new System.Windows.Forms.Padding(4);
            this.txtScreenName.Name = "txtScreenName";
            this.txtScreenName.Size = new System.Drawing.Size(399, 22);
            this.txtScreenName.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(577, 528);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 17);
            this.label17.TabIndex = 26;
            this.label17.Text = "Account ID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(577, 496);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 17);
            this.label18.TabIndex = 25;
            this.label18.Text = "Screen Name";
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(703, 14);
            this.btnLoadFromFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(136, 31);
            this.btnLoadFromFile.TabIndex = 29;
            this.btnLoadFromFile.Text = "&Load from File";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(645, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 17);
            this.label19.TabIndex = 30;
            this.label19.Text = "OR";
            // 
            // txtConfigFilePath
            // 
            this.txtConfigFilePath.Location = new System.Drawing.Point(703, 52);
            this.txtConfigFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfigFilePath.Name = "txtConfigFilePath";
            this.txtConfigFilePath.Size = new System.Drawing.Size(399, 22);
            this.txtConfigFilePath.TabIndex = 31;
            this.txtConfigFilePath.Text = "config.ini";
            // 
            // frmSecurityDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 594);
            this.Controls.Add(this.txtConfigFilePath);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.txtAccountId);
            this.Controls.Add(this.txtScreenName);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtAccessTokenSecret);
            this.Controls.Add(this.txtAccessToken);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnAuthorise);
            this.Controls.Add(this.txtAuthCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnLaunchTwitter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtConsumerSecret);
            this.Controls.Add(this.txtConsumerKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSecurityDetails";
            this.Text = "Log in to harness";
            this.Load += new System.EventHandler(this.frmSecurityDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConsumerKey;
        private System.Windows.Forms.TextBox txtConsumerSecret;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLaunchTwitter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Button btnAuthorise;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAccessTokenSecret;
        private System.Windows.Forms.TextBox txtAccessToken;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtAccountId;
        private System.Windows.Forms.TextBox txtScreenName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtConfigFilePath;
    }
}