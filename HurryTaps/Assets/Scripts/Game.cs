using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject enemyPrefab;

    private GameSettings _gameSetting;
    private Board _board;
    private float _startTime;
    private float _currentTime;
    private float _genTimeStamp;
    private float _lastGenMilestone;

    void Start()
    {
        _gameSetting = new GameSettings();
        _board = new Board(enemyPrefab);

        _startTime = 0.0f;
        _currentTime = 0.0f;
        _genTimeStamp = _gameSetting.GetGenerateTimeStamp(_currentTime);
        _lastGenMilestone = 0.0f;
        Debug.Log("_genTimeStamp: " + _genTimeStamp);
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        float newGenTimeStamp = _gameSetting.GetGenerateTimeStamp(_currentTime);
        if (newGenTimeStamp != _genTimeStamp)
        {
            _genTimeStamp = newGenTimeStamp;
            Debug.Log("_genTimeStamp: " + _genTimeStamp);
        }

        if ((_currentTime - _lastGenMilestone) > _genTimeStamp)
        {
            _board.GenerateEnemy();
            _lastGenMilestone = _currentTime;
        }
    }
}
