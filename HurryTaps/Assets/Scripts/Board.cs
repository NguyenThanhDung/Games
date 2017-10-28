﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private float MARGIN = 0.5f;
    private float PADDING = 0.3f;
    private int COUNT_V = 3;
    private int COUNT_H = 3;

    private List<Enemy> _inactiveEnemies;
    private List<Enemy> _activeEnemies;

    public Board(GameObject enemyPrefab)
    {
        float screenWidth = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float enemyWidth = (screenWidth - MARGIN * 2 - PADDING * 2) / COUNT_V;
        float enemyHeight = enemyWidth;
        enemyPrefab.transform.localScale = new Vector3(enemyWidth, enemyHeight, 1.0f);

        float translateX = (enemyPrefab.transform.localScale.x * COUNT_V + (PADDING * (COUNT_V - 1)) - enemyPrefab.transform.localScale.x) / 2;
        float translateY = (enemyPrefab.transform.localScale.y * COUNT_H + (PADDING * (COUNT_H - 1)) - enemyPrefab.transform.localScale.y) / 2;

        _inactiveEnemies = new List<Enemy>();
        _activeEnemies = new List<Enemy>();
        for (int i = 0; i < COUNT_V; i++)
        {
            for (int j = 0; j < COUNT_H; j++)
            {
                GameObject enemyObj = MonoBehaviour.Instantiate(enemyPrefab);
                float positionX = (enemyObj.transform.localScale.x + PADDING) * i - translateX;
                float positionY = (enemyObj.transform.localScale.y + PADDING) * j - translateY;
                enemyObj.transform.position = new Vector3(positionX, positionY, 0.0f);
                enemyObj.SetActive(false);

                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemy.HP = Random.Range(1, 4);
                _inactiveEnemies.Add(enemy);
            }
        }
    }

    public void GenerateEnemy()
    {
        if (_inactiveEnemies.Count > 0)
        {
            int index = Random.Range(0, _inactiveEnemies.Count);
            Enemy enemy = _inactiveEnemies[index];
            _inactiveEnemies.RemoveAt(index);

            enemy.HP = Random.Range(1, 4);
            enemy.gameObject.SetActive(true);
            _activeEnemies.Add(enemy);
        }
    }
}
