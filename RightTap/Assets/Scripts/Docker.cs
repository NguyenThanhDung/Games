using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker : MonoBehaviour
{
    private float deltaNumber;
    public float NumberSpeed
    {
        set
        {
            float totalNumberPerSecond = value / 10 * 100;
            float fps = 1 / Time.fixedDeltaTime;
            deltaNumber = totalNumberPerSecond / fps;
        }
    }

    private float time;
    private float number;
    private TextMesh textMesh;

    public int Number
    {
        get
        {
            return (int)this.number;
        }
    }

    void Start()
    {
        this.number = 0;
        this.textMesh = this.transform.GetChild(0).GetComponent<TextMesh>();
        this.textMesh.text = this.number.ToString();
    }

    void FixedUpdate()
    {
        this.number += deltaNumber;
        if(this.number > 100)
        {
            this.number = 0;
        }
        this.textMesh.text = ((int)this.number).ToString();
    }
}
