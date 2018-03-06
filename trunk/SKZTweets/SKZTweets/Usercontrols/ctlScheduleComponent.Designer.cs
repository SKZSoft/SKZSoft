namespace SKZTweets.Usercontrols
{
    partial class ctlScheduleComponent
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
            this.lblThenEvery = new System.Windows.Forms.Label();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblThenEvery
            // 
            this.lblThenEvery.AutoSize = true;
            this.lblThenEvery.Location = new System.Drawing.Point(122, 3);
            this.lblThenEvery.Name = "lblThenEvery";
            this.lblThenEvery.Size = new System.Drawing.Size(33, 13);
            this.lblThenEvery.TabIndex = 0;
            this.lblThenEvery.Text = "every";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Location = new System.Drawing.Point(161, 0);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(26, 20);
            this.txtMinutes.TabIndex = 1;
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(193, 3);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(65, 13);
            this.lblMinutes.TabIndex = 2;
            this.lblMinutes.Text = "minutes until";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(59, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(57, 20);
            this.dtpStartTime.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dtpStartTime, "The time to stop this part of the schedule");
            this.dtpStartTime.Value = new System.DateTime(2017, 3, 8, 0, 0, 0, 0);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(264, 0);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(57, 20);
            this.dtpEndTime.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dtpEndTime, "The time to stop this part of the schedule");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "starting at";
            // 
            // ctlScheduleComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.txtMinutes);
            this.Controls.Add(this.lblThenEvery);
            this.Name = "ctlScheduleComponent";
            this.Size = new System.Drawing.Size(332, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblThenEvery;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
    }
}
