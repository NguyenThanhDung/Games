﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Color NormalColor = new Color(255, 255, 255);
    public Color HighLightColor = new Color(255, 248, 0);

    private List<GameSettings.Level> _levels;
    private float _number;
    private float _deltaNumber;
    private TextMesh _textMesh;
    private bool _isInRange;
    private SpriteRenderer _spriteRender;
    private bool _isRunning;

    public List<GameSettings.Level> Levels
    {
        set
        {
            _levels = value;
        }
    }

    public int Number
    {
        get
        {
            return (int)_number;
        }
    }

    public int Level
    {
        set
        {
            float speed = (float)_levels[value].NumberSpeed;
            float totalNumberPerSecond = speed / 10 * 100;
            float fps = 1 / Time.fixedDeltaTime;
            _deltaNumber = totalNumberPerSecond / fps;
        }
    }

    public bool IsInRange
    {
        set
        {
            if (_isInRange != value)
            {
                _isInRange = value;
            }
        }
    }

    void Start()
    {
        _textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        _textMesh.text = _number.ToString();
        _isInRange = false;
        _spriteRender = GetComponent<SpriteRenderer>();
        _spriteRender.color = NormalColor;
        _isRunning = false;
    }

    public void Begin()
    {
        _isRunning = true;
    }

    public void Pause()
    {
        _isRunning = false;
    }

    public void Stop()
    {
        _isRunning = false;
    }

    void FixedUpdate()
    {
        if (_isRunning)
        {
            _number += _deltaNumber;
            if (_number > 100.0f)
            {
                _number = 0.0f;
            }
            _textMesh.text = ((int)_number).ToString();
            _spriteRender.color = NormalColor;
        }
        else
        {
            _spriteRender.color = _isInRange ? HighLightColor : NormalColor;
        }
    }
}
