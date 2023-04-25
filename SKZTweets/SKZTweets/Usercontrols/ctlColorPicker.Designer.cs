namespace SKZSoft.SKZTweets.Usercontrols
{
    partial class ctlColorPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialogBack = new System.Windows.Forms.ColorDialog();
            this.btnSelectBack = new System.Windows.Forms.Button();
            this.btnSelectFore = new System.Windows.Forms.Button();
            this.lblSample = new System.Windows.Forms.Label();
            this.colorDialogFore = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // btnSelectBack
            // 
            this.btnSelectBack.Location = new System.Drawing.Point(73, 0);
            this.btnSelectBack.Name = "btnSelectBack";
            this.btnSelectBack.Size = new System.Drawing.Size(75, 23);
            this.btnSelectBack.TabIndex = 1;
            this.btnSelectBack.Text = "Background";
            this.btnSelectBack.UseVisualStyleBackColor = true;
            this.btnSelectBack.Click += new System.EventHandler(this.btnSelectBack_Click);
            // 
            // btnSelectFore
            // 
            this.btnSelectFore.Location = new System.Drawing.Point(154, 0);
            this.btnSelectFore.Name = "btnSelectFore";
            this.btnSelectFore.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFore.TabIndex = 2;
            this.btnSelectFore.Text = "Foreground";
            this.btnSelectFore.UseVisualStyleBackColor = true;
            this.btnSelectFore.Click += new System.EventHandler(this.btnSelectFore_Click);
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Location = new System.Drawing.Point(3, 5);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(62, 13);
            this.lblSample.TabIndex = 3;
            this.lblSample.Text = "Sample text";
            // 
            // ctlColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.btnSelectFore);
            this.Controls.Add(this.btnSelectBack);
            this.Name = "ctlColorPicker";
            this.Size = new System.Drawing.Size(232, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialogBack;
        private System.Windows.Forms.Button btnSelectBack;
        private System.Windows.Forms.Button btnSelectFore;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.ColorDialog colorDialogFore;
    }
}
