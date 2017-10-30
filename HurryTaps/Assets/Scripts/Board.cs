using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private float SCREEN_TRANSLATE_Y = 1.0f;
    private float MARGIN = 0.5f;
    private float PADDING = 0.3f;
    private int COUNT_V = 3;
    private int COUNT_H = 3;

    private List<Enemy> _inactiveEnemies;
    private List<Enemy> _activeEnemies;

    public Board(GameObject enemyPrefab, System.Action<Enemy> enemyDestroyedCallback, System.Action enemyTimeOutCallback)
    {
        float screenWidth = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float enemyWidth = (screenWidth - MARGIN * 2 - PADDING * 2) / COUNT_V;
        float enemyHeight = enemyWidth;
        enemyPrefab.transform.localScale = new Vector3(enemyWidth, enemyHeight, 1.0f);

        float translateX = (enemyPrefab.transform.localScale.x - enemyPrefab.transform.localScale.x * COUNT_V - (PADDING * (COUNT_V - 1))) / 2;
        float translateY = (enemyPrefab.transform.localScale.y - enemyPrefab.transform.localScale.y * COUNT_H - (PADDING * (COUNT_H - 1))) / 2 + SCREEN_TRANSLATE_Y;
        
        _inactiveEnemies = new List<Enemy>();
        _activeEnemies = new List<Enemy>();
        for (int i = 0; i < COUNT_V; i++)
        {
            for (int j = 0; j < COUNT_H; j++)
            {
                GameObject enemyObj = MonoBehaviour.Instantiate(enemyPrefab);
                float positionX = (enemyObj.transform.localScale.x + PADDING) * i + translateX;
                float positionY = (enemyObj.transform.localScale.y + PADDING) * j + translateY;
                enemyObj.transform.position = new Vector3(positionX, positionY, 0.0f);
                enemyObj.SetActive(true);

                Enemy enemy = enemyObj.GetComponent<Enemy>();
                enemy.HP = Random.Range(1, 4);
                enemy.DestroyCallback = enemyDestroyedCallback + OnEnemyDestroyed;
                enemy.TimeOutCallback = enemyTimeOutCallback;
                _inactiveEnemies.Add(enemy);
            }
        }
    }

    public void Clear()
    {
        while (_activeEnemies.Count > 0)
        {
            Enemy enemy = _activeEnemies[0];
            enemy.gameObject.SetActive(false);
            _activeEnemies.Remove(enemy);
            _inactiveEnemies.Add(enemy);
        }
    }

    public void GenerateEnemy(GameSettings gameSettings, bool isFirstEnemy = false)
    {
        if (_inactiveEnemies.Count > 0)
        {
            int index = Random.Range(0, _inactiveEnemies.Count);
            Enemy enemy = _inactiveEnemies[index];
            _inactiveEnemies.RemoveAt(index);

            enemy.Spawn(gameSettings, isFirstEnemy);
            _activeEnemies.Add(enemy);
        }
    }

    public bool IsHit(Vector3 position)
    {
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            if(_activeEnemies[i].IsHit(position))
            {
                return true;
            }
        }
        return false;
    }

    public void OnEnemyDestroyed(Enemy destroyedEnemy)
    {
        _activeEnemies.Remove(destroyedEnemy);
        _inactiveEnemies.Add(destroyedEnemy);
    }

    public void OnGameOver()
    {
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            _activeEnemies[i].Stop();
        }
    }
}
