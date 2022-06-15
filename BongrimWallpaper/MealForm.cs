using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using HtmlAgilityPack;
using System.Drawing.Text;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace BongrimWallpaper
{
    public partial class MealForm : Form
    {
        public MealForm() { InitializeComponent(); }
        
        private Font titleFont;
        private Font contentFont;
        private List<Meal> meals;

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
                        { "dietDate",  DateTime.Today.ToString("yyyyMMdd") }
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

        private void refreshPreview() {
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

            if (meals.Count > 0) {
                float contentSpace = (float)contentSpaceBox.Value;
                float mealSpace = (float)mealSpaceBox.Value;

                if (verticalBtn.Checked) {
                    if (alignmentBox.SelectedIndex == 0) {
                        foreach (Meal meal in meals) {
                            g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string content in meal.content) {
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                            }
                            mealY += mealSpace;
                        }
                    } else if (alignmentBox.SelectedIndex == 1) {
                        foreach (Meal meal in meals) {
                            SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic);
                            mealX = xBar.Value - (titleSize.Width / 2);
                            g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX,mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += titleSize.Height;
                            foreach (string content in meal.content) {
                                SizeF contentSize = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic);
                                mealX = xBar.Value - (contentSize.Width / 2);
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += contentSize.Height + contentSpace;
                            }
                            mealY += mealSpace;
                        }
                    } else if (alignmentBox.SelectedIndex == 2) {
                        foreach (Meal meal in meals) {
                            SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic);
                            mealX = xBar.Value - titleSize.Width;
                            g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += titleSize.Height;
                            foreach (string content in meal.content) {
                                SizeF contentSize = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic);
                                mealX = xBar.Value - contentSize.Width;
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += contentSize.Height + contentSpace;
                            }
                            mealY += mealSpace;
                        }
                    }
                } else {
                    if (alignmentBox.SelectedIndex == 0) {
                        foreach (Meal meal in meals) {
                            SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic);
                            g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += titleSize.Height;
                            float maxWidth = titleSize.Width;
                            foreach (string content in meal.content) {
                                SizeF contentSize = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic);
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += contentSize.Height + contentSpace;
                                if (contentSize.Width > maxWidth) maxWidth = contentSize.Width;
                            }
                            mealX += maxWidth + mealSpace;
                            mealY = yBar.Maximum - yBar.Value;
                        }
                    } else if (alignmentBox.SelectedIndex == 1) {
                        float allWidth = 0f;
                        int mealCount = meals.Count;
                        float[] maxWidths = new float[mealCount];
                        for (int i = 0; i < mealCount; i++) {
                            maxWidths[i] = g.MeasureString($"{meals[i].title} [{meals[i].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string content in meals[i].content) {
                                float width = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (width > maxWidths[i]) maxWidths[i] = width;
                            }
                            allWidth += maxWidths[i];
                        }

                        allWidth += mealSpace * (mealCount - 1);
                        mealX = xBar.Value - (allWidth / 2);

                        for (int i = 0; i < mealCount; i++) {
                            SizeF titleSize = g.MeasureString($"{meals[i].title} [{meals[i].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic);
                            float pivot = mealX + (maxWidths[i] / 2);
                            mealX = pivot - (titleSize.Width / 2);
                            g.DrawString($"{meals[i].title} [{meals[i].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += titleSize.Height;
                            foreach (string content in meals[i].content) {
                                SizeF contentSize = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericDefault);
                                mealX = pivot - (contentSize.Width / 2);
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += contentSize.Height + contentSpace;
                            }
                            mealX = mealSpace + pivot + (titleSize.Width / 2);
                            mealY = yBar.Maximum - yBar.Value;
                        }
                    } else {
                        foreach (Meal meal in meals) {
                            SizeF titleSize = g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic);
                            float startX = mealX;
                            float maxWidth = titleSize.Width;
                            mealX = startX - titleSize.Width;
                            g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += titleSize.Height;
                            foreach (string content in meal.content) {
                                SizeF contentSize = g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic);
                                mealX = startX - contentSize.Width;
                                g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                                mealY += contentSize.Height + contentSpace;
                                if (contentSize.Width > maxWidth) maxWidth = contentSize.Width;
                            }
                            mealX = startX - (maxWidth + mealSpace);
                            mealY = yBar.Maximum - yBar.Value;
                        }
                    }
                }
            } else {
                g.DrawString("급식이 없습니다", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
            }
            previewBox.Image = image;
        }

        private void MealForm_Load(object sender, EventArgs e)
        {
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
                xBar.Value = (int)Properties.Settings.Default.mealX;
                yBar.Value = yBar.Maximum - (int)Properties.Settings.Default.mealY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            neisCheck.Checked = Properties.Settings.Default.isNeis;
            meals = (neisCheck.Checked) ? getMealNeis() : getMealPage();
            titleFont = Properties.Settings.Default.mealTitleFont;
            contentFont = Properties.Settings.Default.mealContentFont;
            titleColorBox.BackColor = Properties.Settings.Default.mealTitleColor;
            contentColorBox.BackColor = Properties.Settings.Default.mealContentColor;
            contentSpaceBox.Value = (decimal)Properties.Settings.Default.mealContentSpace;
            mealSpaceBox.Value = (decimal)Properties.Settings.Default.mealSpace;
            if (Properties.Settings.Default.mealLayout == 0) verticalBtn.Select();
            else horizontalBtn.Select();
            mealVisibleCheck.Checked = Properties.Settings.Default.mealVisible;
            alignmentBox.SelectedIndex = Properties.Settings.Default.mealAlignment;

            titleFontBox.Text = titleFont.Name;
            titleSizeBox.Text = titleFont.Size.ToString();
            contentFontBox.Text = contentFont.Name;
            contentSizeBox.Text = contentFont.Size.ToString();

            refreshPreview();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings config = Properties.Settings.Default;
            if (mealVisibleCheck.Checked) {
                config.mealTitleFont = titleFont;
                config.mealContentFont = contentFont;
                config.mealTitleColor = titleColorBox.BackColor;
                config.mealContentColor = contentColorBox.BackColor;
                config.mealContentSpace = (float)contentSpaceBox.Value;
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

        private void MealForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "mealTest.png");
            if (File.Exists(path)) File.Delete(path);
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "mealTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        // Change Value Events
        private void yBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void xBar_Scroll(object sender, EventArgs e) { refreshPreview(); }

        private void alignmentBox_SelectedIndexChanged(object sender, EventArgs e) { refreshPreview(); }

        private void mealSpaceBox_ValueChanged(object sender, EventArgs e) { refreshPreview(); }

        private void verticalBtn_CheckedChanged(object sender, EventArgs e) { refreshPreview(); }

        private void neisCheck_CheckedChanged(object sender, EventArgs e) {
            meals = getMealNeis();
            refreshPreview();
        }

        private void hompageCheck_CheckedChanged(object sender, EventArgs e) {
            meals = getMealPage();
            refreshPreview(); 
        }
        
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

        private void titleColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = titleColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                titleColorBox.BackColor = cd.Color;
                refreshPreview();
            }
        }

        private void contentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = contentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                contentColorBox.BackColor = cd.Color;
                refreshPreview();
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
                refreshPreview();
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
                refreshPreview();
            }
        }

        private void mealVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            fontGroup.Enabled = mealVisibleCheck.Checked;
            layoutGroup.Enabled = mealVisibleCheck.Checked;
            xBar.Enabled = mealVisibleCheck.Checked;
            yBar.Enabled = mealVisibleCheck.Checked;
        }

    }
}
