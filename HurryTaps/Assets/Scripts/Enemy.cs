using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TextMesh _hpText;
    public GameObject _redZone;

    private int _hp;
    private float _speed;
    private float _timeOut = 0.0f;
    private System.Action<Enemy> _destroyedHander;
    private System.Action _timeOutHandler;
    private bool _shouldSentTimeOutEvent;

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if(_hpText!=null)
            {
                _hpText.text = _hp.ToString();
            }
        }
    }

    public System.Action<Enemy> DestroyCallback
    {
        set
        {
            _destroyedHander += value;
        }
    }

    public System.Action TimeOutCallback
    {
        set
        {
            _timeOutHandler += value;
        }
    }

    public void Spawn(GameSettings gameSettings, bool isFirstEnemy)
    {
        HP = isFirstEnemy? 1 : Random.Range(1, 4);
        gameObject.SetActive(true);
        _timeOut = 0.0f;
        _speed = gameSettings.GetSpeed(HP);
        _shouldSentTimeOutEvent = true;
        Debug.Log("_speed: " + _speed);
    }

    public bool IsHit(Vector3 hitPosition)
    {
        if (hitPosition.x < (transform.position.x - transform.localScale.x / 2) ||
            hitPosition.x > (transform.position.x + transform.localScale.x / 2) ||
            hitPosition.y < (transform.position.y - transform.localScale.y / 2) ||
            hitPosition.y > (transform.position.y + transform.localScale.y / 2))
            return false;

        HP -= 1;
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            _destroyedHander(this);
        }
        return true;
    }

    public void Stop()
    {
        _speed = 0.0f;
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

        if (_timeOut >= 1.0f && _shouldSentTimeOutEvent)
        {
            _shouldSentTimeOutEvent = false;
            _timeOutHandler();
        }
    }
}
