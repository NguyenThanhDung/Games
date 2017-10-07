using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Vector3 direction;
    private int begin;
    private int end;
    private bool shouldMove;
    private Action touchCharacterHandler;

    void Start()
    {
        this.transform.position = new Vector3(0.0f, 5.5f);
        this.shouldMove = false;
    }

    public void SetParams(float speed, int minRange, int maxRange)
    {
        this.direction = new Vector3(0.0f, -speed);

        int range = UnityEngine.Random.Range(minRange, maxRange);
        this.begin = UnityEngine.Random.Range(0, 100 - range);
        this.end = begin + range;
        this.transform.GetChild(0).GetComponent<TextMesh>().text = this.begin.ToString() + " - " + this.end.ToString();
    }

    public void Move()
    {
        this.shouldMove = true;
    }

    public void SetOnTouchCharacterCallback(Action onBarrierTouchCharacter)
    {
        this.touchCharacterHandler = onBarrierTouchCharacter;
    }

    void FixedUpdate()
    {
        if (this.shouldMove)
        {
            this.transform.position += this.direction;
            if(this.transform.position.y <= -2.2f)
            {
                shouldMove = false;
                touchCharacterHandler();
            }
        }
    }
}