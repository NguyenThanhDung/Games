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

    public List<Level> levels = new List<Level>(0);

    public ObstacleData PickDefaultObstacle()
    {
        return new ObstacleData();
    }

    public ObstacleData PickObstacle(int level)
    {
        if (levels.Count <= 0)
            return PickDefaultObstacle();

        if (level >= levels.Count)
            level = levels.Count - 1;

        if (levels[level].obstacleDatas.Count <= 0)
            return PickDefaultObstacle();

        int index = Random.Range(0, levels[level].obstacleDatas.Count);
        return levels[level].obstacleDatas[index];
    }
}
