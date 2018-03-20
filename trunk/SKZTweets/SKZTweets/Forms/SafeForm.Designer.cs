namespace SKZSoft.SKZTweets
{
    partial class SafeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SafeForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslScreenName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.useThisAccountOnAllFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useAccountColourOnFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslScreenName,
            this.toolStripSplitButton1});
            this.statusStrip.Location = new System.Drawing.Point(0, 312);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(627, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsslScreenName
            // 
            this.tsslScreenName.Name = "tsslScreenName";
            this.tsslScreenName.Size = new System.Drawing.Size(64, 17);
            this.tsslScreenName.Text = "credentials";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useAccountColourOnFormsToolStripMenuItem,
            this.useThisAccountOnAllFormsToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // useThisAccountOnAllFormsToolStripMenuItem
            // 
            this.useThisAccountOnAllFormsToolStripMenuItem.Name = "useThisAccountOnAllFormsToolStripMenuItem";
            this.useThisAccountOnAllFormsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.useThisAccountOnAllFormsToolStripMenuItem.Text = "Use this account on all forms";
            // 
            // useAccountColourOnFormsToolStripMenuItem
            // 
            this.useAccountColourOnFormsToolStripMenuItem.Name = "useAccountColourOnFormsToolStripMenuItem";
            this.useAccountColourOnFormsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.useAccountColourOnFormsToolStripMenuItem.Text = "Use account colour on forms";
            // 
            // SafeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 334);
            this.Controls.Add(this.statusStrip);
            this.Name = "SafeForm";
            this.Text = "SafeForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SafeForm_FormClosed);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslScreenName;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem useThisAccountOnAllFormsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useAccountColourOnFormsToolStripMenuItem;
    }
}