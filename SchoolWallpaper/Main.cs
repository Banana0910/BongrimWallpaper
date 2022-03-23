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

        string[] times = new string[] { 
            "(08:35 ~ 08:40)", "(08:40 ~ 09:30)",
            "(09:30 ~ 09:40)", "(09:40 ~ 10:30)",
            "(10:30 ~ 10:40)", "(10:40 ~ 11:30)",
            "(11:30 ~ 11:40)", "(11:40 ~ 12:30)",
            "(12:30 ~ 13:30)", "(13:30 ~ 14:20)",
            "(14:20 ~ 14:30)", "(14:30 ~ 15:20)",
            "(15:20 ~ 15:35)", "(15:35 ~ 16:25)"
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
            wc.Encoding = Encoding.UTF8;
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
            set_break(1);
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
            
            int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText(subject, subject_title).Height + 
                TextRenderer.MeasureText(teacher, teacher_name).Height + 
                TextRenderer.MeasureText(times[lesson*2+1], time_str).Height) / 2);
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
            center_x = class_x_bar.Value - (TextRenderer.MeasureText(times[lesson*2+1], time_str).Width / 2);

            g.DrawString(times[lesson*2+1], time_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));

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
            
            int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText("쉬는 시간", subject_title).Height + 
                TextRenderer.MeasureText(times[lesson*2+1], time_str).Height) / 2);
            int center_x = class_x_bar.Value - (TextRenderer.MeasureText("Next 영어(정주은)", lesson_str).Width / 2);

            g.DrawString($"Next 영어(정주은)", lesson_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x = class_x_bar.Value - (TextRenderer.MeasureText("쉬는 시간", subject_title).Width / 2);
            center_y += TextRenderer.MeasureText($"{lesson}교시", lesson_str).Height;

            g.DrawString("쉬는 시간", subject_title, color, new RectangleF(center_x,center_y,image.Width,image.Height));
            center_x = class_x_bar.Value - (TextRenderer.MeasureText(times[lesson*2], time_str).Width / 2);
            center_y += TextRenderer.MeasureText("쉬는 시간", subject_title).Height;

            g.DrawString(times[lesson*2], time_str, tiny_color, new RectangleF(center_x,center_y,image.Width,image.Height));

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
            class_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            date_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            font_color_box.BackColor = Color.FromArgb(255, 0, 0, 0);
        }

        private void checker_Tick(object sender, EventArgs e)
        {
            if (DateTime.Parse("08:30:00") < DateTime.Now) {
                //아침시간
                return;
            }

            switch (DateTime.Now.ToString("HH:mm:ss")) {
                case "08:30:00" :
                    // 조례
                    break;
                case "08:35:00" :
                    // 쉬는 시간
                    break;
                case "08:40:00" :
                    // 1교시
                    break;
                case "09:30:00" :
                    // 쉬는 시간
                    break;
                case "09:40:00" :
                    // 2교시
                    break;
                case "10:30:00" :
                    // 쉬는 시간
                    break;
                case "10:40:00" :
                    // 3교시
                    break;
                case "11:30:00" :
                    // 쉬는 시간
                    break;
                case "11:40:00" :
                    // 4교시
                    break;
                case "12:30:00" :
                    //점심 시간
                    break;
                case "13:30:00" :
                    //5교시
                    break;
                case "14:20:00" :
                    // 쉬는 시간
                    break;
                case "14:30:00" :
                    // 6교시
                    break;
                case "15:20:00" :
                    // 청소 시간
                    break;
                case "15:35:00" :
                    // 7교시
                    break;
                case "16:25:00" :
                    // 종례
                    break;
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
    