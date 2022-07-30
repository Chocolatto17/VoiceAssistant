using UnityEditor;
using UnityEngine;

namespace Chocolatto.VoiceAssistance.Editor
{
    [CustomEditor(typeof(KeyPhraseBase), editorForChildClasses: true)]
    public class KeyPhraseEditor : UnityEditor.Editor
    {
        private string keyPhraseTextArea;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (KeyPhraseBase)target;

            GUILayout.Space(50);
            GUILayout.Label("Quick Setup Key Phrase (Paste your paragraph of keyphrases here!)");
            keyPhraseTextArea = EditorGUILayout.TextArea(keyPhraseTextArea, GUILayout.Height(50));
            if (GUILayout.Button("Add to Custom Key Phrases", GUILayout.Height(20)))
            {
                string[] keyPhrases = keyPhraseTextArea.Split(' ', (char)13);
                Debug.Log("text L = " + keyPhraseTextArea.Length + ". keyPhrase L = " + keyPhrases.Length);

                var customKeyPhrases = serializedObject.FindProperty("customKeyPhrases");
                int customKeyPhrasesLength = customKeyPhrases.arraySize;
                for (int i = 0; i < keyPhrases.Length; i++)
                {
                    if (string.IsNullOrEmpty(keyPhrases[i].Trim()))
                        continue;

                    customKeyPhrases.InsertArrayElementAtIndex(customKeyPhrasesLength);
                    customKeyPhrases.GetArrayElementAtIndex(customKeyPhrasesLength).stringValue = keyPhrases[i].Trim();
                    customKeyPhrasesLength++;
                }

                serializedObject.ApplyModifiedProperties();
                serializedObject.UpdateIfRequiredOrScript();
            }
        }
    }
}