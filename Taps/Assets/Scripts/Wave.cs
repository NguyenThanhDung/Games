using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wave : MonoBehaviour
{
    private int _id;
    private List<Obstacle> _obstacles = new List<Obstacle>(0);
    private float _distanceUnit;
    private float _speed;
    private Action<int> _selfDestroyedCallback;
    private Action _obstacleDestroyedCallback;
    private Action _reachScreenBottomCallback;

    public float NextPosition
    {
        get
        {
            return _obstacles[_obstacles.Count - 1].NextPosition;
        }
    }

    public Action<int> SelfDestroyedCallback
    {
        set
        {
            _selfDestroyedCallback += value;
        }
    }

    public Action ObstacleDestroyedCallback
    {
        set
        {
            _obstacleDestroyedCallback += value;
        }
    }

    public Action ReachScreenBottomCallback
    {
        set
        {
            _reachScreenBottomCallback += value;
        }
    }

    public Wave(int id, GameSettings.Template template, float position, float distanceUnit, float speed)
    {
        _id = id;
        for (int i = 0; i < template.obstacleDatas.Count; i++)
        {
            if (i == 0)
                _obstacles.Add(new Obstacle(template.obstacleDatas[i], position, distanceUnit, speed));
            else
                _obstacles.Add(new Obstacle(template.obstacleDatas[i], _obstacles[i - 1].NextPosition, distanceUnit, speed));
        }
    }

    public bool IsHit(float mcPosition)
    {
        bool isAnyObstacleHit = false;
        foreach (Obstacle obstacle in _obstacles)
        {
            if (obstacle.IsHit(mcPosition))
            {
                isAnyObstacleHit = true;
            }
        }
        return isAnyObstacleHit;
    }

    public void OnGameOver()
    {
        foreach (Obstacle obstacle in _obstacles)
        {
            obstacle.OnGameOver();
        }
    }
}
