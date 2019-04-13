using NativeWin32API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static NativeWin32API.NativeMethods;

namespace ControlWindows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
        }
        List<Window_Control> windows = new List<Window_Control>();

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            POINTAPI point = new POINTAPI();//必须用与之相兼容的结构体，类也可以
            //add some wait time
            //System.Threading.Thread.Sleep(8000);
            GetCursorPos(ref point);//获取当前鼠标坐标

            int hwnd = WindowFromPoint(point.X, point.Y);//获取指定坐标处窗口的句柄

            this.Dispatcher.Invoke(() =>
            {
                AddNewWindow(new IntPtr(hwnd));
                sview.ScrollToEnd();
            });
        }

        System.Timers.Timer timer;

        public Window_Control GetNewControl(IntPtr hwnd)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Window_Control temp = new Window_Control();
            temp.hWnd = hwnd;
            //temp.ProcessName = WindowHelper.GetProcessName(temp.hWnd);//耗时
            temp.WindowName = WindowHelper.GetWindowName(temp.hWnd);
            temp.WindowClassName = WindowHelper.GetClassName(temp.hWnd);
            sw.Stop();
            System.IO.File.AppendAllText(@"e:\GetNewControl.txt", $"GetNewControl:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");
            return temp;
        }

        public void AddNewWindow(IntPtr hwnd)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (windows.Exists(x => x.hWnd == hwnd)) return;
                Window_Control temp = GetNewControl(hwnd);
                windows.Add(temp);
                WindowsPanel.Children.Add(temp);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            windows.Clear();
            WindowsPanel.Children.Clear();
            var Desktop = NativeMethods.GetDesktopWindow();
            List<IntPtr> list = new List<IntPtr>();
            NativeMethods.EnumChildWindows(Desktop, (hwnd, plat) => { list.Add(hwnd); return true; }, IntPtr.Zero);
            list.ForEach(hwnd => Task.Run(() => AddNewWindow(hwnd)));
            sview.ScrollToEnd();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            windows.Clear();
            WindowsPanel.Children.Clear();
            NativeMethods.EnumWindows((hwnd, plat) => { Task.Run(() => AddNewWindow(hwnd)); return true; }, 0);
            sview.ScrollToEnd();
        }
    }
}
