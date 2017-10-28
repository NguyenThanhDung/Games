using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TextMesh _hpText;
    public GameObject _redZone;

    private int _hp;
    private float _speed = 0.5f;
    private float _timeOut = 0.0f;

    public int HP
    {
        set
        {
            _hp = value;
            if(_hpText!=null)
            {
                _hpText.text = _hp.ToString();
            }
        }
    }

    public void Hit(Vector3 hitPosition)
    {
        if (hitPosition.x < (transform.position.x - transform.localScale.x / 2) ||
            hitPosition.x > (transform.position.x + transform.localScale.x / 2) ||
            hitPosition.y < (transform.position.y - transform.localScale.y / 2) ||
            hitPosition.y > (transform.position.y + transform.localScale.y / 2))
            return;

        Debug.Log("Hit");
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
