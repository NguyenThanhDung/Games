using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3(0.0f, 5.5f);
    }

    void FixedUpdate()
    {
        this.transform.position -= new Vector3(0.0f, 0.05f);
    }
}