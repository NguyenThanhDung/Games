using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float CirclePositionLeft;
    public float CirclePositionBottom;
    public Color ColorOfHP1;
    public Color ColorOfHP2;
    public Color ColorOfHP3;
    public Color ColorOfHP4;

    private int _hp;
    private int _length;
    private int _space;
    
    private float OBSTACLE_WIDTH_BUFFER = 0.5f;
    private float CIRCLE_SCALE = 0.6f;
    private float _width;
    private float _height;
    private float _distanceUnit = 0.7f;
    private float _speed;
    private TextMesh _hpText;
    private bool _isRunning;
    private SpriteRenderer _spriteRenderer;

    private Action<Obstacle> onDestroyedHandler;
    private Action onReachScreenBottomHandler;
    private bool shouldTriggerReachScreenBottomHandler;

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

            if (_spriteRenderer != null)
            {
                switch (_hp)
                {
                    case 1:
                        _spriteRenderer.color = ColorOfHP1;
                        break;
                    case 2:
                        _spriteRenderer.color = ColorOfHP2;
                        break;
                    case 3:
                        _spriteRenderer.color = ColorOfHP3;
                        break;
                    default:
                        _spriteRenderer.color = ColorOfHP4;
                        break;
                }
            }
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
            return Top + _space * _distanceUnit;
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

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public Action<Obstacle> DestroyedCallback
    {
        set
        {
            onDestroyedHandler += value;
        }
    }

    public Action ReachScreenBottomCallback
    {
        set
        {
            onReachScreenBottomHandler += value;
            shouldTriggerReachScreenBottomHandler = true;
        }
    }

    public void Initialize(GameSettings.ObstacleData data, float position, float distanceUnit)
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        HP = data.hp;
        _length = data.length;
        _space = data.space;

        _distanceUnit = distanceUnit;
        _isRunning = true;

        Transform(position);
    }

    private void Transform(float position)
    {
        _width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        _height = _length * _distanceUnit;

        transform.position = new Vector3(0.0f, position + _height / 2, 0.0f);
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

    public bool IsHit(float mcPosition)
    {
        if (mcPosition < Bottom || mcPosition > Top)
            return false;

        HP -= 1;
        if (HP == 0)
        {
            if (onDestroyedHandler != null)
            {
                onDestroyedHandler(this);
            }
            Destroy(gameObject);
        }
        return true;
    }

    public void OnGameOver()
    {
        Debug.Log("Obstacle OnGameOver");
        _isRunning = false;
    }

    void Update()
    {
        if (_isRunning)
        {
            float distance = _speed * Time.deltaTime;
            Move(distance);
            if (Bottom < (0.0f - Camera.main.orthographicSize) && shouldTriggerReachScreenBottomHandler && onReachScreenBottomHandler != null)
            {
                onReachScreenBottomHandler();
                shouldTriggerReachScreenBottomHandler = false;
            }
        }
    }
}
