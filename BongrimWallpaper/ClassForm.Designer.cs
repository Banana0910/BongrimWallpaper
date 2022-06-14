namespace BongrimWallpaper
{
    partial class ClassForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassForm));
            this.layoutGroup = new System.Windows.Forms.GroupBox();
            this.testCaseBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.alignmentBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.fontGroup = new System.Windows.Forms.GroupBox();
            this.subGroup = new System.Windows.Forms.GroupBox();
            this.subColorBox = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.subSizeBox = new System.Windows.Forms.TextBox();
            this.subFontBox = new System.Windows.Forms.TextBox();
            this.setSubFontBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.mainGroup = new System.Windows.Forms.GroupBox();
            this.mainColorBox = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainSizeBox = new System.Windows.Forms.TextBox();
            this.mainFontBox = new System.Windows.Forms.TextBox();
            this.setMainFontBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.yBar = new System.Windows.Forms.TrackBar();
            this.xCenterBtn = new System.Windows.Forms.Button();
            this.xBar = new System.Windows.Forms.TrackBar();
            this.yCenterBtn = new System.Windows.Forms.Button();
            this.classVisibleCheck = new System.Windows.Forms.CheckBox();
            this.layoutGroup.SuspendLayout();
            this.fontGroup.SuspendLayout();
            this.subGroup.SuspendLayout();
            this.mainGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutGroup
            // 
            this.layoutGroup.Controls.Add(this.testCaseBox);
            this.layoutGroup.Controls.Add(this.label8);
            this.layoutGroup.Controls.Add(this.alignmentBox);
            this.layoutGroup.Controls.Add(this.label5);
            this.layoutGroup.Location = new System.Drawing.Point(449, 253);
            this.layoutGroup.Name = "layoutGroup";
            this.layoutGroup.Size = new System.Drawing.Size(288, 92);
            this.layoutGroup.TabIndex = 21;
            this.layoutGroup.TabStop = false;
            this.layoutGroup.Text = "레이아웃";
            // 
            // testCaseBox
            // 
            this.testCaseBox.BackColor = System.Drawing.Color.White;
            this.testCaseBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testCaseBox.FormattingEnabled = true;
            this.testCaseBox.Items.AddRange(new object[] {
            "수업",
            "쉬는시간",
            "조례"});
            this.testCaseBox.Location = new System.Drawing.Point(96, 50);
            this.testCaseBox.Name = "testCaseBox";
            this.testCaseBox.Size = new System.Drawing.Size(186, 33);
            this.testCaseBox.TabIndex = 3;
            this.testCaseBox.SelectedIndexChanged += new System.EventHandler(this.testCaseBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "테스트 케이스";
            // 
            // alignmentBox
            // 
            this.alignmentBox.BackColor = System.Drawing.Color.White;
            this.alignmentBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignmentBox.FormattingEnabled = true;
            this.alignmentBox.Items.AddRange(new object[] {
            "왼쪽",
            "가운데",
            "오른쪽"});
            this.alignmentBox.Location = new System.Drawing.Point(96, 20);
            this.alignmentBox.Name = "alignmentBox";
            this.alignmentBox.Size = new System.Drawing.Size(186, 33);
            this.alignmentBox.TabIndex = 1;
            this.alignmentBox.SelectedIndexChanged += new System.EventHandler(this.alignmentBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "글자 정렬";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(448, 383);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(289, 23);
            this.saveBtn.TabIndex = 18;
            this.saveBtn.Text = "설정 저장";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // fontGroup
            // 
            this.fontGroup.Controls.Add(this.subGroup);
            this.fontGroup.Controls.Add(this.mainGroup);
            this.fontGroup.Location = new System.Drawing.Point(448, 31);
            this.fontGroup.Name = "fontGroup";
            this.fontGroup.Size = new System.Drawing.Size(289, 216);
            this.fontGroup.TabIndex = 20;
            this.fontGroup.TabStop = false;
            this.fontGroup.Text = "글꼴";
            // 
            // subGroup
            // 
            this.subGroup.Controls.Add(this.subColorBox);
            this.subGroup.Controls.Add(this.label6);
            this.subGroup.Controls.Add(this.label3);
            this.subGroup.Controls.Add(this.subSizeBox);
            this.subGroup.Controls.Add(this.subFontBox);
            this.subGroup.Controls.Add(this.setSubFontBtn);
            this.subGroup.Controls.Add(this.label4);
            this.subGroup.Location = new System.Drawing.Point(6, 121);
            this.subGroup.Name = "subGroup";
            this.subGroup.Size = new System.Drawing.Size(277, 89);
            this.subGroup.TabIndex = 18;
            this.subGroup.TabStop = false;
            this.subGroup.Text = "나머지";
            // 
            // subColorBox
            // 
            this.subColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subColorBox.Location = new System.Drawing.Point(243, 25);
            this.subColorBox.Name = "subColorBox";
            this.subColorBox.Size = new System.Drawing.Size(23, 23);
            this.subColorBox.TabIndex = 17;
            this.subColorBox.Click += new System.EventHandler(this.subColorBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "색";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "폰트";
            // 
            // subSizeBox
            // 
            this.subSizeBox.BackColor = System.Drawing.Color.White;
            this.subSizeBox.Location = new System.Drawing.Point(171, 25);
            this.subSizeBox.Name = "subSizeBox";
            this.subSizeBox.ReadOnly = true;
            this.subSizeBox.Size = new System.Drawing.Size(41, 31);
            this.subSizeBox.TabIndex = 11;
            // 
            // subFontBox
            // 
            this.subFontBox.BackColor = System.Drawing.Color.White;
            this.subFontBox.Location = new System.Drawing.Point(39, 25);
            this.subFontBox.Name = "subFontBox";
            this.subFontBox.ReadOnly = true;
            this.subFontBox.Size = new System.Drawing.Size(90, 31);
            this.subFontBox.TabIndex = 8;
            // 
            // setSubFontBtn
            // 
            this.setSubFontBtn.Location = new System.Drawing.Point(9, 54);
            this.setSubFontBtn.Name = "setSubFontBtn";
            this.setSubFontBtn.Size = new System.Drawing.Size(265, 23);
            this.setSubFontBtn.TabIndex = 2;
            this.setSubFontBtn.Text = "글꼴 설정";
            this.setSubFontBtn.UseVisualStyleBackColor = true;
            this.setSubFontBtn.Click += new System.EventHandler(this.setSubFontBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "크기";
            // 
            // mainGroup
            // 
            this.mainGroup.Controls.Add(this.mainColorBox);
            this.mainGroup.Controls.Add(this.label7);
            this.mainGroup.Controls.Add(this.label1);
            this.mainGroup.Controls.Add(this.mainSizeBox);
            this.mainGroup.Controls.Add(this.mainFontBox);
            this.mainGroup.Controls.Add(this.setMainFontBtn);
            this.mainGroup.Controls.Add(this.label2);
            this.mainGroup.Location = new System.Drawing.Point(6, 22);
            this.mainGroup.Name = "mainGroup";
            this.mainGroup.Size = new System.Drawing.Size(277, 93);
            this.mainGroup.TabIndex = 17;
            this.mainGroup.TabStop = false;
            this.mainGroup.Text = "메인";
            // 
            // mainColorBox
            // 
            this.mainColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainColorBox.Location = new System.Drawing.Point(243, 25);
            this.mainColorBox.Name = "mainColorBox";
            this.mainColorBox.Size = new System.Drawing.Size(23, 23);
            this.mainColorBox.TabIndex = 15;
            this.mainColorBox.Click += new System.EventHandler(this.mainColorBox_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "색";
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
            // mainSizeBox
            // 
            this.mainSizeBox.BackColor = System.Drawing.Color.White;
            this.mainSizeBox.Location = new System.Drawing.Point(171, 25);
            this.mainSizeBox.Name = "mainSizeBox";
            this.mainSizeBox.ReadOnly = true;
            this.mainSizeBox.Size = new System.Drawing.Size(41, 31);
            this.mainSizeBox.TabIndex = 11;
            // 
            // mainFontBox
            // 
            this.mainFontBox.BackColor = System.Drawing.Color.White;
            this.mainFontBox.Location = new System.Drawing.Point(39, 25);
            this.mainFontBox.Name = "mainFontBox";
            this.mainFontBox.ReadOnly = true;
            this.mainFontBox.Size = new System.Drawing.Size(90, 31);
            this.mainFontBox.TabIndex = 8;
            // 
            // setMainFontBtn
            // 
            this.setMainFontBtn.Location = new System.Drawing.Point(6, 59);
            this.setMainFontBtn.Name = "setMainFontBtn";
            this.setMainFontBtn.Size = new System.Drawing.Size(265, 23);
            this.setMainFontBtn.TabIndex = 2;
            this.setMainFontBtn.Text = "글꼴 설정";
            this.setMainFontBtn.UseVisualStyleBackColor = true;
            this.setMainFontBtn.Click += new System.EventHandler(this.setMainFontBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "크기";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Controls.Add(this.yBar);
            this.groupBox1.Controls.Add(this.xCenterBtn);
            this.groupBox1.Controls.Add(this.xBar);
            this.groupBox1.Controls.Add(this.yCenterBtn);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 399);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "미리보기";
            // 
            // previewBox
            // 
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.ErrorImage = null;
            this.previewBox.Location = new System.Drawing.Point(37, 52);
            this.previewBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(355, 262);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            this.previewBox.Click += new System.EventHandler(this.previewBox_Click);
            // 
            // yBar
            // 
            this.yBar.Location = new System.Drawing.Point(11, 39);
            this.yBar.Name = "yBar";
            this.yBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.yBar.Size = new System.Drawing.Size(69, 288);
            this.yBar.TabIndex = 4;
            this.yBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.yBar.Value = 10;
            this.yBar.Scroll += new System.EventHandler(this.yBar_Scroll);
            // 
            // xCenterBtn
            // 
            this.xCenterBtn.Location = new System.Drawing.Point(399, 22);
            this.xCenterBtn.Name = "xCenterBtn";
            this.xCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.xCenterBtn.TabIndex = 5;
            this.xCenterBtn.Text = "C";
            this.xCenterBtn.UseVisualStyleBackColor = true;
            this.xCenterBtn.Click += new System.EventHandler(this.xCenterBtn_Click);
            // 
            // xBar
            // 
            this.xBar.Location = new System.Drawing.Point(24, 22);
            this.xBar.Name = "xBar";
            this.xBar.Size = new System.Drawing.Size(381, 69);
            this.xBar.TabIndex = 3;
            this.xBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.xBar.Value = 10;
            this.xBar.Scroll += new System.EventHandler(this.xBar_Scroll);
            // 
            // yCenterBtn
            // 
            this.yCenterBtn.Location = new System.Drawing.Point(5, 326);
            this.yCenterBtn.Name = "yCenterBtn";
            this.yCenterBtn.Size = new System.Drawing.Size(33, 23);
            this.yCenterBtn.TabIndex = 6;
            this.yCenterBtn.Text = "C";
            this.yCenterBtn.UseVisualStyleBackColor = true;
            this.yCenterBtn.Click += new System.EventHandler(this.yCenterBtn_Click);
            // 
            // classVisibleCheck
            // 
            this.classVisibleCheck.AutoSize = true;
            this.classVisibleCheck.Checked = true;
            this.classVisibleCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.classVisibleCheck.Location = new System.Drawing.Point(448, 12);
            this.classVisibleCheck.Name = "classVisibleCheck";
            this.classVisibleCheck.Size = new System.Drawing.Size(176, 29);
            this.classVisibleCheck.TabIndex = 22;
            this.classVisibleCheck.Text = "교시 알림 활성화";
            this.classVisibleCheck.UseVisualStyleBackColor = true;
            this.classVisibleCheck.CheckedChanged += new System.EventHandler(this.classVisibleCheck_CheckedChanged);
            // 
            // ClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(749, 418);
            this.Controls.Add(this.classVisibleCheck);
            this.Controls.Add(this.layoutGroup);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.fontGroup);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ClassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClassForm_FormClosed);
            this.Load += new System.EventHandler(this.ClassForm_Load);
            this.layoutGroup.ResumeLayout(false);
            this.layoutGroup.PerformLayout();
            this.fontGroup.ResumeLayout(false);
            this.subGroup.ResumeLayout(false);
            this.subGroup.PerformLayout();
            this.mainGroup.ResumeLayout(false);
            this.mainGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox layoutGroup;
        private System.Windows.Forms.ComboBox alignmentBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox fontGroup;
        private System.Windows.Forms.GroupBox subGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox subSizeBox;
        private System.Windows.Forms.TextBox subFontBox;
        private System.Windows.Forms.Button setSubFontBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox mainGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mainSizeBox;
        private System.Windows.Forms.TextBox mainFontBox;
        private System.Windows.Forms.Button setMainFontBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.TrackBar xBar;
        private System.Windows.Forms.Button yCenterBtn;
        private System.Windows.Forms.TrackBar yBar;
        private System.Windows.Forms.Button xCenterBtn;
        private System.Windows.Forms.Panel subColorBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel mainColorBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox classVisibleCheck;
        private System.Windows.Forms.ComboBox testCaseBox;
        private System.Windows.Forms.Label label8;
    }
}