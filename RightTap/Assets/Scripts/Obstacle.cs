using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private List<GameSettings.Level> _levels;
    private Vector3 _direction;
    private int _minRange;
    private int _maxRange;
    private int _begin;
    private int _end;
    private bool _shouldMove;
    private Action<Obstacle> _touchCharacterHandler;
    private bool _shouldHandleCallback;

    public List<GameSettings.Level> Levels
    {
        set
        {
            _levels = value;
        }
    }

    public int Level
    {
        set
        {
            int speed = _levels[value].ObstacleSpeed;
            _direction = new Vector3(0.0f, -speed / 100.0f);

            _minRange = (int)_levels[value].MinRange;
            _maxRange = (int)_levels[value].MaxRange;
        }
    }

    public int Begin
    {
        get
        {
            return _begin;
        }
    }

    public int End
    {
        get
        {
            return _end;
        }
    }

    void Start()
    {
        transform.position = new Vector3(0.0f, 5.5f);
        _shouldMove = false;
    }

    private void RefreshRange()
    {
        int range = UnityEngine.Random.Range(_minRange, _maxRange);
        this._begin = UnityEngine.Random.Range(0, 100 - range);
        this._end = _begin + range;
        this.transform.GetChild(0).GetComponent<TextMesh>().text = _begin.ToString() + " - " + this._end.ToString();
    }

    public void SetOnTouchCharacterCallback(Action<Obstacle> onObstacleTouchCharacter)
    {
        this._touchCharacterHandler = onObstacleTouchCharacter;
        this._shouldHandleCallback = true;
    }

    public void Move()
    {
        this._shouldMove = true;
    }

    public void Stop()
    {
        this._shouldMove = false;
    }

    public bool CanCharacterDestroy(MainCharacter characeter)
    {
        if (characeter.Number >= this._begin && characeter.Number <= this._end)
            return true;
        return false;
    }

    public void Restart()
    {
        this.transform.position = new Vector3(0.0f, 5.5f);
        RefreshRange();
        this._shouldHandleCallback = true;
        this._shouldMove = true;
    }

    void FixedUpdate()
    {
        if (this._shouldMove)
        {
            this.transform.position += this._direction;
            if(this.transform.position.y <= -2.2f)
            {
                if (this._touchCharacterHandler != null && this._shouldHandleCallback)
                {
                    this._shouldHandleCallback = false;
                    this._touchCharacterHandler(this);
                }
            }
        }
    }
}