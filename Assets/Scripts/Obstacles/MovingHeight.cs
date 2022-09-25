using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHeight : MonoBehaviour
{
    private float min = 6;
    private float max = 16;
    private float height;
    void Start()
    {
        height = Random.Range(min, max);
        transform.localScale = new Vector3(1, height, 1);
        StartCoroutine(HeightChange());
    }
    
    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale, 
            new Vector3(1, height, 1), 
            6f * Time.fixedDeltaTime
        );
    }

    private IEnumerator HeightChange()
    {
        yield return new WaitForSeconds(1);
        height = Random.Range(min, max);
        StartCoroutine(HeightChange());
    }
}
