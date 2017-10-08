using UnityEngine;
using UnityEditor;

public class GameSettingsAsset
{
    [MenuItem("RightTap/Create Game Settings")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<GameSettings>();
    }
}