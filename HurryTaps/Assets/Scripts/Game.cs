using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject _enemyPrefab;

    void Start()
    {
        Instantiate(_enemyPrefab);
    }

    void Update()
    {

    }
}
