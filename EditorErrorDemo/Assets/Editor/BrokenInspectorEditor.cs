using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(BrokenInspectorObject))]
public class BrokenInspectorEditor : PropertyDrawer {
    #region NONE OF THIS MATTERS FOR THE EXAMPLE
    //Spacing ratios
    public static float SINGLE_LINE_HEIGHT { get { return EditorGUIUtility.singleLineHeight; } }
    public static float FULL_BORDER { get { return SINGLE_LINE_HEIGHT / 4f; } }
    public static float HALF_BORDER { get { return SINGLE_LINE_HEIGHT / 8f; } }
    public static float ERROR_HEIGHT { get { return (SINGLE_LINE_HEIGHT * 2) + FULL_BORDER; } }
    public static float FOLDOUT_WIDTH { get { return SINGLE_LINE_HEIGHT - HALF_BORDER; } }
    public static float BUTTON_WIDTH { get { return SINGLE_LINE_HEIGHT * 1.5f; } }
    private static readonly GUIContent _deleteButtonContent = new GUIContent("X", "Delete");
    private SerializedProperty List1;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float propHeight = 0;
        List1 = property.FindPropertyRelative("CharList1");
        propHeight += HALF_BORDER;
        propHeight += List1.arraySize * (SINGLE_LINE_HEIGHT + HALF_BORDER);
        propHeight += SINGLE_LINE_HEIGHT + HALF_BORDER;
        return propHeight;
    }
    #endregion

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        List1 = property.FindPropertyRelative("CharList1");
        #region FORMATTING (IGNORE IT)
        GUI.Box(position, "");
        position.height = SINGLE_LINE_HEIGHT;
        position.y += HALF_BORDER;
        position.x += BUTTON_WIDTH + FULL_BORDER + HALF_BORDER;
        position.width -= BUTTON_WIDTH + FULL_BORDER + FULL_BORDER;
        #endregion

        for (int i = 0; i < List1.arraySize; i++) {
            Rect deleteButton = new Rect(position.x - BUTTON_WIDTH - FULL_BORDER, position.y, BUTTON_WIDTH, position.height);
            if (GUI.Button(deleteButton, _deleteButtonContent)) {
                List1.DeleteArrayElementAtIndex(i); //This is the line that breaks things...
                return;
            }

            EditorGUI.PropertyField(position, List1.GetArrayElementAtIndex(i), new GUIContent(""));
            position.y += position.height + HALF_BORDER;
        }
        #region Add new element (IGNORE)
        position.x -= BUTTON_WIDTH + FULL_BORDER;
        position.width += BUTTON_WIDTH + FULL_BORDER;
        if (GUI.Button(position, "Add new element")) {
            List1.arraySize++;
        }
        #endregion
    }
}