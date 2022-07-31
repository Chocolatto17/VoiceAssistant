namespace Chocolatto.VoiceCommand
{
    public class OSCommandExecutor
    {
        private static ICommandOS platformCommandOS = null;

        static OSCommandExecutor()
        {
#if UNITY_STANDALONE_OSX
            // platformCommandOS = new StandaloneFileBrowserMac();
#elif UNITY_STANDALONE_WIN
            platformCommandOS = new WindowsCommand();
#elif UNITY_STANDALONE_LINUX
            // platformCommandOS = new StandaloneFileBrowserLinux();
#elif UNITY_EDITOR
            // platformCommandOS = new StandaloneFileBrowserEditor();
#endif
        }

        public static void OpenApplication(string cmdArguments, string exeFilePath)
        {
            platformCommandOS.OpenApplication(cmdArguments, exeFilePath);
        }

        public static void CloseApplication(string processName)
        {
            platformCommandOS.CloseApplication(processName);
        }

        public static void MinimizeApplication(string processName)
        {
            platformCommandOS.MinimizeApplication(processName);
        }

        public static void FocusApplication(string processName)
        {
            platformCommandOS.FocusApplication(processName);
        }
    }
}