using System.Windows.Input;
using Chocolatto.VoiceCommand;
using UnityEngine;

namespace Chocolatto.VoiceAssistance
{
    public class SpeechToTextEventListener : MonoBehaviour
    {
        public VoskSpeechToText VoskSpeechToText;



        void Awake()
        {
            VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        }

        private void OnTranscriptionResult(string obj)
        {
            Debug.Log(obj);
            var result = new RecognitionResult(obj);
            if (result.Phrases[0].Text.Trim().ToLower() == "stop recording")
            {
                VoskSpeechToText.StopVoskStt();
                return;
            }

            if (result.Phrases[0].Text.Trim().ToLower() == "open steam")
            {
                ICommandOS commandOS = new WindowsCommand();
                commandOS.OpenApplication(null, @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Steam\Steam.exe");
                return;
            }

            // for (int i = 0; i < result.Phrases.Length; i++)
            // {
            //     if (i > 0)
            //     {
            //         ResultText.text += ", ";
            //     }

            //     ResultText.text += result.Phrases[i].Text;
            // }
            // ResultText.text += "\n";
        }
    }
}