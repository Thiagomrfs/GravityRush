using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float height = Random.Range(8, 16);
        transform.localScale = new Vector3(1, height, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
