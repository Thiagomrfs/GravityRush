using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float leftEdge;
    public float speed = 7f;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1.5f;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.left * speed * Time.fixedDeltaTime;

        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
