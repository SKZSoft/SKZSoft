namespace SKZSoft.SKZTweets.Usercontrols
{
    partial class ctlTweetTextAndPictures
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
            this.pnlThumbnails = new System.Windows.Forms.Panel();
            this.lblDragDropError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlThumbnails
            // 
            this.pnlThumbnails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThumbnails.BackColor = System.Drawing.Color.White;
            this.pnlThumbnails.Location = new System.Drawing.Point(3, 90);
            this.pnlThumbnails.Name = "pnlThumbnails";
            this.pnlThumbnails.Size = new System.Drawing.Size(288, 31);
            this.pnlThumbnails.TabIndex = 2;
            this.pnlThumbnails.Visible = false;
            // 
            // lblDragDropError
            // 
            this.lblDragDropError.BackColor = System.Drawing.Color.Red;
            this.lblDragDropError.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragDropError.ForeColor = System.Drawing.Color.White;
            this.lblDragDropError.Location = new System.Drawing.Point(16, 17);
            this.lblDragDropError.Name = "lblDragDropError";
            this.lblDragDropError.Size = new System.Drawing.Size(100, 23);
            this.lblDragDropError.TabIndex = 3;
            this.lblDragDropError.Text = "label1";
            this.lblDragDropError.Visible = false;
            // 
            // ctlTweetTextAndPictures
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDragDropError);
            this.Controls.Add(this.pnlThumbnails);
            this.Name = "ctlTweetTextAndPictures";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ctlTweetTextAndPictures_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ctlTweetTextAndPictures_DragEnter);
            this.DragLeave += new System.EventHandler(this.ctlTweetTextAndPictures_DragLeave);
            this.Controls.SetChildIndex(this.pnlThumbnails, 0);
            this.Controls.SetChildIndex(this.lblDragDropError, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlThumbnails;
        private System.Windows.Forms.Label lblDragDropError;
    }
}
