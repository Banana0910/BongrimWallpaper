using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using hap = HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SchoolWallpaper
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        string[] times = new string[] {
            "08:35", "08:40", "09:30", "09:40", "10:30", "10:40"," 11:30", "11:40","12:30", "13:30", "14:20", "14:30", "15:20", "15:35", "16:25"
        }; 

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

        string now_wallpaper = "";

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
            if (start_btn.Text == "실행") {
                backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
                checker.Start();
                start_btn.Text = "중지";
            } else {
                now_wallpaper = "";
                set_wallpaper(backup_wallpaper);
                checker.Stop();
                start_btn.Text = "실행";
            }
        }

        private void set_subject(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);
            if (background.Length > 0) 
                g.DrawImage(Image.FromFile(background), 0,0,image.Width, image.Height);
            else
                g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 날짜 그리기
            if (date_visible_check.Checked) {
                string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
                string[] font = date_font_box.Text.Split(',');
                Font date_font = new Font(font[0],float.Parse(font[1])); 
                int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);
                
                g.DrawString(today, date_font, new SolidBrush(date_color.BackColor), new Rectangle(date_x,10,image.Width,image.Height));
            }

            //급식 그리기
            if (meal_visible_check.Checked) {
                int meal_y = (meal_y_bar.Maximum - meal_y_bar.Value);
                string[] meal = get_meal();

                string[] font = meal_main_font_box.Text.Split(',');
                Font meal_title = new Font(font[0], float.Parse(font[1]));

                font = meal_sub_font_box.Text.Split(',');
                Font meal_description = new Font(font[0], float.Parse(font[1]));

                SolidBrush meal_title_sb = new SolidBrush(meal_main_color.BackColor);
                SolidBrush meal_description_sb = new SolidBrush(meal_sub_color.BackColor);

                StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
                g.DrawString("중식 [lunch]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText("중식 (lunch)", meal_title).Height;

                g.DrawString(meal[0], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

                if (meal.Length > 1) { // 석식이 있다면
                    g.DrawString("석식 [dinner]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                    meal_y += TextRenderer.MeasureText("석식 (dinner)", meal_title).Height;
                        
                    g.DrawString(meal[1], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                }
            }

            //현재 시간 및 과목 그리기
            if (class_visible_check.Checked) {
                string[] main_font_sp = class_main_font_box.Text.Split(',');
                string[] sub_font_sp = class_sub_font_box.Text.Split(',');

                Font main_font = new Font(main_font_sp[0], float.Parse(main_font_sp[1]));
                Font sub_font = new Font(sub_font_sp[0], float.Parse(sub_font_sp[1]));

                SolidBrush main_sb = new SolidBrush(class_main_color.BackColor);
                SolidBrush sub_sb = new SolidBrush(class_sub_color.BackColor);

                string time = $"{times[lesson*2-1]} ~ {times[lesson*2]}";
                
                string subject = timetables[((int)DateTime.Now.DayOfWeek)-1,0,lesson-1];
                string teacher = timetables[((int)DateTime.Now.DayOfWeek)-1,1,lesson-1];
                
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText(subject, main_font).Height + 
                    TextRenderer.MeasureText(time, sub_font).Height + 
                    TextRenderer.MeasureText(time, main_font).Height) / 2);
                int center_x = class_x_bar.Value - (TextRenderer.MeasureText($"{lesson} 교시", sub_font).Width / 2);

                g.DrawString($"{lesson} 교시", sub_font, sub_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
                center_x = class_x_bar.Value - ((TextRenderer.MeasureText(subject, main_font).Width + 
                    TextRenderer.MeasureText(teacher, sub_font).Width + 20) / 2);
                center_y += TextRenderer.MeasureText($"{lesson} 교시", sub_font).Height;

                g.DrawString(subject, main_font, main_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
                center_x += TextRenderer.MeasureText(subject, main_font).Width - 20;
                center_y += TextRenderer.MeasureText(subject, main_font).Height - TextRenderer.MeasureText(teacher, sub_font).Height - 6;

                g.DrawString(teacher, sub_font, sub_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
                center_y += TextRenderer.MeasureText(teacher, sub_font).Height + 10;
                center_x = class_x_bar.Value - (TextRenderer.MeasureText(time, sub_font).Width / 2);

                g.DrawString(time, sub_font, sub_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
            }

            //저장 및 적용
            image.Save("wallpaper.png", System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = "./wallpaper.png";
            set_wallpaper(Path.GetFullPath(pictureBox1.ImageLocation));
        }

        private void set_break(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);
            if (background.Length > 0)  g.DrawImage(Image.FromFile(background), 0,0,image.Width, image.Height);
            else g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 날짜 그리기
            if (date_visible_check.Checked) {
                string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
                string[] font = date_font_box.Text.Split(',');
                Font date_font = new Font(font[0],float.Parse(font[1])); 
                int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);
                
                g.DrawString(today, date_font, new SolidBrush(date_color.BackColor), new Rectangle(date_x,10,image.Width,image.Height));
            }

            //급식 그리기
            if (meal_visible_check.Checked) {
                int meal_y = (meal_y_bar.Maximum - meal_y_bar.Value);
                string[] meal = get_meal();

                string[] font = meal_main_font_box.Text.Split(',');
                Font meal_title = new Font(font[0], float.Parse(font[1]));

                font = meal_sub_font_box.Text.Split(',');
                Font meal_description = new Font(font[0], float.Parse(font[1]));

                SolidBrush meal_title_sb = new SolidBrush(meal_main_color.BackColor);
                SolidBrush meal_description_sb = new SolidBrush(meal_sub_color.BackColor);

                StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
                g.DrawString("중식 [lunch]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText("중식 (lunch)", meal_title).Height;

                g.DrawString(meal[0], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

                if (meal.Length > 1) { // 석식이 있다면
                    g.DrawString("석식 [dinner]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                    meal_y += TextRenderer.MeasureText("석식 (dinner)", meal_title).Height;
                        
                    g.DrawString(meal[1], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                }
            }

            if (class_visible_check.Checked) {
                string[] main_font_sp = class_main_font_box.Text.Split(',');
                string[] sub_font_sp = class_sub_font_box.Text.Split(',');

                Font main_font = new Font(main_font_sp[0], float.Parse(main_font_sp[1]));
                Font sub_font = new Font(sub_font_sp[0], float.Parse(sub_font_sp[1]));

                SolidBrush main_sb = new SolidBrush(class_main_color.BackColor);
                SolidBrush sub_sb = new SolidBrush(class_sub_color.BackColor);

                string time = $"{times[lesson*2-2]} ~ {times[lesson*2-1]}";

                string subject = timetables[((int)DateTime.Now.DayOfWeek)-1,0,lesson-1];
                string teacher = timetables[((int)DateTime.Now.DayOfWeek)-1,1,lesson-1];
            
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((TextRenderer.MeasureText("쉬는 시간", main_font).Height + 
                    TextRenderer.MeasureText(time, sub_font).Height) / 2);
                int center_x = class_x_bar.Value - (TextRenderer.MeasureText($"Next {subject}({teacher})", sub_font).Width / 2);

                g.DrawString($"Next {subject}({teacher})", sub_font, sub_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
                center_x = class_x_bar.Value - (TextRenderer.MeasureText("쉬는 시간", main_font).Width / 2);
                center_y += TextRenderer.MeasureText($"Next {subject}({teacher})", sub_font).Height;

                g.DrawString("쉬는 시간", main_font, main_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
                center_x = class_x_bar.Value - (TextRenderer.MeasureText(time, sub_font).Width / 2);
                center_y += TextRenderer.MeasureText("쉬는 시간", main_font).Height;

                g.DrawString(time, sub_font, sub_sb, new RectangleF(center_x,center_y,image.Width,image.Height));
            }

            //저장 및 적용
            image.Save("wallpaper.png", System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = "./wallpaper.png";
            set_wallpaper(Path.GetFullPath(pictureBox1.ImageLocation));
        }

        private void set_event(string text) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);
            if (background.Length > 0)  g.DrawImage(Image.FromFile(background), 0,0,image.Width, image.Height);
            else g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 날짜 그리기
            if (date_visible_check.Checked) {
                string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
                string[] font = date_font_box.Text.Split(',');
                Font date_font = new Font(font[0],float.Parse(font[1])); 
                int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);
                
                g.DrawString(today, date_font, new SolidBrush(date_color.BackColor), new Rectangle(date_x,10,image.Width,image.Height));
            }

            //급식 그리기
            if (meal_visible_check.Checked) {
                int meal_y = (meal_y_bar.Maximum - meal_y_bar.Value);
                string[] meal = get_meal();

                string[] font = meal_main_font_box.Text.Split(',');
                Font meal_title = new Font(font[0], float.Parse(font[1]));

                font = meal_sub_font_box.Text.Split(',');
                Font meal_description = new Font(font[0], float.Parse(font[1]));

                SolidBrush meal_title_sb = new SolidBrush(meal_main_color.BackColor);
                SolidBrush meal_description_sb = new SolidBrush(meal_sub_color.BackColor);

                StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
                g.DrawString("중식 [lunch]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText("중식 (lunch)", meal_title).Height;

                g.DrawString(meal[0], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                meal_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

                if (meal.Length > 1) { // 석식이 있다면
                    g.DrawString("석식 [dinner]", meal_title, meal_title_sb, new RectangleF(-8,meal_y,image.Width,image.Height), format);
                    meal_y += TextRenderer.MeasureText("석식 (dinner)", meal_title).Height;
                        
                    g.DrawString(meal[1], meal_description, meal_description_sb, new RectangleF(-10,meal_y,image.Width,image.Height), format);
                }
            }

            if (class_visible_check.Checked) {
                string[] main_font_sp = class_main_font_box.Text.Split(',');
                Font font = new Font(main_font_sp[0], float.Parse(main_font_sp[1]));
                SolidBrush sb = new SolidBrush(class_main_color.BackColor);
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - (TextRenderer.MeasureText(text, font).Height / 2);
                int center_x = class_x_bar.Value - (TextRenderer.MeasureText(text, font).Width / 2);

                g.DrawString(text, font, sb, new RectangleF(center_x,center_y,image.Width,image.Height));
            }

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

        private void Main_Load(object sender, EventArgs e)
        {
            Properties.Settings settings = new Properties.Settings();
            class_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            class_x_bar.Value = (settings.class_x_bar != 0) ? settings.class_x_bar : Screen.PrimaryScreen.Bounds.Width / 2;

            class_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            class_y_bar.Value = (settings.class_y_bar != 0) ? settings.class_y_bar : Screen.PrimaryScreen.Bounds.Height / 2;

            date_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            date_x_bar.Value = (settings.date_x_bar != 0) ? settings.date_x_bar : Screen.PrimaryScreen.Bounds.Width / 2;

            meal_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            meal_y_bar.Value = (settings.meal_y_bar != 0) ? settings.meal_y_bar : Screen.PrimaryScreen.Bounds.Height - 10;

            date_font_box.Text = (settings.date_font != "") ? settings.date_font : $"{date_font_box.Font.Name},{date_font_box.Font.Size}";
            meal_main_font_box.Text = (settings.meal_main_font != "") ? settings.meal_main_font : $"{meal_main_font_box.Font.Name},{meal_main_font_box.Font.Size}";
            meal_sub_font_box.Text = (settings.meal_sub_font != "") ? settings.meal_sub_font : $"{meal_sub_font_box.Font.Name},{meal_sub_font_box.Font.Size}";
            class_main_font_box.Text = (settings.class_main_font != "") ? settings.class_main_font : $"{class_main_font_box.Font.Name},{class_main_font_box.Font.Size}";
            class_sub_font_box.Text = (settings.class_sub_font != "") ? settings.class_sub_font : $"{class_main_font_box.Font.Name},{class_main_font_box.Font.Size}";

            date_color.BackColor = settings.date_color;
            meal_main_color.BackColor = settings.meal_main_color;
            meal_sub_color.BackColor = settings.meal_sub_color;
            class_main_color.BackColor = settings.class_main_color;
            class_sub_color.BackColor = settings.class_sub_color;

            startup_check.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue(this.Name) != null);

            backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
            checker.Start();
            start_btn.Text = "중지";
        }

        private bool in_time(string start, string end) {
            return DateTime.Parse(start) <= DateTime.Now && DateTime.Parse(end) > DateTime.Now;
        }

        private void checker_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Parse("08:30:00") > now && now_wallpaper != "morning") {
                set_event("조례 및 아침 시간");
                now_wallpaper = "morning";
            } else if (in_time(times[0], times[1]) && now_wallpaper != "break 1") {
                set_break(1);
                now_wallpaper = "break 1";
            } else if (in_time(times[1], times[2]) && now_wallpaper != "subject 1") {
                set_subject(1);
                now_wallpaper = "subject 1";
            } else if (in_time(times[2], times[3]) && now_wallpaper != "break 2") { 
                set_break(2);
                now_wallpaper = "break 2";
            } else if (in_time(times[3], times[4]) && now_wallpaper != "subject 2") { 
                set_subject(2);
                now_wallpaper = "subject 2";
            } else if (in_time(times[4], times[5]) && now_wallpaper != "break 3") { 
                set_break(3);
                now_wallpaper = "break 3";
            } else if (in_time(times[5], times[6]) && now_wallpaper != "subject 3") { 
                set_subject(3);
                now_wallpaper = "subject 3";
            } else if (in_time(times[6], times[7]) && now_wallpaper != "break 4") { 
                set_break(4);
                now_wallpaper = "break 4";
            } else if (in_time(times[7], times[8]) && now_wallpaper != "subject 4") { 
                set_subject(4);
                now_wallpaper = "subject 4";
            } else if (in_time(times[8], times[9]) && now_wallpaper != "break 5") { 
                set_break(5);
                now_wallpaper = "break 5";
            } else if (in_time(times[9], times[10]) && now_wallpaper != "subject 5") { 
                set_subject(5);
                now_wallpaper = "subject 5";
            } else if (in_time(times[10], times[11]) && now_wallpaper != "break 6") { 
                set_break(6);
                now_wallpaper = "break 6";
            } else if (in_time(times[11], times[12]) && now_wallpaper != "subject 6") { 
                set_subject(6);
                now_wallpaper = "subject 6";
            } else if (in_time(times[12], times[13]) && now.DayOfWeek != DayOfWeek.Wednesday && now_wallpaper != "break 7") { 
                set_break(7);
                now_wallpaper = "break 7";
            } else if (in_time(times[13], times[14]) && now.DayOfWeek != DayOfWeek.Wednesday && now_wallpaper != "subject 7") { 
                set_subject(7);
                now_wallpaper = "subject 7";
            } else if (DateTime.Parse(times[12]) < now && now_wallpaper != "after") {
                set_event("종레 시간");
                now_wallpaper = "after";
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backup_wallpaper.Length > 0) {
                set_wallpaper(backup_wallpaper);
            }
        }

        private void date_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = date_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                date_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void date_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                date_color.BackColor = cd.Color;
            }
        }

        private void meal_main_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = meal_main_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                meal_main_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void meal_main_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                meal_main_color.BackColor = cd.Color;
            }
        }

        private void meal_sub_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = meal_sub_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                meal_sub_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void meal_sub_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                meal_sub_color.BackColor = cd.Color;
            }
        }

        private void class_main_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = class_main_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                class_main_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void class_main_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                class_main_color.BackColor = cd.Color;
            }
        }

        private void class_sub_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = class_sub_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                class_sub_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void class_sub_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                class_sub_color.BackColor = cd.Color;
            }
        }

        private void startup_check_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (startup_check.Checked) {
                key.SetValue(this.Name, Application.ExecutablePath);
            } else {
                key.DeleteValue(this.Name);
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings settings = new Properties.Settings();
            settings.class_x_bar = class_x_bar.Value;
            settings.class_y_bar = class_y_bar.Value;
            settings.date_x_bar = date_x_bar.Value;
            settings.meal_y_bar = meal_y_bar.Value;

            settings.date_font = date_font_box.Text;
            settings.meal_main_font = meal_main_font_box.Text;
            settings.meal_sub_font = meal_sub_font_box.Text;
            settings.class_main_font = class_main_font_box.Text;
            settings.class_sub_font = class_sub_font_box.Text;

            settings.date_color = date_color.BackColor;
            settings.meal_main_color = meal_main_color.BackColor;
            settings.meal_sub_color = meal_sub_color.BackColor;
            settings.class_main_color = class_main_color.BackColor;
            settings.class_sub_color = class_sub_color.BackColor;

            settings.Save();
        }
    }
}
    