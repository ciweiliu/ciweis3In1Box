using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ciweisShortCuts
{
    public class windowsAPI
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32", EntryPoint = "SetWindowLong", SetLastError = true)]
        public static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref Rect lpRect);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, WNDENUMPROC lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(int hWnd, ref Rect lpRect, bool bErase);

        public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);

        public static List<WindowInfo> EnumChildWindowsCallback(IntPtr handle, string name, string classname)
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            EnumChildWindows(handle, delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();

                // get window size
                Rect rec = new Rect();
                GetWindowRect(hWnd, ref rec);
                wnd.szSize = rec;

                //add it into list 
                wndList.Add(wnd);
                return true;
            }, 0);
            return wndList;
            // return wndList.Where(it => it.szWindowName == name && it.szClassName == classname).ToList();
        }
    }

    public struct WindowInfo
    {
        public IntPtr hWnd;
        public string szWindowName;
        public string szClassName;
        public Rect szSize;
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public class winWH
    {
        public winWH(int w, int h)
        {
            _w = w;
            _h = h;
        }
        int _w;
        int _h;
        public int Width { get { return _w; } }
        public int Height { get { return _h; } }
    }
}
