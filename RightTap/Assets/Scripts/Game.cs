using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    private int _score;

    public GameObject _restartButton;
    public Text _scoreText;
    public GameObject _mcObject;
    public GameObject _obstablePrefab;
    private GameObject _obstacleObj;
    private MainCharacter _mainCharacter;
    private Obstacle _obstable;

    public int Score
    {
        set
        {
            _score = value;
            _scoreText.text = "Score: " + _score.ToString();
        }
        get
        {
            return _score;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        _gameState = GameState.INITIAL;
        Score = 0;

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
        {
            Score++;
            obstacle.Restart();
        }
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
            Score = 0;
            _mainCharacter.Begin();
            _obstable.Restart();
        }
    }
}
