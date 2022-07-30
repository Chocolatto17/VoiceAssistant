// using UnityEditor;
// using UnityEditorInternal;
// using UnityEngine;

// namespace Chocolatto.VoiceAssistance.Editor
// {
//     [CustomEditor(typeof(KeyPhraseBase))]
//     public class KeyPhraseReorderableListEditor : UnityEditor.Editor
//     {
//         private SerializedProperty _property;
//         private ReorderableList _list;

//         private void OnEnable()
//         {
//             _property = serializedObject.FindProperty("customKeyPhrases");
//             _list = new ReorderableList(serializedObject, _property, true, true, true, true)
//             {
//                 drawHeaderCallback = DrawListHeader,
//                 drawElementCallback = DrawListElement
//             };
//         }

//         private void DrawListHeader(Rect rect)
//         {
//             GUI.Label(rect, "");
//         }

//         private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
//         {
//             var item = _property.GetArrayElementAtIndex(index);
//             EditorGUI.PropertyField(rect, item);

//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             EditorGUILayout.Space();
//             _list.DoLayoutList();
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }