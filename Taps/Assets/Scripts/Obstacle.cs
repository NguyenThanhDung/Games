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

    private float OBSTACLE_WIDTH_BUFFER = 0.5f;
    private float CIRCLE_SCALE = 0.6f;
    private float _height;
    private TextMesh _hpText;

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
        
    void Start()
    {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        _height = _length * LengthUnit;
        transform.localScale = new Vector3(width + OBSTACLE_WIDTH_BUFFER, _height, 1.0f);

        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 childPosition;
            if (_length > 1)
                childPosition = new Vector3(CirclePositionLeft - width / 2, CirclePositionBottom - _height / 2, -0.1f * (i + 1));
            else
                childPosition = new Vector3(CirclePositionLeft - width / 2, 0.35f - _height / 2, -0.1f * (i + 1));
            transform.GetChild(i).transform.position = childPosition;

            Vector3 childScale = new Vector3(CIRCLE_SCALE / transform.localScale.x, CIRCLE_SCALE / transform.localScale.y, 1.0f);
            transform.GetChild(i).transform.localScale = childScale;
        }

        _hpText = transform.GetChild(1).GetComponent<TextMesh>();
        _hpText.text = _hp.ToString();
    }
    
    void Update()
    {

    }
}
