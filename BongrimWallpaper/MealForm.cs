using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
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
        Font titleFont;
        Font contentFont;
        List<Meal> meals;

        public MealForm()
        {
            InitializeComponent();
        }

        private string[] trimMeal(string target) {
            string[] output = Regex.Replace(target.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br/>", "\n").Replace("&nbsp", " ").Replace("()", "").Split('\n');
            int outputLength = output.Length;
            for (int i = 0; i < outputLength; i++) output[i] = output[i].Trim();
            return output;
        }

        private List<Meal> get_meal() {
            try {
                List<Meal> output = new List<Meal>();
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
                if (xmlDoc.GetElementsByTagName("head").Count == 0) return null;

                XmlNodeList meals = xmlDoc.GetElementsByTagName("row");
                foreach (XmlNode meal in meals)
                    output.Add(new Meal(meal["MMEAL_SC_NM"].InnerText, trimMeal(meal["DDISH_NM"].InnerText), meal["CAL_INFO"].InnerText));
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
            float contentSpace = (float)contentSpaceBox.Value;

            if (verticalBtn.Checked) {
                if (alignmentBox.SelectedIndex == 0) {
                    foreach (Meal meal in meals) {
                        g.DrawString($"{meal.title} [{meal.calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString($"{meal.title} [{meal.calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach (string content in meal.content) {
                            g.DrawString(content, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(content, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        mealY += (float)mealSpaceBox.Value;
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
                            mealY = contentSize.Height = contentSpace;
                        }
                        mealY += (float)mealSpaceBox.Value;
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
                        mealY += (float)mealSpaceBox.Value;
                    }
                }
            } else {
                if (alignmentBox.SelectedIndex == 0) {
                    int[] maxs = new int[3];
                    foreach (Meal meal in meals) {
                        
                    }
                    if (meals.Count >= 1) {
                        g.DrawString($"{meals[0].title} [{meals[0].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0].content) {
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }

                        if (meals.Count == 2) {
                            float maxWidth = g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[0].content) { 
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth < width) maxWidth = width;
                            }
                            mealX = xBar.Value + maxWidth + (float)mealSpaceBox.Value;
                            mealY = yBar.Maximum - yBar.Value;
                            g.DrawString($"{meals[1].title} [{meals[1].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1].content) {
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
                        float maxWidth = g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                        foreach (string meal in meals[0].content) {
                            float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                            if (maxWidth < width) maxWidth = width;
                        }
                        mealX = xBar.Value - (halfMealSpace + (maxWidth / 2) + g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                        g.DrawString($"{meals[0].title} [{meals[0].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0].content) {
                            mealX = xBar.Value - (halfMealSpace + (maxWidth / 2) + g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width / 2);
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            maxWidth = g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[1].content) {
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth < width) maxWidth = width;
                            }
                            mealY = yBar.Maximum - yBar.Value;
                            mealX = xBar.Value + (halfMealSpace + (maxWidth / 2) - (g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width / 2));
                            g.DrawString($"{meals[1].title} [{meals[1].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1].content) {
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
                        mealX = xBar.Value - g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                        g.DrawString($"{meals[0].title} [{meals[0].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                        mealY += g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                        foreach(string meal in meals[0].content) {
                            mealX = xBar.Value - g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString(meal, contentFont, mealContentSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Height + contentSpace;
                        }
                        if (meals.Count == 2) {
                            float maxWidth = g.MeasureString($"{meals[0].title} [{meals[0].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            foreach (string meal in meals[0].content) {
                                float width = g.MeasureString(meal, contentFont, baseSize, StringFormat.GenericTypographic).Width;
                                if (maxWidth< width) maxWidth = width;
                            }
                            float space = maxWidth + (float)mealSpaceBox.Value;
                            mealY = yBar.Maximum - yBar.Value;
                            mealX = xBar.Value - space - g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Width;
                            g.DrawString($"{meals[1].title} [{meals[1].calorie}]", titleFont, mealTitleSB, new RectangleF(mealX, mealY, image.Width, image.Height), StringFormat.GenericTypographic);
                            mealY += g.MeasureString($"{meals[1].title} [{meals[1].calorie}]", titleFont, baseSize, StringFormat.GenericTypographic).Height;
                            foreach (string meal in meals[1].content) {
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
