using System;
using System.Drawing;
using System.IO;
using System.Drawing.Text;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace BongrimWallpaper
{
    public partial class WeekForm : Form
    {
        public WeekForm()
        {
            InitializeComponent();
        }
        
        Font font;
        int[] nums;

        private void refresh_preview() {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Properties.Settings.Default.backgroundPath.Length > 0) 
                g.DrawImage(Image.FromFile(Properties.Settings.Default.backgroundPath), 0, 0, image.Width, image.Height);
            else g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);
            string text = "주번 : ";
            foreach (int i in nums)
                text += $"{studentList.Items[i]} ";
            SolidBrush sb = new SolidBrush(colorBox.BackColor);

            SizeF baseSize = new SizeF(image.Width, image.Height);
            SizeF strSize = g.MeasureString(text, font, baseSize, StringFormat.GenericTypographic);
            float weekX = xBar.Value;
            float weekY = (yBar.Maximum - yBar.Value);
            g.DrawString(text, font, sb, weekX, weekY, StringFormat.GenericTypographic);
            previewBox.Image = image;
        }

        private void studentAddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(studentBox.Text)) {
                MessageBox.Show("학생 이름을 입력해주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (studentList.Items.Contains(studentBox.Text)) {
                MessageBox.Show($"{studentBox.Text}(은)는 이미 등록되어있습니다", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            studentList.Items.Add(studentBox.Text);
            studentBox.Clear();
        }

        private void studentDelBtn_Click(object sender, EventArgs e)
        {
            if (studentList.Items.Count-1 < (int)weekCountBox.Value) {
                MessageBox.Show("주번의 수보다 학생 수가 적을 수 없습니다!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (studentList.SelectedIndex > 0) {
                studentList.Items.RemoveAt(studentList.SelectedIndex);
            }
            refresh_preview();
        }

        private void studentUpBtn_Click(object sender, EventArgs e)
        {
            if (studentList.SelectedIndex > 0) {
                int selectedIndex = studentList.SelectedIndex;
                string temp = studentList.Items[selectedIndex-1].ToString();
                studentList.Items[selectedIndex-1] = studentList.Items[selectedIndex];
                studentList.Items[selectedIndex] = temp; 
                studentList.SelectedIndex = selectedIndex - 1;
            }
            refresh_preview();
        }

        private void studentDownBtn_Click(object sender, EventArgs e)
        {
            if (studentList.SelectedIndex < studentList.Items.Count-1) {
                int selectedIndex = studentList.SelectedIndex;
                string temp = studentList.Items[selectedIndex+1].ToString();
                studentList.Items[selectedIndex+1] = studentList.Items[selectedIndex];
                studentList.Items[selectedIndex] = temp; 
                studentList.SelectedIndex = selectedIndex + 1;
            }
            refresh_preview();
        }

        private void studentBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) {
                e.Handled = true;
                studentAddBtn_Click(sender, e);
            }
        }

        private void setFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog() { Font = font };
            if (fd.ShowDialog() == DialogResult.OK) {
                font = fd.Font;
                fontBox.Text = fd.Font.Name;
                sizeBox.Text = fd.Font.Size.ToString();
            }
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog() { Color = colorBox.BackColor };
            if (cd.ShowDialog() == DialogResult.OK) {
                colorBox.BackColor = cd.Color;
            }
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

        private void previewBox_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "weekTest.png");
            previewBox.Image.Save(path);
            Process.Start(path);
        }

        private void WeekForm_Load(object sender, EventArgs e)
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
                xBar.Value = (int)Properties.Settings.Default.weekX;
                yBar.Value = yBar.Maximum - (int)Properties.Settings.Default.weekY;
            } catch {
                xBar.Value = 0;
                yBar.Value = yBar.Maximum;
            }

            font = Properties.Settings.Default.weekFont;
            colorBox.BackColor = Properties.Settings.Default.weekColor;
            nums = Properties.Settings.Default.weekLastNums;
            var list = Properties.Settings.Default.students;
            foreach (string s in list) {
                studentList.Items.Add(s);
            }
            weekCountBox.Value = nums.Length;

            fontBox.Text = font.Name;
            sizeBox.Text = font.Size.ToString();

            refresh_preview();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (weekVisibleCheck.Checked) {
                StringCollection sc = new StringCollection();
                foreach (string name in studentList.Items) {
                    sc.Add(name);
                }
                Properties.Settings.Default.students = sc;
                Properties.Settings.Default.weekFont = font;
                Properties.Settings.Default.weekColor = colorBox.BackColor;
                Properties.Settings.Default.weekX = xBar.Value;
                Properties.Settings.Default.weekY = yBar.Maximum - yBar.Value;
                Properties.Settings.Default.weekVisible = true;
                Properties.Settings.Default.weekLastNums = nums;
                Properties.Settings.Default.weekLastDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            } else {
                Properties.Settings.Default.weekVisible = false;
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("저장 되었습니다", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WeekForm_FormClosed(object sender, FormClosedEventArgs e) {
            string path = Path.Combine(Application.StartupPath, "weekTest.png");
            if (File.Exists(path)) {
                File.Delete(path);
            }
        }

        private void weekVisibleCheck_CheckedChanged(object sender, EventArgs e)
        {
            weekGroup.Enabled = weekVisibleCheck.Checked;
            fontGroup.Enabled = weekVisibleCheck.Checked;
        }

        private void nextWeekBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nums.Length; i++) {
                nums[i] = (nums[i] + nums.Length) % studentList.Items.Count;
            }
            refresh_preview();
        }

        private void backWeekBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nums.Length; i++) {
                nums[i] = (studentList.Items.Count + (nums[i] - nums.Length)) % studentList.Items.Count;
            }
            refresh_preview();
        }

        private void weekCountBox_ValueChanged(object sender, EventArgs e)
        {
            if ((int)weekCountBox.Value > studentList.Items.Count) {
                weekCountBox.Value--;
                MessageBox.Show("주번의 수는 학생의 수보다 클 수 없습니다", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nums = new int[(int)weekCountBox.Value];
            for (int i = 0; i < nums.Length; i++) {
                nums[i] = i;
            }

            refresh_preview();
        }
    }
}
