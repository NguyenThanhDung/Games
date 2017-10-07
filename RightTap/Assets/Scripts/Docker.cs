using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docker : MonoBehaviour
{
    private float time;
    private int number;
    private TextMesh textMesh;

    public int Number
    {
        get
        {
            return this.number;
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
        this.time += Time.fixedDeltaTime;
        if (this.time > 0.05f)
        {
            this.time = 0.0f;
            this.number += 1;
            if(this.number > 100)
            {
                this.number = 0;
            }
            this.textMesh.text = this.number.ToString();
        }
    }
}
