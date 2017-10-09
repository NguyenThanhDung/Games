﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GameSettings targetSetting = (GameSettings)target;

        EditorGUILayout.Foldout(true, "Obstacle");
        targetSetting.ObstacleSpeed = EditorGUILayout.IntSlider("Moving Speed", targetSetting.ObstacleSpeed, 1, 20);
        EditorGUILayout.MinMaxSlider(ref targetSetting.MinRange, ref targetSetting.MaxRange, 0, 100);
        EditorGUILayout.LabelField("Min:" + ((int)targetSetting.MinRange).ToString() + " - Max: " + ((int)targetSetting.MaxRange).ToString());

        EditorGUILayout.Foldout(true, "Main Character");
        targetSetting.NumberSpeed = EditorGUILayout.IntSlider("Number Changing Speed", targetSetting.NumberSpeed, 1, 50);

        if(GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(targetSetting);
            AssetDatabase.SaveAssets();
        }
    }
}
