using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;
    public GameSettings _gameSetting;

    private MainCharacter _mainCharacter;
    private Obstacle[] _obstacles;
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

        _obstacles = new Obstacle[5];
        for (int i = 0; i < _obstacles.Length; i++)
        {
            _obstacles[i] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            _obstacles[i].DistanceUnit = _gameSetting.DistanceUnit;
            _obstacles[i].Speed = _gameSetting.ObstacleSpeed;
            if (i == 0)
                _obstacles[i].Generate(i, _gameSetting.GetObstacleData(_currentLevelIndex++));
            else
                _obstacles[i].Generate(i, _gameSetting.GetObstacleData(_currentLevelIndex++), _obstacles[i - 1].NextPosition);
            _obstacles[i].DestroyedCallback = OnObstacleIsDestroyed;
            _obstacles[i].ReachScreenBottomCallback = OnGameOver;
        }
    }

    void OnObstacleIsDestroyed(int index)
    {
        int previousIndex = (index == 0) ? _obstacles.Length - 1 : index - 1;
        _obstacles[index].Generate(index, _gameSetting.GetObstacleData(_currentLevelIndex++), _obstacles[previousIndex].NextPosition);
        _score++;
        Debug.Log("Score: " + _score);
    }

    void OnGameOver()
    {
        for (int i = 0; i < _obstacles.Length; i++)
        {
            _obstacles[i].OnGameOver();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            bool isAnyObstacleHit = false;
            foreach (Obstacle obstacle in _obstacles)
            {
                if (_mainCharacter.Position > obstacle.Bottom && _mainCharacter.Position < obstacle.Top)
                {
                    isAnyObstacleHit = true;
                    obstacle.IsHit();
                }
            }
            if(!isAnyObstacleHit)
            {
                OnGameOver();
            }
        }
    }
}
