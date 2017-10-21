using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;

    private GameSettings _gameSetting;
    private MainCharacter _mainCharacter;
    private Obstacle[] _obstacle;

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

        _obstacle = new Obstacle[5];
        for (int i = 0; i < _obstacle.Length; i++)
        {
            _obstacle[i] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            if (i == 0)
                _obstacle[i].Generate(i, _gameSetting.GetObstacleData());
            else
                _obstacle[i].Generate(i, _gameSetting.GetObstacleData(), _obstacle[i - 1].NextPosition);
            _obstacle[i].DestroyedCallback = OnObstacleIsDestroyed;
            _obstacle[i].ReachScreenBottomCallback = OnGameOver;
        }
    }

    void OnObstacleIsDestroyed(int index)
    {
        Debug.Log("Obstacle[" + index + "] is detroyed");
        int previousIndex = (index == 0) ? _obstacle.Length - 1 : index - 1;
        _obstacle[index].Generate(index, _gameSetting.GetObstacleData(), _obstacle[previousIndex].NextPosition);
        //TODO:
        // - Increase score
    }

    void OnGameOver()
    {
        Debug.Log("GameOver");
        //TODO: Stop game
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            bool isAnyObstacleHit = false;
            foreach (Obstacle obstacle in _obstacle)
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
