namespace SchoolWallpaper
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.class_y_bar = new System.Windows.Forms.TrackBar();
            this.class_x_bar = new System.Windows.Forms.TrackBar();
            this.checker = new System.Windows.Forms.Timer(this.components);
            this.date_x_bar = new System.Windows.Forms.TrackBar();
            this.main_wall_btn = new System.Windows.Forms.Button();
            this.meal_y_bar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.class_sub_color_btn = new System.Windows.Forms.Button();
            this.class_sub_color = new System.Windows.Forms.Panel();
            this.class_sub_font_btn = new System.Windows.Forms.Button();
            this.class_sub_font_box = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.class_main_color_btn = new System.Windows.Forms.Button();
            this.class_main_color = new System.Windows.Forms.Panel();
            this.class_visible_check = new System.Windows.Forms.CheckBox();
            this.class_main_font_btn = new System.Windows.Forms.Button();
            this.class_main_font_box = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.meal_sub_color_btn = new System.Windows.Forms.Button();
            this.meal_sub_color = new System.Windows.Forms.Panel();
            this.meal_sub_font_btn = new System.Windows.Forms.Button();
            this.meal_sub_font_box = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.meal_main_color_btn = new System.Windows.Forms.Button();
            this.meal_main_color = new System.Windows.Forms.Panel();
            this.meal_visible_check = new System.Windows.Forms.CheckBox();
            this.meal_main_font_btn = new System.Windows.Forms.Button();
            this.meal_main_font_box = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.date_color_btn = new System.Windows.Forms.Button();
            this.date_color = new System.Windows.Forms.Panel();
            this.date_visible_check = new System.Windows.Forms.CheckBox();
            this.date_font_btn = new System.Windows.Forms.Button();
            this.date_font_box = new System.Windows.Forms.TextBox();
            this.startup_check = new System.Windows.Forms.CheckBox();
            this.SchoolWallpaper = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_y_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_x_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_x_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meal_y_bar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // start_btn
            // 
            this.start_btn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.start_btn.Location = new System.Drawing.Point(12, 412);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(548, 23);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "실행";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // class_y_bar
            // 
            this.class_y_bar.Location = new System.Drawing.Point(491, 67);
            this.class_y_bar.Maximum = 1080;
            this.class_y_bar.Minimum = 1;
            this.class_y_bar.Name = "class_y_bar";
            this.class_y_bar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.class_y_bar.Size = new System.Drawing.Size(45, 296);
            this.class_y_bar.TabIndex = 3;
            this.class_y_bar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.class_y_bar.Value = 540;
            // 
            // class_x_bar
            // 
            this.class_x_bar.Location = new System.Drawing.Point(0, 346);
            this.class_x_bar.Maximum = 1920;
            this.class_x_bar.Minimum = 1;
            this.class_x_bar.Name = "class_x_bar";
            this.class_x_bar.Size = new System.Drawing.Size(506, 45);
            this.class_x_bar.TabIndex = 4;
            this.class_x_bar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.class_x_bar.Value = 960;
            // 
            // checker
            // 
            this.checker.Interval = 1000;
            this.checker.Tick += new System.EventHandler(this.checker_Tick);
            // 
            // date_x_bar
            // 
            this.date_x_bar.Location = new System.Drawing.Point(0, 53);
            this.date_x_bar.Maximum = 1920;
            this.date_x_bar.Minimum = 1;
            this.date_x_bar.Name = "date_x_bar";
            this.date_x_bar.Size = new System.Drawing.Size(506, 45);
            this.date_x_bar.TabIndex = 5;
            this.date_x_bar.Value = 960;
            // 
            // main_wall_btn
            // 
            this.main_wall_btn.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.main_wall_btn.Location = new System.Drawing.Point(12, 441);
            this.main_wall_btn.Name = "main_wall_btn";
            this.main_wall_btn.Size = new System.Drawing.Size(548, 23);
            this.main_wall_btn.TabIndex = 7;
            this.main_wall_btn.Text = "백그라운드 사진";
            this.main_wall_btn.UseVisualStyleBackColor = true;
            this.main_wall_btn.Click += new System.EventHandler(this.main_wall_btn_Click);
            // 
            // meal_y_bar
            // 
            this.meal_y_bar.Location = new System.Drawing.Point(524, 67);
            this.meal_y_bar.Maximum = 1080;
            this.meal_y_bar.Minimum = 1;
            this.meal_y_bar.Name = "meal_y_bar";
            this.meal_y_bar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.meal_y_bar.Size = new System.Drawing.Size(45, 296);
            this.meal_y_bar.TabIndex = 8;
            this.meal_y_bar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.meal_y_bar.Value = 540;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(566, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 462);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "글자 설정";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.class_sub_color_btn);
            this.groupBox6.Controls.Add(this.class_sub_color);
            this.groupBox6.Controls.Add(this.class_sub_font_btn);
            this.groupBox6.Controls.Add(this.class_sub_font_box);
            this.groupBox6.Location = new System.Drawing.Point(6, 370);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(211, 81);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "교시 부 글꼴";
            // 
            // class_sub_color_btn
            // 
            this.class_sub_color_btn.Location = new System.Drawing.Point(130, 48);
            this.class_sub_color_btn.Name = "class_sub_color_btn";
            this.class_sub_color_btn.Size = new System.Drawing.Size(75, 23);
            this.class_sub_color_btn.TabIndex = 4;
            this.class_sub_color_btn.Text = "색상 설정";
            this.class_sub_color_btn.UseVisualStyleBackColor = true;
            this.class_sub_color_btn.Click += new System.EventHandler(this.class_sub_color_btn_Click);
            // 
            // class_sub_color
            // 
            this.class_sub_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.class_sub_color.Location = new System.Drawing.Point(7, 48);
            this.class_sub_color.Name = "class_sub_color";
            this.class_sub_color.Size = new System.Drawing.Size(117, 23);
            this.class_sub_color.TabIndex = 3;
            // 
            // class_sub_font_btn
            // 
            this.class_sub_font_btn.Location = new System.Drawing.Point(130, 18);
            this.class_sub_font_btn.Name = "class_sub_font_btn";
            this.class_sub_font_btn.Size = new System.Drawing.Size(75, 23);
            this.class_sub_font_btn.TabIndex = 1;
            this.class_sub_font_btn.Text = "폰트 설정";
            this.class_sub_font_btn.UseVisualStyleBackColor = true;
            this.class_sub_font_btn.Click += new System.EventHandler(this.class_sub_font_btn_Click);
            // 
            // class_sub_font_box
            // 
            this.class_sub_font_box.Location = new System.Drawing.Point(6, 18);
            this.class_sub_font_box.Name = "class_sub_font_box";
            this.class_sub_font_box.Size = new System.Drawing.Size(118, 23);
            this.class_sub_font_box.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.class_main_color_btn);
            this.groupBox5.Controls.Add(this.class_main_color);
            this.groupBox5.Controls.Add(this.class_visible_check);
            this.groupBox5.Controls.Add(this.class_main_font_btn);
            this.groupBox5.Controls.Add(this.class_main_font_box);
            this.groupBox5.Location = new System.Drawing.Point(5, 283);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(211, 81);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "교시 주 글꼴";
            // 
            // class_main_color_btn
            // 
            this.class_main_color_btn.Location = new System.Drawing.Point(130, 48);
            this.class_main_color_btn.Name = "class_main_color_btn";
            this.class_main_color_btn.Size = new System.Drawing.Size(75, 23);
            this.class_main_color_btn.TabIndex = 4;
            this.class_main_color_btn.Text = "색상 설정";
            this.class_main_color_btn.UseVisualStyleBackColor = true;
            this.class_main_color_btn.Click += new System.EventHandler(this.class_main_color_btn_Click);
            // 
            // class_main_color
            // 
            this.class_main_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.class_main_color.Location = new System.Drawing.Point(7, 48);
            this.class_main_color.Name = "class_main_color";
            this.class_main_color.Size = new System.Drawing.Size(117, 23);
            this.class_main_color.TabIndex = 3;
            // 
            // class_visible_check
            // 
            this.class_visible_check.AutoSize = true;
            this.class_visible_check.Checked = true;
            this.class_visible_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.class_visible_check.Location = new System.Drawing.Point(78, 1);
            this.class_visible_check.Name = "class_visible_check";
            this.class_visible_check.Size = new System.Drawing.Size(15, 14);
            this.class_visible_check.TabIndex = 2;
            this.class_visible_check.UseVisualStyleBackColor = true;
            // 
            // class_main_font_btn
            // 
            this.class_main_font_btn.Location = new System.Drawing.Point(130, 18);
            this.class_main_font_btn.Name = "class_main_font_btn";
            this.class_main_font_btn.Size = new System.Drawing.Size(75, 23);
            this.class_main_font_btn.TabIndex = 1;
            this.class_main_font_btn.Text = "폰트 설정";
            this.class_main_font_btn.UseVisualStyleBackColor = true;
            this.class_main_font_btn.Click += new System.EventHandler(this.class_main_font_btn_Click);
            // 
            // class_main_font_box
            // 
            this.class_main_font_box.Location = new System.Drawing.Point(6, 18);
            this.class_main_font_box.Name = "class_main_font_box";
            this.class_main_font_box.Size = new System.Drawing.Size(118, 23);
            this.class_main_font_box.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.meal_sub_color_btn);
            this.groupBox4.Controls.Add(this.meal_sub_color);
            this.groupBox4.Controls.Add(this.meal_sub_font_btn);
            this.groupBox4.Controls.Add(this.meal_sub_font_box);
            this.groupBox4.Location = new System.Drawing.Point(6, 196);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(211, 81);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "급식 내용";
            // 
            // meal_sub_color_btn
            // 
            this.meal_sub_color_btn.Location = new System.Drawing.Point(130, 48);
            this.meal_sub_color_btn.Name = "meal_sub_color_btn";
            this.meal_sub_color_btn.Size = new System.Drawing.Size(75, 23);
            this.meal_sub_color_btn.TabIndex = 4;
            this.meal_sub_color_btn.Text = "색상 설정";
            this.meal_sub_color_btn.UseVisualStyleBackColor = true;
            this.meal_sub_color_btn.Click += new System.EventHandler(this.meal_sub_color_btn_Click);
            // 
            // meal_sub_color
            // 
            this.meal_sub_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meal_sub_color.Location = new System.Drawing.Point(7, 48);
            this.meal_sub_color.Name = "meal_sub_color";
            this.meal_sub_color.Size = new System.Drawing.Size(117, 23);
            this.meal_sub_color.TabIndex = 3;
            // 
            // meal_sub_font_btn
            // 
            this.meal_sub_font_btn.Location = new System.Drawing.Point(130, 18);
            this.meal_sub_font_btn.Name = "meal_sub_font_btn";
            this.meal_sub_font_btn.Size = new System.Drawing.Size(75, 23);
            this.meal_sub_font_btn.TabIndex = 1;
            this.meal_sub_font_btn.Text = "폰트 설정";
            this.meal_sub_font_btn.UseVisualStyleBackColor = true;
            this.meal_sub_font_btn.Click += new System.EventHandler(this.meal_sub_font_btn_Click);
            // 
            // meal_sub_font_box
            // 
            this.meal_sub_font_box.Location = new System.Drawing.Point(6, 18);
            this.meal_sub_font_box.Name = "meal_sub_font_box";
            this.meal_sub_font_box.Size = new System.Drawing.Size(118, 23);
            this.meal_sub_font_box.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.meal_main_color_btn);
            this.groupBox3.Controls.Add(this.meal_main_color);
            this.groupBox3.Controls.Add(this.meal_visible_check);
            this.groupBox3.Controls.Add(this.meal_main_font_btn);
            this.groupBox3.Controls.Add(this.meal_main_font_box);
            this.groupBox3.Location = new System.Drawing.Point(6, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 81);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "급식 타이틀";
            // 
            // meal_main_color_btn
            // 
            this.meal_main_color_btn.Location = new System.Drawing.Point(130, 48);
            this.meal_main_color_btn.Name = "meal_main_color_btn";
            this.meal_main_color_btn.Size = new System.Drawing.Size(75, 23);
            this.meal_main_color_btn.TabIndex = 4;
            this.meal_main_color_btn.Text = "색상 설정";
            this.meal_main_color_btn.UseVisualStyleBackColor = true;
            this.meal_main_color_btn.Click += new System.EventHandler(this.meal_main_color_btn_Click);
            // 
            // meal_main_color
            // 
            this.meal_main_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meal_main_color.Location = new System.Drawing.Point(7, 48);
            this.meal_main_color.Name = "meal_main_color";
            this.meal_main_color.Size = new System.Drawing.Size(117, 23);
            this.meal_main_color.TabIndex = 3;
            // 
            // meal_visible_check
            // 
            this.meal_visible_check.AutoSize = true;
            this.meal_visible_check.Checked = true;
            this.meal_visible_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.meal_visible_check.Location = new System.Drawing.Point(74, 1);
            this.meal_visible_check.Name = "meal_visible_check";
            this.meal_visible_check.Size = new System.Drawing.Size(15, 14);
            this.meal_visible_check.TabIndex = 2;
            this.meal_visible_check.UseVisualStyleBackColor = true;
            // 
            // meal_main_font_btn
            // 
            this.meal_main_font_btn.Location = new System.Drawing.Point(130, 18);
            this.meal_main_font_btn.Name = "meal_main_font_btn";
            this.meal_main_font_btn.Size = new System.Drawing.Size(75, 23);
            this.meal_main_font_btn.TabIndex = 1;
            this.meal_main_font_btn.Text = "폰트 설정";
            this.meal_main_font_btn.UseVisualStyleBackColor = true;
            this.meal_main_font_btn.Click += new System.EventHandler(this.meal_main_font_btn_Click);
            // 
            // meal_main_font_box
            // 
            this.meal_main_font_box.Location = new System.Drawing.Point(6, 18);
            this.meal_main_font_box.Name = "meal_main_font_box";
            this.meal_main_font_box.Size = new System.Drawing.Size(118, 23);
            this.meal_main_font_box.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.date_color_btn);
            this.groupBox2.Controls.Add(this.date_color);
            this.groupBox2.Controls.Add(this.date_visible_check);
            this.groupBox2.Controls.Add(this.date_font_btn);
            this.groupBox2.Controls.Add(this.date_font_box);
            this.groupBox2.Location = new System.Drawing.Point(6, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 81);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "날짜";
            // 
            // date_color_btn
            // 
            this.date_color_btn.Location = new System.Drawing.Point(130, 48);
            this.date_color_btn.Name = "date_color_btn";
            this.date_color_btn.Size = new System.Drawing.Size(75, 23);
            this.date_color_btn.TabIndex = 4;
            this.date_color_btn.Text = "색상 설정";
            this.date_color_btn.UseVisualStyleBackColor = true;
            this.date_color_btn.Click += new System.EventHandler(this.date_color_btn_Click);
            // 
            // date_color
            // 
            this.date_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.date_color.Location = new System.Drawing.Point(7, 48);
            this.date_color.Name = "date_color";
            this.date_color.Size = new System.Drawing.Size(117, 23);
            this.date_color.TabIndex = 3;
            // 
            // date_visible_check
            // 
            this.date_visible_check.AutoSize = true;
            this.date_visible_check.Checked = true;
            this.date_visible_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.date_visible_check.Location = new System.Drawing.Point(34, 1);
            this.date_visible_check.Name = "date_visible_check";
            this.date_visible_check.Size = new System.Drawing.Size(15, 14);
            this.date_visible_check.TabIndex = 2;
            this.date_visible_check.UseVisualStyleBackColor = true;
            // 
            // date_font_btn
            // 
            this.date_font_btn.Location = new System.Drawing.Point(130, 18);
            this.date_font_btn.Name = "date_font_btn";
            this.date_font_btn.Size = new System.Drawing.Size(75, 23);
            this.date_font_btn.TabIndex = 1;
            this.date_font_btn.Text = "폰트 설정";
            this.date_font_btn.UseVisualStyleBackColor = true;
            this.date_font_btn.Click += new System.EventHandler(this.date_font_btn_Click);
            // 
            // date_font_box
            // 
            this.date_font_box.Location = new System.Drawing.Point(6, 18);
            this.date_font_box.Name = "date_font_box";
            this.date_font_box.Size = new System.Drawing.Size(118, 23);
            this.date_font_box.TabIndex = 0;
            // 
            // startup_check
            // 
            this.startup_check.AutoSize = true;
            this.startup_check.Location = new System.Drawing.Point(13, 12);
            this.startup_check.Name = "startup_check";
            this.startup_check.Size = new System.Drawing.Size(106, 19);
            this.startup_check.TabIndex = 10;
            this.startup_check.Text = "컴터 킬때 실행";
            this.startup_check.UseVisualStyleBackColor = true;
            this.startup_check.CheckedChanged += new System.EventHandler(this.startup_check_CheckedChanged);
            // 
            // SchoolWallpaper
            // 
            this.SchoolWallpaper.ContextMenuStrip = this.contextMenuStrip1;
            this.SchoolWallpaper.Icon = ((System.Drawing.Icon)(resources.GetObject("SchoolWallpaper.Icon")));
            this.SchoolWallpaper.Text = "notifyIcon";
            this.SchoolWallpaper.Visible = true;
            this.SchoolWallpaper.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.종료ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 26);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(796, 475);
            this.Controls.Add(this.startup_check);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.meal_y_bar);
            this.Controls.Add(this.main_wall_btn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.date_x_bar);
            this.Controls.Add(this.class_y_bar);
            this.Controls.Add(this.class_x_bar);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SchoolWallpaper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_y_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_x_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_x_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meal_y_bar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.TrackBar class_y_bar;
        private System.Windows.Forms.TrackBar class_x_bar;
        private System.Windows.Forms.Timer checker;
        private System.Windows.Forms.TrackBar date_x_bar;
        private System.Windows.Forms.Button main_wall_btn;
        private System.Windows.Forms.TrackBar meal_y_bar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button class_sub_color_btn;
        private System.Windows.Forms.Panel class_sub_color;
        private System.Windows.Forms.Button class_sub_font_btn;
        private System.Windows.Forms.TextBox class_sub_font_box;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button class_main_color_btn;
        private System.Windows.Forms.Panel class_main_color;
        private System.Windows.Forms.CheckBox class_visible_check;
        private System.Windows.Forms.Button class_main_font_btn;
        private System.Windows.Forms.TextBox class_main_font_box;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button meal_sub_color_btn;
        private System.Windows.Forms.Panel meal_sub_color;
        private System.Windows.Forms.Button meal_sub_font_btn;
        private System.Windows.Forms.TextBox meal_sub_font_box;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button meal_main_color_btn;
        private System.Windows.Forms.Panel meal_main_color;
        private System.Windows.Forms.CheckBox meal_visible_check;
        private System.Windows.Forms.Button meal_main_font_btn;
        private System.Windows.Forms.TextBox meal_main_font_box;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button date_color_btn;
        private System.Windows.Forms.Panel date_color;
        private System.Windows.Forms.CheckBox date_visible_check;
        private System.Windows.Forms.Button date_font_btn;
        private System.Windows.Forms.TextBox date_font_box;
        private System.Windows.Forms.CheckBox startup_check;
        private System.Windows.Forms.NotifyIcon SchoolWallpaper;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
    }
}

