using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        // Duration
        public int DurationMinutes;
        public int DurationSeconds;

        // Obstacle
        public int ObstacleSpeed;
        public float MinRange;
        public float MaxRange;

        // Main Character
        public int NumberSpeed;

        public Level()
        {
            DurationMinutes = 1;
            DurationSeconds = 0;

            ObstacleSpeed = 1;
            MinRange = 80.0f;
            MaxRange = 90.0f;

            NumberSpeed = 1;
        }

        public int Duration
        {
            get
            {
                return DurationMinutes * 60 + DurationSeconds;
            }
        }
    }

    public List<Level> levels = new List<Level>(0);

    public GameSettings()
    {
        levels.Add(new Level());
    }

    public int CurrentLevel(TimeSpan playedTime)
    {
        int currentLevel = 0;
        int startTime = 0;
        for (int i = 1; i < levels.Count; i++)
        {
            startTime += levels[i - 1].Duration;
            TimeSpan startTimeSpan = TimeSpan.FromSeconds(startTime);
            if (TimeSpan.Compare(playedTime, startTimeSpan) < 0)
            {
                break;
            }
            currentLevel++;
        }
        return currentLevel;
    }
}
