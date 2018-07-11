namespace SKZBanners
{
    partial class frmMain
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
            this.txtText = new System.Windows.Forms.TextBox();
            this.cmbFonts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(120, 80);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(438, 66);
            this.txtText.TabIndex = 0;
            this.txtText.Text = "Buy \"The Little Book of Brexit Logic: recipes for disaster\"\r\nfor only £10 plus p&" +
    "p from www.skzcartoons.co.uk!\r\n";
            // 
            // cmbFonts
            // 
            this.cmbFonts.FormattingEnabled = true;
            this.cmbFonts.Location = new System.Drawing.Point(120, 168);
            this.cmbFonts.Name = "cmbFonts";
            this.cmbFonts.Size = new System.Drawing.Size(226, 21);
            this.cmbFonts.TabIndex = 1;
            this.cmbFonts.SelectedIndexChanged += new System.EventHandler(this.cmbFonts_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 466);
            this.Controls.Add(this.cmbFonts);
            this.Controls.Add(this.txtText);
            this.Name = "frmMain";
            this.Text = "SKZ Banners";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ComboBox cmbFonts;
    }
}

