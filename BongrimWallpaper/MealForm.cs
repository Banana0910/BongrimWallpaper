using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;
using hap = HtmlAgilityPack;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace BongrimWallpaper
{
    public partial class MealForm : Form
    {
        Font titleFont;
        Font contentFont;
        List<string[]> meals;

        public MealForm()
        {
            InitializeComponent();
        }

        private List<string[]> get_meal() {
            try {
                List<string[]> output = new List<string[]>();
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
                output.Add(Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n'));
                for(int i = 0; i < output[0].Length; i++) {
                    output[0][i] = output[0][i].Trim();
                }
                if (dinner != null) {
                    output.Add(Regex.Replace(dinner.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n'));
                    for(int i = 0; i < output[1].Length; i++) {
                        output[1][i] = output[1][i].Trim();
                }
                }
                return output;
            }
            catch {
                return null;
            }
        }

        private void refresh_preview() {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SolidBrush mealTitleSB = new SolidBrush(titleColorBox.BackColor);
            SolidBrush mealContentSB = new SolidBrush(contentColorBox.BackColor);

            SizeF baseSize = new SizeF(image.Width, image.Height);

            float mealX = xBar.Value;
            float mealY = (yBar.Maximum - yBar.Value);
            float contentSpace = float.Parse(contentSpaceBox.Text);

            if (verticalBtn.Checked) {
                if (alignmentBox.SelectedIndex == 0) {
                    if (meals.Count >= 1) {
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            mealY += (float)mealSpaceBox.Value;
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, new SizeF(image.Width, image.Height), StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                } else if (alignmentBox.SelectedIndex == 1) {
                    if (meals.Count >= 1) {
                        mealX = xBar.Value - (g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            mealX = xBar.Value - (g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            mealY += (float)mealSpaceBox.Value;
                            mealX = xBar.Value - (g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                mealX = xBar.Value - (g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                } else if (alignmentBox.SelectedIndex == 2) {
                    if (meals.Count >= 1) {
                        mealX = xBar.Value - g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            mealX = xBar.Value - g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            mealY += (float)mealSpaceBox.Value;
                            mealX = xBar.Value - g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                mealX = xBar.Value - g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                }
            } else {
                if (alignmentBox.SelectedIndex == 0) {
                    if (meals.Count >= 1) {
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }

                        if (meals.Count == 2) {
                            float maxWidth = g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[0]) { 
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth < width) maxWidth = width;
                            }
                            mealX = xBar.Value + maxWidth + (float)mealSpaceBox.Value;
                            mealY = yBar.Maximum - yBar.Value;
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                } else if (alignmentBox.SelectedIndex == 1) {
                    if (meals.Count >= 1) {
                        float halfMealSpace = (float)mealSpaceBox.Value / 2;
                        float maxWidth = g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                        foreach (string meal in meals[0]) {
                            float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                            if (maxWidth < width) maxWidth = width;
                        }
                        mealX = xBar.Value - (halfMealSpace + (maxWidth / 2) + g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            mealX = xBar.Value - (halfMealSpace + (maxWidth / 2) + g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            maxWidth = g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[1]) {
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth < width) maxWidth = width;
                            }
                            mealY = yBar.Maximum - yBar.Value;
                            mealX = xBar.Value + (halfMealSpace + (maxWidth / 2) - (g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2));
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                mealX = xBar.Value + (halfMealSpace + (maxWidth / 2) - (g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width / 2));
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                } else {
                    if (meals.Count >= 1) {
                        mealX = xBar.Value - g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                        g.DrawString("중식 [Lunch]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0]) {
                            mealX = xBar.Value - g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            float maxWidth = g.MeasureString("중식 [Lunch]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[0]) {
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth< width) maxWidth = width;
                            }
                            float space = maxWidth + (float)mealSpaceBox.Value;
                            mealY = yBar.Maximum - yBar.Value;
                            mealX = xBar.Value - space - g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString("석식 [Dinner]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString("석식 [Dinner]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1]) {
                                mealX = xBar.Value - space - g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                        }
                    } else {
                        g.DrawString("급식이 없습니다..", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height));
                    }
                }
            }

            previewBox.Image = image;
        }

        private void MealForm_Load(object sender, EventArgs e)
        {
            int screen_width = Screen.PrimaryScreen.Bounds.Width;
            int screen_height = Screen.PrimaryScreen.Bounds.Height;

            if (screen_width >= screen_height) {
                int height = (screen_height*previewBox.Size.Width)/screen_width;
                previewBox.Size = new Size(previewBox.Size.Width, height);
                yBar.Height = height+25;
                yCenterBtn.Location = new Point(yCenterBtn.Location.X, yBar.Height+40);
            } else {
                int width = (screen_width*previewBox.Size.Height)/screen_height;
                previewBox.Size = new Size(width,previewBox.Size.Height);
                xBar.Width = width+25;
                xCenterBtn.Location = new Point(xBar.Width+40, xCenterBtn.Location.Y);
            }

            xBar.Maximum = screen_width;
            yBar.Maximum = screen_height;
            try {
                xBar.Value = (int)Properties.Settings.Default.mealX;
                yBar.Value = yBar.Maximum - (int)Properties.Settings.Default.mealY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            meals = get_meal();
            titleFont = Properties.Settings.Default.mealTitleFont;
            contentFont = Properties.Settings.Default.mealContentFont;
            titleColorBox.BackColor = Properties.Settings.Default.mealTitleColor;
            contentColorBox.BackColor = Properties.Settings.Default.mealContentColor;
            contentSpaceBox.Text = Properties.Settings.Default.mealContentSpace.ToString();
            mealSpaceBox.Value = (decimal)Properties.Settings.Default.mealSpace;
            if (Properties.Settings.Default.mealLayout == 0) verticalBtn.Select();
            else horizontalBtn.Select();
            mealVisibleCheck.Checked = Properties.Settings.Default.mealVisible;
            alignmentBox.SelectedIndex = Properties.Settings.Default.mealAlignment;

            titleFontBox.Text = titleFont.Name;
            titleSizeBox.Text = titleFont.Size.ToString();
            contentFontBox.Text = contentFont.Name;
            contentSizeBox.Text = contentFont.Size.ToString();

            refresh_preview();
        }

        private void xCenterBtn_Click(object sender, EventArgs e)
        {
            xBar.Value = xBar.Maximum / 2;
            refresh_preview();
        }

        private void yCenterBtn_Click(object sender, EventArgs e)
        {
            yBar.Value = yBar.Maximum / 2;
            refresh_preview();
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void xBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void titleColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = titleColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                titleColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void contentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = contentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                contentColorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void setContentFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = contentFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                contentFont = fd.Font;
                contentFontBox.Text = fd.Font.Name;
                contentSizeBox.Text = fd.Font.Size.ToString();
                refresh_preview();
            }
        }

        private void setTitleFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = titleFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                titleFont = fd.Font;
                titleFontBox.Text = fd.Font.Name;
                titleSizeBox.Text = fd.Font.Size.ToString();
                refresh_preview();
            }
        }

        private void alignmentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void verticalBtn_CheckedChanged(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings config = Properties.Settings.Default;
            if (mealVisibleCheck.Checked) {
                config.mealTitleFont = titleFont;
                config.mealContentFont = contentFont;
                config.mealTitleColor = titleColorBox.BackColor;
                config.mealContentColor = contentColorBox.BackColor;
                config.mealContentSpace = float.Parse(contentSpaceBox.Text);
                config.mealAlignment = alignmentBox.SelectedIndex;
                config.mealLayout = (verticalBtn.Checked) ? 0 : 1;
                config.mealSpace = (float)mealSpaceBox.Value;
                config.mealX = xBar.Value;
                config.mealY = yBar.Maximum - yBar.Value;
                config.mealVisible = true;
            } else {
                config.mealVisible = false;
            }
            config.Save();
            MessageBox.Show("저장 되었습니다!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "mealTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        private void mealVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            fontGroup.Enabled = mealVisibleCheck.Checked;
            layoutGroup.Enabled = mealVisibleCheck.Checked;
        }

        private void mealSpaceBox_ValueChanged(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void MealForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "mealTest.png"))) {
                File.Delete(Path.Combine(Application.StartupPath, "mealTest.png"));
            }
        }
    }
}
