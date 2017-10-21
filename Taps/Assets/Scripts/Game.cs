using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;

    private GameSettings _gameSetting;
    private int _currentLevel;
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
        _currentLevel = 0;

        _mainCharacter = mainCharacterObject.GetComponent<MainCharacter>();

        _obstacle = new Obstacle[5];
        for (int i = 0; i < _obstacle.Length; i++)
        {
            _obstacle[i] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            if (i == 0)
                _obstacle[i].Generate(_gameSetting.GetObstacleData(_currentLevel));
            else
                _obstacle[i].Generate(_gameSetting.GetObstacleData(_currentLevel), _obstacle[i - 1].NextPosition);
        }
    }

    void OnObstacleIsDestroyed()
    {
        Debug.Log("Obstacle is detroyed");
        //TODO:
        // - Increase score
        // - Regen
    }

    void OnObstacleReachScreenBottom()
    {
        Debug.Log("Obstacle reach screen bottom");
        //TODO: Stop game
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("MC Position: " + _mainCharacter.Position);
            foreach (Obstacle obstacle in _obstacle)
            {
                Debug.Log("Obstacle: " + obstacle.Bottom + "~" + obstacle.Top);
                if (_mainCharacter.Position > obstacle.Bottom && _mainCharacter.Position < obstacle.Top)
                {
                    obstacle.IsHit();
                }
            }
        }
    }
}
