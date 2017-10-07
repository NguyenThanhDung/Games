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

        this.docker = this.dockerObject.GetComponent<Docker>();
        this.mainCharacter = this.mcObject.GetComponent<MainCharacter>();

        this.barrierObj = (GameObject)Instantiate(barrierPrefab);
        this.barrier = this.barrierObj.GetComponent<Barrier>();
        this.barrier.SetParams(0.05f, 10, 20);
        this.barrier.SetOnTouchCharacterCallback(OnBarrierTouchCharacter);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.mainCharacter.Number = this.docker.Number;
        }
        if(Input.GetMouseButtonDown(1))
        {
            this.barrier.Move();
        }
    }

    public void OnBarrierTouchCharacter(Barrier barrier)
    {
        if (barrier.CanCharacterDestroy(this.mainCharacter))
            barrier.Destroy();
        else
            barrier.Stop();
    }
}
