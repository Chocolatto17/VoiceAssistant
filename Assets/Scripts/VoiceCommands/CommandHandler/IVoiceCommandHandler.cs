namespace Chocolatto.VoiceAssistance
{
    public interface IVoiceCommandHandler
    {
        IVoiceCommandHandler NextHandler { get; set; }
        KeyPhraseBase KeyPhraseData { get; set; }

        void HandleVoiceCommand(string commandPhrase);
    }
}