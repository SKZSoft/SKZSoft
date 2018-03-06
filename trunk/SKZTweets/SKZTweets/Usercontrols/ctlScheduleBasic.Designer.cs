namespace SKZTweets.Usercontrols
{
    partial class ctlScheduleBasic
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStartAt = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtEndAt = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting at";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "retweet every";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "until";
            // 
            // txtStartAt
            // 
            this.txtStartAt.Location = new System.Drawing.Point(79, 3);
            this.txtStartAt.Name = "txtStartAt";
            this.txtStartAt.Size = new System.Drawing.Size(41, 20);
            this.txtStartAt.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtStartAt, "Time to start");
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(80, 29);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(40, 20);
            this.txtInterval.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtInterval, "Minutes between retweets");
            // 
            // txtEndAt
            // 
            this.txtEndAt.Location = new System.Drawing.Point(80, 55);
            this.txtEndAt.Name = "txtEndAt";
            this.txtEndAt.Size = new System.Drawing.Size(40, 20);
            this.txtEndAt.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtEndAt, "Time to stop at. If this is earlier than the\r\nstart time, it will be treated as t" +
        "omorrow.");
            // 
            // ctlScheduleBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEndAt);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtStartAt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctlScheduleBasic";
            this.Size = new System.Drawing.Size(183, 82);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStartAt;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TextBox txtEndAt;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
