using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject _redZone;

    private float _timeOut;

    void Start()
    {
        _timeOut = 1.0f;

        float positionY = _timeOut / 2 - 0.5f;
        float scaleY = _timeOut;

        _redZone.transform.position = new Vector3(0.0f, positionY, -0.1f);
        _redZone.transform.localScale = new Vector3(1.0f, scaleY, 1.0f);
    }
}
