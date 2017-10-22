using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    public float DistanceUnit = 0.7f;
    public float ObstacleSpeed = 1.0f;
    public LevelData levelData = null;
    public List<int> levelIndices = new List<int>(0);
    private int currentIndex = -1;

    public LevelData.ObstacleData GetObstacleData()
    {
        if (levelData != null)
        {
            currentIndex++;
            if (currentIndex >= levelIndices.Count)
                currentIndex = levelIndices.Count - 1;
            int level = levelIndices[currentIndex];
            return levelData.PickObstacle(level);
        }
        else
        {
            return new LevelData.ObstacleData();
        }
    }
}
