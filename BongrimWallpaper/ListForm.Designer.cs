namespace BongrimWallpaper
{
    partial class ListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.listVisibleCheck = new System.Windows.Forms.CheckBox();
            this.layoutGroup = new System.Windows.Forms.GroupBox();
            this.listSpaceBox = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.horizontalBtn = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.verticalBtn = new System.Windows.Forms.RadioButton();
            this.saveBtn = new System.Windows.Forms.Button();
            this.fontGroup = new System.Windows.Forms.GroupBox();
            this.teacherGroup = new System.Windows.Forms.GroupBox();
            this.teacherAccentColorBox = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.teacherColorBox = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.teacherSizeBox = new System.Windows.Forms.TextBox();
            this.teacherFontBox = new System.Windows.Forms.TextBox();
            this.setTeacherFontBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.subjectGroup = new System.Windows.Forms.GroupBox();
            this.subjectAccentColorBox = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.subjectColorBox = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.subjectSizeBox = new System.Windows.Forms.TextBox();
            this.subjectFontBox = new System.Windows.Forms.TextBox();
            this.setSubjectFontBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xCenterBtn = new System.Windows.Forms.Button();
            this.yCenterBtn = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.yBar = new System.Windows.Forms.TrackBar();
            this.xBar = new System.Windows.Forms.TrackBar();
            this.teacherVisibleCheck = new System.Windows.Forms.CheckBox();
            this.layoutGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listSpaceBox)).BeginInit();
            this.fontGroup.SuspendLayout();
            this.teacherGroup.SuspendLayout();
            this.subjectGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).BeginInit();
            this.SuspendLayout();
            // 
            // listVisibleCheck
            // 
            this.listVisibleCheck.AutoSize = true;
            this.listVisibleCheck.Checked = true;
            this.listVisibleCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listVisibleCheck.Location = new System.Drawing.Point(448, 12);
            this.listVisibleCheck.Name = "listVisibleCheck";
            this.listVisibleCheck.Size = new System.Drawing.Size(194, 29);
            this.listVisibleCheck.TabIndex = 23;
            this.listVisibleCheck.Text = "과목 표시기 활성화";
            this.listVisibleCheck.UseVisualStyleBackColor = true;
            this.listVisibleCheck.CheckedChanged += new System.EventHandler(this.listVisibleCheck_CheckedChanged);
            // 
            // layoutGroup
            // 
            this.layoutGroup.Controls.Add(this.listSpaceBox);
            this.layoutGroup.Controls.Add(this.label9);
            this.layoutGroup.Controls.Add(this.panel3);
            this.layoutGroup.Controls.Add(this.panel4);
            this.layoutGroup.Controls.Add(this.horizontalBtn);
            this.layoutGroup.Controls.Add(this.panel2);
            this.layoutGroup.Controls.Add(this.panel1);
            this.layoutGroup.Controls.Add(this.verticalBtn);
            this.layoutGroup.Location = new System.Drawing.Point(445, 294);
            this.layoutGroup.Name = "layoutGroup";
            this.layoutGroup.Size = new System.Drawing.Size(289, 81);
            this.layoutGroup.TabIndex = 22;
            this.layoutGroup.TabStop = false;
            this.layoutGroup.Text = "레이아웃";
            // 
            // listSpaceBox
            // 
            this.listSpaceBox.Location = new System.Drawing.Point(219, 34);
            this.listSpaceBox.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.listSpaceBox.Name = "listSpaceBox";
            this.listSpaceBox.Size = new System.Drawing.Size(58, 31);
            this.listSpaceBox.TabIndex = 17;
            this.listSpaceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.listSpaceBox.ValueChanged += new System.EventHandler(this.listSpaceBox_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(182, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 25);
            this.label9.TabIndex = 16;
            this.label9.Text = "간격";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(119, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 20);
            this.panel3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(93, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 20);
            this.panel4.TabIndex = 6;
            // 
            // horizontalBtn
            // 
            this.horizontalBtn.AutoSize = true;
            this.horizontalBtn.Location = new System.Drawing.Point(73, 40);
            this.horizontalBtn.Name = "horizontalBtn";
            this.horizontalBtn.Size = new System.Drawing.Size(21, 20);
            this.horizontalBtn.TabIndex = 5;
            this.horizontalBtn.TabStop = true;
            this.horizontalBtn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(40, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 20);
            this.panel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(40, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 3;
            // 
            // verticalBtn
            // 
            this.verticalBtn.AutoSize = true;
            this.verticalBtn.Location = new System.Drawing.Point(15, 40);
            this.verticalBtn.Name = "verticalBtn";
            this.verticalBtn.Size = new System.Drawing.Size(21, 20);
            this.verticalBtn.TabIndex = 2;
            this.verticalBtn.TabStop = true;
            this.verticalBtn.UseVisualStyleBackColor = true;
            this.verticalBtn.CheckedChanged += new System.EventHandler(this.verticalBtn_CheckedChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(445, 406);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(289, 23);
            this.saveBtn.TabIndex = 19;
            this.saveBtn.Text = "설정 저장";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // fontGroup
            // 
            this.fontGroup.Controls.Add(this.teacherGroup);
            this.fontGroup.Controls.Add(this.subjectGroup);
            this.fontGroup.Location = new System.Drawing.Point(445, 30);
            this.fontGroup.Name = "fontGroup";
            this.fontGroup.Size = new System.Drawing.Size(289, 258);
            this.fontGroup.TabIndex = 21;
            this.fontGroup.TabStop = false;
            this.fontGroup.Text = "글꼴";
            // 
            // teacherGroup
            // 
            this.teacherGroup.Controls.Add(this.teacherAccentColorBox);
            this.teacherGroup.Controls.Add(this.label3);
            this.teacherGroup.Controls.Add(this.teacherColorBox);
            this.teacherGroup.Controls.Add(this.label4);
            this.teacherGroup.Controls.Add(this.label6);
            this.teacherGroup.Controls.Add(this.teacherSizeBox);
            this.teacherGroup.Controls.Add(this.teacherFontBox);
            this.teacherGroup.Controls.Add(this.setTeacherFontBtn);
            this.teacherGroup.Controls.Add(this.label8);
            this.teacherGroup.Location = new System.Drawing.Point(6, 141);
            this.teacherGroup.Name = "teacherGroup";
            this.teacherGroup.Size = new System.Drawing.Size(277, 113);
            this.teacherGroup.TabIndex = 18;
            this.teacherGroup.TabStop = false;
            this.teacherGroup.Text = "선생님";
            // 
            // teacherAccentColorBox
            // 
            this.teacherAccentColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacherAccentColorBox.Location = new System.Drawing.Point(143, 53);
            this.teacherAccentColorBox.Name = "teacherAccentColorBox";
            this.teacherAccentColorBox.Size = new System.Drawing.Size(23, 23);
            this.teacherAccentColorBox.TabIndex = 15;
            this.teacherAccentColorBox.Click += new System.EventHandler(this.teacherAccentColorBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "강조 색상";
            // 
            // teacherColorBox
            // 
            this.teacherColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacherColorBox.Location = new System.Drawing.Point(39, 53);
            this.teacherColorBox.Name = "teacherColorBox";
            this.teacherColorBox.Size = new System.Drawing.Size(23, 23);
            this.teacherColorBox.TabIndex = 13;
            this.teacherColorBox.Click += new System.EventHandler(this.teacherColorBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "색상";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "폰트";
            // 
            // teacherSizeBox
            // 
            this.teacherSizeBox.BackColor = System.Drawing.Color.White;
            this.teacherSizeBox.Location = new System.Drawing.Point(227, 25);
            this.teacherSizeBox.Name = "teacherSizeBox";
            this.teacherSizeBox.ReadOnly = true;
            this.teacherSizeBox.Size = new System.Drawing.Size(41, 31);
            this.teacherSizeBox.TabIndex = 11;
            // 
            // teacherFontBox
            // 
            this.teacherFontBox.BackColor = System.Drawing.Color.White;
            this.teacherFontBox.Location = new System.Drawing.Point(39, 25);
            this.teacherFontBox.Name = "teacherFontBox";
            this.teacherFontBox.ReadOnly = true;
            this.teacherFontBox.Size = new System.Drawing.Size(146, 31);
            this.teacherFontBox.TabIndex = 8;
            // 
            // setTeacherFontBtn
            // 
            this.setTeacherFontBtn.Location = new System.Drawing.Point(6, 82);
            this.setTeacherFontBtn.Name = "setTeacherFontBtn";
            this.setTeacherFontBtn.Size = new System.Drawing.Size(265, 23);
            this.setTeacherFontBtn.TabIndex = 2;
            this.setTeacherFontBtn.Text = "글꼴 설정";
            this.setTeacherFontBtn.UseVisualStyleBackColor = true;
            this.setTeacherFontBtn.Click += new System.EventHandler(this.setTeacherFontBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(191, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 25);
            this.label8.TabIndex = 10;
            this.label8.Text = "크기";
            // 
            // subjectGroup
            // 
            this.subjectGroup.Controls.Add(this.subjectAccentColorBox);
            this.subjectGroup.Controls.Add(this.label5);
            this.subjectGroup.Controls.Add(this.subjectColorBox);
            this.subjectGroup.Controls.Add(this.label7);
            this.subjectGroup.Controls.Add(this.label1);
            this.subjectGroup.Controls.Add(this.subjectSizeBox);
            this.subjectGroup.Controls.Add(this.subjectFontBox);
            this.subjectGroup.Controls.Add(this.setSubjectFontBtn);
            this.subjectGroup.Controls.Add(this.label2);
            this.subjectGroup.Location = new System.Drawing.Point(6, 22);
            this.subjectGroup.Name = "subjectGroup";
            this.subjectGroup.Size = new System.Drawing.Size(277, 113);
            this.subjectGroup.TabIndex = 17;
            this.subjectGroup.TabStop = false;
            this.subjectGroup.Text = "과목 이름";
            // 
            // subjectAccentColorBox
            // 
            this.subjectAccentColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectAccentColorBox.Location = new System.Drawing.Point(143, 53);
            this.subjectAccentColorBox.Name = "subjectAccentColorBox";
            this.subjectAccentColorBox.Size = new System.Drawing.Size(23, 23);
            this.subjectAccentColorBox.TabIndex = 15;
            this.subjectAccentColorBox.Click += new System.EventHandler(this.subjectAccentColorBox_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "강조 색상";
            // 
            // subjectColorBox
            // 
            this.subjectColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectColorBox.Location = new System.Drawing.Point(39, 53);
            this.subjectColorBox.Name = "subjectColorBox";
            this.subjectColorBox.Size = new System.Drawing.Size(23, 23);
            this.subjectColorBox.TabIndex = 13;
            this.subjectColorBox.Click += new System.EventHandler(this.subjectColorBox_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "색상";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "폰트";
            // 
            // subjectSizeBox
            // 
            this.subjectSizeBox.BackColor = System.Drawing.Color.White;
            this.subjectSizeBox.Location = new System.Drawing.Point(227, 25);
            this.subjectSizeBox.Name = "subjectSizeBox";
            this.subjectSizeBox.ReadOnly = true;
            this.subjectSizeBox.Size = new System.Drawing.Size(41, 31);
            this.subjectSizeBox.TabIndex = 11;
            // 
            // subjectFontBox
            // 
            this.subjectFontBox.BackColor = System.Drawing.Color.White;
            this.subjectFontBox.Location = new System.Drawing.Point(39, 25);
            this.subjectFontBox.Name = "subjectFontBox";
            this.subjectFontBox.ReadOnly = true;
            this.subjectFontBox.Size = new System.Drawing.Size(146, 31);
            this.subjectFontBox.TabIndex = 8;
            // 
            // setSubjectFontBtn
            // 
            this.setSubjectFontBtn.Location = new System.Drawing.Point(6, 82);
            this.setSubjectFontBtn.Name = "setSubjectFontBtn";
            this.setSubjectFontBtn.Size = new System.Drawing.Size(265, 23);
            this.setSubjectFontBtn.TabIndex = 2;
            this.setSubjectFontBtn.Text = "글꼴 설정";
            this.setSubjectFontBtn.UseVisualStyleBackColor = true;
            this.setSubjectFontBtn.Click += new System.EventHandler(this.setSubjectFontBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "크기";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xCenterBtn);
            this.groupBox1.Controls.Add(this.yCenterBtn);
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Controls.Add(this.yBar);
            this.groupBox1.Controls.Add(this.xBar);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 422);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "미리보기";
            // 
            // xCenterBtn
            // 
            this.xCenterBtn.Location = new System.Drawing.Point(402, 17);
            this.xCenterBtn.Name = "xCenterBtn";
            this.xCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.xCenterBtn.TabIndex = 10;
            this.xCenterBtn.Text = "C";
            this.xCenterBtn.UseVisualStyleBackColor = true;
            this.xCenterBtn.Click += new System.EventHandler(this.xCenterBtn_Click);
            // 
            // yCenterBtn
            // 
            this.yCenterBtn.Location = new System.Drawing.Point(3, 317);
            this.yCenterBtn.Name = "yCenterBtn";
            this.yCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.yCenterBtn.TabIndex = 11;
            this.yCenterBtn.Text = "C";
            this.yCenterBtn.UseVisualStyleBackColor = true;
            this.yCenterBtn.Click += new System.EventHandler(this.yCenterBtn_Click);
            // 
            // previewBox
            // 
            this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.ErrorImage = null;
            this.previewBox.InitialImage = null;
            this.previewBox.Location = new System.Drawing.Point(38, 48);
            this.previewBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(355, 262);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewBox.TabIndex = 7;
            this.previewBox.TabStop = false;
            this.previewBox.Click += new System.EventHandler(this.previewBox_Click);
            // 
            // yBar
            // 
            this.yBar.Location = new System.Drawing.Point(8, 37);
            this.yBar.Name = "yBar";
            this.yBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.yBar.Size = new System.Drawing.Size(69, 282);
            this.yBar.TabIndex = 9;
            this.yBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.yBar.Value = 10;
            this.yBar.Scroll += new System.EventHandler(this.yBar_Scroll);
            // 
            // xBar
            // 
            this.xBar.Location = new System.Drawing.Point(25, 18);
            this.xBar.Name = "xBar";
            this.xBar.Size = new System.Drawing.Size(380, 69);
            this.xBar.TabIndex = 8;
            this.xBar.TickFrequency = 0;
            this.xBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.xBar.Scroll += new System.EventHandler(this.xBar_Scroll);
            // 
            // teacherVisibleCheck
            // 
            this.teacherVisibleCheck.AutoSize = true;
            this.teacherVisibleCheck.Location = new System.Drawing.Point(498, 173);
            this.teacherVisibleCheck.Name = "teacherVisibleCheck";
            this.teacherVisibleCheck.Size = new System.Drawing.Size(22, 21);
            this.teacherVisibleCheck.TabIndex = 24;
            this.teacherVisibleCheck.UseVisualStyleBackColor = true;
            this.teacherVisibleCheck.CheckedChanged += new System.EventHandler(this.teacherVisibleCheck_CheckedChanged);
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(743, 438);
            this.Controls.Add(this.teacherVisibleCheck);
            this.Controls.Add(this.listVisibleCheck);
            this.Controls.Add(this.layoutGroup);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.fontGroup);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ListForm_FormClosed);
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.layoutGroup.ResumeLayout(false);
            this.layoutGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listSpaceBox)).EndInit();
            this.fontGroup.ResumeLayout(false);
            this.teacherGroup.ResumeLayout(false);
            this.teacherGroup.PerformLayout();
            this.subjectGroup.ResumeLayout(false);
            this.subjectGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox listVisibleCheck;
        private System.Windows.Forms.GroupBox layoutGroup;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton horizontalBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton verticalBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox fontGroup;
        private System.Windows.Forms.GroupBox subjectGroup;
        private System.Windows.Forms.Panel subjectColorBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox subjectSizeBox;
        private System.Windows.Forms.TextBox subjectFontBox;
        private System.Windows.Forms.Button setSubjectFontBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button xCenterBtn;
        private System.Windows.Forms.Button yCenterBtn;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.TrackBar yBar;
        private System.Windows.Forms.TrackBar xBar;
        private System.Windows.Forms.GroupBox teacherGroup;
        private System.Windows.Forms.Panel teacherAccentColorBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel teacherColorBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox teacherSizeBox;
        private System.Windows.Forms.TextBox teacherFontBox;
        private System.Windows.Forms.Button setTeacherFontBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel subjectAccentColorBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox teacherVisibleCheck;
        private System.Windows.Forms.NumericUpDown listSpaceBox;
    }
}