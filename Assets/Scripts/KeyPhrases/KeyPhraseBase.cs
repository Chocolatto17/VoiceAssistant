using System.Collections.Generic;
using UnityEngine;

namespace Chocolatto.VoiceAssistance
{
    public abstract class KeyPhraseBase : ScriptableObject
    {
        [SerializeField] protected KeyPhraseBase[] includedKeyPhrases;
        [SerializeField] protected List<string> customKeyPhrases;



        public List<string> GetAllKeyPhrases()
        {
            int keyPhraseLength = customKeyPhrases.Count;
            List<string> allKeyPhrases = new List<string>(keyPhraseLength);
            allKeyPhrases.AddRange(customKeyPhrases);

            for (int i = 0; i < includedKeyPhrases.Length; i++)
                allKeyPhrases.AddRange(includedKeyPhrases[i].GetAllKeyPhrases());

            return allKeyPhrases;
        }
    }
}