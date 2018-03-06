namespace SKZTweets.Usercontrols
{
    partial class Schedule
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlScheduleComponentEditor1 = new SKZTweets.Usercontrols.ctlScheduleComponentEditor();
            this.SuspendLayout();
            // 
            // ctlScheduleComponentEditor1
            // 
            this.ctlScheduleComponentEditor1.Location = new System.Drawing.Point(0, 0);
            this.ctlScheduleComponentEditor1.Name = "ctlScheduleComponentEditor1";
            this.ctlScheduleComponentEditor1.Size = new System.Drawing.Size(427, 25);
            this.ctlScheduleComponentEditor1.TabIndex = 0;
            // 
            // Schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlScheduleComponentEditor1);
            this.Name = "Schedule";
            this.Size = new System.Drawing.Size(436, 26);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private ctlScheduleComponentEditor ctlScheduleComponentEditor1;
    }
}
