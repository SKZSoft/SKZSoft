namespace SKZTweets.Forms
{
    partial class frmScheduleEditor
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
            this.schedule1 = new SKZTweets.Usercontrols.Schedule();
            this.SuspendLayout();
            // 
            // schedule1
            // 
            this.schedule1.Location = new System.Drawing.Point(0, 0);
            this.schedule1.Name = "schedule1";
            this.schedule1.Size = new System.Drawing.Size(436, 26);
            this.schedule1.TabIndex = 0;
            this.schedule1.RequestResize += new System.EventHandler<SKZTweets.ResizedArgs>(this.schedule1_RequestResize);
            // 
            // frmScheduleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 261);
            this.Controls.Add(this.schedule1);
            this.Name = "frmScheduleEditor";
            this.Text = "Create Schedule";
            this.Load += new System.EventHandler(this.frmScheduleEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Usercontrols.Schedule schedule1;
    }
}