using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;

    private GameSettings _gameSetting;
    private MainCharacter _mainCharacter;
    private Obstacle[] _obstacles;

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

        _gameSetting = new GameSettings();

        _mainCharacter = mainCharacterObject.GetComponent<MainCharacter>();

        _obstacles = new Obstacle[5];
        for (int i = 0; i < _obstacles.Length; i++)
        {
            _obstacles[i] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            if (i == 0)
                _obstacles[i].Generate(i, _gameSetting.GetObstacleData());
            else
                _obstacles[i].Generate(i, _gameSetting.GetObstacleData(), _obstacles[i - 1].NextPosition);
            _obstacles[i].DestroyedCallback = OnObstacleIsDestroyed;
            _obstacles[i].ReachScreenBottomCallback = OnGameOver;
        }
    }

    void OnObstacleIsDestroyed(int index)
    {
        Debug.Log("Obstacle[" + index + "] is detroyed");
        int previousIndex = (index == 0) ? _obstacles.Length - 1 : index - 1;
        _obstacles[index].Generate(index, _gameSetting.GetObstacleData(), _obstacles[previousIndex].NextPosition);
        //TODO: Increase score
    }

    void OnGameOver()
    {
        Debug.Log("GameOver");
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
