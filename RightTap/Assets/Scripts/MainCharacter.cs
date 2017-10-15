using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private List<GameSettings.Level> _levels;
    private float _number;
    private float _deltaNumber;
    private TextMesh _textMesh;
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

    void Start()
    {
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
