#if UNITY_STANDALONE_WIN
using System.Diagnostics;

namespace Chocolatto.VoiceCommand
{
    public class WindowsCommand : ICommandOS
    {
        public void OpenApplication(string cmdArguments, string exeFilePath)
        {
            // Process.Start
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
            process.Dispose();
        }

        public void CloseApplication()
        {
            throw new System.NotImplementedException();
        }

        public void FocusApplication()
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif