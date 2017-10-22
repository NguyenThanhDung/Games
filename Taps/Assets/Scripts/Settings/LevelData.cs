using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class ObstacleData
    {
        public int hp;
        public int length;
        public int space;
    }

    [System.Serializable]
    public class Level
    {
        public List<ObstacleData> obstacleDatas = new List<ObstacleData>(0);
    }

    public List<Level> levels = new List<Level>(0);
}
