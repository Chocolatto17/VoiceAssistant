#if UNITY_STANDALONE_WIN
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Debug = UnityEngine.Debug;

namespace Chocolatto.VoiceCommand
{
    internal class WindowsCommand : ICommandOS
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        // https://stackoverflow.com/questions/39458046/c-sharp-how-to-minimize-another-application-by-a-given-process-id
        private const int SW_HIDE = 0;
        private const int SW_NORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWMINNOACTIVE = 7;
        private const int SW_SHOWNA = 8;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;
        private const int SW_FORCEMINIMIZE = 11;



        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);




        public void OpenApplication(string cmdArguments, string exeFilePath)
        {
            try
            {
                // Prepare the process to run
                ProcessStartInfo processInfo = new ProcessStartInfo();
                // Enter in the command line arguments, everything you would enter after the executable name itself
                processInfo.Arguments = cmdArguments;
                // Enter the executable to run, including the complete path
                processInfo.FileName = exeFilePath;
                // Do you want to show a console window?
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.CreateNoWindow = true;

                Process process = Process.Start(processInfo);
                process.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void MinimizeApplication(string processName)
        {
            try
            {
                IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, processName);
                ShowWindow(hwnd, SW_MINIMIZE);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void CloseApplication(string processName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                for (int i = 0; i < processes.Length; i++)
                {
                    processes[i].Kill();
                    processes[i].Close();
                }
            }
            catch (Exception e) when (e is Win32Exception || e is FileNotFoundException)
            {
                Debug.LogException(e);
            }
        }

        public void FocusApplication(string processName)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif