﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace BongrimWallpaper
{
    public partial class ListForm : Form
    {
        public ListForm()
        {
            InitializeComponent();
        }

        Font subjectFont;
        Font teacherFont;

        string[] subject = new string[] { "영어", "통합사회", "영어", "통합사회", "영어", "통합사회", "영어" };
        string[] teacher = new string[] { "정주은", "김현우", "정주은", "김현우", "정주은", "김현우", "정주은" };

        private void refresh_preview() {
            int lesson = 1;
            
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SizeF baseSize = new SizeF(image.Width, image.Height);

            List<SizeF> subjectSizes = new List<SizeF>();

            SolidBrush subjectSB = new SolidBrush(subjectColorBox.BackColor);
            SolidBrush subjectAccentSB = new SolidBrush(subjectAccentColorBox.BackColor);

            float listSpace = float.Parse(listSpaceBox.Text);

            if (teacherVisibleCheck.Checked) {
                List<SizeF> teacherSizes = new List<SizeF>();
                SolidBrush teacherSB = new SolidBrush(teacherColorBox.BackColor);
                SolidBrush teacherAccentSB = new SolidBrush(teacherAccentColorBox.BackColor);
                for (int i = 0; i < subject.Length; i++) {
                    subjectSizes.Add(g.MeasureString(subject[i], subjectFont, baseSize, StringFormat.GenericTypographic));
                    teacherSizes.Add(g.MeasureString(teacher[i], teacherFont, baseSize, StringFormat.GenericTypographic));
                }
                if (verticalBtn.Checked) {
                    float allHeight = subjectSizes.Sum(subject => subject.Height);
                    float listX = xBar.Value;
                    float listY = (yBar.Maximum - yBar.Value) - ((allHeight + (subject.Length - 1) * listSpace) / 2);
                    
                    for (int i = 0; i < subject.Length; i++) {
                        g.DrawString(subject[i], subjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += subjectSizes[i].Width + 10;
                        listY += subjectSizes[i].Height - teacherSizes[i].Height;
                        g.DrawString(teacher[i], teacherFont, (i==lesson-1) ? teacherAccentSB : teacherSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX = xBar.Value;
                        listY += teacherSizes[i].Height + listSpace;
                    }
                } else {
                    float allSubjectWidth = subjectSizes.Sum(subject => subject.Width);
                    float allTeacherWidth = teacherSizes.Sum(teacher => teacher.Width);
                    float listX = xBar.Value - ((((allSubjectWidth > allTeacherWidth) ? allSubjectWidth : allTeacherWidth) + ((subject.Length - 1) * listSpace)) / 2);
                    float listY = (yBar.Maximum - yBar.Value);

                    for (int i = 0; i < subject.Length; i++) {
                        float maxWidth = (subjectSizes[i].Width > teacherSizes[i].Width) ? subjectSizes[i].Width : teacherSizes[i].Width;
                        listX += (maxWidth - subjectSizes[i].Width) / 2;
                        g.DrawString(subject[i], subjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += (subjectSizes[i].Width - teacherSizes[i].Width) / 2;
                        listY += subjectSizes[i].Height;
                        g.DrawString(teacher[i], teacherFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += (teacherSizes[i].Width + maxWidth) / 2 + listSpace;
                        listY = (yBar.Maximum - yBar.Value);
                    }
                }
            } else {
                foreach (string s in subject) subjectSizes.Add(g.MeasureString(s, subjectFont, baseSize, StringFormat.GenericTypographic));
                if (verticalBtn.Checked) {
                    float allHeight = subjectSizes.Sum(subject => subject.Height);
                    float listX = xBar.Value;
                    float listY = (yBar.Maximum - yBar.Value) - ((allHeight + (subject.Length - 1) * listSpace) / 2);
                    for (int i = 0 ; i < subject.Length; i++) {
                        g.DrawString(subject[i], subjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listY += subjectSizes[i].Height + listSpace;
                    }
                } else {
                    float allWidth = subjectSizes.Sum(subject => subject.Width);
                    float listX = xBar.Value - ((allWidth + (subject.Length - 1) * listSpace) / 2);
                    float listY = (yBar.Maximum - yBar.Value);
                    for (int i = 0; i < subject.Length; i++) {
                        g.DrawString(subject[i], subjectFont, (i == lesson-1) ? subjectAccentSB : subjectSB, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += subjectSizes[i].Width + listSpace;
                    }
                }
            }

            image.Save(Path.Combine(Application.StartupPath, "listTest.png"), System.Drawing.Imaging.ImageFormat.Png);
            previewBox.ImageLocation = Path.Combine(Application.StartupPath, "listTest.png");
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            Process.Start(previewBox.ImageLocation);   
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void xBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void yCenterBtn_Click(object sender, EventArgs e)
        {
            yBar.Value = yBar.Maximum / 2;
            refresh_preview();
        }

        private void xCenterBtn_Click(object sender, EventArgs e)
        {
            xBar.Value = xBar.Maximum / 2;
            refresh_preview();
        }

        private void listVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            fontGroup.Enabled = listVisibleCheck.Checked;
            layoutGroup.Enabled = listVisibleCheck.Checked;
        }

        private void setSubjectFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = subjectFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                subjectFont = fd.Font;
                subjectFontBox.Text = fd.Font.Name;
                subjectSizeBox.Text = fd.Font.Size.ToString();
                refresh_preview();
            }
        }

        private void setTeacherFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = teacherFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                teacherFont = fd.Font;
                teacherFontBox.Text = fd.Font.Name;
                teacherSizeBox.Text = fd.Font.Size.ToString();
                refresh_preview();
            }
        }

        private void verticalBtn_CheckedChanged(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings config = Properties.Settings.Default;
            if (listVisibleCheck.Checked) {
                if (teacherVisibleCheck.Checked) {
                    config.listTeacherFont = teacherFont;
                    config.listTeacherColor = teacherColorBox.BackColor;
                    config.listTeacherAccentColor = teacherAccentColorBox.BackColor;
                    config.listTeacherVisible = true;
                } else {
                    config.listTeacherVisible = false;
                }
                config.listSubjectFont = subjectFont;
                config.listSubjectColor = subjectColorBox.BackColor;
                config.listSubjectAccentColor = subjectAccentColorBox.BackColor;
                config.listLayout = (verticalBtn.Checked) ? 0 : 1;
                config.listSpace = float.Parse(listSpaceBox.Text);
                config.listX = xBar.Value;
                config.listY = yBar.Maximum - yBar.Value;
                config.listVisible = true;
            } else {
                config.listVisible = false;
            }
            config.Save();
            MessageBox.Show("저장 되었습니다!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void subjectColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = subjectColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                subjectColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void teacherColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = teacherColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                teacherColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void teacherAccentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = teacherAccentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                teacherAccentColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void subjectAccentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = subjectAccentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                subjectAccentColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            Properties.Settings config = Properties.Settings.Default;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (screenWidth >= screenHeight) {
                int height = (screenHeight*previewBox.Size.Width)/screenWidth;
                previewBox.Size = new Size(previewBox.Size.Width, height);
                yBar.Height = height+25;
                yCenterBtn.Location = new Point(yCenterBtn.Location.X, yBar.Height+40);
            } else {
                int width = (screenWidth*previewBox.Size.Height)/screenHeight;
                previewBox.Size = new Size(width,previewBox.Size.Height);
                xBar.Width = width+25;
                xCenterBtn.Location = new Point(xBar.Width+40, xCenterBtn.Location.Y);
            }

            xBar.Maximum = screenWidth;
            yBar.Maximum = screenHeight;
            try {
                xBar.Value = (int)config.listX;
                yBar.Value = yBar.Maximum - (int)config.listY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            subjectFont = config.listSubjectFont;
            teacherFont = config.listTeacherFont;
            subjectColorBox.BackColor = config.listSubjectColor;
            subjectAccentColorBox.BackColor = config.listSubjectAccentColor;
            teacherColorBox.BackColor = config.listTeacherColor;
            teacherAccentColorBox.BackColor = config.listTeacherAccentColor;
            listSpaceBox.Text = config.listSpace.ToString();
            if (config.listLayout == 0) verticalBtn.Checked = true;
            else horizontalBtn.Checked = true;
            listVisibleCheck.Checked = config.listVisible;
            teacherVisibleCheck.Checked = config.listTeacherVisible;

            subjectFontBox.Text = subjectFont.Name;
            subjectSizeBox.Text = subjectFont.Size.ToString();
            teacherFontBox.Text = teacherFont.Name;
            teacherSizeBox.Text = teacherFont.Size.ToString();

            refresh_preview();
        }

        private void teacherVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            teacherGroup.Enabled = teacherVisibleCheck.Checked;
            refresh_preview();
        }
    }
}
