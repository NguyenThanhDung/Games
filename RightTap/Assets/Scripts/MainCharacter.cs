using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private bool mIsMoving;

    void Start()
    {
        this.gameObject.SetActive(false);
        this.transform.position = new Vector3(0.0f, -4.0f, 0.0f);
        this.mIsMoving = false;
    }

    public void Move()
    {
        this.gameObject.SetActive(true);
        this.mIsMoving = true;
    }

    void FixedUpdate()
    {
        if (mIsMoving)
        {
            this.transform.position += new Vector3(0.0f, 0.05f, 0.0f);
        }
    }
}
