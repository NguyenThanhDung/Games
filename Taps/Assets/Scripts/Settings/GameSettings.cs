using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class ObstacleData
    {
        public int hp;
        public int length;
        public int space;

        public ObstacleData()
        {
            hp = 1;
            length = 3;
            space = 3;
        }
    }

    [System.Serializable]
    public class Level
    {
        public List<ObstacleData> obstacleDatas = new List<ObstacleData>(0);
    }

    public float DistanceUnit = 0.7f;
    public float ObstacleSpeed = 1.0f;
    public List<Level> levels = new List<Level>(0);
    public List<int> levelIndices = new List<int>(0);
    private int currentIndex = -1;

    public ObstacleData GetObstacleData()
    {
        currentIndex++;
        if (currentIndex >= levelIndices.Count)
            currentIndex = levelIndices.Count - 1;
        int level = levelIndices[currentIndex];
        return PickObstacle(level);
    }

    private ObstacleData PickObstacle(int level)
    {
        if (levels.Count <= 0)
            return PickDefaultObstacle();

        if (level >= levels.Count)
            level = levels.Count - 1;

        if (levels[level].obstacleDatas.Count <= 0)
            return PickDefaultObstacle();

        int index = UnityEngine.Random.Range(0, levels[level].obstacleDatas.Count);
        return levels[level].obstacleDatas[index];
    }

    private ObstacleData PickDefaultObstacle()
    {
        return new ObstacleData();
    }
}
