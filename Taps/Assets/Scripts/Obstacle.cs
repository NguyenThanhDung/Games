using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float LengthUnit = 0.8f;

    public int _hp;
    public int _length = 3;
    public int _space;

    private TextMesh _hpText;
        
    void Start()
    {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height + 0.5f;
        float height = _length * LengthUnit;
        transform.localScale = new Vector3(width, height, 1.0f);
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 childScale = new Vector3(1.0f / transform.localScale.x, 1.0f / transform.localScale.y, 1.0f);
            transform.GetChild(i).transform.localScale = childScale;
        }

        _hpText = transform.GetChild(1).GetComponent<TextMesh>();
        _hpText.text = _hp.ToString();
    }
    
    void Update()
    {

    }
}
