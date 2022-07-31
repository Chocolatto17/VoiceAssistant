using System;
using System.Collections.Generic;
using System.IO;
using Chocolatto.VoiceAssistance.Data;
using Jitbit.Utils;
using Newtonsoft.Json;
using SFB;
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
            var script = (KeyPhraseBase)target;

            DrawAddCustomKeyPhrase();
            DrawExportButtons();
            DrawImportButtons();

            GUILayout.Space(10);
            base.OnInspectorGUI();
        }

        private void DrawAddCustomKeyPhrase()
        {
            GUILayout.Label("Quick Setup Key Phrase (Paste your paragraph of phrases here!)");

            keyPhraseTextArea = EditorGUILayout.TextArea(keyPhraseTextArea, GUILayout.Height(50));

            if (GUILayout.Button("Add to Custom Key Phrases", GUILayout.Height(20)))
            {
                SerializeToList();
                SaveSerializedData();
            }
        }

        private void SerializeToList()
        {
            string[] keyPhrases = keyPhraseTextArea.Split(' ', (char)13);
            Debug.Log("text L = " + keyPhraseTextArea.Length + ". keyPhrase L = " + keyPhrases.Length);

            var customKeyPhrases = serializedObject.FindProperty("customKeyPhrases");
            int customKeyPhrasesLength = customKeyPhrases.arraySize;
            string phraseKeyPropertyName = nameof(PhraseKVP.key);
            string phraseValuePropertyName = nameof(PhraseKVP.value);

            for (int i = 0; i < keyPhrases.Length; i++)
            {
                string phrase = keyPhrases[i].Trim();
                if (string.IsNullOrEmpty(phrase))
                    continue;

                customKeyPhrases.InsertArrayElementAtIndex(customKeyPhrasesLength);
                var element = customKeyPhrases.GetArrayElementAtIndex(customKeyPhrasesLength);
                element.FindPropertyRelative(phraseKeyPropertyName).stringValue = phrase;
                element.FindPropertyRelative(phraseValuePropertyName).stringValue = phrase;
                customKeyPhrasesLength++;
            }
        }

        private void SaveSerializedData()
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private void DrawExportButtons()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Export to JSON", GUILayout.Height(20)))
                ExportToJsonFile();
            if (GUILayout.Button("Export to CSV", GUILayout.Height(20)))
                ExportToCSVFile();

            EditorGUILayout.EndHorizontal();
        }

        private void ExportToJsonFile()
        {
            var customKeyPhrases = serializedObject.FindProperty("customKeyPhrases");
            int customKeyPhrasesLength = customKeyPhrases.arraySize;
            string phraseKeyPropertyName = nameof(PhraseKVP.key);
            string phraseValuePropertyName = nameof(PhraseKVP.value);

            List<PhraseKVP> phraseKVPs = new List<PhraseKVP>(customKeyPhrasesLength);
            for (int i = 0; i < customKeyPhrasesLength; i++)
            {
                var element = customKeyPhrases.GetArrayElementAtIndex(i);

                phraseKVPs.Add(new PhraseKVP()
                {
                    key = element.FindPropertyRelative(phraseKeyPropertyName).stringValue,
                    value = element.FindPropertyRelative(phraseValuePropertyName).stringValue
                });
            }

            string filePath = StandaloneFileBrowser.SaveFilePanel("Save to", Application.dataPath, "Command", "json");
            if (!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(phraseKVPs, Formatting.Indented));
                Debug.Log($"File saved at: {filePath}");
            }
        }

        private void ExportToCSVFile()
        {
            var customKeyPhrases = serializedObject.FindProperty("customKeyPhrases");
            int customKeyPhrasesLength = customKeyPhrases.arraySize;
            string phraseKeyPropertyName = nameof(PhraseKVP.key);
            string phraseValuePropertyName = nameof(PhraseKVP.value);

            CsvExport csvExport = new CsvExport();
            for (int i = 0; i < customKeyPhrasesLength; i++)
            {
                var element = customKeyPhrases.GetArrayElementAtIndex(i);

                csvExport.AddRow();
                csvExport["SPEECH PHRASE"] = element.FindPropertyRelative(phraseKeyPropertyName).stringValue;
                csvExport["VALUE"] = element.FindPropertyRelative(phraseValuePropertyName).stringValue;
            }

            string filePath = StandaloneFileBrowser.SaveFilePanel("Save to", Application.dataPath, "Command", "csv");
            if (!string.IsNullOrEmpty(filePath))
            {
                csvExport.ExportToFile(filePath);
                Debug.Log($"File saved at: {filePath}");
            }
        }

        private void DrawImportButtons()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Import from JSON", GUILayout.Height(20)))
                ImportFromJsonFiles();
            if (GUILayout.Button("Import from CSV", GUILayout.Height(20)))
                ImportFromCSVFiles();

            EditorGUILayout.EndHorizontal();
        }

        private void ImportFromJsonFiles()
        {
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Import Phrase", Application.dataPath, "json", true);

            for (int i = 0; i < filePaths.Length; i++)
            {
                string jsonContent = File.ReadAllText(filePaths[i]);
                // List<PhraseKVP> phraseKVPs = JsonConvert.DeserializeObject<List<PhraseKVP>>(jsonContent);

                Debug.Log(jsonContent);
            }
        }

        private void ImportFromCSVFiles()
        {

        }
    }
}