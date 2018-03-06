namespace SKZSoft.Common.Logging.GUI
{
    partial class ctlLoggingSettings
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
            this.grpLevel = new System.Windows.Forms.GroupBox();
            this.optDebug = new System.Windows.Forms.RadioButton();
            this.optAPIActions = new System.Windows.Forms.RadioButton();
            this.optWarningsAndErrors = new System.Windows.Forms.RadioButton();
            this.optErrorsOnly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeleteAfter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLevel
            // 
            this.grpLevel.Controls.Add(this.optDebug);
            this.grpLevel.Controls.Add(this.optAPIActions);
            this.grpLevel.Controls.Add(this.optWarningsAndErrors);
            this.grpLevel.Controls.Add(this.optErrorsOnly);
            this.grpLevel.Location = new System.Drawing.Point(3, 3);
            this.grpLevel.Name = "grpLevel";
            this.grpLevel.Size = new System.Drawing.Size(135, 116);
            this.grpLevel.TabIndex = 2;
            this.grpLevel.TabStop = false;
            this.grpLevel.Text = "Logging Level";
            // 
            // optDebug
            // 
            this.optDebug.Location = new System.Drawing.Point(6, 86);
            this.optDebug.Name = "optDebug";
            this.optDebug.Size = new System.Drawing.Size(104, 24);
            this.optDebug.TabIndex = 6;
            this.optDebug.TabStop = true;
            this.optDebug.Text = "&Debug";
            this.optDebug.UseVisualStyleBackColor = true;
            // 
            // optAPIActions
            // 
            this.optAPIActions.Location = new System.Drawing.Point(6, 66);
            this.optAPIActions.Name = "optAPIActions";
            this.optAPIActions.Size = new System.Drawing.Size(104, 24);
            this.optAPIActions.TabIndex = 5;
            this.optAPIActions.TabStop = true;
            this.optAPIActions.Text = "&API Actions";
            this.optAPIActions.UseVisualStyleBackColor = true;
            // 
            // optWarningsAndErrors
            // 
            this.optWarningsAndErrors.Location = new System.Drawing.Point(6, 41);
            this.optWarningsAndErrors.Name = "optWarningsAndErrors";
            this.optWarningsAndErrors.Size = new System.Drawing.Size(123, 24);
            this.optWarningsAndErrors.TabIndex = 4;
            this.optWarningsAndErrors.TabStop = true;
            this.optWarningsAndErrors.Text = "&Warnings and Errors";
            this.optWarningsAndErrors.UseVisualStyleBackColor = true;
            // 
            // optErrorsOnly
            // 
            this.optErrorsOnly.Location = new System.Drawing.Point(6, 19);
            this.optErrorsOnly.Name = "optErrorsOnly";
            this.optErrorsOnly.Size = new System.Drawing.Size(104, 24);
            this.optErrorsOnly.TabIndex = 3;
            this.optErrorsOnly.TabStop = true;
            this.optErrorsOnly.Text = "&Errors only";
            this.optErrorsOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Delete logs after";
            // 
            // txtDeleteAfter
            // 
            this.txtDeleteAfter.Location = new System.Drawing.Point(96, 128);
            this.txtDeleteAfter.Name = "txtDeleteAfter";
            this.txtDeleteAfter.Size = new System.Drawing.Size(42, 20);
            this.txtDeleteAfter.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "days";
            // 
            // ctlLoggingSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeleteAfter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpLevel);
            this.Name = "ctlLoggingSettings";
            this.Size = new System.Drawing.Size(180, 162);
            this.grpLevel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLevel;
        private System.Windows.Forms.RadioButton optErrorsOnly;
        private System.Windows.Forms.RadioButton optDebug;
        private System.Windows.Forms.RadioButton optAPIActions;
        private System.Windows.Forms.RadioButton optWarningsAndErrors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeleteAfter;
        private System.Windows.Forms.Label label2;
    }
}
