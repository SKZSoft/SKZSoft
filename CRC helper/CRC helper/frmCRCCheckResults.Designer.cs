namespace CRC_helper
{
    partial class frmCRCCheckResults
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
            label1 = new Label();
            lblOK = new Label();
            label3 = new Label();
            label4 = new Label();
            lblMoved = new Label();
            lblDeleted = new Label();
            label7 = new Label();
            lblChanged = new Label();
            label2 = new Label();
            lblNew = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(23, 15);
            label1.TabIndex = 0;
            label1.Text = "OK";
            // 
            // lblOK
            // 
            lblOK.AutoSize = true;
            lblOK.Location = new Point(142, 9);
            lblOK.Name = "lblOK";
            lblOK.Size = new Size(23, 15);
            lblOK.TabIndex = 1;
            lblOK.Text = "OK";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 37);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 2;
            label3.Text = "Renamed or moved";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 64);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 3;
            label4.Text = "Deleted";
            // 
            // lblMoved
            // 
            lblMoved.AutoSize = true;
            lblMoved.Location = new Point(142, 37);
            lblMoved.Name = "lblMoved";
            lblMoved.Size = new Size(23, 15);
            lblMoved.TabIndex = 4;
            lblMoved.Text = "OK";
            // 
            // lblDeleted
            // 
            lblDeleted.AutoSize = true;
            lblDeleted.Location = new Point(142, 64);
            lblDeleted.Name = "lblDeleted";
            lblDeleted.Size = new Size(23, 15);
            lblDeleted.TabIndex = 5;
            lblDeleted.Text = "OK";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 93);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 6;
            label7.Text = "Changed";
            // 
            // lblChanged
            // 
            lblChanged.AutoSize = true;
            lblChanged.Location = new Point(142, 93);
            lblChanged.Name = "lblChanged";
            lblChanged.Size = new Size(23, 15);
            lblChanged.TabIndex = 7;
            lblChanged.Text = "OK";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 123);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 8;
            label2.Text = "New";
            // 
            // lblNew
            // 
            lblNew.AutoSize = true;
            lblNew.Location = new Point(142, 123);
            lblNew.Name = "lblNew";
            lblNew.Size = new Size(23, 15);
            lblNew.TabIndex = 9;
            lblNew.Text = "OK";
            // 
            // frmCRCCheckResults
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblNew);
            Controls.Add(label2);
            Controls.Add(lblChanged);
            Controls.Add(label7);
            Controls.Add(lblDeleted);
            Controls.Add(lblMoved);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lblOK);
            Controls.Add(label1);
            Name = "frmCRCCheckResults";
            Text = "Check Results";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblOK;
        private Label label3;
        private Label label4;
        private Label lblMoved;
        private Label lblDeleted;
        private Label label7;
        private Label lblChanged;
        private Label label2;
        private Label lblNew;
    }
}