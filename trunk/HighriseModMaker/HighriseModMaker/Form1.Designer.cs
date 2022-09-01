namespace HighriseModMaker
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
            this.picPicToUse = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.picMod = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.picBackGroundColor = new System.Windows.Forms.PictureBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkBorder = new System.Windows.Forms.CheckBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picPicToUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGroundColor)).BeginInit();
            this.SuspendLayout();
            // 
            // picPicToUse
            // 
            this.picPicToUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picPicToUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPicToUse.Location = new System.Drawing.Point(638, 12);
            this.picPicToUse.Name = "picPicToUse";
            this.picPicToUse.Size = new System.Drawing.Size(100, 100);
            this.picPicToUse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPicToUse.TabIndex = 0;
            this.picPicToUse.TabStop = false;
            this.picPicToUse.DragDrop += new System.Windows.Forms.DragEventHandler(this.picPicToUse_DragDrop);
            this.picPicToUse.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rooms Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rooms Height";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(115, 9);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(27, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.Text = "1";
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(115, 38);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(27, 23);
            this.txtHeight.TabIndex = 4;
            this.txtHeight.Text = "1";
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            // 
            // picMod
            // 
            this.picMod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMod.Location = new System.Drawing.Point(115, 135);
            this.picMod.Name = "picMod";
            this.picMod.Size = new System.Drawing.Size(230, 169);
            this.picMod.TabIndex = 5;
            this.picMod.TabStop = false;
            this.picMod.Click += new System.EventHandler(this.picMod_Click);
            this.picMod.Paint += new System.Windows.Forms.PaintEventHandler(this.picMod_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "BackGround Color";
            // 
            // picBackGroundColor
            // 
            this.picBackGroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBackGroundColor.Location = new System.Drawing.Point(276, 12);
            this.picBackGroundColor.Name = "picBackGroundColor";
            this.picBackGroundColor.Size = new System.Drawing.Size(20, 20);
            this.picBackGroundColor.TabIndex = 7;
            this.picBackGroundColor.TabStop = false;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(255, 41);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(41, 23);
            this.txtSize.TabIndex = 10;
            this.txtSize.Text = "7";
            this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSize.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Pic Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "%";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(396, 38);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(53, 23);
            this.txtY.TabIndex = 15;
            this.txtY.Text = "1";
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY.TextChanged += new System.EventHandler(this.txtY_TextChanged);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(396, 9);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(53, 23);
            this.txtX.TabIndex = 14;
            this.txtX.Text = "1";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtX.TextChanged += new System.EventHandler(this.txtX_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "X";
            // 
            // chkBorder
            // 
            this.chkBorder.AutoSize = true;
            this.chkBorder.Location = new System.Drawing.Point(481, 9);
            this.chkBorder.Name = "chkBorder";
            this.chkBorder.Size = new System.Drawing.Size(61, 19);
            this.chkBorder.TabIndex = 16;
            this.chkBorder.Text = "Border";
            this.chkBorder.UseVisualStyleBackColor = true;
            this.chkBorder.CheckedChanged += new System.EventHandler(this.chkBorder_CheckedChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(95, 135);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 169);
            this.vScrollBar1.TabIndex = 17;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(115, 115);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(230, 17);
            this.hScrollBar1.TabIndex = 18;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 475);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.chkBorder);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picBackGroundColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picMod);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picPicToUse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picPicToUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGroundColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picPicToUse;
        private Label label1;
        private Label label2;
        private TextBox txtWidth;
        private TextBox txtHeight;
        private PictureBox picMod;
        private ColorDialog colorDialog1;
        private Label label3;
        private PictureBox picBackGroundColor;
        private TextBox txtSize;
        private Label label5;
        private Label label4;
        private TextBox txtY;
        private TextBox txtX;
        private Label label6;
        private Label label7;
        private CheckBox chkBorder;
        private VScrollBar vScrollBar1;
        private HScrollBar hScrollBar1;
    }
}