﻿namespace OpenRandom
{
    partial class Form1
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
            txtWildcardsVideo = new TextBox();
            button1 = new Button();
            txtPath = new TextBox();
            txtWildCardsPics = new TextBox();
            chkVideos = new CheckBox();
            chkPics = new CheckBox();
            txtMinPicSize = new TextBox();
            lblMinSize = new Label();
            lstOpened = new ListBox();
            button2 = new Button();
            label1 = new Label();
            lblSearching = new Label();
            label2 = new Label();
            txtExclude = new TextBox();
            SuspendLayout();
            // 
            // txtWildcardsVideo
            // 
            txtWildcardsVideo.Location = new Point(196, 46);
            txtWildcardsVideo.Name = "txtWildcardsVideo";
            txtWildcardsVideo.Size = new Size(339, 23);
            txtWildcardsVideo.TabIndex = 2;
            txtWildcardsVideo.Text = "*.avi;*.mp4";
            // 
            // button1
            // 
            button1.Location = new Point(196, 143);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "Open random";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtPath
            // 
            txtPath.Location = new Point(196, 17);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(339, 23);
            txtPath.TabIndex = 0;
            // 
            // txtWildCardsPics
            // 
            txtWildCardsPics.Location = new Point(196, 75);
            txtWildCardsPics.Name = "txtWildCardsPics";
            txtWildCardsPics.Size = new Size(339, 23);
            txtWildCardsPics.TabIndex = 4;
            txtWildCardsPics.Text = "*.jpg;*.png";
            // 
            // chkVideos
            // 
            chkVideos.AutoSize = true;
            chkVideos.Checked = true;
            chkVideos.CheckState = CheckState.Checked;
            chkVideos.Location = new Point(118, 46);
            chkVideos.Name = "chkVideos";
            chkVideos.Size = new Size(60, 19);
            chkVideos.TabIndex = 1;
            chkVideos.Text = "videos";
            chkVideos.UseVisualStyleBackColor = true;
            // 
            // chkPics
            // 
            chkPics.AutoSize = true;
            chkPics.Location = new Point(118, 75);
            chkPics.Name = "chkPics";
            chkPics.Size = new Size(47, 19);
            chkPics.TabIndex = 3;
            chkPics.Text = "Pics";
            chkPics.UseVisualStyleBackColor = true;
            chkPics.CheckedChanged += chkPics_CheckedChanged;
            // 
            // txtMinPicSize
            // 
            txtMinPicSize.Location = new Point(541, 75);
            txtMinPicSize.Name = "txtMinPicSize";
            txtMinPicSize.Size = new Size(46, 23);
            txtMinPicSize.TabIndex = 5;
            txtMinPicSize.Text = "100";
            // 
            // lblMinSize
            // 
            lblMinSize.AutoSize = true;
            lblMinSize.Location = new Point(593, 78);
            lblMinSize.Name = "lblMinSize";
            lblMinSize.Size = new Size(21, 15);
            lblMinSize.TabIndex = 7;
            lblMinSize.Text = "KB";
            // 
            // lstOpened
            // 
            lstOpened.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstOpened.FormattingEnabled = true;
            lstOpened.ItemHeight = 15;
            lstOpened.Location = new Point(196, 181);
            lstOpened.Name = "lstOpened";
            lstOpened.Size = new Size(474, 319);
            lstOpened.TabIndex = 9;
            lstOpened.DoubleClick += lstOpened_DoubleClick;
            lstOpened.KeyDown += lstOpened_KeyDown;
            lstOpened.KeyPress += lstOpened_KeyPress;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(685, 181);
            button2.Name = "button2";
            button2.Size = new Size(110, 23);
            button2.TabIndex = 10;
            button2.Text = "Open Folder";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 20);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 10;
            label1.Text = "Path";
            // 
            // lblSearching
            // 
            lblSearching.AutoSize = true;
            lblSearching.Location = new Point(277, 143);
            lblSearching.Name = "lblSearching";
            lblSearching.Size = new Size(475, 15);
            lblSearching.TabIndex = 11;
            lblSearching.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 107);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 6;
            label2.Text = "Exclude";
            // 
            // txtExclude
            // 
            txtExclude.Location = new Point(196, 104);
            txtExclude.Name = "txtExclude";
            txtExclude.Size = new Size(339, 23);
            txtExclude.TabIndex = 7;
            // 
            // Form1
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 525);
            Controls.Add(label2);
            Controls.Add(txtExclude);
            Controls.Add(lblSearching);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(lstOpened);
            Controls.Add(lblMinSize);
            Controls.Add(txtMinPicSize);
            Controls.Add(chkPics);
            Controls.Add(chkVideos);
            Controls.Add(txtWildCardsPics);
            Controls.Add(txtPath);
            Controls.Add(button1);
            Controls.Add(txtWildcardsVideo);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtWildcardsVideo;
        private Button button1;
        private TextBox txtPath;
        private TextBox txtWildCardsPics;
        private CheckBox chkVideos;
        private CheckBox chkPics;
        private TextBox txtMinPicSize;
        private Label lblMinSize;
        private ListBox lstOpened;
        private Button button2;
        private Label label1;
        private Label lblSearching;
        private Label label2;
        private TextBox txtExclude;
    }
}