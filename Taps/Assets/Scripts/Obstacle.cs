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
        transform.localScale = new Vector3(8.0f, _length * LengthUnit, 1.0f);
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 childScale = new Vector3(1.0f, 1.0f);
            childScale.x /= transform.localScale.x;
            childScale.y /= transform.localScale.y;
            transform.GetChild(i).transform.localScale = childScale;
        }

        _hpText = transform.GetChild(1).GetComponent<TextMesh>();
        _hpText.text = _hp.ToString();
    }
    
    void Update()
    {

    }
}
