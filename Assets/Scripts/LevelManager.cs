using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.onGameStateChanged += gameStateChange;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= gameStateChange;
    }

    private void gameStateChange(GameState state)
    {
        switch (state) {
            case GameState.waitingStart:
                break;
            case GameState.running:
                break;
            case GameState.dashEvent:
                break;
            case GameState.explosionEvent:
                break;
        }
    }
}
