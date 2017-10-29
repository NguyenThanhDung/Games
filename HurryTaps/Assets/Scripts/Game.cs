using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    enum GameState
    {
        INITIAL,
        PLAYING,
        STOPPED
    }

    public GameObject enemyPrefab;
    public GameObject restartButton;
    public Text _scoreText;

    private GameState _gameState;
    private GameSettings _gameSetting;
    private Board _board;
    private float _currentTime;
    private float _genInterval;
    private float _lastGenMilestone;
    private int _score;

    int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (_scoreText != null)
            {
                _scoreText.text = "Score: " + _score.ToString();
            }
        }
    }

    void Start()
    {
        _gameState = GameState.INITIAL;
        _gameSetting = new GameSettings();
        _board = new Board(enemyPrefab, OnEnemyIsDestroyed, OnGameOver);
    }

    public void Play()
    {
        restartButton.SetActive(false);
        _currentTime = 0.0f;
        _board.Clear();
        _genInterval = _gameSetting.GetGenerateInterval(_currentTime);
        _board.GenerateEnemy(_gameSetting, true);
        _lastGenMilestone = 0.0f;
        Score = 0;
        _gameState = GameState.PLAYING;
    }

    void OnEnemyIsDestroyed(Enemy destroyedEnemy)
    {
        Score++;
    }

    void OnGameOver()
    {
        _gameState = GameState.STOPPED;
        _board.OnGameOver();
        restartButton.SetActive(true);
    }

    void Update()
    {
        if (_gameState == GameState.PLAYING)
        {
            _currentTime += Time.deltaTime;
            float newGenInterval = _gameSetting.GetGenerateInterval(_currentTime);
            if (newGenInterval != _genInterval)
            {
                _genInterval = newGenInterval;
            }

            if ((_currentTime - _lastGenMilestone) > _genInterval)
            {
                _board.GenerateEnemy(_gameSetting);
                _lastGenMilestone = _currentTime;
            }
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            switch (_gameState)
            {
                case GameState.INITIAL:
                    Play();
                    break;

                case GameState.PLAYING:
                    {
                        Vector3 position = new Vector3();
                        if (Input.GetMouseButtonDown(0))
                            position = Input.mousePosition;
                        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                            position = Input.GetTouch(0).position;
                        Ray ray = Camera.main.ScreenPointToRay(position);

                        if (!_board.IsHit(ray.origin))
                        {
                            OnGameOver();
                        }
                    }
                    break;

                case GameState.STOPPED:
                    break;
            }
        }
    }
}
