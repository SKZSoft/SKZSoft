namespace DupeFinder
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
            txtPaths = new TextBox();
            label1 = new Label();
            btnCheck = new Button();
            lblStatus = new Label();
            grid = new DataGridView();
            button1 = new Button();
            btnDelete = new Button();
            lblCount = new Label();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // txtPaths
            // 
            txtPaths.Location = new Point(47, 12);
            txtPaths.Multiline = true;
            txtPaths.Name = "txtPaths";
            txtPaths.Size = new Size(305, 48);
            txtPaths.TabIndex = 0;
            txtPaths.Text = "K:\r\nL:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 32);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 1;
            label1.Text = "Paths";
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(141, 72);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(75, 23);
            btnCheck.TabIndex = 2;
            btnCheck.Text = "Check";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += button1_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(356, 72);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(25, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "aaa";
            // 
            // grid
            // 
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(2, 101);
            grid.Name = "grid";
            grid.RowTemplate.Height = 25;
            grid.Size = new Size(1256, 535);
            grid.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(12, 72);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(222, 72);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new Point(358, 45);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(25, 15);
            lblCount.TabIndex = 7;
            lblCount.Text = "aaa";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1270, 671);
            Controls.Add(lblCount);
            Controls.Add(btnDelete);
            Controls.Add(button1);
            Controls.Add(grid);
            Controls.Add(lblStatus);
            Controls.Add(btnCheck);
            Controls.Add(label1);
            Controls.Add(txtPaths);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox txtPaths;
        private Label label1;
        private Button btnCheck;
        private Label lblStatus;
        private DataGridView grid;
        private Button button1;
        private Button btnDelete;
        private Label lblCount;
    }
}