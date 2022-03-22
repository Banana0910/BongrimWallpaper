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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.font_color_box = new System.Windows.Forms.PictureBox();
            this.class_y_bar = new System.Windows.Forms.TrackBar();
            this.class_x_bar = new System.Windows.Forms.TrackBar();
            this.checker = new System.Windows.Forms.Timer(this.components);
            this.date_x_bar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.font_color_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_y_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_x_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_x_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("나눔고딕 Light", 8F);
            this.button1.Location = new System.Drawing.Point(88, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(404, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "만들기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // font_color_box
            // 
            this.font_color_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.font_color_box.Location = new System.Drawing.Point(12, 352);
            this.font_color_box.Name = "font_color_box";
            this.font_color_box.Size = new System.Drawing.Size(70, 23);
            this.font_color_box.TabIndex = 2;
            this.font_color_box.TabStop = false;
            this.font_color_box.Click += new System.EventHandler(this.font_color_box_Click);
            // 
            // class_y_bar
            // 
            this.class_y_bar.Location = new System.Drawing.Point(489, 27);
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
            this.class_x_bar.Location = new System.Drawing.Point(-1, 305);
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
            this.checker.Tick += new System.EventHandler(this.checker_Tick);
            // 
            // date_x_bar
            // 
            this.date_x_bar.Location = new System.Drawing.Point(-1, 12);
            this.date_x_bar.Maximum = 1920;
            this.date_x_bar.Minimum = 1;
            this.date_x_bar.Name = "date_x_bar";
            this.date_x_bar.Size = new System.Drawing.Size(506, 45);
            this.date_x_bar.TabIndex = 5;
            this.date_x_bar.Value = 960;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(543, 387);
            this.Controls.Add(this.font_color_box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.date_x_bar);
            this.Controls.Add(this.class_x_bar);
            this.Controls.Add(this.class_y_bar);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SchoolWallpaper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.font_color_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_y_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.class_x_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_x_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox font_color_box;
        private System.Windows.Forms.TrackBar class_y_bar;
        private System.Windows.Forms.TrackBar class_x_bar;
        private System.Windows.Forms.Timer checker;
        private System.Windows.Forms.TrackBar date_x_bar;
    }
}

