using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        // Obstacle
        public int ObstacleSpeed;
        public float MinRange;
        public float MaxRange;

        // Main Character
        public int NumberSpeed;
    }

    public List<Level> levels = new List<Level>(0);
}
