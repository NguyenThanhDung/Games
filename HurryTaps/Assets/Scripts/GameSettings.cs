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

        _milestones[1] = 10.0f;
        _genTimeStamp[1] = 2.0f;

        _milestones[2] = 20.0f;
        _genTimeStamp[2] = 1.0f;
    }

    public float GetGenerateTimeStamp(float time)
    {
        int i = 0;
        int j = 1;
        while(_milestones[j] < time)
        {
            i = j;
            j++;
        }

        return _genTimeStamp[i];
    }
}
