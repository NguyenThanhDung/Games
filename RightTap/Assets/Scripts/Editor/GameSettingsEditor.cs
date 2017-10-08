using System.Collections;
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
        targetSetting.BarrierSpeed = EditorGUILayout.IntSlider("Moving Speed", targetSetting.BarrierSpeed, 1, 20);

        EditorGUILayout.Foldout(true, "Main Character");
        targetSetting.NumberSpeed = EditorGUILayout.IntSlider("Number Changing Speed", targetSetting.NumberSpeed, 1, 50);
    }
}
