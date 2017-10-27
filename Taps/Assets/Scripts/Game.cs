﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;
    public GameSettings _gameSetting;
    public GameObject _restartButton;
    public Text _scoreText;

    private MainCharacter _mainCharacter;
    private Wave[] _waves = new Wave[2];
    private int _score;
    private int _currentLevelIndex;

    private int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            _scoreText.text = "Score: " + _score.ToString();
        }
    }

    void Start()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#else
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
#endif
        Screen.SetResolution(480, 800, false);

        _mainCharacter = mainCharacterObject.GetComponent<MainCharacter>();
        Initialize();
    }

    void Initialize()
    {
        Score = 0;
        _currentLevelIndex = 0;

        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i] = new Wave(i, _gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, (i == 0) ? Camera.main.orthographicSize : _waves[i - 1].NextPosition,
                _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed,
                OnWaveIsDestroyed, OnObstacleIsDestroyed, OnGameOver);
        }
        SetGameSpeed(_waves[0].Speed);
    }

    void SetGameSpeed(float speed)
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].SetObstacleSpeed(speed);
        }
    }

    public void Restart()
    {
        Score = 0;
        _currentLevelIndex = 0;
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].Destroy();
            _waves[i].Regen(_gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, (i == 0) ? Camera.main.orthographicSize : _waves[i - 1].NextPosition);
        }
        SetGameSpeed(_waves[0].Speed);
    }

    void OnObstacleIsDestroyed(Obstacle destroyedObstacle)
    {
        Score++;
        Debug.Log("Score: " + Score);
    }

    void OnWaveIsDestroyed(int index)
    {
        Debug.Log("OnWaveIsDestroyed(" + index + ")");
        int previousIndex = (index == 0) ? _waves.Length - 1 : index - 1;
        _waves[index].Regen(_gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, _waves[previousIndex].NextPosition);

        int nextIndex = index + 1;
        if (nextIndex >= _waves.Length)
            nextIndex = 0;
        SetGameSpeed(_waves[nextIndex].Speed);
    }

    void OnGameOver()
    {
        Debug.Log("Game Over!");
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].OnGameOver();
        }
        _restartButton.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            _mainCharacter.HighLight(true);
            bool isAnyObstacleHit = false;
            foreach (Wave wave in _waves)
            {
                if (wave.IsHit(_mainCharacter.Position))
                {
                    isAnyObstacleHit = true;
                    break;
                }
            }
            if (!isAnyObstacleHit)
            {
                OnGameOver();
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            _mainCharacter.HighLight(false);
        }
    }
}
