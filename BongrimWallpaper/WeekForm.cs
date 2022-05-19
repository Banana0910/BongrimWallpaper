using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (studentList.SelectedIndex > 0) {
                studentList.Items.RemoveAt(studentList.SelectedIndex);
            }
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

        }

        private void xCenterBtn_Click(object sender, EventArgs e)
        {

        }

        private void yCenterBtn_Click(object sender, EventArgs e)
        {

        }

        private void yBar_Scroll(object sender, EventArgs e)
        {

        }

        private void xBar_Scroll(object sender, EventArgs e)
        {

        }

        private void previewBox_Click(object sender, EventArgs e)
        {

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
            Properties.Settings.Default.students.Add("s"); //this is broke :<
        }
    }
}
