namespace Chocolatto.VoiceCommand
{
    public struct OpenApplicationCommand : ICommand
    {
        private string cmdArguments;
        private string exeFilePath;



        public OpenApplicationCommand(string cmdArguments, string exeFilePath)
        {
            this.cmdArguments = cmdArguments;
            this.exeFilePath = exeFilePath;
        }

        public void Execute()
        {
            OSCommandExecutor.OpenApplication(cmdArguments, exeFilePath);
        }
    }
}