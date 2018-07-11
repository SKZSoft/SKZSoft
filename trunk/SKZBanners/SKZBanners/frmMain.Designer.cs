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
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.picBackColor = new System.Windows.Forms.PictureBox();
            this.picForeColor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForeColor)).BeginInit();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(160, 98);
            this.txtText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(583, 80);
            this.txtText.TabIndex = 0;
            this.txtText.Text = "Buy \"The Little Book of Brexit Logic: recipes for disaster\"\r\nfor only £10 plus p&" +
    "p from www.skzcartoons.co.uk!\r\n";
            // 
            // cmbFonts
            // 
            this.cmbFonts.FormattingEnabled = true;
            this.cmbFonts.Location = new System.Drawing.Point(160, 207);
            this.cmbFonts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFonts.Name = "cmbFonts";
            this.cmbFonts.Size = new System.Drawing.Size(300, 24);
            this.cmbFonts.TabIndex = 1;
            this.cmbFonts.SelectedIndexChanged += new System.EventHandler(this.cmbFonts_SelectedIndexChanged);
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(790, 98);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(330, 315);
            this.picPreview.TabIndex = 2;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            // 
            // picBackColor
            // 
            this.picBackColor.Location = new System.Drawing.Point(160, 258);
            this.picBackColor.Name = "picBackColor";
            this.picBackColor.Size = new System.Drawing.Size(49, 50);
            this.picBackColor.TabIndex = 3;
            this.picBackColor.TabStop = false;
            // 
            // picForeColor
            // 
            this.picForeColor.Location = new System.Drawing.Point(234, 258);
            this.picForeColor.Name = "picForeColor";
            this.picForeColor.Size = new System.Drawing.Size(49, 50);
            this.picForeColor.TabIndex = 4;
            this.picForeColor.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 574);
            this.Controls.Add(this.picForeColor);
            this.Controls.Add(this.picBackColor);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.cmbFonts);
            this.Controls.Add(this.txtText);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMain";
            this.Text = "SKZ Banners";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picForeColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ComboBox cmbFonts;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.PictureBox picBackColor;
        private System.Windows.Forms.PictureBox picForeColor;
    }
}

