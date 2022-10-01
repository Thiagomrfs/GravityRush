using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;
    private Canvas canvas;
    private GameObject map;
    private Camera cam;
    private GameManager gm;

    private void Awake()
    {
        GameManager.onGameStateChanged += gameStateChange;
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        canvas = FindObjectOfType<Canvas>();
        map = GameObject.Find("Map");
        cam = Camera.main;
        gm = FindObjectOfType<GameManager>();
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
                startGame();
                break;
            case GameState.dashEvent:
                dashEvent();
                break;
            case GameState.explosionEvent:
                explosionEvent();
                break;
        }
    }

    private void waitStart()
    {
        player.GetComponent<Rigidbody2D>().simulated = false;
        player.GetComponent<Player>().enabled = false;
        spawner.enabled = false;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
    }

    private void startGame()
    {
        canvas.transform.Find("Initial text").gameObject.SetActive(false);
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Player>().enabled = true;
        spawner.enabled = true;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = true;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = true;
    }

    private void dashEvent()
    {
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.7490196f, 0.8784314f, 0.7632619f),
            50f
        ));
    }

    private void explosionEvent()
    {
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.8759063f, 0.8784314f, 0.7490196f),
            50f
        ));
    }

    private IEnumerator lerpIn(Color startColor, Color endColor, float fadeInTime)
    {
        for (float t = 0.01f; t < fadeInTime; t += 0.1f) {
            cam.backgroundColor = Color.Lerp(startColor, endColor, t/fadeInTime);
            yield return null;
        }
    }
}
