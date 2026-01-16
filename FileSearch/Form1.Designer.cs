namespace FileSearch
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
            txtPath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtIncludeOnly = new TextBox();
            txtExclude = new TextBox();
            label3 = new Label();
            btnSearch = new Button();
            grdResults = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)grdResults).BeginInit();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Location = new Point(114, 3);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(584, 23);
            txtPath.TabIndex = 0;
            txtPath.Text = "a:\\";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "Path";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 43);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 2;
            label2.Text = "Include Only";
            // 
            // txtIncludeOnly
            // 
            txtIncludeOnly.Location = new Point(114, 43);
            txtIncludeOnly.Multiline = true;
            txtIncludeOnly.Name = "txtIncludeOnly";
            txtIncludeOnly.Size = new Size(229, 150);
            txtIncludeOnly.TabIndex = 3;
            txtIncludeOnly.Text = ".avi";
            // 
            // txtExclude
            // 
            txtExclude.Location = new Point(469, 43);
            txtExclude.Multiline = true;
            txtExclude.Name = "txtExclude";
            txtExclude.Size = new Size(229, 150);
            txtExclude.TabIndex = 5;
            txtExclude.Text = "\\r\\";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(416, 43);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 4;
            label3.Text = "Exclude";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(114, 199);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "&Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // grdResults
            // 
            grdResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grdResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdResults.Location = new Point(12, 228);
            grdResults.Name = "grdResults";
            grdResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdResults.Size = new Size(740, 337);
            grdResults.TabIndex = 7;
            grdResults.CellDoubleClick += grdResults_CellDoubleClick;
            grdResults.DoubleClick += grdResults_DoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(764, 577);
            Controls.Add(grdResults);
            Controls.Add(btnSearch);
            Controls.Add(txtExclude);
            Controls.Add(label3);
            Controls.Add(txtIncludeOnly);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPath);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)grdResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPath;
        private Label label1;
        private Label label2;
        private TextBox txtIncludeOnly;
        private TextBox txtExclude;
        private Label label3;
        private Button btnSearch;
        private DataGridView grdResults;
    }
}
