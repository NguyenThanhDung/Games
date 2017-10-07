using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject dockerObject;
    public GameObject mcObject;

    private Docker docker;
    private MainCharacter mainCharacter;

    void Start()
    {
        Application.targetFrameRate = 30;

        this.docker = dockerObject.GetComponent<Docker>();
        this.mainCharacter = mcObject.GetComponent<MainCharacter>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.mainCharacter.Number = this.docker.Number;
        }
    }
}
