using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
                output.Add(Regex.Replace(lunch.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Split('\n'));
                if (dinner != null) {
                    output.Add(Regex.Replace(dinner.InnerHtml.Trim(), @"\n|[0-9\.]{2,}", "").Replace("<br>", "\n").Replace("&nbsp", " ").Split('\n'));
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
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            SolidBrush meal_title_sb = new SolidBrush(titleColorBox.BackColor);
            SolidBrush meal_content_sb = new SolidBrush(contentColorBox.BackColor);

            int meal_x = xBar.Value;
            int meal_y = (yBar.Maximum - yBar.Value);

            StringFormat format = new StringFormat() { Alignment = StringAlignment.Far  };

            // if (meals.Count < 1) {
            //     g.DrawString("중식 [lunch]", titleFont, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
            //     meal_y += TextRenderer.MeasureText("중식 [lunch]", titleFont).Height + 10;

            //     g.DrawString(meal[0], contentFont, meal_content_sb, new RectangleF(-8, meal_y, image.Width, image.Height), format);
            //     meal_y += TextRenderer.MeasureText(meal[0], contentFont).Height + 50;

            //     if (meal.Length > 1) { // 석식이 있다면
            //         g.DrawString("석식 [dinner]", meal_title, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
            //         meal_y += TextRenderer.MeasureText("석식 [dinner]", meal_title).Height + 10;

            //         g.DrawString(meal[1], meal_description, meal_content_sb, new RectangleF(-8, meal_y, image.Width, image.Height), format);
            //     }
            // }
            // else {
            //     g.DrawString("급식이 없습니다..", meal_title, meal_title_sb, new RectangleF(0, meal_y, image.Width, image.Height), format);
            // }

            image.Save(Path.Combine(Application.StartupPath, "mealTest.png"), System.Drawing.Imaging.ImageFormat.Png);
            previewBox.ImageLocation = Path.Combine(Application.StartupPath, "mealTest.png");
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
                xBar.Value = Properties.Settings.Default.mealX;
                yBar.Value = yBar.Maximum - Properties.Settings.Default.mealY;
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
            alignmentBox.SelectedIndex = (int)Properties.Settings.Default.mealAlignment;
            if (Properties.Settings.Default.mealLayout == 0) verticalBtn.Select();
            else horizontalBtn.Select();

            titleFontBox.Text = titleFont.Name;
            titleSizeBox.Text = titleFont.Size.ToString();
            contentFontBox.Text = contentFont.Name;
            contentSizeBox.Text = contentFont.Size.ToString();
        }

        private void xCenterBtn_Click(object sender, EventArgs e)
        {
            xBar.Value = xBar.Maximum / 2;
        }

        private void yCenterBtn_Click(object sender, EventArgs e)
        {
            yBar.Value = yBar.Maximum / 2;
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {

        }

        private void xBar_Scroll(object sender, EventArgs e)
        {

        }

        private void titleColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = titleColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                titleColorBox.BackColor = cd.Color;
            }
        }

        private void contentColorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = contentColorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                contentColorBox.BackColor = cd.Color;
            }
        }

        private void setContentFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = contentFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                contentFont = fd.Font;
            }
        }

        private void setTitleFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = titleFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                titleFont = fd.Font;
            }
        }

        private void alignmentBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void verticalBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void previewBox_Click(object sender, EventArgs e)
        {

        }
    }
}
