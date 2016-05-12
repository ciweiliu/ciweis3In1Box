using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace ciweisShortCuts
{
    public class exploreOP
    {
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;

        public static void addWindowsToPanel(Panel windowsPanel,ref IntPtr hwnd,ref bool isRunning)
        {
            //   originalSize.Clear();
            hwnd = windowsAPI.FindWindow(null, "库");
            if (hwnd != IntPtr.Zero)
            {
                AppEmbeded(hwnd, windowsPanel);
                windowsAPI.MoveWindow(hwnd, 0, 0, windowsPanel.Width, windowsPanel.Height, true);
                isRunning = true;               
            }
            // winPanelOriginH = windowsPanel.Height;
            //  winPanelOriginW = windowsPanel.Width;
        }

        public static void startExe()
        {
            string exeName = @"C:\Windows\explorer.exe";
            Process p = new Process();
            ProcessStartInfo startinfo = new ProcessStartInfo();//创建进程时使用的一组值，如下面的属性

            startinfo.FileName = exeName;//设定需要执行的命令程序
            //以下是隐藏cmd窗口的方法
            startinfo.Arguments = null;//设定参数，要输入到命令程序的字符，其中"/c"表示执行完命令后马上退出
            startinfo.UseShellExecute = false;//不使用系统外壳程序启动
            startinfo.RedirectStandardInput = false;//不重定向输入
            startinfo.RedirectStandardOutput = false;//重定向输出，而不是默认的显示在dos控制台上
            startinfo.CreateNoWindow = true;//不创建窗口
            p.StartInfo = startinfo;
            p.Start();
        }

        //嵌入APP程序
        private static void AppEmbeded(IntPtr _handle, Control _control)
        {
            //改变父窗口
            windowsAPI.SetParent(_handle, _control.Handle);
            windowsAPI.SetWindowLong(_handle, GWL_STYLE, WS_VISIBLE);
        }

        #region
        //找到“库”窗口的所有子窗口，form变化时，所有子窗口按比例变化
        //List<WindowInfo> winList =windowsAPI.EnumChildWindowsCallback(hwnd, string.Empty, string.Empty);

        //foreach (WindowInfo win in winList)
        //{
        //    int w = win.szSize.Right - win.szSize.Left;
        //    int h = win.szSize.Bottom - win.szSize.Top;
        //    winWH  wh= new winWH(w,h);
        ////    originalSize.Add(win.hWnd,wh);
        //}
        #endregion
    }
}
