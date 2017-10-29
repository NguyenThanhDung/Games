using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private int COUNT = 3;
    private float[] _milestones;
    private float[] _genTimeStamp;

    public GameSettings()
    {
        _milestones = new float[COUNT];
        _genTimeStamp = new float[COUNT];

        GenerateExampleData();
    }

    private void GenerateExampleData()
    {
        _milestones[0] = 0.0f;
        _genTimeStamp[0] = 3.0f;

        _milestones[1] = 3.0f;
        _genTimeStamp[1] = 2.0f;

        _milestones[2] = 6.0f;
        _genTimeStamp[2] = 1.0f;
    }

    public float GetGenerateTimeStamp(float time)
    {
        int i = 0;
        int j = 1;
        while (j < _milestones.Length && _milestones[j] < time)
        {
            i = j;
            j++;
        }

        return _genTimeStamp[i];
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
