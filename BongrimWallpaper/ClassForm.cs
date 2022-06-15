using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Text;

namespace BongrimWallpaper
{
    public partial class ClassForm : Form
    {
        public ClassForm() { InitializeComponent(); }

        private Font mainFont;
        private Font subFont;

        private void refreshPreview() {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SolidBrush mainSB = new SolidBrush(mainColorBox.BackColor);
            SolidBrush subSB = new SolidBrush(subColorBox.BackColor);

            SizeF baseSize = new SizeF(image.Width, image.Height);

            // Example Values
            int lesson = 3;
            string subjectName = "영어";
            string teacher = "정주은";
            string time = "12:34 ~ 56:78";

            if (testCaseBox.SelectedIndex == 0) { // drawSubject
                SizeF lessonSize = g.MeasureString($"{lesson} 교시", subFont, baseSize);
                SizeF nameSize = g.MeasureString(subjectName, mainFont, baseSize, StringFormat.GenericTypographic);
                SizeF teacherSize = g.MeasureString(teacher, subFont, baseSize, StringFormat.GenericTypographic);
                SizeF timeSize = g.MeasureString(time, subFont, baseSize);
                if (alignmentBox.SelectedIndex == 0) { // Left Alignment
                    float classX = xBar.Value;
                    float classY = (yBar.Maximum - yBar.Value) - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

                    g.DrawString($"{lesson} 교시", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += lessonSize.Height;

                    g.DrawString(subjectName, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX += nameSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(teacher, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = xBar.Value;
                    classY += teacherSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (alignmentBox.SelectedIndex == 1) { // Center Alignment
                    float classX = xBar.Value - (lessonSize.Width / 2);
                    float classY = (yBar.Maximum - yBar.Value) - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);
                    
                    g.DrawString($"{lesson} 교시", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = xBar.Value - ((nameSize.Width + teacherSize.Width) / 2);
                    classY += lessonSize.Height;

                    g.DrawString(subjectName, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX += nameSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(teacher, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = xBar.Value - (timeSize.Width / 2);
                    classY += teacherSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (alignmentBox.SelectedIndex == 2) { // Right Alignment
                    float classX = xBar.Value - lessonSize.Width;
                    float classY = (yBar.Maximum - yBar.Value) - ((lessonSize.Height + nameSize.Height + timeSize.Height) / 2);

                    g.DrawString($"{lesson} 교시", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = xBar.Value - nameSize.Width;
                    classY += lessonSize.Height;

                    g.DrawString(subjectName, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX -= teacherSize.Width + 20;
                    classY += nameSize.Height - teacherSize.Height;

                    g.DrawString(teacher, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = xBar.Value - timeSize.Width;
                    classY += teacherSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                }
            } else if (testCaseBox.SelectedIndex == 1) { // darwBreak
                string title = "쉬는 시간";

                SizeF nextSize = g.MeasureString($"Next {subjectName}({teacher})", subFont, baseSize);
                SizeF titleSize = g.MeasureString(title, mainFont, baseSize, StringFormat.GenericTypographic);
                SizeF timeSize = g.MeasureString(time, subFont, baseSize);
                
                if (alignmentBox.SelectedIndex == 0) { // Left Alignment
                    float classX = xBar.Value;
                    float classY = (yBar.Maximum - yBar.Value) - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subjectName}({teacher})", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classY += nextSize.Height;

                    g.DrawString(title, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classY += titleSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (alignmentBox.SelectedIndex == 1) { // Center Alignment
                    float classX = xBar.Value - (nextSize.Width / 2);
                    float classY = (yBar.Maximum - yBar.Value) - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subjectName}({teacher})", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = xBar.Value - (titleSize.Width / 2);
                    classY += nextSize.Height;

                    g.DrawString(title, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = xBar.Value - (timeSize.Width / 2);
                    classY += titleSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                } else if (alignmentBox.SelectedIndex == 2) { // Right Alignment
                    float classX = xBar.Value - nextSize.Width;
                    float classY = (yBar.Maximum - yBar.Value) - ((nextSize.Height + titleSize.Height + timeSize.Height) / 2);

                    g.DrawString($"Next {subjectName}({teacher})", subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                    classX = xBar.Value - titleSize.Width;
                    classY += nextSize.Height;

                    g.DrawString(title, mainFont, mainSB, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
                    classX = xBar.Value - timeSize.Width;
                    classY += titleSize.Height;

                    g.DrawString(time, subFont, subSB, new RectangleF(classX, classY, image.Width, image.Height));
                }
            } else if (testCaseBox.SelectedIndex == 2) { // drawEvent
                string text = "종례 시간";
                SolidBrush sb = new SolidBrush(mainColorBox.BackColor);
                SizeF textSize = g.MeasureString(text, mainFont, baseSize, StringFormat.GenericTypographic);
                int alignment = alignmentBox.SelectedIndex;
                float classX = (alignment == 1) ? xBar.Value - (textSize.Width / 2) : ((alignment== 2) ? xBar.Value - textSize.Width : xBar.Value);
                float classY = (yBar.Maximum - yBar.Value) - (textSize.Height / 2); 
                g.DrawString(text, mainFont, sb, new RectangleF(classX, classY, image.Width, image.Height), StringFormat.GenericTypographic);
            }
            previewBox.Image = image;
        }

        private void ClassForm_Load(object sender, EventArgs e) {
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
                xBar.Value = (int)Properties.Settings.Default.classX;
                yBar.Value = yBar.Maximum - (int)Properties.Settings.Default.classY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            mainFont = Properties.Settings.Default.classMainFont;
            subFont = Properties.Settings.Default.classSubFont;
            mainColorBox.BackColor = Properties.Settings.Default.classMainColor;
            subColorBox.BackColor = Properties.Settings.Default.classSubColor;
            classVisibleCheck.Checked = Properties.Settings.Default.classVisible;
            alignmentBox.SelectedIndex = Properties.Settings.Default.classAlignment;
            testCaseBox.SelectedIndex = 0;

            mainFontBox.Text = mainFont.Name;
            mainSizeBox.Text = mainFont.Size.ToString();
            subFontBox.Text = subFont.Name;
            subSizeBox.Text = subFont.Size.ToString();

            refreshPreview();
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "classTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (classVisibleCheck.Checked) {
                Properties.Settings.Default.classMainFont = mainFont;
                Properties.Settings.Default.classMainColor = mainColorBox.BackColor;
                Properties.Settings.Default.classSubFont = subFont;
                Properties.Settings.Default.classSubColor = subColorBox.BackColor;
                Properties.Settings.Default.classAlignment = alignmentBox.SelectedIndex;
                Properties.Settings.Default.classY = yBar.Maximum - yBar.Value;
                Properties.Settings.Default.classX = xBar.Value;
                Properties.Settings.Default.classVisible = true;
            } else {
                Properties.Settings.Default.classVisible = false;
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("저장 되었습니다", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        
        private void ClassForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "classTest.png");
            if (File.Exists(path)) File.Delete(path);
        }

        // Change Value Events
        private void testCaseBox_SelectedIndexChanged(object sender, EventArgs e) { refreshPreview(); }

        private void alignmentBox_SelectedIndexChanged(object sender, EventArgs e) { refreshPreview(); }

        private void xBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void yBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void xCenterBtn_Click(object sender, EventArgs e)
        {
            xBar.Value = xBar.Maximum / 2;
            refreshPreview();
        }

        private void yCenterBtn_Click(object sender, EventArgs e)
        {
            yBar.Value = yBar.Maximum / 2;
            refreshPreview();
        }

        private void classVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            fontGroup.Enabled = classVisibleCheck.Checked;
            layoutGroup.Enabled = classVisibleCheck.Checked;
            xBar.Enabled = classVisibleCheck.Checked;
            yBar.Enabled = classVisibleCheck.Checked;
        }

        private void mainColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = mainColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                mainColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void subColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = subColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                subColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void setMainFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = mainFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                if (subFont.Size > fd.Font.Size) {
                    MessageBox.Show("메인 폰트 크기가 절대 나머지 폰트 크기보다 클 수 없어요!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                mainFont = fd.Font;
                mainFontBox.Text = fd.Font.Name;
                mainSizeBox.Text = fd.Font.Size.ToString();
                refreshPreview();
            }
        }

        private void setSubFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = subFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                if (mainFont.Size < fd.Font.Size) {
                    MessageBox.Show("메인 폰트 크기가 절대 나머지 폰트 크기보다 클 수 없어요!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                subFont = fd.Font;
                subFontBox.Text = fd.Font.Name;
                subSizeBox.Text = fd.Font.Size.ToString();
                refreshPreview();
            }
        }
    }
}
