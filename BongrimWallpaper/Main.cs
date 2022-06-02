using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using hap = HtmlAgilityPack;
using System.Drawing.Text;

namespace BongrimWallpaper
{
    public partial class Main : Form
    {
        public Main() {
            InitializeComponent();
        }

        readonly string[] times = new string[] { "08:30", "08:40", "09:30", "09:40", "10:30", "10:40", "11:30", "11:40", "12:30", "13:30", "14:20", "14:30", "15:20", "15:35", "16:25" };

        bool force_exit = false;
        string backup_wallpaper = "";
        int now_wallpaper = -2;

        //배경설정 메서드
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void set_wallpaper(string path) {
            new Thread(() => { SystemParametersInfo(20, 0, path, 0x01 | 0x02); }).Start();
        }

        //json 관련 메서드
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

       private List<string[]> get_meal() {
            try {
                List<string[]> output = new List<string[]>();
                WebClient wc = new WebClient();
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
                wc.QueryString.Add("dietDate",DateTime.Now.ToString("yyyy/MM/dd"));
                wc.Encoding = Encoding.UTF8;
                string html = wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do");

                hap.HtmlDocument htmlDoc = new hap.HtmlDocument();
                htmlDoc.LoadHtml(html);

                hap.HtmlNode lunch = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[2]/table/tbody/tr[2]/td");
                hap.HtmlNode dinner = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[3]/table/tbody/tr[2]/td");

                if (string.IsNullOrWhiteSpace(lunch.InnerHtml.Trim())) {
                    return null;
                }
                output.Add(Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n'));
                int length = output[0].Length;
                for(int i = 0; i < length; i++) {
                    output[0][i] = output[0][i].Trim();
                }
                if (dinner != null) {
                    output.Add(Regex.Replace(dinner.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n'));
                    length = output[1].Length;
                    for(int i = 0; i < length; i++) {
                        output[1][i] = output[1][i].Trim();
                }
                }
                return output;
            }
            catch {
                return null;
            }
        }

        private void draw_base(ref Graphics g, Bitmap image, int lesson)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SizeF baseSize = new SizeF(image.Width, image.Height);
            Properties.Settings config = Properties.Settings.Default;
            if (config.dateVisible) {
                string today = DateTime.Now.ToString(config.dateFormat);
                SizeF dateSize = g.MeasureString(today, config.dateFont, baseSize, StringFormat.GenericTypographic);
                float dateX = config.dateX - (dateSize.Width / 2);
                float dateY = config.dateY - (dateSize.Height / 2);

                g.DrawString(today, config.dateFont, new SolidBrush(config.dateColor), new RectangleF(dateX, dateY, image.Width, image.Height), StringFormat.GenericTypographic);
            }

            if (config.mealVisible) {
                SolidBrush mealTitleSB = new SolidBrush(config.mealTitleColor);
                SolidBrush mealContentSB = new SolidBrush(config.mealContentColor);

                List<string[]> meals = get_meal();
                float mealX = config.mealX;
                float mealY = config.mealY;
                float contentSpace = config.mealContentSpace;

                if (config.mealLayout == 0) {
                    if (config.mealAlignment == 0) {
                        if (meals.Count >= 1) {
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            if (meals.Count == 2) {
                                mealY += config.mealSpace;
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, new SizeF(image.Width, image.Height), StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    } else if (config.mealAlignment == 1) {
                        if (meals.Count >= 1) {
                            mealX = config.mealX - (g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                mealX = config.mealX - (g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            if (meals.Count == 2) {
                                mealY += config.mealSpace;
                                mealX = config.mealX - (g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    mealX = config.mealX - (g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    } else if (config.mealAlignment == 2) {
                        if (meals.Count >= 1) {
                            mealX = config.mealX - g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                mealX = config.mealX - g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            if (meals.Count == 2) {
                                mealY += config.mealSpace;
                                mealX = config.mealX - g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    mealX = config.mealX - g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    }
                } else {
                    if (config.mealAlignment == 0) {
                        if (meals.Count >= 1) {
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }

                            if (meals.Count == 2) {
                                float maxWidth = g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                foreach (string meal in meals[0]) { 
                                    float width = g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    if (maxWidth < width) maxWidth = width;
                                }
                                mealX = config.mealX + maxWidth + config.mealSpace;
                                mealY = config.mealY;
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    } else if (config.mealAlignment == 1) {
                        if (meals.Count >= 1) {
                            float halfMealSpace = config.mealSpace / 2;
                            float maxWidth = g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[0]) {
                                float width = g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth < width) maxWidth = width;
                            }
                            mealX = config.mealX - (halfMealSpace + (maxWidth / 2) + g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                mealX = config.mealX - (halfMealSpace + (maxWidth / 2) + g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            if (meals.Count == 2) {
                                maxWidth = g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                foreach (string meal in meals[1]) {
                                    float width = g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    if (maxWidth < width) maxWidth = width;
                                }
                                mealY = config.mealY;
                                mealX = config.mealX + (halfMealSpace + (maxWidth / 2) - (g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width / 2));
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    mealX = config.mealX + (halfMealSpace + (maxWidth / 2) - (g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width / 2));
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    } else {
                        if (meals.Count >= 1) {
                            mealX = config.mealX - g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString("중식 [Lunch]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach(string meal in meals[0]) {
                                mealX = config.mealX - g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            if (meals.Count == 2) {
                                float maxWidth = g.MeasureString("중식 [Lunch]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                foreach (string meal in meals[0]) {
                                    float width = g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    if (maxWidth< width) maxWidth = width;
                                }
                                float space = maxWidth + config.mealSpace;
                                mealY = config.mealY;
                                mealX = config.mealX - space - g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString("석식 [Dinner]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString("석식 [Dinner]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string meal in meals[1]) {
                                    mealX = config.mealX - space - g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    g.DrawString(meal, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(meal, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                            }
                        } else {
                            g.DrawString("급식이 없습니다..", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                        }
                    }
                }
            }
            if (config.listVisible) {
                Subject subject = get_timetable();
                int subjectCount = subject.name.Length;
                List<SizeF> subjectSizes = new List<SizeF>();

                SolidBrush subjectSB = new SolidBrush(config.listSubjectColor);
                SolidBrush subjectAccentSB = new SolidBrush(config.listSubjectAccentColor);

                float listSpace = config.listSpace;

                if (config.listTeacherVisible) {
                    List<SizeF> teacherSizes = new List<SizeF>();
                    SolidBrush teacherSB = new SolidBrush(config.listTeacherColor);
                    SolidBrush teacherAccentSB = new SolidBrush(config.listTeacherAccentColor);
                    for (int i = 0; i < subjectCount; i++) {
                        subjectSizes.Add(g.MeasureString(subject.name[i], config.listSubjectFont, baseSize, StringFormat.GenericTypographic));
                        teacherSizes.Add(g.MeasureString(subject.teacher[i], config.listTeacherFont, baseSize, StringFormat.GenericTypographic));
                    }
                    if (config.listLayout == 0) {
                        float allHeight = subjectSizes.Sum(s => s.Height);
                        float listX = config.listX;
                        float listY = config.listY - ((allHeight + (subjectCount - 1) * listSpace) / 2);
                        
                        for (int i = 0; i < subjectCount; i++) {
                            g.DrawString(subject.name[i], config.listSubjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listX += subjectSizes[i].Width + 10;
                            listY += subjectSizes[i].Height - teacherSizes[i].Height;
                            g.DrawString(subject.teacher[i], config.listTeacherFont, (i==lesson-1) ? teacherAccentSB : teacherSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listX = config.listX;
                            listY += teacherSizes[i].Height + listSpace;
                        }
                    } else {
                        float allSubjectWidth = subjectSizes.Sum(s => s.Width);
                        float allTeacherWidth = teacherSizes.Sum(t => t.Width);
                        float listX = config.listX - ((((allSubjectWidth > allTeacherWidth) ? allSubjectWidth : allTeacherWidth) + ((subjectCount - 1) * listSpace)) / 2);
                        float listY = config.listY;

                        for (int i = 0; i < subjectCount; i++) {
                            float maxWidth = (subjectSizes[i].Width > teacherSizes[i].Width) ? subjectSizes[i].Width : teacherSizes[i].Width;
                            listX += (maxWidth - subjectSizes[i].Width) / 2;
                            g.DrawString(subject.name[i], config.listSubjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listX += (subjectSizes[i].Width - teacherSizes[i].Width) / 2;
                            listY += subjectSizes[i].Height;
                            g.DrawString(subject.teacher[i], config.listTeacherFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listX += (teacherSizes[i].Width + maxWidth) / 2 + listSpace;
                            listY = config.listY;
                        }
                    }
                } else {
                    foreach (string s in subject.name) subjectSizes.Add(g.MeasureString(s, config.listSubjectFont, baseSize, StringFormat.GenericTypographic));
                    if (config.mealLayout == 0) {
                        float allHeight = subjectSizes.Sum(s => s.Height);
                        float listX = config.listX;
                        float listY = config.listY - ((allHeight + (subjectCount - 1) * listSpace) / 2);
                        for (int i = 0 ; i < subjectCount; i++) {
                            g.DrawString(subject.name[i], config.listSubjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listY += subjectSizes[i].Height + listSpace;
                        }
                    } else {
                        float allWidth = subjectSizes.Sum(s => s.Width);
                        float listX = config.listX - ((allWidth + (subjectCount - 1) * listSpace) / 2);
                        float listY = config.listY;
                        for (int i = 0; i < subjectCount; i++) {
                            g.DrawString(subject.name[i], config.listSubjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                            listX += subjectSizes[i].Width + listSpace;
                        }
                    }
                }
            }
            if (config.weekVisible) {
                int dayOfYear = DateTime.Now.DayOfYear;
                if (dayOfYear != config.weekLastWeek) {
                    for (int i = 0; i < config.weekLastNums.Length; i++)
                        config.weekLastNums[i] = (config.weekLastNums[i] + config.weekLastNums.Length) % config.students.Count;
                    config.weekLastWeek = dayOfYear;
                    config.Save();
                }
                string text = "주번 : ";
                foreach (int i in config.weekLastNums)
                    text += $"{config.students[i]} ";
                SolidBrush sb = new SolidBrush(config.weekColor);

                g.DrawString(text, config.weekFont, sb, config.weekX, config.weekY, StringFormat.GenericTypographic);
            }
        }
        private void set_subject(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image, lesson);
            
            //현재 시간 및 과목 그리기
            Properties.Settings config = Properties.Settings.Default;
            if (config.classVisible) {
                Subject subject = get_timetable();
                string time = $"{times[lesson * 2 - 1]} ~ {times[lesson * 2]}";

                SolidBrush mainSB = new SolidBrush(config.classMainColor);
                SolidBrush subSB = new SolidBrush(config.classSubColor);

                SizeF baseSize = new SizeF(image.Width, image.Height);

                SizeF lessonSize = g.MeasureString($"{lesson} 교시", config.classSubFont, baseSize);
                SizeF nameSize = g.MeasureString(subject.name[lesson - 1], config.classMainFont, baseSize, StringFormat.GenericTypographic);
                SizeF teacherSize = g.MeasureString(subject.teacher[lesson - 1], config.classSubFont, baseSize, StringFormat.GenericTypographic);
                SizeF timeSize = g.MeasureString(time, config.classSubFont, baseSize);

                if (config.classAlignment == 0) {
                    float classX = config.classX;
                    float classY = config.classY - 
                        ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

                    g.DrawString($"{lesson} 교시", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += lessonSize.Height;

                    g.DrawString(subject.name[lesson-1], config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX += nameSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(subject.teacher[lesson-1], config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX;
                    classY += teacherSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (config.classAlignment == 1) {
                    float classX = config.classX - (lessonSize.Width / 2);
                    float classY = config.classY - 
                        ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);
                    
                    g.DrawString($"{lesson} 교시", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - ((nameSize.Width + teacherSize.Width + 10) / 2);
                    classY += lessonSize.Height;

                    g.DrawString(subject.name[lesson-1], config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX += nameSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(subject.teacher[lesson-1], config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - (timeSize.Width / 2);
                    classY += teacherSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else {
                    float classX = config.classX - lessonSize.Width;
                    float classY = config.classY - 
                        ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

                    g.DrawString($"{lesson} 교시", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - nameSize.Width;
                    classY += lessonSize.Height;

                    g.DrawString(subject.name[lesson-1], config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX -= teacherSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(subject.teacher[lesson-1], config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - timeSize.Width;
                    classY += teacherSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                }
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            wallpaper_preview.ImageLocation = save_path;
            set_wallpaper(save_path);
        }

        private void set_break(int lesson) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image, lesson);

            Properties.Settings config = Properties.Settings.Default;

            if (config.classVisible) {
                string title = "쉬는 시간";
                switch (lesson) {
                    case 5:
                        title = "점심 시간";
                        break;
                    case 7:
                        title = "청소 시간";
                        break;
                }

                Subject subject = get_timetable();
                string time = $"{times[lesson * 2 - 2]} ~ {times[lesson * 2 - 1]}";

                SolidBrush mainSB = new SolidBrush(config.classMainColor);
                SolidBrush subSB = new SolidBrush(config.classSubColor);

                SizeF baseSize = new SizeF(image.Width, image.Height);

                SizeF nextSize = g.MeasureString($"Next {subject.name[lesson - 1]}({subject.teacher[lesson - 1]})", config.classSubFont, baseSize);
                SizeF titleSize = g.MeasureString(title, config.classMainFont, baseSize, StringFormat.GenericTypographic);
                SizeF timeSize = g.MeasureString(time, config.classSubFont, baseSize);
                
                if (config.classAlignment == 0) {
                    float classX = config.classX;
                    float classY = config.classY - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subject.name[lesson-1]}({subject.teacher[lesson-1]})", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (config.classAlignment == 1) {
                    float classX = config.classX - (nextSize.Width / 2);
                    float classY = config.classY - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subject.name[lesson-1]}({subject.teacher[lesson-1]})", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - (titleSize.Width / 2);
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - (timeSize.Width / 2);
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else {
                    float classX = config.classX - nextSize.Width;
                    float classY = config.classY - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subject.name[lesson-1]}({subject.teacher[lesson-1]})", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - titleSize.Width;
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - timeSize.Width;
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                }
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            wallpaper_preview.ImageLocation = save_path;
            set_wallpaper(save_path);
        }

        private void set_event(string text) {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            draw_base(ref g, image, -1);

            Properties.Settings config = Properties.Settings.Default;

            if (config.classVisible) {
                SolidBrush sb = new SolidBrush(config.classMainColor);
                SizeF textSize = g.MeasureString(text, config.classMainFont, new SizeF(image.Width, image.Height), StringFormat.GenericTypographic);
                float classX = config.classX;
                float classY = config.classY- (textSize.Height / 2); 
                if (config.classAlignment == 1) {
                    classX = config.classX - (textSize.Width / 2);
                } else if (config.classAlignment == 2) {
                    classX = config.classX - textSize.Width;
                }

                g.DrawString(text, config.classMainFont, sb, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
            }

            //저장 및 적용
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            wallpaper_preview.ImageLocation = save_path;
            set_wallpaper(save_path);
        }
        
        private void contextUpdate(bool state) {
            contextMenuStrip1.Items[0].Enabled = !state;
            contextMenuStrip1.Items[1].Enabled = state;
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
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[1].Enabled = true;
                contextUpdate(true);
            } else {
                now_wallpaper = -2;
                set_wallpaper(backup_wallpaper);
                checker.Stop();
                start_btn.Text = "실행";
                contextUpdate(false);
            }
        }

        private void wallpaper_preview_Click(object sender, EventArgs e) {
            ProcessStartInfo psi = new ProcessStartInfo(wallpaper_preview.ImageLocation);
            Process.Start(psi);
        }

        private void Main_Load(object sender, EventArgs e) {
            timetable_path_box.Text = Properties.Settings.Default.timetablePath;
            timetable_path_box.Text = (verify_json()) ? timetable_path_box.Text : "";
            startup_check.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue(this.Text) != null);

            if (timetable_path_box.Text != "") {
                backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
                checker.Start();
                start_btn.Text = "중지";
                contextUpdate(true);
            }
        }

        private void checker_Tick(object sender, EventArgs e) {
            DateTime now = DateTime.Now;

            if (DateTime.Parse("08:30:00") > now && now_wallpaper != -1) {
                set_event("조례 및 아침 시간");
                now_wallpaper = -1;
            }

            int loopCount = (now.DayOfWeek == DayOfWeek.Wednesday) ? 12 : 14;
            for (int i = 1; i <= loopCount; i++) {
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
            fd.Filter = "Image Files (*.jpg *.jpeg *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.backgroundPath = fd.FileName;
                Properties.Settings.Default.Save();
                wallpaper_preview.ImageLocation = fd.FileName;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (!force_exit) {
                e.Cancel = true;
                this.Hide();
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
            this.Activate();
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
                    Properties.Settings.Default.timetablePath = ofd.FileName;
                    MessageBox.Show("올바른 시간표 파일로 확인이 되었음!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("내가 원하는 양식의 시간표가 아님..", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    timetable_path_box.Text = "";
                }
            }
        }
        private void openClassFormBtn_Click(object sender, EventArgs e)
        {
            ClassForm cf = new ClassForm();
            cf.Show();
        }

        private void openMealFormBtn_Click(object sender, EventArgs e)
        {
            MealForm mf = new MealForm();
            mf.Show();
        }

        private void openDateFormBtn_Click(object sender, EventArgs e)
        {
            DateForm df = new DateForm();
            df.Show();
        }

        private void openListFormBtn_Click(object sender, EventArgs e)
        {
            ListForm lf = new ListForm();
            lf.Show();
        }

        private void 시작ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timetable_path_box.Text == "") {
                MessageBox.Show("먼저 시간표 파일을 선택해주셈", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            backup_wallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
            checker.Start();
            start_btn.Text = "중지";
            contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[1].Enabled = true;
            contextUpdate(true);
        }

        private void 중지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            now_wallpaper = -2;
            set_wallpaper(backup_wallpaper);
            checker.Stop();
            start_btn.Text = "실행";
            contextUpdate(false);
        }

        private void openWeekFormBtn_Click(object sender, EventArgs e)
        {
            WeekForm wf = new WeekForm();
            wf.Show();
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
