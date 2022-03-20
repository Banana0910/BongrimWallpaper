using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using hap = HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SchoolWallpaper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] get_meal() {
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
            wc.QueryString.Add("dietDate", "2022/03/03");
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString("http://bongrim-h.gne.go.kr/bongrim-h/dv/dietView/selectDietDetailView.do");

            hap.HtmlDocument htmlDoc = new hap.HtmlDocument();
            htmlDoc.LoadHtml(html);

            string lunch = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[2]/table/tbody/tr[2]/td").InnerHtml.Trim().Replace("<br>", "\n");
            string dinner = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='subContent']/div/div[3]/div[3]/table/tbody/tr[2]/td").InnerHtml.Trim().Replace("<br>", "\n");

            return new string[] { lunch, dinner };
        }

        private void button1_Click(object sender, EventArgs e)                                                                                          
        {
            Bitmap bitmap = new Bitmap(1920, 1080);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, 0, 0, 1920, 1080);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            string[] meal = get_meal();
            int leftheight = 10;

            g.DrawString("중식 (lunch)", new Font("나눔고딕", 20), Brushes.Black, new RectangleF(10,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText("중식 (lunch)", new Font("나눔고딕 Light", 20)).Height;

            g.DrawString(meal[0], new Font("나눔고딕 Light", 16), Brushes.DimGray, new RectangleF(20,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText(meal[0], new Font("나눔고딕 Light", 16)).Height;

            g.DrawString("석식 (dinner)", new Font("나눔고딕", 20), Brushes.Black, new RectangleF(10,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText("석식 (dinner)", new Font("나눔고딕 Light", 20)).Height;

            g.DrawString(meal[1], new Font("나눔고딕 Light", 16), Brushes.DimGray, new RectangleF(20,leftheight,1920,1080), format);
            leftheight += TextRenderer.MeasureText(meal[1], new Font("나눔고딕 Light", 16)).Height;

            format.Alignment = StringAlignment.Far;
            g.DrawString("현재 수업", new Font("나눔스퀘어라운드 ExtraBold", 30), Brushes.Black, new RectangleF(0,0,1920,1080), format);
            bitmap.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
    