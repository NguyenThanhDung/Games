using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty levels = serializedObject.FindProperty("levels");

        for (int i = 0; i < levels.arraySize; i++)
        {
            SerializedProperty level = levels.GetArrayElementAtIndex(i);

            // Header
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Foldout(true, "Level " + (i + 1).ToString());
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                levels.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();

            // Duration
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Duration");
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider(level.FindPropertyRelative("DurationMinutes"), 0, 10, "Minute");
            EditorGUILayout.IntSlider(level.FindPropertyRelative("DurationSeconds"), 0, 59, "Second");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Result");
            EditorGUILayout.LabelField(level.FindPropertyRelative("DurationMinutes").intValue.ToString() + " mins "
                + level.FindPropertyRelative("DurationSeconds").intValue.ToString() + " secs");
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            // Obstacle
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Obstacle");
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider(level.FindPropertyRelative("ObstacleSpeed"), 1, 20, "Moving Speed");

            EditorGUILayout.BeginHorizontal();
            float MinRange = level.FindPropertyRelative("MinRange").floatValue;
            float MaxRange = level.FindPropertyRelative("MaxRange").floatValue;
            EditorGUILayout.MinMaxSlider("Range", ref MinRange, ref MaxRange, 0, 100);
            level.FindPropertyRelative("MinRange").floatValue = MinRange;
            level.FindPropertyRelative("MaxRange").floatValue = MaxRange;

            EditorGUILayout.LabelField(((int)MinRange).ToString() + " - " + ((int)MaxRange).ToString(), GUILayout.Width(70));
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            // Character
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Main Character");
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider(level.FindPropertyRelative("NumberSpeed"), 1, 50, "Number Changing Speed");
            EditorGUI.indentLevel--;

            // Footer
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

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
