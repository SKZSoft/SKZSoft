namespace SKZTweets.Usercontrols
{
    partial class ctlTweetNumbering
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
            this.cmbNumberPosition = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbNumberStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbNumberPosition
            // 
            this.cmbNumberPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumberPosition.FormattingEnabled = true;
            this.cmbNumberPosition.Location = new System.Drawing.Point(199, 2);
            this.cmbNumberPosition.Name = "cmbNumberPosition";
            this.cmbNumberPosition.Size = new System.Drawing.Size(164, 21);
            this.cmbNumberPosition.Sorted = true;
            this.cmbNumberPosition.TabIndex = 25;
            this.cmbNumberPosition.SelectionChangeCommitted += new System.EventHandler(this.cmbNumberPosition_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Position";
            // 
            // cmbNumberStyle
            // 
            this.cmbNumberStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumberStyle.FormattingEnabled = true;
            this.cmbNumberStyle.Location = new System.Drawing.Point(36, 3);
            this.cmbNumberStyle.Name = "cmbNumberStyle";
            this.cmbNumberStyle.Size = new System.Drawing.Size(107, 21);
            this.cmbNumberStyle.Sorted = true;
            this.cmbNumberStyle.TabIndex = 23;
            this.cmbNumberStyle.SelectionChangeCommitted += new System.EventHandler(this.cmbNumberStyle_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Style";
            // 
            // ctlTweetNumbering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbNumberPosition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbNumberStyle);
            this.Controls.Add(this.label3);
            this.Name = "ctlTweetNumbering";
            this.Size = new System.Drawing.Size(377, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbNumberPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbNumberStyle;
        private System.Windows.Forms.Label label3;
    }
}
