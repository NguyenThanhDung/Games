using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Vector3 INITIAL_POSITION = new Vector3(0.0f, -4.0f, 0.0f);
    const float END_Y = 6.0f;
    private bool mIsMoving;

    void Start()
    {
        this.gameObject.SetActive(false);
        this.transform.position = INITIAL_POSITION;
        this.mIsMoving = false;
    }

    public void Move()
    {
        this.gameObject.SetActive(true);
        this.mIsMoving = true;
    }

    void FixedUpdate()
    {
        if (this.mIsMoving)
        {
            this.transform.position += new Vector3(0.0f, 0.05f, 0.0f);
            if(this.transform.position.y > END_Y)
            {
                this.gameObject.SetActive(false);
                this.transform.position = INITIAL_POSITION;
                this.mIsMoving = false;
            }
        }
    }
}
