using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float ExplodeTime = 1.5f;
    private float spawnTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > ExplodeTime)
        {
            Explode();
        }

    }

    void Explode()
    {
        Destroy(this.gameObject);
    }
}
