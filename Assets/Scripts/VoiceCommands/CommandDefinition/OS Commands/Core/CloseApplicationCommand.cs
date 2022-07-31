namespace Chocolatto.VoiceCommand
{
    public struct CloseApplicationCommand : ICommand
    {
        private string processName;



        public CloseApplicationCommand(string processName)
        {
            this.processName = processName;
        }

        public void Execute()
        {
            OSCommandExecutor.CloseApplication(processName);
        }
    }
}