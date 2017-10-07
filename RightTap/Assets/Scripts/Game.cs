using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject dockerObject;
    public GameObject mcObject;
    public GameObject barrierPrefab;

    private GameObject barrierObj;

    private Docker docker;
    private MainCharacter mainCharacter;
    private Barrier barrier;

    void Start()
    {
        Application.targetFrameRate = 30;

        this.docker = dockerObject.GetComponent<Docker>();
        this.mainCharacter = mcObject.GetComponent<MainCharacter>();

        this.barrierObj = (GameObject)Instantiate(barrierPrefab);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.mainCharacter.Number = this.docker.Number;
        }
    }
}
