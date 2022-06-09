namespace BongrimWallpaper
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.wallpaperPreview = new System.Windows.Forms.PictureBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.checker = new System.Windows.Forms.Timer(this.components);
            this.mainWallpaperBtn = new System.Windows.Forms.Button();
            this.startupCheck = new System.Windows.Forms.CheckBox();
            this.notifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.timetablePathBox = new System.Windows.Forms.TextBox();
            this.timetablePathBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openClassFormBtn = new System.Windows.Forms.Button();
            this.openMealFormBtn = new System.Windows.Forms.Button();
            this.openDateFormBtn = new System.Windows.Forms.Button();
            this.openListFormBtn = new System.Windows.Forms.Button();
            this.openWeekFormBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaperPreview)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // wallpaperPreview
            // 
            this.wallpaperPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallpaperPreview.InitialImage = null;
            this.wallpaperPreview.Location = new System.Drawing.Point(14, 39);
            this.wallpaperPreview.Name = "wallpaperPreview";
            this.wallpaperPreview.Size = new System.Drawing.Size(480, 270);
            this.wallpaperPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wallpaperPreview.TabIndex = 0;
            this.wallpaperPreview.TabStop = false;
            this.wallpaperPreview.Click += new System.EventHandler(this.wallpaperPreview_Click);
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.startBtn.Location = new System.Drawing.Point(14, 315);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(480, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "실행";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // checker
            // 
            this.checker.Interval = 1000;
            this.checker.Tick += new System.EventHandler(this.checker_Tick);
            // 
            // mainWallpaperBtn
            // 
            this.mainWallpaperBtn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.mainWallpaperBtn.Location = new System.Drawing.Point(14, 344);
            this.mainWallpaperBtn.Name = "mainWallpaperBtn";
            this.mainWallpaperBtn.Size = new System.Drawing.Size(480, 23);
            this.mainWallpaperBtn.TabIndex = 7;
            this.mainWallpaperBtn.Text = "뒷 배경";
            this.mainWallpaperBtn.UseVisualStyleBackColor = true;
            this.mainWallpaperBtn.Click += new System.EventHandler(this.mainWallpaperBtn_Click);
            // 
            // startupCheck
            // 
            this.startupCheck.AutoSize = true;
            this.startupCheck.Location = new System.Drawing.Point(531, 12);
            this.startupCheck.Name = "startupCheck";
            this.startupCheck.Size = new System.Drawing.Size(106, 19);
            this.startupCheck.TabIndex = 10;
            this.startupCheck.Text = "컴터 킬때 실행";
            this.startupCheck.UseVisualStyleBackColor = true;
            this.startupCheck.CheckedChanged += new System.EventHandler(this.startupCheck_CheckedChanged);
            // 
            // notifyicon
            // 
            this.notifyicon.ContextMenuStrip = this.menu;
            this.notifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyicon.Icon")));
            this.notifyicon.Text = "봉림고 바탕화면";
            this.notifyicon.Visible = true;
            this.notifyicon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStart,
            this.menuStop,
            this.menuClose});
            this.menu.Name = "contextMenuStrip1";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(181, 92);
            // 
            // menuStart
            // 
            this.menuStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuStart.Name = "menuStart";
            this.menuStart.Size = new System.Drawing.Size(180, 22);
            this.menuStart.Text = "시작";
            this.menuStart.Click += new System.EventHandler(this.menuStart_Click);
            // 
            // menuStop
            // 
            this.menuStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuStop.Enabled = false;
            this.menuStop.Name = "menuStop";
            this.menuStop.Size = new System.Drawing.Size(180, 22);
            this.menuStop.Text = "중지";
            this.menuStop.Click += new System.EventHandler(this.menuStop_Click);
            // 
            // menuClose
            // 
            this.menuClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuClose.Name = "menuClose";
            this.menuClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuClose.Size = new System.Drawing.Size(180, 22);
            this.menuClose.Text = "종료";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // timetablePathBox
            // 
            this.timetablePathBox.BackColor = System.Drawing.Color.White;
            this.timetablePathBox.Location = new System.Drawing.Point(89, 10);
            this.timetablePathBox.Name = "timetablePathBox";
            this.timetablePathBox.ReadOnly = true;
            this.timetablePathBox.Size = new System.Drawing.Size(316, 23);
            this.timetablePathBox.TabIndex = 12;
            // 
            // timetablePathBtn
            // 
            this.timetablePathBtn.Location = new System.Drawing.Point(419, 10);
            this.timetablePathBtn.Name = "timetablePathBtn";
            this.timetablePathBtn.Size = new System.Drawing.Size(75, 23);
            this.timetablePathBtn.TabIndex = 13;
            this.timetablePathBtn.Text = "경로";
            this.timetablePathBtn.UseVisualStyleBackColor = true;
            this.timetablePathBtn.Click += new System.EventHandler(this.timetablePathBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "시간표 파일";
            // 
            // openClassFormBtn
            // 
            this.openClassFormBtn.Location = new System.Drawing.Point(498, 314);
            this.openClassFormBtn.Name = "openClassFormBtn";
            this.openClassFormBtn.Size = new System.Drawing.Size(139, 23);
            this.openClassFormBtn.TabIndex = 15;
            this.openClassFormBtn.Text = "교시 관련 설정";
            this.openClassFormBtn.UseVisualStyleBackColor = true;
            this.openClassFormBtn.Click += new System.EventHandler(this.openClassFormBtn_Click);
            // 
            // openMealFormBtn
            // 
            this.openMealFormBtn.Location = new System.Drawing.Point(498, 285);
            this.openMealFormBtn.Name = "openMealFormBtn";
            this.openMealFormBtn.Size = new System.Drawing.Size(139, 23);
            this.openMealFormBtn.TabIndex = 16;
            this.openMealFormBtn.Text = "급식 관련 설정";
            this.openMealFormBtn.UseVisualStyleBackColor = true;
            this.openMealFormBtn.Click += new System.EventHandler(this.openMealFormBtn_Click);
            // 
            // openDateFormBtn
            // 
            this.openDateFormBtn.Location = new System.Drawing.Point(498, 256);
            this.openDateFormBtn.Name = "openDateFormBtn";
            this.openDateFormBtn.Size = new System.Drawing.Size(139, 23);
            this.openDateFormBtn.TabIndex = 17;
            this.openDateFormBtn.Text = "날짜 관련 설정";
            this.openDateFormBtn.UseVisualStyleBackColor = true;
            this.openDateFormBtn.Click += new System.EventHandler(this.openDateFormBtn_Click);
            // 
            // openListFormBtn
            // 
            this.openListFormBtn.Location = new System.Drawing.Point(498, 343);
            this.openListFormBtn.Name = "openListFormBtn";
            this.openListFormBtn.Size = new System.Drawing.Size(139, 23);
            this.openListFormBtn.TabIndex = 18;
            this.openListFormBtn.Text = "수업 목록 설정";
            this.openListFormBtn.UseVisualStyleBackColor = true;
            this.openListFormBtn.Click += new System.EventHandler(this.openListFormBtn_Click);
            // 
            // openWeekFormBtn
            // 
            this.openWeekFormBtn.Location = new System.Drawing.Point(498, 227);
            this.openWeekFormBtn.Name = "openWeekFormBtn";
            this.openWeekFormBtn.Size = new System.Drawing.Size(139, 23);
            this.openWeekFormBtn.TabIndex = 19;
            this.openWeekFormBtn.Text = "주번 관련 설정";
            this.openWeekFormBtn.UseVisualStyleBackColor = true;
            this.openWeekFormBtn.Click += new System.EventHandler(this.openWeekFormBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(644, 375);
            this.Controls.Add(this.openWeekFormBtn);
            this.Controls.Add(this.openListFormBtn);
            this.Controls.Add(this.openDateFormBtn);
            this.Controls.Add(this.openMealFormBtn);
            this.Controls.Add(this.openClassFormBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timetablePathBtn);
            this.Controls.Add(this.timetablePathBox);
            this.Controls.Add(this.startupCheck);
            this.Controls.Add(this.mainWallpaperBtn);
            this.Controls.Add(this.wallpaperPreview);
            this.Controls.Add(this.startBtn);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wallpaperPreview)).EndInit();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox wallpaperPreview;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Timer checker;
        private System.Windows.Forms.Button mainWallpaperBtn;
        private System.Windows.Forms.CheckBox startupCheck;
        private System.Windows.Forms.NotifyIcon notifyicon;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.TextBox timetablePathBox;
        private System.Windows.Forms.Button timetablePathBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openClassFormBtn;
        private System.Windows.Forms.Button openMealFormBtn;
        private System.Windows.Forms.Button openDateFormBtn;
        private System.Windows.Forms.Button openListFormBtn;
        private System.Windows.Forms.ToolStripMenuItem menuStop;
        private System.Windows.Forms.ToolStripMenuItem menuStart;
        private System.Windows.Forms.Button openWeekFormBtn;
    }
}

