using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float MARGIN = 0.5f;
    private float PADDING = 0.3f;
    private int COUNT_V = 3;
    private int COUNT_H = 3;

    private Enemy[] _enemies;

    void Start()
    {
        float screenWidth = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float enemyWidth = (screenWidth - MARGIN * 2 - PADDING * 2) / COUNT_V;
        float enemyHeight = enemyWidth;
        enemyPrefab.transform.localScale = new Vector3(enemyWidth, enemyHeight, 1.0f);

        float translateX = (enemyPrefab.transform.localScale.x * COUNT_V + (PADDING * (COUNT_V - 1)) - enemyPrefab.transform.localScale.x) / 2;
        float translateY = (enemyPrefab.transform.localScale.y * COUNT_H + (PADDING * (COUNT_H - 1)) - enemyPrefab.transform.localScale.y) / 2;

        _enemies = new Enemy[COUNT_V * COUNT_H];
        for (int i = 0; i < COUNT_V; i++)
        {
            for (int j = 0; j < COUNT_H; j++)
            {
                GameObject enemyObj = Instantiate(enemyPrefab);
                float positionX = (enemyObj.transform.localScale.x + PADDING) * i - translateX;
                float positionY = (enemyObj.transform.localScale.y + PADDING) * j - translateY;
                enemyObj.transform.position = new Vector3(positionX, positionY, 0.0f);

                _enemies[i * 3 + j] = enemyObj.GetComponent<Enemy>();
            }
        }
    }

    void Update()
    {

    }
}
