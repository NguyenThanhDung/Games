using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty levels = serializedObject.FindProperty("levels");

        if (GUILayout.Button("Add"))
        {
            levels.InsertArrayElementAtIndex(levels.arraySize);
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Save", GUILayout.Height(40)))
        {
            AssetDatabase.SaveAssets();
        }
    }
}
