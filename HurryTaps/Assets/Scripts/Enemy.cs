using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject _redZone;

    private float _speed = 0.5f;
    private float _timeOut = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        float deltaTimeOut = _speed * Time.deltaTime;
        _timeOut += deltaTimeOut;
        if (_timeOut > 1.0f)
            _timeOut = 1.0f;

        float positionX = transform.position.x;
        float positionY = transform.position.y - transform.localScale.y / 2 + _timeOut * transform.localScale.y / 2;
        float scaleY = _timeOut;

        _redZone.transform.position = new Vector3(positionX, positionY, -0.1f);
        _redZone.transform.localScale = new Vector3(1.0f, scaleY, 1.0f);
    }
}
