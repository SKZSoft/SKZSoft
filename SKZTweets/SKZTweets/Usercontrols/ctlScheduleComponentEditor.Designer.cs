namespace SKZTweets.Usercontrols
{
    partial class ctlScheduleComponentEditor
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
            this.btnRemoveComponent = new System.Windows.Forms.Button();
            this.btnAddComponentBefore = new System.Windows.Forms.Button();
            this.btnAddComponentAfter = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnRemoveComponent
            // 
            this.btnRemoveComponent.Image = global::SKZTweets.Properties.Resources.DeleteClause_16x;
            this.btnRemoveComponent.Location = new System.Drawing.Point(393, 0);
            this.btnRemoveComponent.Name = "btnRemoveComponent";
            this.btnRemoveComponent.Size = new System.Drawing.Size(27, 23);
            this.btnRemoveComponent.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnRemoveComponent, "Delete this section");
            this.btnRemoveComponent.UseVisualStyleBackColor = true;
            this.btnRemoveComponent.Click += new System.EventHandler(this.btnRemoveComponent_Click);
            // 
            // btnAddComponentBefore
            // 
            this.btnAddComponentBefore.Image = global::SKZTweets.Properties.Resources.TextSpaceBefore_16x;
            this.btnAddComponentBefore.Location = new System.Drawing.Point(327, 0);
            this.btnAddComponentBefore.Name = "btnAddComponentBefore";
            this.btnAddComponentBefore.Size = new System.Drawing.Size(27, 23);
            this.btnAddComponentBefore.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAddComponentBefore, "Insert a new section above this one");
            this.btnAddComponentBefore.UseVisualStyleBackColor = true;
            this.btnAddComponentBefore.Click += new System.EventHandler(this.btnAddComponentBefore_Click);
            // 
            // btnAddComponentAfter
            // 
            this.btnAddComponentAfter.Image = global::SKZTweets.Properties.Resources.TextSpaceAfter_16x;
            this.btnAddComponentAfter.Location = new System.Drawing.Point(360, 0);
            this.btnAddComponentAfter.Name = "btnAddComponentAfter";
            this.btnAddComponentAfter.Size = new System.Drawing.Size(27, 23);
            this.btnAddComponentAfter.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnAddComponentAfter, "Insert a new section below this one");
            this.btnAddComponentAfter.UseVisualStyleBackColor = true;
            this.btnAddComponentAfter.Click += new System.EventHandler(this.btnAddComponentAfter_Click);
            // 
            // ctlScheduleComponentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddComponentAfter);
            this.Controls.Add(this.btnAddComponentBefore);
            this.Controls.Add(this.btnRemoveComponent);
            this.Name = "ctlScheduleComponentEditor";
            this.Size = new System.Drawing.Size(427, 25);
            this.Controls.SetChildIndex(this.btnRemoveComponent, 0);
            this.Controls.SetChildIndex(this.btnAddComponentBefore, 0);
            this.Controls.SetChildIndex(this.btnAddComponentAfter, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddComponentBefore;
        private System.Windows.Forms.Button btnRemoveComponent;
        private System.Windows.Forms.Button btnAddComponentAfter;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
