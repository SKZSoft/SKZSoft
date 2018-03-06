namespace TestHarness
{
    partial class Form1
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
            this.skzSplitterWithGrabber1 = new SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber();
            this.skzSplitterWithGrabber2 = new SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.skzSplitterWithGrabber1)).BeginInit();
            this.skzSplitterWithGrabber1.Panel1.SuspendLayout();
            this.skzSplitterWithGrabber1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skzSplitterWithGrabber2)).BeginInit();
            this.skzSplitterWithGrabber2.Panel1.SuspendLayout();
            this.skzSplitterWithGrabber2.SuspendLayout();
            this.SuspendLayout();
            // 
            // skzSplitterWithGrabber1
            // 
            this.skzSplitterWithGrabber1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skzSplitterWithGrabber1.GrabberDots = true;
            this.skzSplitterWithGrabber1.GrabberLine = false;
            this.skzSplitterWithGrabber1.Location = new System.Drawing.Point(0, 0);
            this.skzSplitterWithGrabber1.Name = "skzSplitterWithGrabber1";
            this.skzSplitterWithGrabber1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // skzSplitterWithGrabber1.Panel1
            // 
            this.skzSplitterWithGrabber1.Panel1.Controls.Add(this.skzSplitterWithGrabber2);
            this.skzSplitterWithGrabber1.Size = new System.Drawing.Size(511, 193);
            this.skzSplitterWithGrabber1.SplitterDistance = 164;
            this.skzSplitterWithGrabber1.TabIndex = 0;
            // 
            // skzSplitterWithGrabber2
            // 
            this.skzSplitterWithGrabber2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skzSplitterWithGrabber2.GrabberDots = false;
            this.skzSplitterWithGrabber2.GrabberLine = true;
            this.skzSplitterWithGrabber2.Location = new System.Drawing.Point(0, 0);
            this.skzSplitterWithGrabber2.Name = "skzSplitterWithGrabber2";
            // 
            // skzSplitterWithGrabber2.Panel1
            // 
            this.skzSplitterWithGrabber2.Panel1.Controls.Add(this.textBox1);
            this.skzSplitterWithGrabber2.Size = new System.Drawing.Size(511, 164);
            this.skzSplitterWithGrabber2.SplitterDistance = 170;
            this.skzSplitterWithGrabber2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 193);
            this.Controls.Add(this.skzSplitterWithGrabber1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.skzSplitterWithGrabber1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skzSplitterWithGrabber1)).EndInit();
            this.skzSplitterWithGrabber1.ResumeLayout(false);
            this.skzSplitterWithGrabber2.Panel1.ResumeLayout(false);
            this.skzSplitterWithGrabber2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skzSplitterWithGrabber2)).EndInit();
            this.skzSplitterWithGrabber2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber skzSplitterWithGrabber1;
        private SKZSoft.Common.SplitterWithGrabber.skzSplitterWithGrabber skzSplitterWithGrabber2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

