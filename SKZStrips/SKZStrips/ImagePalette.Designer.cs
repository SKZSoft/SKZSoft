namespace SKZStrips
{
    partial class ImagePalette
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
            this.imagePicker = new SKZStrips.Usercontrols.Palettes.ctlImagePicker();
            this.SuspendLayout();
            // 
            // imagePicker
            // 
            this.imagePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePicker.Location = new System.Drawing.Point(0, 0);
            this.imagePicker.Name = "imagePicker";
            this.imagePicker.Size = new System.Drawing.Size(781, 148);
            this.imagePicker.TabIndex = 0;
            // 
            // ImagePalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 148);
            this.Controls.Add(this.imagePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ImagePalette";
            this.Text = "ImagePalette";
            this.ResumeLayout(false);

        }

        #endregion

        private Usercontrols.Palettes.ctlImagePicker imagePicker;
    }
}