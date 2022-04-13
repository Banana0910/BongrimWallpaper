using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading;
using System.Windows.Forms;
using hap = HtmlAgilityPack;

namespace BongrimWallpaper
{
    public partial class Main : Form
    {
        public Main() {
            InitializeComponent();
        }
        private bool verify_json() {
            if (timetable_path_box.Text == "") return false;
            string jsonString = File.ReadAllText(timetable_path_box.Text);
            try {
                TimeTable timetable = JsonSerializer.Deserialize<TimeTable>(jsonString);
                if (timetable.weekday.Count == 5) {
                    for (int i = 0; i < 5; i++) {
                        if (i != 2) {
                            if (timetable.weekday[i].name.Length != 7 || timetable.weekday[i].teacher.Length != 7)
                                return false;
                        } else {
                            if (timetable.weekday[i].name.Length != 6 || timetable.weekday[i].teacher.Length != 6)
                                return false;
                        }
                    }
                } else {
                    return false;
                }
                return true;
            } catch {
                return false;
            }
        }
        private Subject get_timetable() {
            string jsonString = File.ReadAllText(timetable_path_box.Text);
            TimeTable timetable = JsonSerializer.Deserialize<TimeTable>(jsonString);
            return timetable.weekday[(int)DateTime.Now.DayOfWeek-1];
        }

        readonly string[] times = new string[] { "08:35", "08:40", "09:30", "09:40", "10:30", "10:40", " 11:30", "11:40", "12:30", "13:30", "14:20", "14:30", "15:20", "15:35", "16:25" };

        bool force_exit = false;

        string backup_wallpaper = "";
        string background = "";

        int now_wallpaper = -2;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void set_wallpaper(string path) {
            new Thread(() => { SystemParametersInfo(20, 0, path, 0x01 | 0x02); }).Start();
        }

        private Font toFont(TextBox tb) {
            string[] splited = tb.Text.Split(',');
            return new Font(splited[0], float.Parse(splited[1]));
        }

