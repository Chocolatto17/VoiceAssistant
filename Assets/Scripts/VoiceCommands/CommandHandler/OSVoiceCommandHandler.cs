using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chocolatto.VoiceAssistance
{
    public class OSVoiceCommandHandler : IVoiceCommandHandler
    {
        public IVoiceCommandHandler NextHandler { get; set; }
        public KeyPhraseBase KeyPhraseData { get; set; }

        public void HandleVoiceCommand(string commandPhrase)
        {
            if (KeyPhraseData.IsPhraseExist(commandPhrase))
                // TODO
                ;
            else
                NextHandler.HandleVoiceCommand(commandPhrase);
        }
    }
}