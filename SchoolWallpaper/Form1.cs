using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Web;
using System.Net;
using hap = HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SchoolWallpaper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

         public static void set_wallpaper(Style style)
        {
            string tempPath = Path.Combine(Application.StartupPath,"test.png");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private string[] get_meal() {
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
            wc.QueryString.Add("dietDate", DateTime.Now.ToString("yyyy/MM/dd"));
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do");

            hap.HtmlDocument htmlDoc = new hap.HtmlDocument();
            htmlDoc.LoadHtml(html);

            string lunch = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[2]/table/tbody/tr[2]/td").InnerHtml.Trim();
            string dinner = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[3]/table/tbody/tr[2]/td").InnerHtml.Trim();

            lunch = Regex.Replace(lunch, @"\n|[0-9\.]+", "").Replace("<br>", "\n");;
            dinner = Regex.Replace(dinner, @"\n|[0-9\.]+", "").Replace("<br>", "\n");;

            return new string[] { lunch, dinner };
        }

        private void button1_Click(object sender, EventArgs e)                                                                                          
        {
            Bitmap bitmap = new Bitmap(1920, 1080);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, 0, 0, 1920, 1080);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            string[] meal = get_meal();
            int leftheight = 10;
            int centerheight = 0;

            g.DrawString("중식 [lunch]", new Font("나눔고딕", 20), Brushes.Black, new RectangleF(-8,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText("중식 (lunch)", new Font("나눔고딕", 20)).Height;

            g.DrawString(meal[0], new Font("나눔고딕 Light", 16), Brushes.DimGray, new RectangleF(-10,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText(meal[0], new Font("나눔고딕 Light", 16)).Height;

            g.DrawString("석식 [dinner]", new Font("나눔고딕", 20), Brushes.Black, new RectangleF(-8,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText("석식 (dinner)", new Font("나눔고딕", 20)).Height;

            g.DrawString(meal[1], new Font("나눔고딕 Light", 16), Brushes.DimGray, new RectangleF(-10,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText(meal[1], new Font("나눔고딕 Light", 16)).Height;

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            
            centerheight = ((int)g.MeasureString("국어", new Font("나눔스퀘어 ExtraBold", 75)).Height / 2 + 10 +
                (int)g.MeasureString("임양경", new Font("나눔스퀘어 ExtraBold", 30)).Height) / -2;

            g.DrawString("국어", new Font("나눔스퀘어 Bold", 75), Brushes.Black, new RectangleF(0,centerheight,1920,1080), format);
            centerheight += (int)g.MeasureString("국어", new Font("나눔스퀘어 ExtraBold", 75)).Height / 2 + 10;

            g.DrawString("임양경 (12:00 ~ 12:00)", new Font("나눔고딕 Light", 30), Brushes.Gray, new RectangleF(0,centerheight,1920,1080), format);
            centerheight += (int)g.MeasureString("임양경 (12:00 ~ 12:00)", new Font("나눔고딕 Light", 20)).Height - 10;

            bitmap.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.ImageLocation = "./test.png";
            set_wallpaper(Style.Stretched);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Application.StartupPath,"test.png"));
            Process.Start(psi);
        }
    }
}
    