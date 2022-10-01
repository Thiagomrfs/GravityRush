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

        switch (score) {
            case 2:
                updateGameState(GameState.dashEvent);
                break;
            case 5:
                updateGameState(GameState.explosionEvent);
                break;
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

    private void Update()
    {
        if (state == GameState.waitingStart && Input.GetKeyDown(KeyCode.Space)) {
            updateGameState(GameState.running);
        }

        if (state == GameState.dashEvent) {
            if (Input.GetKeyDown(KeyCode.D)) {
                updateGameState(GameState.running);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                updateGameState(GameState.running);
            } else if (Input.GetKeyDown(KeyCode.W)) {
                updateGameState(GameState.running);
            } else if (Input.GetKeyDown(KeyCode.S)) {
                updateGameState(GameState.running);
            } 
        }
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
