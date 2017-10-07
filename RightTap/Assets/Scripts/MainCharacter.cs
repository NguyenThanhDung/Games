using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private int number;
    private TextMesh textMesh;

    public int Number
    {
        set
        {
            if (number != value)
            {
                number = value;
                if (this.textMesh != null)
                {
                    this.textMesh.text = number.ToString();
                }
            }
        }
    }

    void Start()
    {
        this.textMesh = this.transform.GetChild(0).GetComponent<TextMesh>();
        this.Number = 0;
    }
}
