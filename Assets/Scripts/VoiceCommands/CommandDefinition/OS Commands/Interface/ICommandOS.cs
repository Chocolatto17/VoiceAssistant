namespace Chocolatto.VoiceCommand
{
    public interface ICommandOS
    {
        void OpenApplication(string cmdArguments, string exeFilePath);
        void CloseApplication(string processName);
        void MinimizeApplication(string processName);
        void FocusApplication(string processName);
    }
}