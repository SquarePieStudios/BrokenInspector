using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(BrokenInspectorObject))]
public class BrokenInspectorEditor : PropertyDrawer {
    private static readonly GUIContent _deleteButtonContent = new GUIContent("X", "Delete");
    private SerializedProperty List1;
    private SerializedProperty List2;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return (property.FindPropertyRelative("CharList1").arraySize + 2) * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        List1 = property.FindPropertyRelative("CharList1");
        List2 = property.FindPropertyRelative("CharList2");

        GUI.Box(position, "");
        position.height = EditorGUIUtility.singleLineHeight;
        position.y += EditorGUIUtility.singleLineHeight / 8;
        position.x += EditorGUIUtility.singleLineHeight / 4 + 30;
        position.width -= EditorGUIUtility.singleLineHeight / 2 + 30;

        for (int i = 0; i < List1.arraySize; i++) {
            Rect deleteButton = new Rect(position.x - 30, position.y, 25, position.height);
            ShowDeleteButton(deleteButton, i);

            Rect entryRect = new Rect(position.x, position.y, position.width / 2, position.height);
            EditorGUI.PropertyField(entryRect, List1.GetArrayElementAtIndex(i), new GUIContent(""));
            entryRect.x += entryRect.width;
            EditorGUI.PropertyField(entryRect, List2.GetArrayElementAtIndex(i), new GUIContent(""));

            position.y += position.height + EditorGUIUtility.singleLineHeight / 8;
        }
        position.x -= 30;
        position.width += 30;
        if (GUI.Button(position, "Add new element")) {
            List1.arraySize++;
            List2.arraySize++;
        }
    }
    /// <summary>
    /// Displays a delete button that corresponds to an entry in the dictionary
    /// </summary>
    protected void ShowDeleteButton(Rect position, int deleteIndex) {
        if (GUI.Button(position, _deleteButtonContent)) {
            DeleteElementFromArray(List1, deleteIndex);
            DeleteElementFromArray(List2, deleteIndex);
        }
    }


    /// <summary>
    /// Cleanly deletes an element from a property thats an array
    /// </summary>
    /// <param name="arrayProp">The array property</param>
    /// <param name="deleteIndex">The index to delete</param>
    public static void DeleteElementFromArray(SerializedProperty arrayProp, int deleteIndex) {
        if (arrayProp.isArray) {
            SerializedProperty propToDelete = arrayProp.GetArrayElementAtIndex(deleteIndex);
            for (int k = deleteIndex; k < arrayProp.arraySize - 1; k++) {
                arrayProp.GetArrayElementAtIndex(k).isExpanded = arrayProp.GetArrayElementAtIndex(k + 1).isExpanded;
            }
            int startSize = arrayProp.arraySize;
            arrayProp.DeleteArrayElementAtIndex(deleteIndex);
            //Sometimes a single delete only deletes the value and not the actual element
            if (startSize == arrayProp.arraySize) {
                arrayProp.DeleteArrayElementAtIndex(deleteIndex);
            }
        }
    }
}