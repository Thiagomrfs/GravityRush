using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapMovement : MonoBehaviour
{
    public float animSpeed = 1f;
    public float zindex = 0f;

    void FixedUpdate()
    {
        transform.position += new Vector3(animSpeed * Time.fixedDeltaTime * -1, 0, 0);
        

        if (transform.position.x < -10) {
            transform.position = new Vector3(0, 0, zindex);
        }
    }
}
