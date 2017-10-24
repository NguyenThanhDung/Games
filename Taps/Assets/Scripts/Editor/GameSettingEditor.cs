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

        SerializedProperty obstacleSpeed = serializedObject.FindProperty("ObstacleSpeed");
        EditorGUILayout.PropertyField(obstacleSpeed);

        // LEVELS
        SerializedProperty levels = serializedObject.FindProperty("levels");
        EditorGUILayout.Space();
        EditorGUILayout.Foldout(true, "Level Data");
        EditorGUI.indentLevel++;
        for (int i = 0; i < levels.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Foldout(true, "Level " + i.ToString());
            if (GUILayout.Button("Remove Level", GUILayout.Width(150)))
            {
                SerializedProperty childObstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i + "].obstacleDatas");
                childObstacleDatas.DeleteCommand();
                levels.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            SerializedProperty templates = serializedObject.FindProperty("levels.Array.data[" + i + "].templates");
            EditorGUI.indentLevel++;
            for (int j = 0; j < templates.arraySize; j++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Foldout(true, "Template " + j.ToString());
                if (GUILayout.Button("Remove Template", GUILayout.Width(120)))
                {
                    SerializedProperty childObstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i + "].templates.Array.data[" + j + "].obstacleDatas");
                    childObstacleDatas.DeleteCommand();
                    templates.DeleteArrayElementAtIndex(j);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i + "].templates.Array.data[" + j + "].obstacleDatas");
                EditorGUI.indentLevel++;
                for (int k = 0; k < obstacleDatas.arraySize; k++)
                {
                    SerializedProperty obstacleData_hp = serializedObject.FindProperty("levels.Array.data[" + i + "].templates.Array.data[" + j + "].obstacleDatas.Array.data[" + k + "].hp");
                    SerializedProperty obstacleData_length = serializedObject.FindProperty("levels.Array.data[" + i + "].templates.Array.data[" + j + "].obstacleDatas.Array.data[" + k + "].length");
                    SerializedProperty obstacleData_space = serializedObject.FindProperty("levels.Array.data[" + i + "].templates.Array.data[" + j + "].obstacleDatas.Array.data[" + k + "].space");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Obstacle " + k.ToString(), GUILayout.Width(120));
                    EditorGUILayout.PropertyField(obstacleData_hp, GUIContent.none, GUILayout.Width(80));
                    EditorGUILayout.PropertyField(obstacleData_length, GUIContent.none, GUILayout.Width(80));
                    EditorGUILayout.PropertyField(obstacleData_space, GUIContent.none, GUILayout.Width(80));
                    EditorGUILayout.Space();
                    if (GUILayout.Button("-", GUILayout.Width(30)))
                    {
                        obstacleDatas.DeleteArrayElementAtIndex(k);
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
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Add Template", GUILayout.Width(120)))
            {
                templates.InsertArrayElementAtIndex(templates.arraySize);
                SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + i.ToString() + "].templates.Array.data[" + (templates.arraySize - 1) + "].obstacleDatas");
                obstacleDatas.ClearArray();
                obstacleDatas.InsertArrayElementAtIndex(obstacleDatas.arraySize);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Level", GUILayout.Width(150)))
        {
            levels.InsertArrayElementAtIndex(levels.arraySize);

            SerializedProperty templates = serializedObject.FindProperty("levels.Array.data[" + (levels.arraySize - 1) + "].templates");
            templates.ClearArray();
            templates.InsertArrayElementAtIndex(templates.arraySize);

            SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + (levels.arraySize - 1) + "].templates.Array.data[" + (templates.arraySize - 1) + "].obstacleDatas");
            obstacleDatas.ClearArray();
            obstacleDatas.InsertArrayElementAtIndex(obstacleDatas.arraySize);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;

        // LEVEL INDICES
        SerializedProperty levelIndices = serializedObject.FindProperty("levelIndices");
        GUIContent[] popupContent = new GUIContent[levels.arraySize];
        int[] popupIndices = new int[levels.arraySize];
        for (int i = 0; i < popupIndices.Length; i++)
        {
            popupContent[i] = new GUIContent("Level " + i.ToString());
            popupIndices[i] = i;
        }
        SerializedProperty speeds = serializedObject.FindProperty("speeds");

        EditorGUILayout.Space();
        EditorGUILayout.Foldout(true, "Level Indices");
        EditorGUI.indentLevel++;
        for (int i = 0; i < levelIndices.arraySize; i++)
        {
            SerializedProperty obstacleData = levelIndices.GetArrayElementAtIndex(i);
            SerializedProperty speed = speeds.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();
            GUIContent popupLabel = new GUIContent("Wave " + ((i >= levelIndices.arraySize - 1) ? (">=" + i.ToString()) : i.ToString()));
            EditorGUILayout.IntPopup(obstacleData, popupContent, popupIndices, popupLabel);
            EditorGUILayout.PropertyField(speed, GUIContent.none, GUILayout.Width(80));
            EditorGUILayout.Space();
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                levelIndices.DeleteArrayElementAtIndex(i);
                speeds.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            levelIndices.InsertArrayElementAtIndex(levelIndices.arraySize);
            speeds.InsertArrayElementAtIndex(speeds.arraySize);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Save", GUILayout.Height(40)))
        {
            AssetDatabase.SaveAssets();
        }
    }
}
