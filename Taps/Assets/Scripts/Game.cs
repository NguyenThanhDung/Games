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
                _waves[i] = new Wave(i, _gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, Camera.main.orthographicSize, 
                    _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed,
                    OnWaveIsDestroyed, OnObstacleIsDestroyed, OnGameOver);
            else
                _waves[i] = new Wave(i, _gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, _waves[i - 1].NextPosition, 
                    _gameSetting.DistanceUnit, _gameSetting.ObstacleSpeed,
                    OnWaveIsDestroyed, OnObstacleIsDestroyed, OnGameOver);
        }
    }

    void OnObstacleIsDestroyed(Obstacle destroyedObstacle)
    {
        _score++;
        Debug.Log("Score: " + _score);
    }

    void OnWaveIsDestroyed(int index)
    {
        Debug.Log("OnWaveIsDestroyed(" + index + ")");
        int previousIndex = (index == 0) ? _waves.Length - 1 : index - 1;
        _waves[index].Regen(_gameSetting.GetWaveData(_currentLevelIndex++), obstaclePrefab, _waves[previousIndex].NextPosition);
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
                    break;
                }
            }
            if (!isAnyObstacleHit)
            {
                OnGameOver();
            }
        }
    }
}
