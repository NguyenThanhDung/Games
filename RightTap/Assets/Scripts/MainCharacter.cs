﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private float _number;
    private float _deltaNumber;
    private TextMesh _textMesh;
    private bool _isRunning;

    public int Number
    {
        get
        {
            return (int)_number;
        }
    }

    public float NumberSpeed
    {
        set
        {
            float totalNumberPerSecond = value / 10 * 100;
            float fps = 1 / Time.fixedDeltaTime;
            _deltaNumber = totalNumberPerSecond / fps;
        }
    }

    void Start()
    {
        _number = 0.0f;
        _textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        _textMesh.text = _number.ToString();
        _isRunning = false;
    }

    public void Begin()
    {
        _isRunning = true;
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
        }
    }
}
