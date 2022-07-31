using System;
using System.Collections.Generic;
using Chocolatto.VoiceAssistance.Data;
using UnityEngine;

namespace Chocolatto.VoiceAssistance
{
    public abstract class KeyPhraseBase : ScriptableObject
    {
        [SerializeField] protected KeyPhraseBase[] includedKeyPhrases;
        [SerializeField] protected List<PhraseKVP> customKeyPhrases;

        private Dictionary<string, string> mapKeyPhrases;



        void OnEnable()
        {
            Debug.Log("On Enable");
        }

        public List<string> GetAllKeyPhrases()
        {
            int keyPhraseLength = customKeyPhrases.Count;
            List<string> allKeyPhrases = new List<string>(keyPhraseLength);

            AddIncludedKeyPhrasesToList(ref allKeyPhrases);
            AddCustomKeyPhrasesToList(ref allKeyPhrases);

            return allKeyPhrases;
        }


        private void AddCustomKeyPhrasesToList(ref List<string> allKeyPhrases)
        {
            for (int i = 0; i < customKeyPhrases.Count; i++)
                allKeyPhrases.Add(customKeyPhrases[i].key);
        }

        private void AddIncludedKeyPhrasesToList(ref List<string> allKeyPhrases)
        {
            for (int i = 0; i < includedKeyPhrases.Length; i++)
                allKeyPhrases.AddRange(includedKeyPhrases[i].GetAllKeyPhrases());
        }

        public bool IsPhraseExist(string phrase)
        {
            return mapKeyPhrases.ContainsKey(phrase);
        }

        void OnDisable()
        {
            Debug.Log("On Disable");
        }
    }
}