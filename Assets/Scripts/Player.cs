using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float leftEdge;

    private bool canDash = true;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5f;

    private bool canExplode = false;
    private float explosionCooldown = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        StartCoroutine(IntialExplosionCD());
    }

    private IEnumerator IntialExplosionCD() {
        yield return new WaitForSeconds(explosionCooldown);
        canExplode = true;
        GetComponent<ParticleSystem>().Play(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.gravityScale *= -1;
        }

        if (Input.GetKeyDown(KeyCode.D) && canDash) {
            StartCoroutine(Dash("right"));
        } else if (Input.GetKeyDown(KeyCode.A) && canDash) {
            StartCoroutine(Dash("left"));
        } else if (Input.GetKeyDown(KeyCode.W) && canDash) {
            StartCoroutine(Dash("up"));
        } else if (Input.GetKeyDown(KeyCode.S) && canDash) {
            StartCoroutine(Dash("down"));
        } 

        if (Input.GetKeyDown(KeyCode.F) && canExplode) {
            StartCoroutine(Explode());
        }

        if (transform.position.x < leftEdge) {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("ScoreZone")) {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.IncreaseScore();
        }
    }

    private IEnumerator Dash(string dir)
    {
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        GetComponent<SpriteRenderer>().color = new Color(0.2039215f, 0.2511322f, 0.8196079f);

        switch (dir) {
            case "right":
                rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
                break;
            case "left":
                rb.velocity = new Vector2(transform.localScale.x * dashingPower * -1, 0f);
                break;
            case "up":
                rb.velocity = new Vector2(0f, transform.localScale.x * dashingPower);
                break;
            case "down":
                rb.velocity = new Vector2(0f, transform.localScale.x * dashingPower * -1);
                break;
        }

        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        GetComponent<SpriteRenderer>().color = new Color(0.8207547f, 0.240032f, 0.2051887f);
    }

    private IEnumerator Explode()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem explosion = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

        canExplode = false;
        ps.Stop(false);
        explosion.Play(false);

        yield return new WaitForSeconds(0.3f);
        ObstacleMovement[] obstacles = FindObjectsOfType<ObstacleMovement>();
        foreach(ObstacleMovement obs in obstacles)
        {
            Destroy(obs.gameObject);
        }

        yield return new WaitForSeconds(explosionCooldown);

        ps.Play(false);
        explosion.Stop(false);
        yield return new WaitForSeconds(1f);
        canExplode = true;
    }
}
