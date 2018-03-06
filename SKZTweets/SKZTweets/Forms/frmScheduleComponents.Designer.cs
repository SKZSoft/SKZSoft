namespace SKZTweets.Forms
{
    partial class frmScheduleComponents
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
            this.ctlScheduleComponent1 = new SKZTweets.Usercontrols.ctlScheduleComponent();
            this.SuspendLayout();
            // 
            // ctlScheduleComponent1
            // 
            this.ctlScheduleComponent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlScheduleComponent1.Location = new System.Drawing.Point(12, 12);
            this.ctlScheduleComponent1.Name = "ctlScheduleComponent1";
            this.ctlScheduleComponent1.Size = new System.Drawing.Size(332, 22);
            this.ctlScheduleComponent1.TabIndex = 0;
            // 
            // frmScheduleComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 108);
            this.Controls.Add(this.ctlScheduleComponent1);
            this.Name = "frmScheduleComponents";
            this.Text = "frmScheduleComponents";
            this.ResumeLayout(false);

        }

        #endregion

        private Usercontrols.ctlScheduleComponent ctlScheduleComponent1;
    }
}