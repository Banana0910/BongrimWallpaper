using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using HtmlAgilityPack;
using System.IO;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;

namespace BongrimWallpaper
{
    public partial class Main : Form
    {
        public Main() { InitializeComponent(); }

        private readonly string[] times = new string[] { 
            "08:30", "08:40", 
            "09:30", "09:40", 
            "10:30", "10:40", 
            "11:30", "11:40", 
            "12:30", "13:30", 
            "14:20", "14:30", 
            "15:20", "15:35", 
            "16:25"
        };
        private bool forceExit = false;
        private string backupWallpaper = "";
        private int nowWallpaper = -2;

        // Methods
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public static void setWallpaper(string path) { SystemParametersInfo(20, 0, path, 0x01 | 0x02); }

        private bool verifyTimeTable() {
            if (timetablePathBox.Text == "") return false;
            if (!File.Exists(timetablePathBox.Text)) return false;
            string jsonString = File.ReadAllText(timetablePathBox.Text);
            try {
                TimeTable timetable = JsonSerializer.Deserialize<TimeTable>(jsonString);
                if (timetable.weekday.Count == 5) {
                    for (int i = 0; i < 5; i++) {
                        if (i != 2) { if (timetable.weekday[i].name.Length != 7 || timetable.weekday[i].teacher.Length != 7) return false; } 
                        else { if (timetable.weekday[i].name.Length != 6 || timetable.weekday[i].teacher.Length != 6) return false; }
                    }
                } else { return false; }
                return true;
            } catch { return false; }
        }

        private Subject getTimeTable() {
            string jsonString = File.ReadAllText(timetablePathBox.Text);
            TimeTable timetable = JsonSerializer.Deserialize<TimeTable>(jsonString);
            return timetable.weekday[(int)DateTime.Now.DayOfWeek-1];
        }
        
        private int getNowWeekCount() {
            int weekCount = 0;
            DateTime today = DateTime.Today;
            for (int i = 1; i <= today.AddMonths(1).AddDays(-today.Day).Day; i ++) {
                DateTime d = new DateTime(today.Year, today.Month, i);
                if (d.DayOfWeek == DayOfWeek.Sunday) weekCount++;
                if (d == today) return weekCount;  
            } 
            return -1;
        }

        private string[] trimMeal(string target) {
            string[] output = Regex.Replace(Regex.Replace(target.Trim(), @"\n|[0-9\.]{2,}", ""), @"<br\s*/?>", "\n")
                .Replace("&nbsp", " ").Replace("()", "").Split('\n');
            int outputLength = output.Length;
            for (int i = 0; i < outputLength; i++) output[i] = output[i].Trim();
            return output;
        }

        private List<Meal> getMealNeis() {
            List<Meal> output = new List<Meal>();
            try {
                WebClient wc = new WebClient() {
                    QueryString = new System.Collections.Specialized.NameValueCollection() {
                        { "KEY", "f0491ec9a1784e2cb92d2a4070f1392b" },
                        { "ATPT_OFCDC_SC_CODE", "S10" },
                        { "SD_SCHUL_CODE", "9010277" },
                        { "MLSV_YMD", DateTime.Today.ToString("yyyyMMdd") }
                    },
                    Encoding = Encoding.UTF8
                };     
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(wc.DownloadString("https://open.neis.go.kr/hub/mealServiceDietInfo"));
                if (xmlDoc.GetElementsByTagName("head").Count == 0) return output;

                XmlNodeList meals = xmlDoc.GetElementsByTagName("row");
                foreach (XmlNode meal in meals)
                    output.Add(new Meal(meal["MMEAL_SC_NM"].InnerText, trimMeal(meal["DDISH_NM"].InnerText), meal["CAL_INFO"].InnerText));
                return output;
            } catch { return output; }
        }

        private List<Meal> getMealPage() {
            List<Meal> output = new List<Meal>();
            try {
                WebClient wc = new WebClient() {
                    QueryString = new System.Collections.Specialized.NameValueCollection() {
                        { "dietDate", DateTime.Today.ToString("yyyyMMdd") }
                    },
                    Encoding = Encoding.UTF8,
                };
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do"));

                HtmlNodeCollection meals = htmlDoc.DocumentNode.SelectNodes("//*[@id='subContent']/div/div[3]/div[contains(@class, 'BD_table')]");
                if (string.IsNullOrWhiteSpace(meals[0].SelectSingleNode(".//table/tbody/tr[1]/td").InnerText.Trim())) return output;
                foreach (HtmlNode meal in meals) {
                    output.Add(new Meal(
                        meal.SelectSingleNode(".//table/tbody/tr[1]/td").InnerText.Trim(),
                        trimMeal(meal.SelectSingleNode(".//table/tbody/tr[2]/td").InnerHtml.Trim()),
                        meal.SelectSingleNode(".//table/tbody/tr[4]/td").InnerText.Trim()
                    ));
                }
                return output;
            } catch { return output; }
        }

