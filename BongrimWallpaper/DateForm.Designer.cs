namespace BongrimWallpaper
{
    partial class DateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateForm));
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.setFontBtn = new System.Windows.Forms.Button();
            this.xBar = new System.Windows.Forms.TrackBar();
            this.yBar = new System.Windows.Forms.TrackBar();
            this.xCenterBtn = new System.Windows.Forms.Button();
            this.yCenterBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fontBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sizeBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.colorBox = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateOutputLbl = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateFormatBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.dateVisibleCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Location = new System.Drawing.Point(38, 52);
            this.previewBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(355, 262);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            this.previewBox.Click += new System.EventHandler(this.previewBox_Click);
            // 
            // setFontBtn
            // 
            this.setFontBtn.Location = new System.Drawing.Point(9, 57);
            this.setFontBtn.Name = "setFontBtn";
            this.setFontBtn.Size = new System.Drawing.Size(257, 23);
            this.setFontBtn.TabIndex = 2;
            this.setFontBtn.Text = "글꼴 설정";
            this.setFontBtn.UseVisualStyleBackColor = true;
            this.setFontBtn.Click += new System.EventHandler(this.setFontBtn_Click);
            // 
            // xBar
            // 
            this.xBar.Location = new System.Drawing.Point(25, 22);
            this.xBar.Name = "xBar";
            this.xBar.Size = new System.Drawing.Size(380, 45);
            this.xBar.TabIndex = 3;
            this.xBar.TickFrequency = 0;
            this.xBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.xBar.Scroll += new System.EventHandler(this.xBar_Scroll);
            // 
            // yBar
            // 
            this.yBar.Location = new System.Drawing.Point(8, 41);
            this.yBar.Name = "yBar";
            this.yBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.yBar.Size = new System.Drawing.Size(45, 282);
            this.yBar.TabIndex = 4;
            this.yBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.yBar.Value = 10;
            this.yBar.Scroll += new System.EventHandler(this.yBar_Scroll);
            // 
            // xCenterBtn
            // 
            this.xCenterBtn.Location = new System.Drawing.Point(402, 20);
            this.xCenterBtn.Name = "xCenterBtn";
            this.xCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.xCenterBtn.TabIndex = 5;
            this.xCenterBtn.Text = "C";
            this.xCenterBtn.UseVisualStyleBackColor = true;
            this.xCenterBtn.Click += new System.EventHandler(this.xCenterBtn_Click);
            // 
            // yCenterBtn
            // 
            this.yCenterBtn.Location = new System.Drawing.Point(3, 321);
            this.yCenterBtn.Name = "yCenterBtn";
            this.yCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.yCenterBtn.TabIndex = 6;
            this.yCenterBtn.Text = "C";
            this.yCenterBtn.UseVisualStyleBackColor = true;
            this.yCenterBtn.Click += new System.EventHandler(this.yCenterBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Controls.Add(this.xCenterBtn);
            this.groupBox1.Controls.Add(this.xBar);
            this.groupBox1.Controls.Add(this.yCenterBtn);
            this.groupBox1.Controls.Add(this.yBar);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 350);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "미리보기";
            // 
            // fontBox
            // 
            this.fontBox.BackColor = System.Drawing.Color.White;
            this.fontBox.Location = new System.Drawing.Point(39, 25);
            this.fontBox.Name = "fontBox";
            this.fontBox.ReadOnly = true;
            this.fontBox.Size = new System.Drawing.Size(90, 23);
            this.fontBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "폰트";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "크기";
            // 
            // sizeBox
            // 
            this.sizeBox.BackColor = System.Drawing.Color.White;
            this.sizeBox.Location = new System.Drawing.Point(171, 25);
            this.sizeBox.Name = "sizeBox";
            this.sizeBox.ReadOnly = true;
            this.sizeBox.Size = new System.Drawing.Size(41, 23);
            this.sizeBox.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.colorBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.sizeBox);
            this.groupBox2.Controls.Add(this.setFontBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.fontBox);
            this.groupBox2.Location = new System.Drawing.Point(448, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 90);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "글꼴";
            // 
            // colorBox
            // 
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(243, 25);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(23, 23);
            this.colorBox.TabIndex = 15;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "색";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateOutputLbl);
            this.groupBox3.Controls.Add(this.testBtn);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dateFormatBox);
            this.groupBox3.Location = new System.Drawing.Point(448, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 99);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "날짜 유형";
            // 
            // dateOutputLbl
            // 
            this.dateOutputLbl.AutoSize = true;
            this.dateOutputLbl.Location = new System.Drawing.Point(8, 72);
            this.dateOutputLbl.Name = "dateOutputLbl";
            this.dateOutputLbl.Size = new System.Drawing.Size(0, 15);
            this.dateOutputLbl.TabIndex = 3;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(200, 42);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(66, 23);
            this.testBtn.TabIndex = 2;
            this.testBtn.Text = "테스트";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "날짜 형식 (yyyy=년도 MM=월 dd=일)";
            // 
            // dateFormatBox
            // 
            this.dateFormatBox.BackColor = System.Drawing.Color.White;
            this.dateFormatBox.Location = new System.Drawing.Point(9, 42);
            this.dateFormatBox.Name = "dateFormatBox";
            this.dateFormatBox.Size = new System.Drawing.Size(185, 23);
            this.dateFormatBox.TabIndex = 0;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(448, 334);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(267, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "설정 저장";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // dateVisibleCheck
            // 
            this.dateVisibleCheck.AutoSize = true;
            this.dateVisibleCheck.Checked = true;
            this.dateVisibleCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dateVisibleCheck.Location = new System.Drawing.Point(448, 12);
            this.dateVisibleCheck.Name = "dateVisibleCheck";
            this.dateVisibleCheck.Size = new System.Drawing.Size(90, 19);
            this.dateVisibleCheck.TabIndex = 14;
            this.dateVisibleCheck.Text = "날짜 활성화";
            this.dateVisibleCheck.UseVisualStyleBackColor = true;
            this.dateVisibleCheck.CheckedChanged += new System.EventHandler(this.dateVisibleCheck_CheckedChanged);
            // 
            // DateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(725, 362);
            this.Controls.Add(this.dateVisibleCheck);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.Load += new System.EventHandler(this.DateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button setFontBtn;
        private System.Windows.Forms.TrackBar xBar;
        private System.Windows.Forms.TrackBar yBar;
        private System.Windows.Forms.Button xCenterBtn;
        private System.Windows.Forms.Button yCenterBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fontBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sizeBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dateFormatBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Panel colorBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label dateOutputLbl;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.CheckBox dateVisibleCheck;
    }
}