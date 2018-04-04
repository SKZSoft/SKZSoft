namespace SKZSoft.SKZTweets
{
    partial class frmDMFollowers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDMFollowers));
            this.btnGetFollowers = new System.Windows.Forms.Button();
            this.txtFollowerIds = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtDMBody1 = new System.Windows.Forms.TextBox();
            this.btnSendDMs = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtDMBody2 = new System.Windows.Forms.TextBox();
            this.txtDMBody3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetFollowers
            // 
            this.btnGetFollowers.Location = new System.Drawing.Point(12, 12);
            this.btnGetFollowers.Name = "btnGetFollowers";
            this.btnGetFollowers.Size = new System.Drawing.Size(97, 23);
            this.btnGetFollowers.TabIndex = 0;
            this.btnGetFollowers.Text = "&Get Followers";
            this.btnGetFollowers.UseVisualStyleBackColor = true;
            this.btnGetFollowers.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFollowerIds
            // 
            this.txtFollowerIds.Location = new System.Drawing.Point(12, 41);
            this.txtFollowerIds.Multiline = true;
            this.txtFollowerIds.Name = "txtFollowerIds";
            this.txtFollowerIds.Size = new System.Drawing.Size(279, 354);
            this.txtFollowerIds.TabIndex = 1;
            this.txtFollowerIds.Text = "806131578174930948";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(12, 402);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "&Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtDMBody1
            // 
            this.txtDMBody1.Location = new System.Drawing.Point(392, 41);
            this.txtDMBody1.Multiline = true;
            this.txtDMBody1.Name = "txtDMBody1";
            this.txtDMBody1.Size = new System.Drawing.Size(314, 82);
            this.txtDMBody1.TabIndex = 3;
            this.txtDMBody1.Text = resources.GetString("txtDMBody1.Text");
            // 
            // btnSendDMs
            // 
            this.btnSendDMs.Location = new System.Drawing.Point(392, 343);
            this.btnSendDMs.Name = "btnSendDMs";
            this.btnSendDMs.Size = new System.Drawing.Size(75, 23);
            this.btnSendDMs.TabIndex = 4;
            this.btnSendDMs.Text = "Send DMs";
            this.btnSendDMs.UseVisualStyleBackColor = true;
            this.btnSendDMs.Click += new System.EventHandler(this.btnSendDMs_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(389, 380);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(98, 13);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "Progress goes here";
            // 
            // txtDMBody2
            // 
            this.txtDMBody2.Location = new System.Drawing.Point(392, 144);
            this.txtDMBody2.Multiline = true;
            this.txtDMBody2.Name = "txtDMBody2";
            this.txtDMBody2.Size = new System.Drawing.Size(314, 82);
            this.txtDMBody2.TabIndex = 6;
            this.txtDMBody2.Text = "https://twitter.com/SKZAnnounce/status/978922752693465088";
            // 
            // txtDMBody3
            // 
            this.txtDMBody3.Location = new System.Drawing.Point(392, 243);
            this.txtDMBody3.Multiline = true;
            this.txtDMBody3.Name = "txtDMBody3";
            this.txtDMBody3.Size = new System.Drawing.Size(314, 82);
            this.txtDMBody3.TabIndex = 7;
            this.txtDMBody3.Text = "This is a test DM. Woo.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(790, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 352);
            this.textBox1.TabIndex = 8;
            // 
            // frmDMFollowers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtDMBody3);
            this.Controls.Add(this.txtDMBody2);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnSendDMs);
            this.Controls.Add(this.txtDMBody1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtFollowerIds);
            this.Controls.Add(this.btnGetFollowers);
            this.Name = "frmDMFollowers";
            this.Text = "DM Followers";
            this.Controls.SetChildIndex(this.btnGetFollowers, 0);
            this.Controls.SetChildIndex(this.txtFollowerIds, 0);
            this.Controls.SetChildIndex(this.btnCopy, 0);
            this.Controls.SetChildIndex(this.txtDMBody1, 0);
            this.Controls.SetChildIndex(this.btnSendDMs, 0);
            this.Controls.SetChildIndex(this.lblProgress, 0);
            this.Controls.SetChildIndex(this.txtDMBody2, 0);
            this.Controls.SetChildIndex(this.txtDMBody3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetFollowers;
        private System.Windows.Forms.TextBox txtFollowerIds;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtDMBody1;
        private System.Windows.Forms.Button btnSendDMs;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtDMBody2;
        private System.Windows.Forms.TextBox txtDMBody3;
        private System.Windows.Forms.TextBox textBox1;
    }
}