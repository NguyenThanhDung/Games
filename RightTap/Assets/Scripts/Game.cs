using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameObject dockerObject;
    public GameObject mcObject;
    public GameObject obstablePrefab;

    private GameObject obstacleObj;

    private Docker docker;
    private MainCharacter mainCharacter;
    private Obstacle obstable;

    private bool isGameRunning;

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        this.isGameRunning = false;

        this.docker = this.dockerObject.GetComponent<Docker>();
        this.docker.NumberSpeed = gameSettings.NumberSpeed;
        this.mainCharacter = this.mcObject.GetComponent<MainCharacter>();

        this.obstacleObj = (GameObject)Instantiate(obstablePrefab);
        this.obstable = this.obstacleObj.GetComponent<Obstacle>();
        this.obstable.SetParams(gameSettings.ObstacleSpeed, 10, 30);
        this.obstable.SetOnTouchCharacterCallback(OnObstacleTouchCharacter);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!this.isGameRunning)
            {
                this.isGameRunning = true;                
                this.obstable.Restart();
            }
            this.mainCharacter.Number = this.docker.Number;
        }
    }

    public void OnObstacleTouchCharacter(Obstacle obstacle)
    {
        if (obstacle.CanCharacterDestroy(this.mainCharacter))
            obstacle.Restart();
        else
        {
            this.isGameRunning = false;
            obstacle.Stop();
        }
    }
}
