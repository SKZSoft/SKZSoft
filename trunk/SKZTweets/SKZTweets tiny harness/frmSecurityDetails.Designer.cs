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
            this.txtConsumerKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConsumerKey.Name = "txtConsumerKey";
            this.txtConsumerKey.Size = new System.Drawing.Size(399, 22);
            this.txtConsumerKey.TabIndex = 8;
            this.txtConsumerKey.Text = "Y9i9hTLPh5pIOkgPZoPdPsECw";
            // 
            // txtConsumerSecret
            // 
            this.txtConsumerSecret.Location = new System.Drawing.Point(203, 315);
            this.txtConsumerSecret.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConsumerSecret.Name = "txtConsumerSecret";
            this.txtConsumerSecret.Size = new System.Drawing.Size(399, 22);
            this.txtConsumerSecret.TabIndex = 9;
            this.txtConsumerSecret.Text = "POylBr9eaAGBXGJipk1TF3U9CAO3qLzpTuWvU3vm2NhB1qSSGq";
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
            this.btnLaunchTwitter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.txtAuthCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(399, 22);
            this.txtAuthCode.TabIndex = 13;
            // 
            // btnAuthorise
            // 
            this.btnAuthorise.Location = new System.Drawing.Point(72, 490);
            this.btnAuthorise.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            // frmSecurityDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 559);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}