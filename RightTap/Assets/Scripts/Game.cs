using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameObject dockerObject;
    public GameObject mcObject;
    public GameObject barrierPrefab;

    private GameObject barrierObj;

    private Docker docker;
    private MainCharacter mainCharacter;
    private Barrier barrier;

    private bool isGameRunning;

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        this.isGameRunning = false;

        this.docker = this.dockerObject.GetComponent<Docker>();
        this.docker.NumberSpeed = gameSettings.NumberSpeed;
        this.mainCharacter = this.mcObject.GetComponent<MainCharacter>();

        this.barrierObj = (GameObject)Instantiate(barrierPrefab);
        this.barrier = this.barrierObj.GetComponent<Barrier>();
        this.barrier.SetParams(0.05f, 10, 30);
        this.barrier.SetOnTouchCharacterCallback(OnBarrierTouchCharacter);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!this.isGameRunning)
            {
                this.isGameRunning = true;
                this.barrier.Restart();
            }
            this.mainCharacter.Number = this.docker.Number;
        }
    }

    public void OnBarrierTouchCharacter(Barrier barrier)
    {
        if (barrier.CanCharacterDestroy(this.mainCharacter))
            barrier.Restart();
        else
        {
            this.isGameRunning = false;
            barrier.Stop();
        }
    }
}
