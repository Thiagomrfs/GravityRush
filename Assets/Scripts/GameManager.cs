using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public int score { get; private set; }
    private GameObject spawner;
    public GameState state;
    public static event Action<GameState> onGameStateChanged;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score == 2) {
            updateGameState(GameState.running);
        }
    }
    private void Awake()
    {
        instance = this;
        spawner = GameObject.FindWithTag("Spawner");
    }

    private void Start()
    {
        StartCoroutine(InvokeCoroutine());
    }
 
    IEnumerator InvokeCoroutine()
    {
        yield return null;
        onGameStateChanged?.Invoke(GameState.waitingStart);
    }

    public void updateGameState (GameState newState)
    {
        state = newState;
        onGameStateChanged?.Invoke(newState);
    }
}

public enum GameState {
    waitingStart,
    running,
    dashEvent,
    explosionEvent
}
