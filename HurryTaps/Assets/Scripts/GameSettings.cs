using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private int COUNT = 3;
    private float[] _milestones;
    private float[] _genInterval;

    public GameSettings()
    {
        _milestones = new float[COUNT];
        _genInterval = new float[COUNT];

        _milestones[0] = 0.0f;
        _genInterval[0] = 1.5f;

        _milestones[1] = 5.0f;
        _genInterval[1] = 1.0f;

        _milestones[2] = 10.0f;
        _genInterval[2] = 0.5f;
    }

    public float GetGenerateInterval(float time)
    {
        int i = 0;
        int j = 1;
        while (j < _milestones.Length && _milestones[j] < time)
        {
            i = j;
            j++;
        }

        return _genInterval[i];
    }

    public float GetSpeed(int hp)
    {
        switch(hp)
        {
            case 1:
                return Random.Range(0.2f, 0.4f);
            case 2:
                return Random.Range(0.4f, 0.6f);
            case 3:
                return Random.Range(0.6f, 0.8f);
            case 4:
                return Random.Range(0.8f, 1.0f);
            default:
                return Random.Range(10.0f, 20.0f);
        }
    }
}
