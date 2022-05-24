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
            this.wallpaper_preview = new System.Windows.Forms.PictureBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.checker = new System.Windows.Forms.Timer(this.components);
            this.main_wall_btn = new System.Windows.Forms.Button();
            this.startup_check = new System.Windows.Forms.CheckBox();
            this.notifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.시작ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.중지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timetable_path_box = new System.Windows.Forms.TextBox();
            this.timetable_path_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openClassFormBtn = new System.Windows.Forms.Button();
            this.openMealFormBtn = new System.Windows.Forms.Button();
            this.openDateFormBtn = new System.Windows.Forms.Button();
            this.openListFormBtn = new System.Windows.Forms.Button();
            this.openWeekFormBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper_preview)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wallpaper_preview
            // 
            this.wallpaper_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallpaper_preview.InitialImage = null;
            this.wallpaper_preview.Location = new System.Drawing.Point(14, 39);
            this.wallpaper_preview.Name = "wallpaper_preview";
            this.wallpaper_preview.Size = new System.Drawing.Size(480, 270);
            this.wallpaper_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wallpaper_preview.TabIndex = 0;
            this.wallpaper_preview.TabStop = false;
            this.wallpaper_preview.Click += new System.EventHandler(this.wallpaper_preview_Click);
            // 
            // start_btn
            // 
            this.start_btn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.start_btn.Location = new System.Drawing.Point(14, 315);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(480, 23);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "실행";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // checker
            // 
            this.checker.Interval = 1000;
            this.checker.Tick += new System.EventHandler(this.checker_Tick);
            // 
            // main_wall_btn
            // 
            this.main_wall_btn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.main_wall_btn.Location = new System.Drawing.Point(14, 344);
            this.main_wall_btn.Name = "main_wall_btn";
            this.main_wall_btn.Size = new System.Drawing.Size(480, 23);
            this.main_wall_btn.TabIndex = 7;
            this.main_wall_btn.Text = "뒷 배경";
            this.main_wall_btn.UseVisualStyleBackColor = true;
            this.main_wall_btn.Click += new System.EventHandler(this.main_wall_btn_Click);
            // 
            // startup_check
            // 
            this.startup_check.AutoSize = true;
            this.startup_check.Location = new System.Drawing.Point(531, 12);
            this.startup_check.Name = "startup_check";
            this.startup_check.Size = new System.Drawing.Size(106, 19);
            this.startup_check.TabIndex = 10;
            this.startup_check.Text = "컴터 킬때 실행";
            this.startup_check.UseVisualStyleBackColor = true;
            this.startup_check.CheckedChanged += new System.EventHandler(this.startup_check_CheckedChanged);
            // 
            // notifyicon
            // 
            this.notifyicon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyicon.Icon")));
            this.notifyicon.Text = "SchoolWallpaper";
            this.notifyicon.Visible = true;
            this.notifyicon.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.시작ToolStripMenuItem,
            this.중지ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 70);
            // 
            // 시작ToolStripMenuItem
            // 
            this.시작ToolStripMenuItem.Name = "시작ToolStripMenuItem";
            this.시작ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.시작ToolStripMenuItem.Text = "시작";
            this.시작ToolStripMenuItem.Click += new System.EventHandler(this.시작ToolStripMenuItem_Click);
            // 
            // 중지ToolStripMenuItem
            // 
            this.중지ToolStripMenuItem.Name = "중지ToolStripMenuItem";
            this.중지ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.중지ToolStripMenuItem.Text = "중지";
            this.중지ToolStripMenuItem.Click += new System.EventHandler(this.중지ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // timetable_path_box
            // 
            this.timetable_path_box.BackColor = System.Drawing.Color.White;
            this.timetable_path_box.Location = new System.Drawing.Point(89, 10);
            this.timetable_path_box.Name = "timetable_path_box";
            this.timetable_path_box.ReadOnly = true;
            this.timetable_path_box.Size = new System.Drawing.Size(316, 23);
            this.timetable_path_box.TabIndex = 12;
            // 
            // timetable_path_btn
            // 
            this.timetable_path_btn.Location = new System.Drawing.Point(419, 10);
            this.timetable_path_btn.Name = "timetable_path_btn";
            this.timetable_path_btn.Size = new System.Drawing.Size(75, 23);
            this.timetable_path_btn.TabIndex = 13;
            this.timetable_path_btn.Text = "경로";
            this.timetable_path_btn.UseVisualStyleBackColor = true;
            this.timetable_path_btn.Click += new System.EventHandler(this.timetable_path_btn_Click);
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
            this.Controls.Add(this.timetable_path_btn);
            this.Controls.Add(this.timetable_path_box);
            this.Controls.Add(this.startup_check);
            this.Controls.Add(this.main_wall_btn);
            this.Controls.Add(this.wallpaper_preview);
            this.Controls.Add(this.start_btn);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "봉림고 바탕화면";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper_preview)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox wallpaper_preview;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Timer checker;
        private System.Windows.Forms.Button main_wall_btn;
        private System.Windows.Forms.CheckBox startup_check;
        private System.Windows.Forms.NotifyIcon notifyicon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.TextBox timetable_path_box;
        private System.Windows.Forms.Button timetable_path_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openClassFormBtn;
        private System.Windows.Forms.Button openMealFormBtn;
        private System.Windows.Forms.Button openDateFormBtn;
        private System.Windows.Forms.Button openListFormBtn;
        private System.Windows.Forms.ToolStripMenuItem 중지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 시작ToolStripMenuItem;
        private System.Windows.Forms.Button openWeekFormBtn;
    }
}

