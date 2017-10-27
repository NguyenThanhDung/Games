using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Color NormalColor;
    public Color HighLightColor;

    private SpriteRenderer _spriteRender;

    public float Position
    {
        get
        {
            return transform.position.y;
        }
    }

    void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _spriteRender.color = NormalColor;
    }

    public void HighLight(bool shouldHighLight)
    {
        _spriteRender.color = shouldHighLight ? HighLightColor : NormalColor;
    }
}
