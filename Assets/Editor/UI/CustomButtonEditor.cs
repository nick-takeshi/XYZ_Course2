using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(CustomButton), true)]
public class CustomButtonEditor : ButtonEditor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_normal"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_select"));
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
