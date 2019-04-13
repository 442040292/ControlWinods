using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeWin32API
{
    public class WindowHelper
    {
        public static string GetProcessName(IntPtr intPtr)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            int processId = 0;
            NativeMethods.GetWindowThreadProcessId(intPtr, out processId);
            //Process process = Process.GetProcesses().First(x => x.Handle == intPtr);
            Process process = Process.GetProcessById(processId);
            if (process != null)
            {
                sw.Stop();
                System.IO.File.AppendAllText(@"e:\GetProcessName.txt", $"GetProcessName:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");
                return process.ProcessName;
            }
            sw.Stop();
            System.IO.File.AppendAllText(@"e:\GetProcessName.txt", $"GetProcessName:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");
            return string.Empty;
        }

        public static string GetWindowName(IntPtr intPtr)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start(); int length = NativeMethods.GetWindowTextLength(intPtr);
            StringBuilder sb = new StringBuilder(length + 1);
            NativeMethods.GetWindowText(intPtr, sb, sb.Capacity);
            sw.Stop();
            System.IO.File.AppendAllText(@"e:\GetWindowName.txt", $"GetWindowName:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");

            return sb.ToString();
        }

        public static string GetWindowNameOld(IntPtr intPtr)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            StringBuilder sb = new StringBuilder(512);
            NativeMethods.GetWindowText(intPtr, sb, sb.Capacity);
            sw.Stop();
            System.IO.File.AppendAllText(@"e:\GetWindowName.txt", $"GetWindowName:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");

            return sb.ToString();
        }
        public static string GetClassName(IntPtr intPtr)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start(); StringBuilder sbuilder = new StringBuilder(256);
            NativeMethods.GetClassName(intPtr, sbuilder, sbuilder.Capacity);
            sw.Stop();
            System.IO.File.AppendAllText(@"e:\GetClassName.txt", $"GetClassName:time:[{sw.Elapsed.TotalMilliseconds}]{Environment.NewLine}");

            return sbuilder.ToString();
        }
    }
}
