using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Web;
using System.Net;
using hap = HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace SchoolWallpaper
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        // string[] starttimes = new string[] { 
        //     "(08:35 ~ 08:40)", "(08:40 ~ 09:30)",
        //     "(09:30 ~ 09:40)", "(09:40 ~ 10:30)",
        //     "(10:30 ~ 10:40)", "(10:40 ~ 11:30)",
        //     "(11:30 ~ 11:40)", "(11:40 ~ 12:30)",
        //     "(12:30 ~ 13:30)", "(13:30 ~ 14:20)",
        //     "(14:20 ~ 14:30)", "(14:30 ~ 15:20)",
        //     "(15:20 ~ 15:35)", "(15:35 ~ 16:25)"
        // };

        string[] times = new string[] {
            "08:35", "08:40", "09:30", "09:40", "10:30", "10:40"," 11:30", "11:40","12:30", "13:30", "14:20", "14:30", "15:20", "15:35", "16:25"
        };

        // 1교시 : lesson = 1, times[lesson] ~ times[lesson+1], lesson = 2, times[lesson+2], times[lesson+2+1], lesson = 3
        // 0 : 월, 1 : 화, 2 : 수, 3 : 목, 4 : 금
        string[,,] timetables = new string[,,] {
            { 
                { "한문", "수학", "과학탐구", "체육", "영어", "통합과학", "국어"},
                { "송윤정", "배주경", "문정원", "박효열", "이은진", "백분이", "임양경"}
            },
            { 
                { "한국사", "수학", "한국사", "국어", "통합사회", "진로", "영어"},
                { "이현선", "김석룡", "하상억", "장미정", "장봉선", "김화정", "정주은"}
            },
            { 
                {"통합사회", "미술", "미술", "자율", "창체", "창체", null},
                {"김현우", "김영은", "김영은", "송윤정", "장미정", "장미정", null}
            },
            { 
                {"체육", "영어", "수학", "통합과학", "국어", "기가", "기가"},
                {"박효열", "이은진", "배주경", "김수영", "장미정", "서현희", "서현희"}
            },
            { 
                {"국어", "한국사", "영어", "통합사회", "통합과학", "수학", "한문"},
                {"임양경", "하상억", "정주은", "김현우", "정송은", "김석룡", "송윤정"}
            },

        };

        string backup_wallpaper = "";
        string background = "";

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void set_wallpaper(string path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER,0,path,SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        private string[] get_meal() {
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
            wc.QueryString.Add("dietDate", DateTime.Now.ToString("yyyy/MM/dd"));
            wc.Encoding = Encoding. UTF8;
            string html = wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do");

            hap.HtmlDocument htmlDoc = new hap.HtmlDocument();
            htmlDoc.LoadHtml(html);

            hap.HtmlNode lunch = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[2]/table/tbody/tr[2]/td");
            hap.HtmlNode dinner = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[3]/table/tbody/tr[2]/td");

            if (dinner != null) {
                string _lunch = Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                string _dinner = Regex.Replace(dinner.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                return new string[] { _lunch, _dinner };
            } else {
                string _lunch = Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                return new string[] { _lunch };
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            // checker.Interval = 1000;
            // checker.Start();
            backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
            set_subject(7, "한문", "송윤정");
        }

        private void set_subject(int lesson, string subject, string teacher) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);
            if (background.Length > 0) 
                g.DrawImage(Image.FromFile(background), 0,0,image.Width, image.Height);
            else
                g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SolidBrush color = new SolidBrush(font_color_box.BackColor);
            SolidBrush tiny_color = new SolidBrush(Color.FromArgb(font_color_box.BackColor.A-60, font_color_box.BackColor.R, font_color_box.BackColor.G, font_color_box.BackColor.B));

            // 날짜 그리기
            string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
            Font date_font = new Font("나눔스퀘어", 30);
            int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);
            
            g.DrawString(today, date_font, color, new Rectangle(date_x,10,image.Width,image.Height));

            //급식 그리기
            int meal_y = (meal_y_bar.Maximum - meal_y_bar.Value);
            string[] meal = get_meal();
            Font meal_title = new Font("나눔스퀘어 Bold", 20);
            Font meal_description = new Font("나눔고딕", 16);
            StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };

            g.DrawString("중식 [lunch]", new Font("나눔스퀘어 Bold", 20), color, new RectangleF(-8,meal_y,image.Width,image.Height), format);
            meal_y += TextRenderer.MeasureText("중식 (lunch)", meal_title).Height;

            g.DrawString(meal[0], meal_description, tiny_color, new RectangleF(-10,meal_y,image.Width,image.Height), format);
            meal_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

            if (meal.Length > 1) { // 석식이 있다면
                g.DrawString("석식 [dinner]", meal_title, color, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText("석식 (dinner)", meal_title).Height;
                    
                g.DrawString(meal[1], meal_description, tiny_color, new RectangleF(-10,meal_y,image.Width,image.Height), format);
            }

            //현재 시간 및 과목 그리기
            Font subject_title = new Font("나눔스퀘어 ExtraBold", 75);
            Font teacher_name = new Font("나눔스퀘어 ExtraBold", 35);
            Font lesson_str = new Font("나눔스퀘어 Bold", 35);
            Font time_str = new Font("나눔고딕", 30);

            string time = $"{times[lesson*2-1]} ~ {times[lesson*2]}";
            
            int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText(subject, subject_title).Height + 
                TextRenderer.MeasureText(teacher, teacher_name).Height + 
                TextRenderer.MeasureText(time, time_str).Height) / 2);
            int center_x = class_x_bar.Value - (TextRenderer.MeasureText($"{lesson}교시", lesson_str).Width / 2);

            g.DrawString($"{lesson}교시", lesson_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x = class_x_bar.Value - ((TextRenderer.MeasureText(subject, subject_title).Width + 
                TextRenderer.MeasureText(teacher, teacher_name).Width + 20) / 2);
            center_y += TextRenderer.MeasureText($"{lesson}교시", lesson_str).Height;

            g.DrawString(subject, subject_title, color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x += TextRenderer.MeasureText(subject, subject_title).Width - 20;
            center_y += TextRenderer.MeasureText(subject, subject_title).Height - TextRenderer.MeasureText(teacher, teacher_name).Height - 6;

            g.DrawString(teacher, teacher_name, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_y += TextRenderer.MeasureText(teacher, teacher_name).Height + 10;
            center_x = class_x_bar.Value - (TextRenderer.MeasureText(time, time_str).Width / 2);

            g.DrawString(time, time_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));

            //저장 및 적용
            image.Save("wallpaper.png", System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = "./wallpaper.png";
            set_wallpaper(Path.GetFullPath(pictureBox1.ImageLocation));
        }

        private void set_break(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);
            if (background.Length > 0) 
                g.DrawImage(Image.FromFile(background), 0,0,image.Width, image.Height);
            else
                g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SolidBrush color = new SolidBrush(font_color_box.BackColor);
            SolidBrush tiny_color = new SolidBrush(Color.FromArgb(font_color_box.BackColor.A-60, font_color_box.BackColor.R, font_color_box.BackColor.G, font_color_box.BackColor.B));

            // 날짜 그리기
            string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
            Font date_font = new Font("나눔스퀘어", 30);
            int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);
            
            g.DrawString(today, date_font, color, new Rectangle(date_x,10,image.Width,image.Height));

            //급식 그리기
            int left_y = 10;
            string[] meal = get_meal();
            Font meal_title = new Font("나눔스퀘어 Bold", 20);
            Font meal_description = new Font("나눔고딕", 16);
            StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };

            g.DrawString("중식 [lunch]", new Font("나눔스퀘어 Bold", 20), color, new RectangleF(-8,left_y,image.Width,image.Height), format);
            left_y += TextRenderer.MeasureText("중식 (lunch)", meal_title).Height;

            g.DrawString(meal[0], meal_description, tiny_color, new RectangleF(-10,left_y,image.Width,image.Height), format);
            left_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

            if (meal.Length > 1) { // 석식이 있다면
                g.DrawString("석식 [dinner]", meal_title, color, new RectangleF(-8,left_y,image.Width,image.Height), format);
                left_y += TextRenderer.MeasureText("석식 (dinner)", meal_title).Height;
                    
                g.DrawString(meal[1], meal_description, tiny_color, new RectangleF(-10,left_y,image.Width,image.Height), format);
            }

            //현재 시간 및 과목 그리기
            Font subject_title = new Font("나눔스퀘어 ExtraBold", 75);
            Font teacher_name = new Font("나눔스퀘어 ExtraBold", 35);
            Font lesson_str = new Font("나눔스퀘어 Bold", 35);
            Font time_str = new Font("나눔고딕", 30);
            
            string time = $"{times[lesson*2-2]} ~ {times[lesson*2-1]}";
            
            int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText("쉬는 시간", subject_title).Height + 
                TextRenderer.MeasureText(time, time_str).Height) / 2);
            int center_x = class_x_bar.Value - (TextRenderer.MeasureText("Next 영어(정주은)", lesson_str).Width / 2);

            g.DrawString($"Next 영어(정주은)", lesson_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x = class_x_bar.Value - (TextRenderer.MeasureText("쉬는 시간", subject_title).Width / 2);
            center_y += TextRenderer.MeasureText($"{lesson}교시", lesson_str).Height;

            g.DrawString("쉬는 시간", subject_title, color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x = class_x_bar.Value - (TextRenderer.MeasureText(time, time_str).Width / 2);
            center_y += TextRenderer.MeasureText("쉬는 시간", subject_title).Height;

            g.DrawString(time, time_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));

            //저장 및 적용
            image.Save("wallpaper.png", System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = "./wallpaper.png";
            set_wallpaper(Path.GetFullPath(pictureBox1.ImageLocation));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Path.GetFullPath(pictureBox1.ImageLocation));
            Process.Start(psi);
        }

        private void font_color_box_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) {
                font_color_box.BackColor = cd.Color;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            class_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            class_x_bar.Value = class_x_bar.Maximum / 2;

            class_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            class_y_bar.Value = class_y_bar.Maximum / 2;

            date_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            date_x_bar.Value = date_x_bar.Maximum / 2;
            font_color_box.BackColor = Color.FromArgb(255, 0, 0, 0);
        }

        private bool in_time(string start, string end) {
            return DateTime.Parse(start) >= DateTime.Now && DateTime.Parse(end) < DateTime.Now;
        }

        private void checker_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Parse("08:30:00") < now) {
                //아침시간
                return;
            } else if (in_time(times[0], times[1])) {

            } else if (in_time(times[1], times[2])) {

            } else if (in_time(times[2], times[3])) { 

            } else if (in_time(times[2], times[3])) { 

            } else if (in_time(times[3], times[4])) { 

            } else if (in_time(times[4], times[5])) { 

            } else if (in_time(times[5], times[6])) { 

            } else if (in_time(times[6], times[7])) { 

            } else if (in_time(times[7], times[8])) { 

            } else if (in_time(times[8], times[9])) { 

            } else if (in_time(times[9], times[10])) { 

            } else if (in_time(times[10], times[11])) { 

            } else if (in_time(times[11], times[12])) { 

            } else if (in_time(times[12], times[13])) { 

            } else if (in_time(times[13], times[14])) { 

            } 
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (backup_wallpaper.Length > 0) {
                set_wallpaper(backup_wallpaper);
            }
        }

        private void main_wall_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files (*.jpg *.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK) {
                background = fd.FileName;
                pictureBox1.ImageLocation = fd.FileName;
            }
        }
    }
}
    