using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BongrimWallpaper
{
    internal static class Program
    {
        [DllImport("user32")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] p = Process.GetProcessesByName("BongrimWallpaper");
            if (p.Length > 1 ) {
                IntPtr hWnd = FindWindow(null, "봉림고 바탕화면");
                if (!hWnd.Equals(IntPtr.Zero)){
                    ShowWindowAsync(hWnd, 1);
                }
                SetForegroundWindow(hWnd);
            } else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }
    }
}
