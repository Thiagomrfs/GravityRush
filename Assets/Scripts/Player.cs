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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
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
}
