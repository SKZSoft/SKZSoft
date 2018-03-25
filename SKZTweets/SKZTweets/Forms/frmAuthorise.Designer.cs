namespace SKZSoft.SKZTweets
{
    partial class frmAuthorise
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAuthorise = new System.Windows.Forms.Button();
            this.cmbBrowser = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.ctlColorPicker = new SKZSoft.SKZTweets.Usercontrols.ctlColorPicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You must sign in to Twitter to obtain a code to authorise this app.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click this button to begin to sign in to Twitter.";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(154, 46);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(120, 23);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "&Launch Twitter";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Then enter your code here:";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(154, 74);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "...and click \"Authorise\"";
            // 
            // btnAuthorise
            // 
            this.btnAuthorise.Enabled = false;
            this.btnAuthorise.Location = new System.Drawing.Point(154, 130);
            this.btnAuthorise.Name = "btnAuthorise";
            this.btnAuthorise.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorise.TabIndex = 3;
            this.btnAuthorise.Text = "&Authorise";
            this.btnAuthorise.UseVisualStyleBackColor = true;
            this.btnAuthorise.Click += new System.EventHandler(this.btnAuthorise_Click);
            // 
            // cmbBrowser
            // 
            this.cmbBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrowser.FormattingEnabled = true;
            this.cmbBrowser.Location = new System.Drawing.Point(15, 48);
            this.cmbBrowser.Name = "cmbBrowser";
            this.cmbBrowser.Size = new System.Drawing.Size(133, 21);
            this.cmbBrowser.Sorted = true;
            this.cmbBrowser.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "...select a color...";
            // 
            // ctlColorPicker
            // 
            this.ctlColorPicker.Location = new System.Drawing.Point(154, 100);
            this.ctlColorPicker.Name = "ctlColorPicker";
            this.ctlColorPicker.Size = new System.Drawing.Size(235, 24);
            this.ctlColorPicker.TabIndex = 7;
            // 
            // frmAuthorise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 161);
            this.Controls.Add(this.ctlColorPicker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbBrowser);
            this.Controls.Add(this.btnAuthorise);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAuthorise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Authorise App";
            this.Load += new System.EventHandler(this.frmAuthorise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAuthorise;
        private System.Windows.Forms.ComboBox cmbBrowser;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label label5;
        private Usercontrols.ctlColorPicker ctlColorPicker;
    }
}