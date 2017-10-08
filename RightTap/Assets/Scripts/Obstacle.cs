using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector3 direction;
    private int minRange;
    private int maxRange;
    private int begin;
    private int end;
    private bool shouldMove;
    private Action<Obstacle> touchCharacterHandler;
    private bool shouldHandleCallback;

    void Start()
    {
        this.transform.position = new Vector3(0.0f, 5.5f);
        this.shouldMove = false;
    }

    public void SetParams(int speed, int minRange, int maxRange)
    {
        this.direction = new Vector3(0.0f, -speed / 100.0f);
        this.minRange = minRange;
        this.maxRange = maxRange;
    }

    private void RefreshRange()
    {
        int range = UnityEngine.Random.Range(this.minRange, this.maxRange);
        this.begin = UnityEngine.Random.Range(0, 100 - range);
        this.end = begin + range;
        this.transform.GetChild(0).GetComponent<TextMesh>().text = this.begin.ToString() + " - " + this.end.ToString();
    }

    public void SetOnTouchCharacterCallback(Action<Obstacle> onObstacleTouchCharacter)
    {
        this.touchCharacterHandler = onObstacleTouchCharacter;
        this.shouldHandleCallback = true;
    }

    public void Move()
    {
        this.shouldMove = true;
    }

    public void Stop()
    {
        this.shouldMove = false;
    }

    public bool CanCharacterDestroy(MainCharacter characeter)
    {
        if (characeter.Number >= this.begin && characeter.Number <= this.end)
            return true;
        return false;
    }

    public void Restart()
    {
        this.transform.position = new Vector3(0.0f, 5.5f);
        RefreshRange();
        this.shouldHandleCallback = true;
        this.shouldMove = true;
    }

    void FixedUpdate()
    {
        if (this.shouldMove)
        {
            this.transform.position += this.direction;
            if(this.transform.position.y <= -2.2f)
            {
                if (this.touchCharacterHandler != null && this.shouldHandleCallback)
                {
                    this.shouldHandleCallback = false;
                    this.touchCharacterHandler(this);
                }
            }
        }
    }
}