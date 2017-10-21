using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float LengthUnit;    

    public int _hp;
    public int _length = 3;
    public int _space;

    private float OBSTACLE_WIDTH_BUFFER = 0.5f;
    private float CIRCLE_SCALE = 0.6f;
    private TextMesh _hpText;
        
    void Start()
    {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height + OBSTACLE_WIDTH_BUFFER;
        float height = _length * LengthUnit;
        transform.localScale = new Vector3(width, height, 1.0f);
        for (int i = 0; i < transform.childCount; i++)
        {
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