        private string[] get_meal() {
            try {
                WebClient wc = new WebClient();
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
                wc.QueryString.Add("dietDate", DateTime.Now.ToString("yyyy/MM/dd"));
                wc.Encoding = Encoding.UTF8;
                string html = wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do");

                hap.HtmlDocument htmlDoc = new hap.HtmlDocument();
                htmlDoc.LoadHtml(html);

                hap.HtmlNode lunch = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[2]/table/tbody/tr[2]/td");
                hap.HtmlNode dinner = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[3]/table/tbody/tr[2]/td");

                if (string.IsNullOrWhiteSpace(lunch.InnerHtml.Trim())) {
                    return null;
                }
                if (dinner != null) {
                    string _lunch = Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                    string _dinner = Regex.Replace(dinner.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                    return new string[] { _lunch, _dinner };
                }
                else {
                    string _lunch = Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ");
                    return new string[] { _lunch };
                }
            }
            catch {
                return null;
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (start_btn.Text == "실행") {
                if (timetable_path_box.Text == "") {
                    MessageBox.Show("먼저 시간표 파일을 선택해주셈", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
                checker.Start();
                start_btn.Text = "중지";
            } else {
                now_wallpaper = -2;
                set_wallpaper(backup_wallpaper);
                checker.Stop();
                start_btn.Text = "실행";
            }
        }

        private void draw_base(ref Graphics g, Bitmap image)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (background.Length > 0) g.DrawImage(Image.FromFile(background), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);
            // 날짜 그리기
            if (date_visible_check.Checked) {
                string today = DateTime.Now.ToString("yyyy년 MM월 dd일");
                string[] font = date_font_box.Text.Split(',');
                Font date_font = new Font(font[0], float.Parse(font[1]));
                int date_x = date_x_bar.Value - (TextRenderer.MeasureText(today, date_font).Width / 2);

                g.DrawString(today, date_font, new SolidBrush(date_color.BackColor), new Rectangle(date_x, 10, image.Width, image.Height));
            }

            //급식 그리기
            if (meal_visible_check.Checked) {
                string[] meal = get_meal();


                Font meal_title = toFont(meal_main_font_box);
                Font meal_description = toFont(meal_sub_font_box);

                SolidBrush meal_title_sb = new SolidBrush(meal_main_color.BackColor);
                SolidBrush meal_description_sb = new SolidBrush(meal_sub_color.BackColor);

                int meal_y = (meal_y_bar.Maximum - meal_y_bar.Value);

                StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };

                if (meal != null) {
                    g.DrawString("중식 [lunch]", meal_title, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
                    meal_y += TextRenderer.MeasureText("중식 [lunch]", meal_title).Height + 10;

                    g.DrawString(meal[0], meal_description, meal_description_sb, new RectangleF(-8, meal_y, image.Width, image.Height), format);
                    meal_y += TextRenderer.MeasureText(meal[0], meal_description).Height + 50;

                    if (meal.Length > 1) { // 석식이 있다면
                        g.DrawString("석식 [dinner]", meal_title, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
                        meal_y += TextRenderer.MeasureText("석식 [dinner]", meal_title).Height + 10;

                        g.DrawString(meal[1], meal_description, meal_description_sb, new RectangleF(-8, meal_y, image.Width, image.Height), format);
                    }
                }
                else {
                    g.DrawString("급식이 없습니다..", meal_title, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
                }
            }
        }

        private void set_subject(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image);

            //현재 시간 및 과목 그리기
            if (class_visible_check.Checked) {
                Font main_font = toFont(class_main_font_box);
                Font sub_font = toFont(class_sub_font_box);

                SolidBrush main_sb = new SolidBrush(class_main_color.BackColor);
                SolidBrush sub_sb = new SolidBrush(class_sub_color.BackColor);

                Subject subject = get_timetable();
                string time = $"{times[lesson * 2 - 1]} ~ {times[lesson * 2]}";

                Size lesson_size = TextRenderer.MeasureText($"{lesson} 교시", sub_font);
                Size name_size = TextRenderer.MeasureText(subject.name[lesson - 1], main_font);
                Size teacher_size = TextRenderer.MeasureText(subject.teacher[lesson - 1], sub_font);
                Size time_size = TextRenderer.MeasureText(time, sub_font);

                int center_x = class_x_bar.Value - (lesson_size.Width / 2);
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((lesson_size.Height + name_size.Height + teacher_size.Height + time_size.Height) / 2);

                g.DrawString($"{lesson} 교시", sub_font, sub_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
                center_x = class_x_bar.Value - ((name_size.Width + teacher_size.Width + 20) / 2);
                center_y += lesson_size.Height;

                g.DrawString(subject.name[lesson - 1], main_font, main_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
                center_x += name_size.Width - 10;
                center_y += name_size.Height - teacher_size.Height - 10;

                g.DrawString(subject.teacher[lesson - 1], sub_font, sub_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
                center_y += teacher_size.Height + 10;
                center_x = class_x_bar.Value - (time_size.Width / 2);

                g.DrawString(time, sub_font, sub_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = save_path;
            set_wallpaper(save_path);
        }

        private void set_break(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image);

            if (class_visible_check.Checked) {
                string[] main_font_sp = class_main_font_box.Text.Split(',');
                string[] sub_font_sp = class_sub_font_box.Text.Split(',');

                Font main_font = new Font(main_font_sp[0], float.Parse(main_font_sp[1]));
                Font sub_font = new Font(sub_font_sp[0], float.Parse(sub_font_sp[1]));

                SolidBrush main_sb = new SolidBrush(class_main_color.BackColor);
                SolidBrush sub_sb = new SolidBrush(class_sub_color.BackColor);

                string title = "쉬는 시간";
                switch (lesson) {
                    case 5:
                        title = "점심 시간";
                        break;
                    case 7:
                        title = "청소 시간";
                        break;
                }
                string time = $"{times[lesson * 2 - 2]} ~ {times[lesson * 2 - 1]}";

                Subject subject = get_timetable();

                Size next_size = TextRenderer.MeasureText($"Next {subject.name[lesson - 1]}({subject.teacher[lesson - 1]})", sub_font);
                Size title_size = TextRenderer.MeasureText(title, main_font);
                Size time_size = TextRenderer.MeasureText(time, sub_font);

                int center_x = class_x_bar.Value - (next_size.Width / 2);
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - ((next_size.Height + title_size.Height + time_size.Height) / 2);

                g.DrawString($"Next {subject.name[lesson - 1]}({subject.teacher[lesson - 1]})", sub_font, sub_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
                center_x = class_x_bar.Value - (title_size.Width / 2);
                center_y += next_size.Height;

                g.DrawString(title, main_font, main_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
                center_x = class_x_bar.Value - (time_size.Width / 2);
                center_y += title_size.Height;

                g.DrawString(time, sub_font, sub_sb, new RectangleF(center_x, center_y, image.Width, image.Height));
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = save_path;
            set_wallpaper(save_path);
        }

        private void set_event(string text) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image);

            if (class_visible_check.Checked) {
                string[] main_font_sp = class_main_font_box.Text.Split(',');
                Font font = new Font(main_font_sp[0], float.Parse(main_font_sp[1]));
                SolidBrush sb = new SolidBrush(class_main_color.BackColor);
                int center_y = (class_y_bar.Maximum - class_y_bar.Value) - (TextRenderer.MeasureText(text, font).Height / 2);
                int center_x = class_x_bar.Value - (TextRenderer.MeasureText(text, font).Width / 2);

                g.DrawString(text, font, sb, new RectangleF(center_x, center_y, image.Width, image.Height));
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = save_path;
            set_wallpaper(save_path);
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            ProcessStartInfo psi = new ProcessStartInfo(pictureBox1.ImageLocation);
            Process.Start(psi);
        }

        private void Main_Load(object sender, EventArgs e) {
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

            timetable_path_box.Text = settings.timetable_path;
            timetable_path_box.Text = (verify_json()) ? timetable_path_box.Text : "";
            startup_check.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue(this.Text) != null);

            background = (settings.background != "") ? settings.background : "";

            if (timetable_path_box.Text != "") {
                backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
                checker.Start();
                start_btn.Text = "중지";
            }
        }

        private void checker_Tick(object sender, EventArgs e) {
            DateTime now = DateTime.Now;

            if (DateTime.Parse("08:30:00") > now && now_wallpaper != -1) {
                set_event("조례 및 아침 시간");
                now_wallpaper = -1;
            }

            for (int i = 1; i <= ((now.DayOfWeek == DayOfWeek.Wednesday) ? 12 : 14); i++) {
                if (DateTime.Parse(times[i - 1]) <= now && DateTime.Parse(times[i]) > now && now_wallpaper != i) {
                    if (i % 2 == 1) set_break((i + 1) / 2);
                    else set_subject(i / 2);
                    now_wallpaper = i;
                    return;
                }
            }

            bool condition = (DateTime.Parse(times[12]) <= now && now.DayOfWeek == DayOfWeek.Wednesday) ||
                (DateTime.Parse(times[14]) <= now && now.DayOfWeek != DayOfWeek.Wednesday) && now_wallpaper != 15;
            if (condition) {
                set_event("종례 시간");
                now_wallpaper = 15;
            }
        }

        private void main_wall_btn_Click(object sender, EventArgs e) {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files (*.jpg *.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK) {
                background = fd.FileName;
                pictureBox1.ImageLocation = fd.FileName;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (!force_exit) {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void date_font_btn_Click(object sender, EventArgs e) {
            FontDialog fd = new FontDialog();
            string[] font = date_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK) {
                date_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void date_color_btn_Click(object sender, EventArgs e) {
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
            if (fd.ShowDialog() == DialogResult.OK)
            {
                meal_main_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void meal_main_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                meal_main_color.BackColor = cd.Color;
            }
        }

        private void meal_sub_font_btn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            string[] font = meal_sub_font_box.Text.Split(',');
            fd.Font = new Font(font[0], float.Parse(font[1]));
            if (fd.ShowDialog() == DialogResult.OK)
            {
                meal_sub_font_box.Text = $"{fd.Font.Name},{fd.Font.Size}";
            }
        }

        private void meal_sub_color_btn_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = date_color.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
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
                key.SetValue(this.Text, Application.ExecutablePath);
            } else {
                key.DeleteValue(this.Text);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            this.Show();    
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (backup_wallpaper.Length > 0) {
                set_wallpaper(backup_wallpaper);
            }
            force_exit = true;
            Application.Exit();
        }

        private void timetable_path_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = (timetable_path_box.Text != "") ? timetable_path_box.Text : Application.StartupPath
            };

            if (ofd.ShowDialog() == DialogResult.OK) {
                timetable_path_box.Text = ofd.FileName;
                if (verify_json()) {
                    MessageBox.Show("올바른 시간표 파일로 확인이 되었음!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("내가 원하는 양식의 시간표가 아님..", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    timetable_path_box.Text = "";
                }
            }
        }

        private void settings_save_btn_Click(object sender, EventArgs e) {
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

            settings.timetable_path = timetable_path_box.Text;
            settings.background = background;

            settings.Save();
            MessageBox.Show("설정이 저장됨!", this.Name, MessageBoxButtons.OK, MessageBoxIcon.None);
        }   
    }

    public class Subject {
        public string[] name { get; set; }
        public string[] teacher { get; set; }
    }

    public class TimeTable {
        public List<Subject> weekday { get; set; }
        public TimeTable() {
            this.weekday = new List<Subject>();
        }
    }
}
