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
    public GameObject _mcObject;
    public GameObject _obstablePrefab;
    public GameObject _restartButton;
    public Text _scoreText;
    public Text _timeText;
    public Text _levelText;
    public bool _showDebugInfo;

    private GameState _gameState;
    private DateTime _timer;
    private int _level;
    private int _score;

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

    public int Level
    {
        set
        {
            _level = value;
            if (_mainCharacter != null)
            {
                _mainCharacter.Level = _level;
            }
            if (_obstable != null)
            {
                _obstable.Level = _level;
            }
        }
        get
        {
            return _level;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(450, 800, false);
        _gameState = GameState.INITIAL;
        _timer = DateTime.Now;
        Level = 0;
        Score = 0;

        _restartButton.SetActive(false);
        _timeText.enabled = _showDebugInfo;
        _levelText.enabled = _showDebugInfo;

        _mainCharacter = _mcObject.GetComponent<MainCharacter>();
        _mainCharacter.Levels = _gameSettings.levels;

        _obstacleObj = (GameObject)Instantiate(_obstablePrefab);
        _obstable = _obstacleObj.GetComponent<Obstacle>();
        _obstable.Levels = _gameSettings.levels;
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
                    _timer = DateTime.Now;
                    Level = 0;
                    Score = 0;
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

        if (_gameState == GameState.RUNNING)
        {
            TimeSpan playedTime = DateTime.Now - _timer;
            int newLevel = _gameSettings.CurrentLevel(playedTime);
            if (newLevel > Level)
            {
                Level = newLevel;
            }
            if (_showDebugInfo)
            {
                _timeText.text = "Time: " + playedTime.Minutes.ToString("D2") + ":" + playedTime.Seconds.ToString("D2");
                _levelText.text = "Level: " + (Level + 1).ToString();
            }
        }

        _mainCharacter.IsHighLight = IsMCInRange(_mainCharacter, _obstable);
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
            _timer = DateTime.Now;
            Level = 0;
            Score = 0;
            _mainCharacter.Begin();
            _obstable.Restart();
        }
    }

    public bool IsMCInRange(MainCharacter mainCharacter, Obstacle obstacle)
    {
        return mainCharacter.Number >= obstacle.Begin && mainCharacter.Number <= obstacle.End;
    }
}
