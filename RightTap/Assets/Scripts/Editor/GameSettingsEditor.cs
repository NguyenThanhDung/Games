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

        EditorGUILayout.Foldout(true, "Main Character");
        targetSetting.NumberSpeed = EditorGUILayout.Slider("Number Speed (Loop/Second)", targetSetting.NumberSpeed, 0.1f, 5.0f);
    }
}
