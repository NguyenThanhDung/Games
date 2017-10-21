using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mainCharacterObject;
    public GameObject obstaclePrefab;

    private MainCharacter _mainCharacter;
    private Obstacle[] _obstacle;

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

        _mainCharacter = mainCharacterObject.GetComponent<MainCharacter>();

        _obstacle = new Obstacle[2];
        _obstacle[0] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
        _obstacle[1] = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("MC Position: " + _mainCharacter.Position);
            foreach (Obstacle obstacle in _obstacle)
            {
                Debug.Log("Obstacle: " + obstacle.Bottom + "~" + obstacle.Top);
                if(_mainCharacter.Position>obstacle.Bottom&&_mainCharacter.Position<obstacle.Top)
                {
                    obstacle.IsHit();
                }
            }
        }
    }
}
