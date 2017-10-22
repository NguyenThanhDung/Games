﻿using System.Collections;
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
                EditorGUILayout.PropertyField(obstacleData_hp, GUIContent.none, GUILayout.Width(70));
                EditorGUILayout.PropertyField(obstacleData_length, GUIContent.none, GUILayout.Width(70));
                EditorGUILayout.PropertyField(obstacleData_space, GUIContent.none, GUILayout.Width(70));
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
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Level", GUILayout.Width(100)))
        {
            levels.InsertArrayElementAtIndex(levels.arraySize);
            SerializedProperty obstacleDatas = serializedObject.FindProperty("levels.Array.data[" + (levels.arraySize - 1) + "].obstacleDatas");
            obstacleDatas.ClearArray();
            obstacleDatas.InsertArrayElementAtIndex(obstacleDatas.arraySize);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;

        // LEVEL INDICES
        SerializedProperty levelIndices = serializedObject.FindProperty("levelIndices");
        EditorGUILayout.Space();
        EditorGUILayout.Foldout(true, "Level Indices");
        EditorGUI.indentLevel++;
        for (int i = 0; i < levelIndices.arraySize; i++)
        {
            SerializedProperty obstacleData = levelIndices.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(obstacleData);
            EditorGUILayout.Space();
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                obstacleData.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            levelIndices.InsertArrayElementAtIndex(levelIndices.arraySize);
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