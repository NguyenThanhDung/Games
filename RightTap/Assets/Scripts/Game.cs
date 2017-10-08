using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameSettings _gameSettings;

    public GameObject _mcObject;
    public GameObject _obstablePrefab;

    private GameObject _obstacleObj;

    private MainCharacter _mainCharacter;
    private Obstacle _obstable;

    private bool _isGameRunning;

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        _isGameRunning = false;

        _mainCharacter = _mcObject.GetComponent<MainCharacter>();
        _mainCharacter.NumberSpeed = _gameSettings.NumberSpeed;

        _obstacleObj = (GameObject)Instantiate(_obstablePrefab);
        _obstable = _obstacleObj.GetComponent<Obstacle>();
        _obstable.SetParams(_gameSettings.ObstacleSpeed, 10, 30);
        _obstable.SetOnTouchCharacterCallback(OnObstacleTouchCharacter);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isGameRunning)
            {
                _mainCharacter.Stop();
            }
            else
            {
                _isGameRunning = true;
                _mainCharacter.Begin();
                _obstable.Restart();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(_isGameRunning)
            {
                _mainCharacter.Begin();
            }
        }
    }

    public void OnObstacleTouchCharacter(Obstacle obstacle)
    {
        if (obstacle.CanCharacterDestroy(_mainCharacter))
            obstacle.Restart();
        else
        {
            _isGameRunning = false;
            _mainCharacter.Stop();
            obstacle.Stop();
        }
    }
}
