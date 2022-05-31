using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Text;

namespace BongrimWallpaper
{
    public partial class DateForm : Form
    {
        private Font font;

        public DateForm()
        {
            InitializeComponent();
        }

        private void refresh_preview() {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);

            string today = DateTime.Now.ToString(dateFormatBox.Text);
            SizeF dateSize = g.MeasureString(today, font, new SizeF(image.Width, image.Height), StringFormat.GenericTypographic);
            float dateX = xBar.Value - (dateSize.Width / 2);
            float dateY = (yBar.Maximum - yBar.Value) - (dateSize.Height / 2);

            g.DrawString(today, font, new SolidBrush(colorBox.BackColor), new RectangleF(dateX, dateY, image.Width, image.Height), StringFormat.GenericTypographic);
            previewBox.Image = image;
        }


        private void setFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = font;
            if (fd.ShowDialog() == DialogResult.OK) {
                font = fd.Font;
                fontBox.Text = font.Name;
                sizeBox.Text = font.Size.ToString();
                refresh_preview();
            }
        }

        private void DateForm_Load(object sender, EventArgs e)
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
                xBar.Value = (int)Properties.Settings.Default.dateX;
                yBar.Value = yBar.Maximum - (int)Properties.Settings.Default.dateY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            font = Properties.Settings.Default.dateFont;
            fontBox.Text = font.Name;
            sizeBox.Text = font.Size.ToString();
            dateFormatBox.Text = Properties.Settings.Default.dateFormat;
            colorBox.BackColor = Properties.Settings.Default.dateColor;
            refresh_preview();
        }

        private void xCenterBtn_Click(object sender, EventArgs e) {
            xBar.Value = xBar.Maximum / 2; 
            refresh_preview();
        }

        private void yCenterBtn_Click(object sender, EventArgs e) {
            yBar.Value = yBar.Maximum / 2; 
            refresh_preview();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;
            if (dateVisibleCheck.Checked) {
                settings.dateFont = font;
                settings.dateFormat = dateFormatBox.Text;
                settings.dateColor = colorBox.BackColor;
                settings.dateX = xBar.Value;
                settings.dateY = (yBar.Maximum - yBar.Value);
                settings.dateVisible = true;
            } else {
                settings.dateVisible = false;
            }
            MessageBox.Show("설정이 저장되었습니다", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            dateOutputLbl.Text = DateTime.Today.ToString(dateFormatBox.Text);
            refresh_preview();
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = colorBox.BackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                colorBox.BackColor = cd.Color;
                refresh_preview();
            }
        }

        private void dateVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dateVisibleCheck.Checked) {
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
            } else {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
            }
        }

        private void xBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {
            refresh_preview();
        }

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "dateTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        private void DateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "dateTest.png"))) {
                File.Delete(Path.Combine(Application.StartupPath, "dateTest.png"));
            }
        }
    }
}
