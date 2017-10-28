using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject enemyPrefab;

    private GameSettings _gameSetting;
    private Board _board;
    private float _currentTime;
    private float _genTimeStamp;
    private float _lastGenMilestone;
    private int _score;

    void Start()
    {
        _gameSetting = new GameSettings();
        _board = new Board(enemyPrefab, OnEnemyIsDestroyed);

        _currentTime = 0.0f;
        _genTimeStamp = _gameSetting.GetGenerateTimeStamp(_currentTime);
        _board.GenerateEnemy();
        _lastGenMilestone = 0.0f;
        _score = 0;
    }

    void OnEnemyIsDestroyed(Enemy destroyedEnemy)
    {
        _score++;
        Debug.Log("Score: " + _score);
    }

    void OnGameOver()
    {

    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        float newGenTimeStamp = _gameSetting.GetGenerateTimeStamp(_currentTime);
        if (newGenTimeStamp != _genTimeStamp)
        {
            _genTimeStamp = newGenTimeStamp;
        }

        if ((_currentTime - _lastGenMilestone) > _genTimeStamp)
        {
            _board.GenerateEnemy();
            _lastGenMilestone = _currentTime;
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 position = new Vector3();
            if(Input.GetMouseButtonDown(0))
                position = Input.mousePosition;
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                position = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(position);

            if (!_board.IsHit(ray.origin))
            {
                OnGameOver();
            }
        }
    }
}
