using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmileWei.EmbeddedApp;
using System.Runtime.InteropServices;
using System.Threading;
namespace ciweisShortCuts
{
    public partial class scClientMainForm : Form
    {
        AppContainer appBox;
       
        public scClientMainForm()
        {
            InitializeComponent();
            
         //   appBox.Width = windowsPanel.Width;
        //    appBox.Height = windowsPanel.Height;
        }

        private void scClientMainForm_Resize(object sender, EventArgs e)
        {
            var app = appBox.AppProcess;
            if (app == null) { return; }

            var c = Form.FromHandle(app.MainWindowHandle);
            var f = c as Form;
            if (f != null)
            {
                Console.WriteLine(f.Parent == null);
            }
            appBox.Width = windowsPanel.Width;
            appBox.Height = windowsPanel.Height;
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "MoveWindow")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        [DllImport("user32", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern uint SetWindowLong(IntPtr hwnd,int nIndex, uint dwNewLong);

        private void addWindowsToPanel()
        {
            appBox.AppFilename = @"C:\Windows\explorer.exe";
            appBox.Start();

            Thread.Sleep(3000);
            IntPtr hwnd = new IntPtr(0);
            hwnd = FindWindow(null, "库");
            if (hwnd != IntPtr.Zero)
            {
                MoveWindow(hwnd, 0, 0, windowsPanel.Width, windowsPanel.Height, true);

                SetParent(hwnd, this.Handle);
                Win32API.SetWindowLong(new HandleRef(this.appBox, hwnd), GWL_STYLE, WS_VISIBLE);
            }
        }

        private void scClientMainForm_Load(object sender, EventArgs e)
        {
            appBox = new AppContainer(false);
            windowsPanel.Controls.Add(appBox);
            appBox.Location = new Point(windowsPanel.Location.X, windowsPanel.Location.Y);
            this.appBox.ShowEmbedResult = true;

            addWindowsToPanel();
        }
       
    }
}
