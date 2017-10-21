using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float Position
    {
        get
        {
            return transform.position.y;
        }
    }
}
