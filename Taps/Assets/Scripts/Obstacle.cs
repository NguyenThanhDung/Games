using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float LengthUnit;
    public float CirclePositionLeft;
    public float CirclePositionBottom;

    public int _hp;
    public int _length = 3;
    public int _space;

    private int _id;
    
    private float OBSTACLE_WIDTH_BUFFER = 0.5f;
    private float CIRCLE_SCALE = 0.6f;
    private float _width;
    private float _height;
    private TextMesh _hpText;
    private float _speed = 1.0f;

    private Action<int> onDestroyedHandler;
    private Action onReachScreenBottomHandler;

    private int HP
    {
        set
        {
            _hp = value;

            if (_hpText == null)
            {
                _hpText = transform.GetChild(1).GetComponent<TextMesh>();
            }
            _hpText.text = _hp.ToString();
        }
        get
        {
            return _hp;
        }
    }

    private int Space
    {
        set
        {
            _space = value;
        }
        get
        {
            return _space;
        }
    }

    public float NextPosition
    {
        get
        {
            return transform.position.y + _height / 2 + _space;
        }
    }

    public float Top
    {
        get
        {
            return transform.position.y + _height / 2;
        }
    }

    public float Bottom
    {
        get
        {
            return transform.position.y - _height / 2;
        }
    }

    public void Generate(GameSettings.ObstacleData data, float position = 0.0f)
    {
        HP = data.hp;
        _length = data.length;
        _space = data.space;
        Transform(position);
    }

    private void Transform(float position)
    {
        _width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        _height = _length * LengthUnit;

        transform.position = new Vector3(0.0f, position, 0.0f);
        transform.localScale = new Vector3(_width + OBSTACLE_WIDTH_BUFFER, _height, 1.0f);

        UpdateChildrenPosition();
        ScaleChildren();
    }

    private void Move(float distance)
    {
        Vector3 position = transform.position;
        position.y -= distance;
        transform.position = position;
        UpdateChildrenPosition();
    }

    private void ScaleChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 childScale = new Vector3(CIRCLE_SCALE / transform.localScale.x, CIRCLE_SCALE / transform.localScale.y, 1.0f);
            transform.GetChild(i).transform.localScale = childScale;
        }
    }

    private void UpdateChildrenPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 childPosition;
            if (_length > 1)
                childPosition = new Vector3(transform.position.x - _width / 2 + CirclePositionLeft, transform.position.y - _height / 2 + CirclePositionBottom, -0.1f * (i + 1));
            else
                childPosition = new Vector3(transform.position.x - _width / 2 + CirclePositionLeft, transform.position.y, -0.1f * (i + 1));
            transform.GetChild(i).transform.position = childPosition;
        }
    }

    public void IsHit()
    {
        HP -= 1;
    }

    void Update()
    {
        float distance = _speed * Time.deltaTime;
        Move(distance);
    }
}
