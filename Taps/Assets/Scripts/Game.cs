using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject _mainCharacter;
    public GameObject _obstaclePrefab;

    private Obstacle _obstacle;

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

        _obstacle = Instantiate(_obstaclePrefab).GetComponent<Obstacle>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("MC Position: " + _mainCharacter.transform.position.ToString());
            Debug.Log("Obstacle: " + _obstacle.Bottom + "~" + _obstacle.Top);
        }
    }
}
