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

        public ObstacleData(int hp, int length, int space)
        {
            this.hp = hp;
            this.length = length;
            this.space = space;
        }
    }

    [System.Serializable]
    public class Level
    {
        public List<ObstacleData> obstacleDatas;

        public Level()
        {
            obstacleDatas = new List<ObstacleData>(0);
        }
    }

    public List<Level> levels = new List<Level>(0);
    public int[] levelIndices;

    public GameSettings()
    {
        Level level0 = new Level();
        level0.obstacleDatas.Add(new ObstacleData(1, 1, 1));
        level0.obstacleDatas.Add(new ObstacleData(1, 1, 1));
        level0.obstacleDatas.Add(new ObstacleData(1, 1, 1));

        Level level1 = new Level();
        level1.obstacleDatas.Add(new ObstacleData(2, 2, 2));
        level1.obstacleDatas.Add(new ObstacleData(2, 2, 2));
        level1.obstacleDatas.Add(new ObstacleData(2, 2, 2));

        Level level2 = new Level();
        level2.obstacleDatas.Add(new ObstacleData(3, 3, 3));
        level2.obstacleDatas.Add(new ObstacleData(3, 3, 3));
        level2.obstacleDatas.Add(new ObstacleData(3, 3, 3));

        levels.Add(level0);
        levels.Add(level1);
        levels.Add(level2);

        levelIndices = new int[11] { 0, 0, 0, 1, 1, 1, 2, 2, 3, 4, 5 };
    }

    public ObstacleData GetObstacleData(int currentLevel)
    {
        //TODO: Pick random data
        return new ObstacleData(1, 1, 1);
    }
}
