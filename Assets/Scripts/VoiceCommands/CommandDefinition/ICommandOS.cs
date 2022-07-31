namespace Chocolatto.VoiceCommand
{
    public interface ICommandOS
    {
        void OpenApplication(string cmdArguments, string exeFilePath);
        void CloseApplication();
        void FocusApplication();
    }
}