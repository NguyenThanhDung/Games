using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject _mainCharacter;
    public GameObject _obstaclePrefab;

    private GameObject _obstacle;

    void Start()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#else
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
#endif
        Screen.SetResolution(480, 800, false);

        _obstacle = (GameObject)Instantiate(_obstaclePrefab);
    }
    
    void Update()
    {

    }
}
