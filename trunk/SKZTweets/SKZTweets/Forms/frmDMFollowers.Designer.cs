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
            this.btnGetFollowers = new System.Windows.Forms.Button();
            this.txtFollowerIds = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtDMBody = new System.Windows.Forms.TextBox();
            this.btnSendDMs = new System.Windows.Forms.Button();
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
            this.txtFollowerIds.Size = new System.Drawing.Size(279, 365);
            this.txtFollowerIds.TabIndex = 1;
            this.txtFollowerIds.Text = "806131578174930948";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(12, 415);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "&Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtDMBody
            // 
            this.txtDMBody.Location = new System.Drawing.Point(392, 41);
            this.txtDMBody.Multiline = true;
            this.txtDMBody.Name = "txtDMBody";
            this.txtDMBody.Size = new System.Drawing.Size(314, 127);
            this.txtDMBody.TabIndex = 3;
            this.txtDMBody.Text = "This is a test DM. Woo.";
            // 
            // btnSendDMs
            // 
            this.btnSendDMs.Location = new System.Drawing.Point(392, 174);
            this.btnSendDMs.Name = "btnSendDMs";
            this.btnSendDMs.Size = new System.Drawing.Size(75, 23);
            this.btnSendDMs.TabIndex = 4;
            this.btnSendDMs.Text = "Send DMs";
            this.btnSendDMs.UseVisualStyleBackColor = true;
            this.btnSendDMs.Click += new System.EventHandler(this.btnSendDMs_Click);
            // 
            // frmDMFollowers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendDMs);
            this.Controls.Add(this.txtDMBody);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtFollowerIds);
            this.Controls.Add(this.btnGetFollowers);
            this.Name = "frmDMFollowers";
            this.Text = "DM Followers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetFollowers;
        private System.Windows.Forms.TextBox txtFollowerIds;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtDMBody;
        private System.Windows.Forms.Button btnSendDMs;
    }
}