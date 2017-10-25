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
    public class Template
    {
        public List<ObstacleData> obstacleDatas = new List<ObstacleData>(0);
    }

    [System.Serializable]
    public class Level
    {
        public List<Template> templates = new List<Template>(0);
    }

    public class WaveData
    {
        public Template template;
        public float speed;

        public WaveData(Template template, float speed)
        {
            this.template = template;
            this.speed = speed;
        }
    }

    public float DistanceUnit = 0.7f;
    public float ObstacleSpeed = 1.0f;
    public List<Level> levels = new List<Level>(0);
    public List<int> levelIndices = new List<int>(0);
    public List<float> speeds = new List<float>(0);

    public WaveData GetWaveData(int levelIndex)
    {
        if (levelIndex >= levelIndices.Count)
            levelIndex = levelIndices.Count - 1;
        int level = levelIndices[levelIndex];
        Template template = PickTemplate(level);
        float speed = speeds[levelIndex];
        return new WaveData(template, speed);
    }

    private Template PickTemplate(int level)
    {
        if (levels.Count <= 0)
            return PickDefaultTemplate();

        if (level >= levels.Count)
            level = levels.Count - 1;

        if (levels[level].templates.Count <= 0)
            return PickDefaultTemplate();

        int index = UnityEngine.Random.Range(0, levels[level].templates.Count);
        if (levels[level].templates[index].obstacleDatas.Count <= 0)
            return PickDefaultTemplate();

        return levels[level].templates[index];
    }

    private Template PickDefaultTemplate()
    {
        Template defaultTemplate = new Template();
        defaultTemplate.obstacleDatas.Add(new ObstacleData());
        return defaultTemplate;
    }
}
