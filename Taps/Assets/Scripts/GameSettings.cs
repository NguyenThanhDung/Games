using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public int param;

        public Level()
        {
            param = 0;
        }
    }

    public List<Level> levels = new List<Level>(0);

    public GameSettings()
    {
        levels.Add(new Level());
    }
}
