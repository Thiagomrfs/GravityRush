using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagonal : MonoBehaviour
{
    void Start()
    {
        float height = Random.Range(8, 16);
        transform.localScale = new Vector3(1, height, 1);
    }
}
