using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;
    public GameSettings _gameSetting;

    private MainCharacter _mainCharacter;
    private Wave[] _waves = new Wave[3];
    private int _score;
    private int _currentLevelIndex;

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

        _score = 0;
        _currentLevelIndex = 0;

        _mainCharacter = mainCharacterObject.GetComponent<MainCharacter>();

        for (int i = 0; i < _waves.Length; i++)
        {
            if (i == 0)
                _waves[i] = new Wave(i, _gameSetting.GetTemplate(_currentLevelIndex++), Camera.main.orthographicSize, _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed);
            else
                _waves[i] = new Wave(i, _gameSetting.GetTemplate(_currentLevelIndex++), _waves[i - 1].NextPosition, _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed);
            _waves[i].SelfDestroyedCallback = OnWaveIsDestroyed;
            _waves[i].ObstacleDestroyedCallback = OnObstacleIsDestroyed;
            _waves[i].ReachScreenBottomCallback = OnGameOver;
        }
    }

    void OnObstacleIsDestroyed()
    {
        _score++;
        Debug.Log("Score: " + _score);
    }

    void OnWaveIsDestroyed(int index)
    {
        int previousIndex = (index == 0) ? _waves.Length - 1 : index - 1;
        _waves[index] = new Wave(index, _gameSetting.GetTemplate(_currentLevelIndex++), _waves[previousIndex].NextPosition, _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed);
    }

    void OnGameOver()
    {
        Debug.Log("Game Over!");
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].OnGameOver();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            bool isAnyObstacleHit = false;
            foreach (Wave wave in _waves)
            {
                if (wave.IsHit(_mainCharacter.Position))
                {
                    isAnyObstacleHit = true;
                }
            }
            if (!isAnyObstacleHit)
            {
                OnGameOver();
            }
        }
    }
}
