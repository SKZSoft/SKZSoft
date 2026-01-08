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
            btnSaveNewFile = new Button();
            btnViewOK = new Button();
            btnViewMoved = new Button();
            btnViewDeleted = new Button();
            btnViewChanged = new Button();
            btnViewNew = new Button();
            grdResults = new DataGridView();
            btnCancel = new Button();
            btnViewFailed = new Button();
            lblCouldNotCalculate = new Label();
            label6 = new Label();
            txtExcludePaths = new TextBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)grdResults).BeginInit();
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
            // btnSaveNewFile
            // 
            btnSaveNewFile.Location = new Point(12, 178);
            btnSaveNewFile.Name = "btnSaveNewFile";
            btnSaveNewFile.Size = new Size(111, 23);
            btnSaveNewFile.TabIndex = 10;
            btnSaveNewFile.Text = "Save new CRC";
            btnSaveNewFile.UseVisualStyleBackColor = true;
            btnSaveNewFile.Click += btnSaveNewFile_Click;
            // 
            // btnViewOK
            // 
            btnViewOK.Location = new Point(183, 9);
            btnViewOK.Name = "btnViewOK";
            btnViewOK.Size = new Size(99, 23);
            btnViewOK.TabIndex = 11;
            btnViewOK.Text = "View &OK";
            btnViewOK.UseVisualStyleBackColor = true;
            btnViewOK.Click += btnViewOK_Click;
            // 
            // btnViewMoved
            // 
            btnViewMoved.Location = new Point(183, 37);
            btnViewMoved.Name = "btnViewMoved";
            btnViewMoved.Size = new Size(99, 23);
            btnViewMoved.TabIndex = 12;
            btnViewMoved.Text = "View &Moved";
            btnViewMoved.UseVisualStyleBackColor = true;
            btnViewMoved.Click += btnViewMoved_Click;
            // 
            // btnViewDeleted
            // 
            btnViewDeleted.Location = new Point(183, 66);
            btnViewDeleted.Name = "btnViewDeleted";
            btnViewDeleted.Size = new Size(99, 23);
            btnViewDeleted.TabIndex = 13;
            btnViewDeleted.Text = "View &Deleted";
            btnViewDeleted.UseVisualStyleBackColor = true;
            btnViewDeleted.Click += btnViewDeleted_Click;
            // 
            // btnViewChanged
            // 
            btnViewChanged.Location = new Point(183, 95);
            btnViewChanged.Name = "btnViewChanged";
            btnViewChanged.Size = new Size(99, 23);
            btnViewChanged.TabIndex = 14;
            btnViewChanged.Text = "View &Changed";
            btnViewChanged.UseVisualStyleBackColor = true;
            btnViewChanged.Click += btnViewChanged_Click;
            // 
            // btnViewNew
            // 
            btnViewNew.Location = new Point(183, 124);
            btnViewNew.Name = "btnViewNew";
            btnViewNew.Size = new Size(99, 23);
            btnViewNew.TabIndex = 15;
            btnViewNew.Text = "View &New";
            btnViewNew.UseVisualStyleBackColor = true;
            btnViewNew.Click += btnViewNew_Click;
            // 
            // grdResults
            // 
            grdResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grdResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdResults.Location = new Point(12, 207);
            grdResults.Name = "grdResults";
            grdResults.Size = new Size(855, 418);
            grdResults.TabIndex = 16;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(129, 178);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(99, 23);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnViewFailed
            // 
            btnViewFailed.Location = new Point(183, 153);
            btnViewFailed.Name = "btnViewFailed";
            btnViewFailed.Size = new Size(99, 23);
            btnViewFailed.TabIndex = 20;
            btnViewFailed.Text = "View &Failed";
            btnViewFailed.UseVisualStyleBackColor = true;
            btnViewFailed.Click += btnViewFailed_Click;
            // 
            // lblCouldNotCalculate
            // 
            lblCouldNotCalculate.AutoSize = true;
            lblCouldNotCalculate.Location = new Point(142, 152);
            lblCouldNotCalculate.Name = "lblCouldNotCalculate";
            lblCouldNotCalculate.Size = new Size(23, 15);
            lblCouldNotCalculate.TabIndex = 19;
            lblCouldNotCalculate.Text = "OK";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 152);
            label6.Name = "label6";
            label6.Size = new Size(86, 15);
            label6.TabIndex = 18;
            label6.Text = "Not Calculated";
            // 
            // txtExcludePaths
            // 
            txtExcludePaths.Location = new Point(497, 17);
            txtExcludePaths.Multiline = true;
            txtExcludePaths.Name = "txtExcludePaths";
            txtExcludePaths.Size = new Size(264, 124);
            txtExcludePaths.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(343, 17);
            label5.Name = "label5";
            label5.Size = new Size(148, 15);
            label5.TabIndex = 22;
            label5.Text = "Exclude paths from display";
            // 
            // frmCRCCheckResults
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(872, 628);
            Controls.Add(label5);
            Controls.Add(txtExcludePaths);
            Controls.Add(btnViewFailed);
            Controls.Add(lblCouldNotCalculate);
            Controls.Add(label6);
            Controls.Add(btnCancel);
            Controls.Add(grdResults);
            Controls.Add(btnViewNew);
            Controls.Add(btnViewChanged);
            Controls.Add(btnViewDeleted);
            Controls.Add(btnViewMoved);
            Controls.Add(btnViewOK);
            Controls.Add(btnSaveNewFile);
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
            Load += frmCRCCheckResults_Load;
            ((System.ComponentModel.ISupportInitialize)grdResults).EndInit();
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
        private Button btnSaveNewFile;
        private Button btnViewOK;
        private Button btnViewMoved;
        private Button btnViewDeleted;
        private Button btnViewChanged;
        private Button btnViewNew;
        private DataGridView grdResults;
        private Button btnCancel;
        private Button btnViewFailed;
        private Label lblCouldNotCalculate;
        private Label label6;
        private TextBox txtExcludePaths;
        private Label label5;
    }
}