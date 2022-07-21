using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;

namespace BongrimWallpaper
{
    public partial class ListForm : Form
    {
        public ListForm() { InitializeComponent(); }

        private Font subjectFont;
        private Font teacherFont;

        private void refreshPreview() {
            // Example Values
            int lesson = 1;
            string[] subject = new string[] { "영어", "통합사회", "영어", "통합사회", "영어", "통합사회", "영어" };
            string[] teacher = new string[] { "정주은", "김현우", "정주은", "김현우", "정주은", "김현우", "정주은" };
            
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

            float listSpace = (float)listSpaceBox.Value;

            if (teacherVisibleCheck.Checked) {
                List<SizeF> teacherSizes = new List<SizeF>();
                SolidBrush teacherSB = new SolidBrush(teacherColorBox.BackColor);
                SolidBrush teacherAccentSB = new SolidBrush(teacherAccentColorBox.BackColor);
                for (int i = 0; i < subject.Length; i++) {
                    subjectSizes.Add(g.MeasureString(subject[i], subjectFont, baseSize, StringFormat.GenericTypographic));
                    teacherSizes.Add(g.MeasureString(teacher[i], teacherFont, baseSize, StringFormat.GenericTypographic));
                }
                if (verticalBtn.Checked) {
                    float allHeight = subjectSizes.Sum(s => s.Height) + teacherSizes.Sum(s => s.Height) + (subject.Length - 1) * listSpace;
                    float listX = xBar.Value;
                    float listY = (yBar.Maximum - yBar.Value) - ((allHeight + (subject.Length - 1) * listSpace) / 2);
                    
                    for (int i = 0; i < subject.Length; i++) {
                        SolidBrush sb = (i == lesson-1) ? subjectAccentSB : subjectSB;
                        listX = xBar.Value - (subjectSizes[i].Width / 2);
                        g.DrawString(subject[i], subjectFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listY += subjectSizes[i].Height;

                        sb = (i==lesson-1) ? teacherAccentSB : teacherSB;
                        listX = xBar.Value - (teacherSizes[i].Width / 2);
                        g.DrawString(teacher[i], teacherFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listY += teacherSizes[i].Height + listSpace;
                    }
                } else {
                    float allSubjectWidth = subjectSizes.Sum(s => s.Width);
                    float allTeacherWidth = teacherSizes.Sum(t => t.Width);
                    float listX = xBar.Value - ((((allSubjectWidth > allTeacherWidth) ? allSubjectWidth : allTeacherWidth) + ((subject.Length - 1) * listSpace)) / 2);
                    float listY = (yBar.Maximum - yBar.Value);

                    for (int i = 0; i < subject.Length; i++) {
                        float maxWidth = (subjectSizes[i].Width > teacherSizes[i].Width) ? subjectSizes[i].Width : teacherSizes[i].Width;
                        listX += (maxWidth - subjectSizes[i].Width) / 2;

                        SolidBrush sb = (i == lesson-1) ? subjectAccentSB : subjectSB;
                        g.DrawString(subject[i], subjectFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += (subjectSizes[i].Width - teacherSizes[i].Width) / 2;
                        listY += subjectSizes[i].Height;

                        sb = (i == lesson-1) ? subjectAccentSB : subjectSB;
                        g.DrawString(teacher[i], teacherFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += (teacherSizes[i].Width + maxWidth) / 2 + listSpace;
                        listY = (yBar.Maximum - yBar.Value);
                    }
                }
            } else {
                foreach (string s in subject) subjectSizes.Add(g.MeasureString(s, subjectFont, baseSize, StringFormat.GenericTypographic));
                if (verticalBtn.Checked) {
                    float allHeight = subjectSizes.Sum(s => s.Height);
                    float listX = xBar.Value;
                    float listY = (yBar.Maximum - yBar.Value) - ((allHeight + (subject.Length - 1) * listSpace) / 2);
                    for (int i = 0 ; i < subject.Length; i++) {
                        SolidBrush sb = (i == lesson-1) ? subjectAccentSB : subjectSB;
                        g.DrawString(subject[i], subjectFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listY += subjectSizes[i].Height + listSpace;
                    }
                } else {
                    float allWidth = subjectSizes.Sum(s => s.Width);
                    float listX = xBar.Value - ((allWidth + (subject.Length - 1) * listSpace) / 2);
                    float listY = (yBar.Maximum - yBar.Value);
                    for (int i = 0; i < subject.Length; i++) {
                        SolidBrush sb = (i == lesson-1) ? subjectAccentSB : subjectSB;
                        g.DrawString(subject[i], subjectFont, sb, new RectangleF(listX, listY, image.Width, image.Height), StringFormat.GenericTypographic);
                        listX += subjectSizes[i].Width + listSpace;
                    }
                }
            }

            previewBox.Image = image;
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
            listSpaceBox.Value = (decimal)config.listSpace;
            if (config.listLayout == 0) verticalBtn.Checked = true;
            else horizontalBtn.Checked = true;
            teacherVisibleCheck.Checked = config.listTeacherVisible;
            listVisibleCheck.Checked = config.listVisible;

            subjectFontBox.Text = subjectFont.Name;
            subjectSizeBox.Text = subjectFont.Size.ToString();
            teacherFontBox.Text = teacherFont.Name;
            teacherSizeBox.Text = teacherFont.Size.ToString();

            refreshPreview();
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
                config.listSpace = (float)listSpaceBox.Value;
                config.listX = xBar.Value;
                config.listY = yBar.Maximum - yBar.Value;
                config.listVisible = true;
            } else {
                config.listVisible = false;
            }
            config.Save();
            MessageBox.Show("저장 되었습니다!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "listTest.png");
            if (File.Exists(path)) File.Delete(path);
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "listTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        // Change Value Events
        private void yBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void xBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void verticalBtn_CheckedChanged(object sender, EventArgs e) { refreshPreview(); }

        private void listSpaceBox_ValueChanged(object sender, EventArgs e) { refreshPreview(); }

        private void yCenterBtn_Click(object sender, EventArgs e) {
            yBar.Value = yBar.Maximum / 2;
            refreshPreview();
        }

        private void xCenterBtn_Click(object sender, EventArgs e) {
            xBar.Value = xBar.Maximum / 2;
            refreshPreview();
        }

        private void teacherVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            teacherGroup.Enabled = teacherVisibleCheck.Checked;
            refreshPreview();
        }

        private void listVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            fontGroup.Enabled = listVisibleCheck.Checked;
            layoutGroup.Enabled = listVisibleCheck.Checked;
            xBar.Enabled = listVisibleCheck.Checked;
            yBar.Enabled = listVisibleCheck.Checked;
            xCenterBtn.Enabled = listVisibleCheck.Checked;
            yCenterBtn.Enabled = listVisibleCheck.Checked;
        }

        private void subjectColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = subjectColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                subjectColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void teacherColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = teacherColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                teacherColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void teacherAccentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = teacherAccentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                teacherAccentColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void subjectAccentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = subjectAccentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                subjectAccentColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void setSubjectFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = subjectFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                subjectFont = fd.Font;
                subjectFontBox.Text = fd.Font.Name;
                subjectSizeBox.Text = fd.Font.Size.ToString();
                refreshPreview();
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
                refreshPreview();
            }
        }

    }
}
