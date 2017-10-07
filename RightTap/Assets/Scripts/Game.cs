using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject Docker;
    public GameObject MainCharacter;
    private System.Random mRandomer;

    void Start()
    {
        Application.targetFrameRate = 30;
        mRandomer = new System.Random();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            int number = mRandomer.Next(0, 100);
            MainCharacter.GetComponent<MainCharacter>().Number = number;
        }
    }
}
