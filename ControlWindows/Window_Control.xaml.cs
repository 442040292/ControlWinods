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

namespace ControlWindows
{
    /// <summary>
    /// Window_Control.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Control : UserControl
    {
        public Window_Control()
        {
            InitializeComponent();


        }


        public IntPtr hWnd
        {
            get { return (IntPtr)GetValue(hWndProperty); }
            set { SetValue(hWndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for intPtr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty hWndProperty =
            DependencyProperty.Register("hWnd", typeof(IntPtr), typeof(Window_Control), new PropertyMetadata(IntPtr.Zero));



        public string ProcessName
        {
            get { return (string)GetValue(ProcessNameProperty); }
            set { SetValue(ProcessNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessNameProperty =
            DependencyProperty.Register("ProcessName", typeof(string), typeof(Window_Control), new PropertyMetadata("程序名称"));



        public string WindowName
        {
            get { return (string)GetValue(WindowNameProperty); }
            set { SetValue(WindowNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowNameProperty =
            DependencyProperty.Register("WindowName", typeof(string), typeof(Window_Control), new PropertyMetadata("窗体名称"));



        public string WindowClassName
        {
            get { return (string)GetValue(WindowClassNameProperty); }
            set { SetValue(WindowClassNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowClassName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowClassNameProperty =
            DependencyProperty.Register("WindowClassName", typeof(string), typeof(Window_Control), new PropertyMetadata("窗体类名"));

        private void ControlWindow_Click(object sender, RoutedEventArgs e)
        {
            var nCmdShow = Convert.ToInt32((sender as Button).Tag.ToString());
            NativeWin32API.NativeMethods.ShowWindow(this.hWnd, nCmdShow);
        }

        private void GetProcessName_Click(object sender, RoutedEventArgs e)
        {
            this.ProcessName = WindowHelper.GetProcessName(this.hWnd);
        }

    }
}
