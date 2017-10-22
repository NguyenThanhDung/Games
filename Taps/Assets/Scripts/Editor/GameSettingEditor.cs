using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettings))]
public class GameSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty distanceUnit = serializedObject.FindProperty("DistanceUnit");
        EditorGUILayout.PropertyField(distanceUnit);

        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Save", GUILayout.Height(40)))
        {
            AssetDatabase.SaveAssets();
        }
    }
}
