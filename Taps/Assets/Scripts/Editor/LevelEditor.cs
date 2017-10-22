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

        for (int i = 0; i < levels.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Foldout(true, "Level " + (i + 1).ToString());
            if (GUILayout.Button("Remove Level", GUILayout.Width(100)))
            {
                SerializedProperty childObstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas");
                childObstacleDatas.DeleteCommand();
                levels.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas");
            EditorGUI.indentLevel++;
            for (int j = 0; j < obstacleDatas.arraySize; j++)
            {
                SerializedProperty obstacleData = obstacleDatas.GetArrayElementAtIndex(j);
                SerializedProperty obstacleData_hp = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas.Array.data[" + j + "].hp");
                SerializedProperty obstacleData_length = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas.Array.data[" + j + "].length");
                SerializedProperty obstacleData_space = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas.Array.data[" + j + "].space");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Template " + (j + 1).ToString(), GUILayout.Width(100));
                EditorGUILayout.IntField(obstacleData_hp.intValue, GUILayout.Width(50));
                EditorGUILayout.IntField(obstacleData_length.intValue, GUILayout.Width(50));
                EditorGUILayout.IntField(obstacleData_space.intValue, GUILayout.Width(50));
                EditorGUILayout.Space();
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    obstacleDatas.DeleteArrayElementAtIndex(j);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("+", GUILayout.Width(30)))
            {
                obstacleDatas.InsertArrayElementAtIndex(obstacleDatas.arraySize);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Level"))
        {
            levels.InsertArrayElementAtIndex(levels.arraySize);
            SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + (levels.arraySize - 1) + "].obstacleDatas");
            obstacleDatas.ClearArray();
            obstacleDatas.InsertArrayElementAtIndex(obstacleDatas.arraySize);
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
