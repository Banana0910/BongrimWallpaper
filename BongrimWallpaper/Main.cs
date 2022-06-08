using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
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
        private bool force_exit = false;
        private string backup_wallpaper = "";
        private int now_wallpaper = -2;

        //배경설정 메서드
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        public static void set_wallpaper(string path) { SystemParametersInfo(20, 0, path, 0x01 | 0x02); }

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
                } else { return false; }
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
            string[] output = Regex.Replace(target.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br/>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n');
            int outputLength = output.Length;
            for (int i = 0; i < outputLength; i++) output[i] = output[i].Trim();
            return output;
        }

        private List<Meal> get_meal() {
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

                List<Meal> meals = get_meal();
                float mealX = config.mealX;
                float mealY = config.mealY;

                if (meals.Count > 0) {
                    float contentSpace = config.mealContentSpace;
                    float mealSpace = config.mealSpace;

                    if (config.mealLayout == 0) {
                        if (config.mealAlignment == 0) {
                            foreach (Meal meal in meals) {
                                g.DrawString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic).Height;
                                foreach (string content in meal.content) {
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                                }
                                mealY += mealSpace;
                            }
                        } else if (config.mealAlignment == 1) {
                            foreach (Meal meal in meals) {
                                SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                mealX = config.mealX- (titleSize.Width / 2);
                                g.DrawString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX,mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += titleSize.Height;
                                foreach (string content in meal.content) {
                                    SizeF contentSize = g.MeasureString(content, config.mealContentFont, baseSize, StringFormat.GenericTypographic);
                                    mealX = config.mealX- (contentSize.Width / 2);
                                    g.DrawString(content, config.mealContentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                    mealY += contentSize.Height + contentSpace;
                                }
                                mealY += mealSpace;
                            }
                        } else if (config.mealAlignment == 2) {
                            foreach (Meal meal in meals) {
                                SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                mealX = config.mealX- titleSize.Width;
                                g.DrawString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
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
                    } else {
                        if (config.mealAlignment == 0) {
                            foreach (Meal meal in meals) {
                                SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                g.DrawString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
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
                        } else if (config.mealAlignment == 1) {
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
                            mealX = config.mealX- (allWidth / 2);

                            for (int i = 0; i < mealCount; i++) {
                                SizeF titleSize = g.MeasureString($"{meals[i].title} [{meals[i].calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                float pivot = mealX + (maxWidths[i] / 2);
                                mealX = pivot - (titleSize.Width / 2);
                                g.DrawString($"{meals[i].title} [{meals[i].calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
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
                        } else {
                            foreach (Meal meal in meals) {
                                SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, baseSize, StringFormat.GenericTypographic);
                                float startX = mealX;
                                float maxWidth = titleSize.Width;
                                mealX = startX - titleSize.Width;
                                g.DrawString($"{meal.title} [{meal.calorie}]", config.mealTitleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
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
            if (backup_wallpaper.Length > 0) set_wallpaper(backup_wallpaper);
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
