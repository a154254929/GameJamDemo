using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public MapManager MapMgr;
    public float ExplodeTime = 1.5f;
    private float spawnTime = 0.0f;
    private int x = 0;
    private int y = 0;
    private int z = 0;
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
        MapMgr.OnBombExplod(x, y, z);
    }

    public void SepPos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
