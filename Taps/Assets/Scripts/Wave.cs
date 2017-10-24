using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wave
{
    private int _id;
    private List<Obstacle> _obstacles = new List<Obstacle>(0);
    private float _distanceUnit;
    private float _speed;
    private Action<int> _selfDestroyedCallback;
    private Action<Obstacle> _obstacleDestroyedCallback;
    private Action _reachScreenBottomCallback;

    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }

    public float NextPosition
    {
        get
        {
            return _obstacles[_obstacles.Count - 1].NextPosition;
        }
    }

    public Wave(int id, GameSettings.Template template, GameObject obstaclePrefab, float position, float distanceUnit, float speed,
        Action<int> selfDestroyedCallback, Action<Obstacle> obstacleDestroyedCallback, Action reachScreenBottomCallback)
    {
        ID = id;
        _distanceUnit = distanceUnit;
        _speed = speed;
        for (int i = 0; i < template.obstacleDatas.Count; i++)
        {
            Obstacle obstacle = MonoBehaviour.Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            if (i == 0)
                obstacle.Initialize(template.obstacleDatas[i], position, _distanceUnit, _speed);
            else
                obstacle.Initialize(template.obstacleDatas[i], _obstacles[i - 1].NextPosition, _distanceUnit, _speed);
            _obstacles.Add(obstacle);
        }

        _selfDestroyedCallback = selfDestroyedCallback;
        _obstacleDestroyedCallback = obstacleDestroyedCallback;
        _reachScreenBottomCallback = reachScreenBottomCallback;
        for (int i = 0; i < _obstacles.Count; i++)
        {
            _obstacles[i].DestroyedCallback = _obstacleDestroyedCallback + OnObstacleDestroyed;
            _obstacles[i].ReachScreenBottomCallback = _reachScreenBottomCallback;
        }
    }

    public void Regen(GameSettings.Template template, GameObject obstaclePrefab, float position)
    {
        for (int i = 0; i < template.obstacleDatas.Count; i++)
        {
            Obstacle obstacle = MonoBehaviour.Instantiate(obstaclePrefab).GetComponent<Obstacle>();
            if (i == 0)
                obstacle.Initialize(template.obstacleDatas[i], position, _distanceUnit, _speed);
            else
                obstacle.Initialize(template.obstacleDatas[i], _obstacles[i - 1].NextPosition, _distanceUnit, _speed);
            _obstacles.Add(obstacle);
        }

        for (int i = 0; i < _obstacles.Count; i++)
        {
            _obstacles[i].DestroyedCallback = _obstacleDestroyedCallback + OnObstacleDestroyed;
            _obstacles[i].ReachScreenBottomCallback = _reachScreenBottomCallback;
        }
    }

    public bool IsHit(float mcPosition)
    {
        foreach (Obstacle obstacle in _obstacles)
        {
            if(obstacle.IsHit(mcPosition))
            {
                return true;
            }
        }
        return false;
    }

    private void OnObstacleDestroyed(Obstacle destroyedObstacle)
    {
        Debug.Log("Wave[" + ID + "] OnObstacleDestroyed");
        _obstacles.Remove(destroyedObstacle);
        if (_obstacles.Count <= 0)
        {
            _selfDestroyedCallback(ID);
        }
    }

    public void OnGameOver()
    {
        Debug.Log("Wave[" + ID + "] OnGameOver");
        foreach (Obstacle obstacle in _obstacles)
        {
            obstacle.OnGameOver();
        }
    }
}
