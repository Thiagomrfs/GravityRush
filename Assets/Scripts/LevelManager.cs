using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;
    private Canvas canvas;
    private GameObject map;

    private void Awake()
    {
        GameManager.onGameStateChanged += gameStateChange;
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        canvas = FindObjectOfType<Canvas>();
        map = GameObject.Find("Map");
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= gameStateChange;
    }

    private void gameStateChange(GameState state)
    {
        switch (state) {
            case GameState.waitingStart:
                waitStart();
                break;
            case GameState.running:
                break;
            case GameState.dashEvent:
                break;
            case GameState.explosionEvent:
                break;
        }
    }

    private void waitStart()
    {
        Transform a = canvas.transform.Find("Initial text");
        player.GetComponent<Rigidbody2D>().simulated = false;
        spawner.enabled = false;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
    }
}
