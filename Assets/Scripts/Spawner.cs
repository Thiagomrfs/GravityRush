using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject ceilingPos;
    public GameObject groundPos;
    public float spawnRate = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        float pos = Random.Range(1, 3);
        float height = Random.Range(7, 16);
        GameObject obstacle;
        
        if (pos == 1) {
            obstacle = Instantiate(prefab, ceilingPos.transform.position, Quaternion.identity);
        } else {
            obstacle = Instantiate(prefab, groundPos.transform.position, Quaternion.identity);
        }
        
        obstacle.transform.localScale = new Vector3(1, height, 1);
    }
}
