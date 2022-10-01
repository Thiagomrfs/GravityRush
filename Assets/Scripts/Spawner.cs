using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
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
        GameManager gm = FindObjectOfType<GameManager>();

        int pos = Random.Range(1, 3);
        int obstacleType;

        if (gm.score <= 10) {
            obstacleType = Random.Range(0, 1);
        } else if (gm.score <= 20) {
            obstacleType = Random.Range(0, 2);
        } else {
            obstacleType = Random.Range(0, prefabs.Length);
        }

        GameObject obstacle;
        
        if (pos == 1) {
            obstacle = Instantiate(prefabs[obstacleType], ceilingPos.transform.position, Quaternion.identity);
        } else {
            obstacle = Instantiate(prefabs[obstacleType], groundPos.transform.position, Quaternion.identity);
        }
    }
}
