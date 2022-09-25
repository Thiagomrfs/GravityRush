using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private bool falling = false;
    private float bottomEdge;
    void Start()
    {
        float height = Random.Range(8, 16);
        transform.localScale = new Vector3(1, height, 1);
        StartCoroutine(Fall());
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1.5f;
    }

    void FixedUpdate()
    {
        if (falling && transform.position.y > bottomEdge) {
            transform.position += Vector3.down * 9f * Time.fixedDeltaTime;
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        falling = true;
    }
}
