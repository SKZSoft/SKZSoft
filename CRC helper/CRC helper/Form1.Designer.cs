namespace CRC_helper
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSourceFolders = new TextBox();
            txtCRCFilePath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnGenerateCRCFile = new Button();
            btnVerifyCRCFile = new Button();
            optSingleCRCFile = new RadioButton();
            optCRCFilePerRootFolder = new RadioButton();
            btnSelectCRCPath = new Button();
            label3 = new Label();
            lblProcessingFile = new Label();
            SuspendLayout();
            // 
            // txtSourceFolders
            // 
            txtSourceFolders.Location = new Point(108, 12);
            txtSourceFolders.Multiline = true;
            txtSourceFolders.Name = "txtSourceFolders";
            txtSourceFolders.Size = new Size(543, 140);
            txtSourceFolders.TabIndex = 0;
            txtSourceFolders.Text = "C:\\Users\\skzca\\Desktop\\test files";
            txtSourceFolders.TextChanged += txtSourceFolders_TextChanged;
            // 
            // txtCRCFilePath
            // 
            txtCRCFilePath.Location = new Point(216, 187);
            txtCRCFilePath.Name = "txtCRCFilePath";
            txtCRCFilePath.Size = new Size(435, 23);
            txtCRCFilePath.TabIndex = 1;
            txtCRCFilePath.Text = "C:\\Users\\skzca\\Desktop\\test files\\test files new.sha512";
            txtCRCFilePath.TextChanged += txtCRCFilePath_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 20);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 2;
            label1.Text = "Source Folders";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 166);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 3;
            label2.Text = "CRC file";
            // 
            // btnGenerateCRCFile
            // 
            btnGenerateCRCFile.Location = new Point(106, 221);
            btnGenerateCRCFile.Name = "btnGenerateCRCFile";
            btnGenerateCRCFile.Size = new Size(127, 23);
            btnGenerateCRCFile.TabIndex = 4;
            btnGenerateCRCFile.Text = "&Generate CRC file";
            btnGenerateCRCFile.UseVisualStyleBackColor = true;
            btnGenerateCRCFile.Click += btnGenerateCRCFile_Click;
            // 
            // btnVerifyCRCFile
            // 
            btnVerifyCRCFile.Location = new Point(239, 221);
            btnVerifyCRCFile.Name = "btnVerifyCRCFile";
            btnVerifyCRCFile.Size = new Size(75, 23);
            btnVerifyCRCFile.TabIndex = 5;
            btnVerifyCRCFile.Text = "&Verify CRC file";
            btnVerifyCRCFile.UseVisualStyleBackColor = true;
            btnVerifyCRCFile.Click += btnVerifyCRCFile_Click;
            // 
            // optSingleCRCFile
            // 
            optSingleCRCFile.AutoSize = true;
            optSingleCRCFile.Checked = true;
            optSingleCRCFile.Location = new Point(108, 191);
            optSingleCRCFile.Name = "optSingleCRCFile";
            optSingleCRCFile.Size = new Size(76, 19);
            optSingleCRCFile.TabIndex = 6;
            optSingleCRCFile.TabStop = true;
            optSingleCRCFile.Text = "Single file";
            optSingleCRCFile.UseVisualStyleBackColor = true;
            optSingleCRCFile.CheckedChanged += optSingleCRCFile_CheckedChanged;
            // 
            // optCRCFilePerRootFolder
            // 
            optCRCFilePerRootFolder.AutoSize = true;
            optCRCFilePerRootFolder.Location = new Point(108, 166);
            optCRCFilePerRootFolder.Name = "optCRCFilePerRootFolder";
            optCRCFilePerRootFolder.Size = new Size(285, 19);
            optCRCFilePerRootFolder.TabIndex = 7;
            optCRCFilePerRootFolder.Text = "One per root folder (named [root folder].SHA512)";
            optCRCFilePerRootFolder.UseVisualStyleBackColor = true;
            // 
            // btnSelectCRCPath
            // 
            btnSelectCRCPath.Location = new Point(657, 186);
            btnSelectCRCPath.Name = "btnSelectCRCPath";
            btnSelectCRCPath.Size = new Size(29, 23);
            btnSelectCRCPath.TabIndex = 8;
            btnSelectCRCPath.Text = "...";
            btnSelectCRCPath.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 247);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 9;
            label3.Text = "Processing";
            // 
            // lblProcessingFile
            // 
            lblProcessingFile.AutoSize = true;
            lblProcessingFile.Location = new Point(106, 247);
            lblProcessingFile.Name = "lblProcessingFile";
            lblProcessingFile.Size = new Size(0, 15);
            lblProcessingFile.TabIndex = 10;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 280);
            Controls.Add(lblProcessingFile);
            Controls.Add(label3);
            Controls.Add(btnSelectCRCPath);
            Controls.Add(optCRCFilePerRootFolder);
            Controls.Add(optSingleCRCFile);
            Controls.Add(btnVerifyCRCFile);
            Controls.Add(btnGenerateCRCFile);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCRCFilePath);
            Controls.Add(txtSourceFolders);
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CRC tools";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSourceFolders;
        private TextBox txtCRCFilePath;
        private Label label1;
        private Label label2;
        private Button btnGenerateCRCFile;
        private Button btnVerifyCRCFile;
        private RadioButton optSingleCRCFile;
        private RadioButton optCRCFilePerRootFolder;
        private Button btnSelectCRCPath;
        private Label label3;
        private Label lblProcessingFile;
    }
}
