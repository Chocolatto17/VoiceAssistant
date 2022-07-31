namespace Chocolatto.VoiceCommand
{
    public struct MinimizeApplicationCommand : ICommand
    {
        private string processName;



        public MinimizeApplicationCommand(string processName)
        {
            this.processName = processName;
        }

        public void Execute()
        {
            OSCommandExecutor.MinimizeApplication(processName);
        }
    }
}