        private void updateState(bool state) {
            startBtn.Text = (state) ? "중지" : "시작";
            menu.Items[0].Enabled = !state;
            menu.Items[1].Enabled = state;
            if (state) {
                if (string.IsNullOrWhiteSpace(timetablePathBox.Text)) {
                    MessageBox.Show("먼저 시간표 파일을 선택해주셈", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                backupWallpaper = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop").GetValue("WallPaper").ToString();
                checker.Start();   
            } else {
                nowWallpaper = -2;
                setWallpaper(backupWallpaper);
                checker.Stop();
            }
        }

        // Draw Methods
        private void drawBase(Properties.Settings config, ref Graphics g, Bitmap image, int lesson) {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            string backgroundPath = config.backgroundPath;
            if (backgroundPath.Length > 0) g.DrawImage(Image.FromFile(backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SizeF baseSize = new SizeF(image.Width, image.Height);
            
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

                List<Meal> meals = (config.isNeis) ? getMealNeis() : getMealPage();
                float mealX = config.mealX;
                float mealY = config.mealY;

                if (meals.Count > 0) {
                    float contentSpace = config.mealContentSpace;
                    float mealSpace = config.mealSpace;

                    if (config.mealLayout == 0) { // Vertical
                        if (config.mealAlignment == 0) { // Left Alignmet
                            foreach (Meal meal in meals) {
                                string title = $"{meal.title} [{meal.calorie}]";
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string content in meal.content) {
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                                mealY += mealSpace;
                            }
                        } else if (config.mealAlignment == 1) { // Center Alignment
                            foreach (Meal meal in meals) {
                                string title = $"{meal.title} [{meal.calorie}]";
                                SizeF titleSize = g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                mealX = config.mealX- (titleSize.Width / 2);
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX,mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                foreach (string content in meal.content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic);
                                    mealX = config.mealX- (contentSize.Width / 2);
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                }
                                mealY += mealSpace;
                            }
                        } else if (config.mealAlignment == 2) { // Right Alignment
                            foreach (Meal meal in meals) {
                                string title = $"{meal.title} [{meal.calorie}]";
                                SizeF titleSize = g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                mealX = config.mealX- titleSize.Width;
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                foreach (string content in meal.content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic);
                                    mealX = config.mealX- contentSize.Width;
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                }
                                mealY += mealSpace;
                            }
                        }
                    } else { // Horizontal
                        if (config.mealAlignment == 0) { // Left Alignment
                            foreach (Meal meal in meals) {
                                string title = $"{meal.title} [{meal.calorie}]";
                                SizeF titleSize = g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                float maxWidth = titleSize.Width;
                                foreach (string content in meal.content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic);
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                    if (contentSize.Width > maxWidth) maxWidth = contentSize.Width;
                                }
                                mealX += maxWidth + mealSpace;
                                mealY = config.mealY;
                            }
                        } else if (config.mealAlignment == 1) { // Center Alignment
                            float allWidth = 0f;
                            int mealCount = meals.Count;
                            float[] maxWidths = new float[mealCount];
                            for (int i = 0; i < mealCount; i++) {
                                maxWidths[i] = g.MeasureString($"{meals[i].title} [{meals[i].calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Width;
                                foreach (string content in meals[i].content) {
                                    float width = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Width;
                                    if (width > maxWidths[i]) maxWidths[i] = width;
                                }
                                allWidth += maxWidths[i];
                            }

                            allWidth += mealSpace * (mealCount - 1);
                            mealX = config.mealX - (allWidth / 2);

                            for (int i = 0; i < mealCount; i++) {
                                string title = $"{meals[i].title} [{meals[i].calorie}]";
                                SizeF titleSize = g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                float pivot = mealX + (maxWidths[i] / 2);
                                mealX = pivot - (titleSize.Width / 2);
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                foreach (string content in meals[i].content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericDefault);
                                    mealX = pivot - (contentSize.Width / 2);
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                }
                                mealX = mealSpace + pivot + (titleSize.Width / 2);
                                mealY = config.mealY;
                            }
                        } else if (config.mealAlignment == 2) { // Right Alignment
                            foreach (Meal meal in meals) {
                                string title = $"{meal.title} [{meal.calorie}]";
                                SizeF titleSize = g.MeasureString(title, config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                float startX = mealX;
                                float maxWidth = titleSize.Width;
                                mealX = startX - titleSize.Width;
                                g.DrawString(title, config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                foreach (string content in meal.content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic);
                                    mealX = startX - contentSize.Width;
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                    if (contentSize.Width > maxWidth) maxWidth = contentSize.Width;
                                }
                                mealX = startX - (maxWidth + mealSpace);
                                mealY = config.mealY;
                            }
                        }
                    }
                } else {
                    g.DrawString("급식이 없습니다", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                }
            }
            
            if (config.listVisible) {
                Subject subject = getTimeTable();
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
                    if (config.listLayout == 0) {
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
                int weekCount = getNowWeekCount();
                if (weekCount != config.weekLastWeek) {
                    for (int i = 0; i < config.weekLastNums.Length; i++)
                        config.weekLastNums[i] = (config.weekLastNums[i] + config.weekLastNums.Length) % config.students.Count;
                    config.weekLastWeek = weekCount;
                    config.Save();
                }
                string text = "주번 : ";
                foreach (int i in config.weekLastNums)
                    text += $"{config.students[i]} ";
                SolidBrush sb = new SolidBrush(config.weekColor);

                g.DrawString(text, config.weekFont, sb, config.weekX, config.weekY, StringFormat.GenericTypographic);
            }
        }

        private void drawSubject(int lesson) {
            Properties.Settings config = Properties.Settings.Default;
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            drawBase(config, ref g, image, lesson);
            if (config.classVisible) {
                Subject subject = getTimeTable();
                string time = $"{times[lesson * 2 - 1]} ~ {times[lesson * 2]}";

                SolidBrush mainSB = new SolidBrush(config.classMainColor);
                SolidBrush subSB = new SolidBrush(config.classSubColor);

                SizeF baseSize = new SizeF(image.Width, image.Height);

                SizeF lessonSize = g.MeasureString($"{lesson} 교시", config.classSubFont, baseSize);
                SizeF nameSize = g.MeasureString(subject.name[lesson - 1], config.classMainFont, baseSize, StringFormat.GenericTypographic);
                SizeF teacherSize = g.MeasureString(subject.teacher[lesson - 1], config.classSubFont, baseSize, StringFormat.GenericTypographic);
                SizeF timeSize = g.MeasureString(time, config.classSubFont, baseSize);

                if (config.classAlignment == 0) { // Left Alignment
                    float classX = config.classX;
                    float classY = config.classY - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

                    g.DrawString($"{lesson} 교시", config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += lessonSize.Height;

                    g.DrawString(subject.name[lesson-1], config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX += nameSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(subject.teacher[lesson-1], config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX;
                    classY += teacherSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (config.classAlignment == 1) { // Center Alignment
                    float classX = config.classX - (lessonSize.Width / 2);
                    float classY = config.classY - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

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
                } else { // Right Alignment
                    float classX = config.classX - lessonSize.Width;
                    float classY = config.classY - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);
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
            wallpaperPreview.ImageLocation = save_path;
            setWallpaper(save_path);
        }

        private void drawBreak(int lesson) {
            Properties.Settings config = Properties.Settings.Default;
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            drawBase(config, ref g, image, lesson);
            if (config.classVisible) {
                string title = (lesson == 7) ? "청소 시간" : (lesson == 5) ? "점심 시간" : "쉬는 시간";

                Subject subject = getTimeTable();
                string next = (string.IsNullOrEmpty(subject.teacher[lesson-1]))
                    ? $"Next {subject.name[lesson-1]}" 
                    : $"Next {subject.name[lesson - 1]}({subject.teacher[lesson - 1]})";

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

                    g.DrawString(next, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (config.classAlignment == 1) {
                    float classX = config.classX - (nextSize.Width / 2);
                    float classY = config.classY - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString(next, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - (titleSize.Width / 2);
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - (timeSize.Width / 2);
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else {
                    float classX = config.classX - nextSize.Width;
                    float classY = config.classY - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString(next, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = config.classX - titleSize.Width;
                    classY += nextSize.Height;

                    g.DrawString(title, config.classMainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = config.classX - timeSize.Width;
                    classY += titleSize.Height;

                    g.DrawString(time, config.classSubFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                }
            }
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            wallpaperPreview.ImageLocation = save_path;
            setWallpaper(save_path);
        }

        private void drawEvent(string text) {
            Properties.Settings config = Properties.Settings.Default;
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            drawBase(config, ref g, image, -1);
            if (config.classVisible) {
                SolidBrush sb = new SolidBrush(config.classMainColor);
                SizeF textSize = g.MeasureString(text, config.classMainFont, new SizeF(image.Width, image.Height), StringFormat.GenericTypographic);
                int alignment = config.classAlignment;
                float classX = (alignment == 1) ? config.classX - (textSize.Width / 2) : ((alignment== 2) ? config.classX - textSize.Width : config.classX);
                float classY = config.classY- (textSize.Height / 2); 
                g.DrawString(text, config.classMainFont, sb, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
            }
            string save_path = Path.Combine(Application.StartupPath, "wallpaper.png");
            image.Save(save_path, System.Drawing.Imaging.ImageFormat.Png);
            wallpaperPreview.ImageLocation = save_path;
            setWallpaper(save_path);
        }

        //Interaction Methods
        private void Main_Load(object sender, EventArgs e) {
            timetablePathBox.Text = Properties.Settings.Default.timetablePath;
            timetablePathBox.Text = (verifyTimeTable()) ? timetablePathBox.Text : "";
            startupCheck.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue(this.Text) != null);
            if (!string.IsNullOrEmpty(timetablePathBox.Text)) updateState(true);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (!forceExit) {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void checker_Tick(object sender, EventArgs e) {
            DateTime now = DateTime.Now;

            if (DateTime.Parse("08:30:00") > now && nowWallpaper != -1) {
                drawEvent("조례 시간");
                nowWallpaper = -1;
            }

            int loopCount = (now.DayOfWeek == DayOfWeek.Wednesday) ? 12 : 14;
            for (int i = 1; i <= loopCount; i++) {
                if (DateTime.Parse(times[i - 1]) <= now && DateTime.Parse(times[i]) > now && nowWallpaper != i) {
                    if (i % 2 == 1) drawBreak((i + 1) / 2);
                    else drawSubject(i / 2);
                    nowWallpaper = i;
                    return;
                }
            }

            bool condition = (DateTime.Parse(times[12]) <= now && now.DayOfWeek == DayOfWeek.Wednesday) ||
                (DateTime.Parse(times[14]) <= now && now.DayOfWeek != DayOfWeek.Wednesday) && nowWallpaper != 15;
            if (condition) {
                drawEvent("종례 시간");
                nowWallpaper = 15;
            }
        }

        private void mainWallpaperBtn_Click(object sender, EventArgs e) {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files (*.jpg *.jpeg *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.backgroundPath = fd.FileName;
                Properties.Settings.Default.Save();
                wallpaperPreview.ImageLocation = fd.FileName;
            }
        }

        private void timetablePathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = (string.IsNullOrWhiteSpace(timetablePathBox.Text)) ? Application.StartupPath : timetablePathBox.Text
            };

            if (ofd.ShowDialog() == DialogResult.OK) {
                timetablePathBox.Text = ofd.FileName;
                if (verifyTimeTable()) {
                    Properties.Settings.Default.timetablePath = ofd.FileName;
                    MessageBox.Show("올바른 시간표 파일로 확인이 되었음!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.Save();
                } else {
                    MessageBox.Show("내가 원하는 양식의 시간표가 아님..", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    timetablePathBox.Clear();
                }
            }
        }

        private void startupCheck_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (startupCheck.Checked) key.SetValue(this.Text, Application.ExecutablePath);
            else key.DeleteValue(this.Text);
        }

        private void wallpaperPreview_Click(object sender, EventArgs e) {
            ProcessStartInfo psi = new ProcessStartInfo(wallpaperPreview.ImageLocation);
            Process.Start(psi);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) {
            this.Show();    
            this.Activate();
        }

        private void startBtn_Click(object sender, EventArgs e) { updateState((startBtn.Text == "시작")); }

        // Option Form Open Events
        private void openClassFormBtn_Click(object sender, EventArgs e) { (new ClassForm()).Show(); }
        private void openMealFormBtn_Click(object sender, EventArgs e) { (new MealForm()).Show(); }
        private void openDateFormBtn_Click(object sender, EventArgs e) { (new DateForm()).Show(); }
        private void openListFormBtn_Click(object sender, EventArgs e) { (new ListForm()).Show(); }
        private void openWeekFormBtn_Click(object sender, EventArgs e) { (new WeekForm()).Show(); }

        // Menu Click Events
        private void menuStart_Click(object sender, EventArgs e) { updateState(true); }
        private void menuStop_Click(object sender, EventArgs e) { updateState(false); }
        private void menuClose_Click(object sender, EventArgs e) {
            if (backupWallpaper.Length > 0) setWallpaper(backupWallpaper);
            forceExit = true;
            Application.Exit();
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

    public class Meal {
        public string title { get; set; }
        public string[] content { get; set; }
        public string calorie { get; set;}

        public Meal (string title, string[] content, string calorie) {
            this.title = title;
            this.content = content;
            this.calorie = calorie;
        }
    }
}
