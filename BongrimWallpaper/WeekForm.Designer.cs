namespace BongrimWallpaper
{
    partial class WeekForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeekForm));
            this.weekVisibleCheck = new System.Windows.Forms.CheckBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.weekGroup = new System.Windows.Forms.GroupBox();
            this.backWeekBtn = new System.Windows.Forms.Button();
            this.nextWeekBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.studentBox = new System.Windows.Forms.TextBox();
            this.studentAddBtn = new System.Windows.Forms.Button();
            this.studentDelBtn = new System.Windows.Forms.Button();
            this.studentDownBtn = new System.Windows.Forms.Button();
            this.studentUpBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.weekCountBox = new System.Windows.Forms.NumericUpDown();
            this.studentList = new System.Windows.Forms.ListBox();
            this.fontGroup = new System.Windows.Forms.GroupBox();
            this.colorBox = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sizeBox = new System.Windows.Forms.TextBox();
            this.setFontBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fontBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.xCenterBtn = new System.Windows.Forms.Button();
            this.xBar = new System.Windows.Forms.TrackBar();
            this.yCenterBtn = new System.Windows.Forms.Button();
            this.yBar = new System.Windows.Forms.TrackBar();
            this.weekGroup.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weekCountBox)).BeginInit();
            this.fontGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).BeginInit();
            this.SuspendLayout();
            // 
            // weekVisibleCheck
            // 
            this.weekVisibleCheck.AutoSize = true;
            this.weekVisibleCheck.Checked = true;
            this.weekVisibleCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.weekVisibleCheck.Location = new System.Drawing.Point(450, 10);
            this.weekVisibleCheck.Name = "weekVisibleCheck";
            this.weekVisibleCheck.Size = new System.Drawing.Size(90, 19);
            this.weekVisibleCheck.TabIndex = 19;
            this.weekVisibleCheck.Text = "주번 활성화";
            this.weekVisibleCheck.UseVisualStyleBackColor = true;
            this.weekVisibleCheck.CheckedChanged += new System.EventHandler(this.weekVisibleCheck_CheckedChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(449, 337);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(273, 23);
            this.saveBtn.TabIndex = 15;
            this.saveBtn.Text = "설정 저장";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // weekGroup
            // 
            this.weekGroup.Controls.Add(this.backWeekBtn);
            this.weekGroup.Controls.Add(this.nextWeekBtn);
            this.weekGroup.Controls.Add(this.groupBox4);
            this.weekGroup.Controls.Add(this.label3);
            this.weekGroup.Controls.Add(this.weekCountBox);
            this.weekGroup.Controls.Add(this.studentList);
            this.weekGroup.Location = new System.Drawing.Point(450, 127);
            this.weekGroup.Name = "weekGroup";
            this.weekGroup.Size = new System.Drawing.Size(272, 204);
            this.weekGroup.TabIndex = 18;
            this.weekGroup.TabStop = false;
            this.weekGroup.Text = "주번";
            // 
            // backWeekBtn
            // 
            this.backWeekBtn.Location = new System.Drawing.Point(132, 169);
            this.backWeekBtn.Name = "backWeekBtn";
            this.backWeekBtn.Size = new System.Drawing.Size(134, 23);
            this.backWeekBtn.TabIndex = 24;
            this.backWeekBtn.Text = "저번 주 주번";
            this.backWeekBtn.UseVisualStyleBackColor = true;
            this.backWeekBtn.Click += new System.EventHandler(this.backWeekBtn_Click);
            // 
            // nextWeekBtn
            // 
            this.nextWeekBtn.Location = new System.Drawing.Point(132, 140);
            this.nextWeekBtn.Name = "nextWeekBtn";
            this.nextWeekBtn.Size = new System.Drawing.Size(134, 23);
            this.nextWeekBtn.TabIndex = 23;
            this.nextWeekBtn.Text = "다음 주 주번";
            this.nextWeekBtn.UseVisualStyleBackColor = true;
            this.nextWeekBtn.Click += new System.EventHandler(this.nextWeekBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.studentBox);
            this.groupBox4.Controls.Add(this.studentAddBtn);
            this.groupBox4.Controls.Add(this.studentDelBtn);
            this.groupBox4.Controls.Add(this.studentDownBtn);
            this.groupBox4.Controls.Add(this.studentUpBtn);
            this.groupBox4.Location = new System.Drawing.Point(132, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(134, 73);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "학생 관리";
            // 
            // studentBox
            // 
            this.studentBox.BackColor = System.Drawing.Color.White;
            this.studentBox.Location = new System.Drawing.Point(3, 15);
            this.studentBox.Name = "studentBox";
            this.studentBox.Size = new System.Drawing.Size(79, 23);
            this.studentBox.TabIndex = 16;
            this.studentBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.studentBox_KeyPress);
            // 
            // studentAddBtn
            // 
            this.studentAddBtn.Location = new System.Drawing.Point(88, 15);
            this.studentAddBtn.Name = "studentAddBtn";
            this.studentAddBtn.Size = new System.Drawing.Size(40, 23);
            this.studentAddBtn.TabIndex = 1;
            this.studentAddBtn.Text = "추가";
            this.studentAddBtn.UseVisualStyleBackColor = true;
            this.studentAddBtn.Click += new System.EventHandler(this.studentAddBtn_Click);
            // 
            // studentDelBtn
            // 
            this.studentDelBtn.Location = new System.Drawing.Point(3, 44);
            this.studentDelBtn.Name = "studentDelBtn";
            this.studentDelBtn.Size = new System.Drawing.Size(79, 23);
            this.studentDelBtn.TabIndex = 17;
            this.studentDelBtn.Text = "삭제";
            this.studentDelBtn.UseVisualStyleBackColor = true;
            this.studentDelBtn.Click += new System.EventHandler(this.studentDelBtn_Click);
            // 
            // studentDownBtn
            // 
            this.studentDownBtn.Location = new System.Drawing.Point(112, 44);
            this.studentDownBtn.Name = "studentDownBtn";
            this.studentDownBtn.Size = new System.Drawing.Size(16, 23);
            this.studentDownBtn.TabIndex = 19;
            this.studentDownBtn.Text = "↓";
            this.studentDownBtn.UseVisualStyleBackColor = true;
            this.studentDownBtn.Click += new System.EventHandler(this.studentDownBtn_Click);
            // 
            // studentUpBtn
            // 
            this.studentUpBtn.Location = new System.Drawing.Point(88, 44);
            this.studentUpBtn.Name = "studentUpBtn";
            this.studentUpBtn.Size = new System.Drawing.Size(18, 23);
            this.studentUpBtn.TabIndex = 18;
            this.studentUpBtn.Text = "↑";
            this.studentUpBtn.UseVisualStyleBackColor = true;
            this.studentUpBtn.Click += new System.EventHandler(this.studentUpBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "주번 수";
            // 
            // weekCountBox
            // 
            this.weekCountBox.Location = new System.Drawing.Point(185, 107);
            this.weekCountBox.Name = "weekCountBox";
            this.weekCountBox.Size = new System.Drawing.Size(81, 23);
            this.weekCountBox.TabIndex = 20;
            this.weekCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.weekCountBox.ValueChanged += new System.EventHandler(this.weekCountBox_ValueChanged);
            // 
            // studentList
            // 
            this.studentList.FormattingEnabled = true;
            this.studentList.ItemHeight = 15;
            this.studentList.Location = new System.Drawing.Point(6, 22);
            this.studentList.Name = "studentList";
            this.studentList.Size = new System.Drawing.Size(120, 169);
            this.studentList.TabIndex = 0;
            // 
            // fontGroup
            // 
            this.fontGroup.Controls.Add(this.colorBox);
            this.fontGroup.Controls.Add(this.label7);
            this.fontGroup.Controls.Add(this.label1);
            this.fontGroup.Controls.Add(this.sizeBox);
            this.fontGroup.Controls.Add(this.setFontBtn);
            this.fontGroup.Controls.Add(this.label2);
            this.fontGroup.Controls.Add(this.fontBox);
            this.fontGroup.Location = new System.Drawing.Point(450, 31);
            this.fontGroup.Name = "fontGroup";
            this.fontGroup.Size = new System.Drawing.Size(272, 90);
            this.fontGroup.TabIndex = 17;
            this.fontGroup.TabStop = false;
            this.fontGroup.Text = "글꼴";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "폰트";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "크기";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Controls.Add(this.xCenterBtn);
            this.groupBox1.Controls.Add(this.xBar);
            this.groupBox1.Controls.Add(this.yCenterBtn);
            this.groupBox1.Controls.Add(this.yBar);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 355);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "미리보기";
            // 
            // previewBox
            // 
            this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.InitialImage = null;
            this.previewBox.Location = new System.Drawing.Point(38, 52);
            this.previewBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(355, 262);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            this.previewBox.Click += new System.EventHandler(this.previewBox_Click);
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
            // WeekForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(730, 372);
            this.Controls.Add(this.weekVisibleCheck);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.weekGroup);
            this.Controls.Add(this.fontGroup);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "WeekForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WeekForm_FormClosed);
            this.Load += new System.EventHandler(this.WeekForm_Load);
            this.weekGroup.ResumeLayout(false);
            this.weekGroup.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weekCountBox)).EndInit();
            this.fontGroup.ResumeLayout(false);
            this.fontGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox weekVisibleCheck;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox weekGroup;
        private System.Windows.Forms.Button studentDownBtn;
        private System.Windows.Forms.Button studentUpBtn;
        private System.Windows.Forms.Button studentDelBtn;
        private System.Windows.Forms.TextBox studentBox;
        private System.Windows.Forms.Button studentAddBtn;
        private System.Windows.Forms.ListBox studentList;
        private System.Windows.Forms.GroupBox fontGroup;
        private System.Windows.Forms.Panel colorBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sizeBox;
        private System.Windows.Forms.Button setFontBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fontBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button xCenterBtn;
        private System.Windows.Forms.TrackBar xBar;
        private System.Windows.Forms.Button yCenterBtn;
        private System.Windows.Forms.TrackBar yBar;
        private System.Windows.Forms.Button backWeekBtn;
        private System.Windows.Forms.Button nextWeekBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown weekCountBox;
    }
}