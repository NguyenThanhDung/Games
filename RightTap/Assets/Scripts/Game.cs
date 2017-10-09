using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    enum GameState
    {
        INITIAL,
        RUNNING,
        STOPPED
    };

    public GameSettings _gameSettings;
    private GameState _gameState;
    public GameObject _restartButton;

    public GameObject _mcObject;
    public GameObject _obstablePrefab;
    private GameObject _obstacleObj;
    private MainCharacter _mainCharacter;
    private Obstacle _obstable;

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        _gameState = GameState.INITIAL;
        _restartButton.SetActive(false);

        _mainCharacter = _mcObject.GetComponent<MainCharacter>();
        _mainCharacter.NumberSpeed = _gameSettings.NumberSpeed;

        _obstacleObj = (GameObject)Instantiate(_obstablePrefab);
        _obstable = _obstacleObj.GetComponent<Obstacle>();
        _obstable.SetParams(_gameSettings.ObstacleSpeed, _gameSettings.MinRange, _gameSettings.MaxRange);
        _obstable.SetOnTouchCharacterCallback(OnObstacleTouchCharacter);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            switch(_gameState)
            {
                case GameState.INITIAL:
                    _gameState = GameState.RUNNING;
                    _mainCharacter.Begin();
                    _obstable.Restart();
                    break;
                case GameState.RUNNING:
                    _mainCharacter.Stop();
                    break;
                case GameState.STOPPED:
                    //Do nothing
                    break;
            }
        }
        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            switch (_gameState)
            {
                case GameState.INITIAL:
                    //Do nothing
                    break;
                case GameState.RUNNING:
                    _mainCharacter.Begin();
                    break;
                case GameState.STOPPED:
                    //Do nothing
                    break;
            }
        }
    }

    public void OnObstacleTouchCharacter(Obstacle obstacle)
    {
        if (obstacle.CanCharacterDestroy(_mainCharacter))
            obstacle.Restart();
        else
        {
            _gameState = GameState.STOPPED;
            _mainCharacter.Stop();
            obstacle.Stop();
            _restartButton.SetActive(true);
        }
    }

    public void OnRestart()
    {
        if (_gameState == GameState.STOPPED)
        {
            _restartButton.SetActive(false);
            _gameState = GameState.RUNNING;
            _mainCharacter.Begin();
            _obstable.Restart();
        }
    }
}
