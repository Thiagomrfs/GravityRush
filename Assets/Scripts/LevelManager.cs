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
                StartCoroutine(dashEvent());
                break;
            case GameState.explosionEvent:
                StartCoroutine(explosionEvent());
                break;
            case GameState.gameOver:
                gameOver();
                break;
            case GameState.winGame:
                winGame();
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
        canvas.transform.Find("QuitButton").gameObject.SetActive(false);
        canvas.transform.Find("Initial text").gameObject.SetActive(false);
        canvas.transform.Find("Objective text").gameObject.SetActive(false);
        canvas.transform.Find("Dash text").gameObject.SetActive(false);
        canvas.transform.Find("Explosion text").gameObject.SetActive(false);
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Player>().enabled = true;
        spawner.enabled = true;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = true;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = true;
    }

    private IEnumerator dashEvent()
    {
        spawner.enabled = false;

        yield return new WaitForSeconds(2f);
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.7490196f, 0.8784314f, 0.7632619f),
            100f
        ));

        yield return new WaitForSeconds(0.3f);
        player.unlockDash();
        canvas.transform.Find("Dash text").gameObject.SetActive(true);
        player.GetComponent<Rigidbody2D>().simulated = false;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
    }

    private IEnumerator explosionEvent()
    {
        spawner.enabled = false;

        yield return new WaitForSeconds(2f);
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.8759063f, 0.8784314f, 0.7490196f),
            50f
        ));

        yield return new WaitForSeconds(0.3f);
        
        player.unlockExplosion();
        canvas.transform.Find("Explosion text").gameObject.SetActive(true);
        player.GetComponent<Rigidbody2D>().simulated = false;
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
    }

    private IEnumerator lerpIn(Color startColor, Color endColor, float fadeInTime)
    {
        for (float t = 0.01f; t < fadeInTime; t += 0.1f) {
            cam.backgroundColor = Color.Lerp(startColor, endColor, t/fadeInTime);
            yield return null;
        }
    }

    private void gameOver() {
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.3773585f, 0.3773585f, 0.3773585f),
            40f
        ));
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
        spawner.enabled = false;
        canvas.transform.Find("GameOverPanel").gameObject.SetActive(true);
        canvas.transform.Find("QuitButton").gameObject.SetActive(true);
    }

    private void winGame() {
        StartCoroutine(lerpIn(
            cam.backgroundColor,
            new Color(0.487184f, 0.8679245f, 0.5010607f),
            40f
        ));
        map.transform.Find("Ground").GetComponent<Parallax>().enabled = false;
        map.transform.Find("Ceiling").GetComponent<Parallax>().enabled = false;
        spawner.enabled = false;
        canvas.transform.Find("WinGamePanel").gameObject.SetActive(true);
        canvas.transform.Find("QuitButton").gameObject.SetActive(true);
    }
}
