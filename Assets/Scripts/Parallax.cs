using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float animSpeed = 1f;
    private MeshRenderer mesh;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        mesh.material.mainTextureOffset += new Vector2(animSpeed*Time.fixedDeltaTime, 0);
    }
}
