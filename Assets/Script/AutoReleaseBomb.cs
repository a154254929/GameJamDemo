using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReleaseBomb : MonoBehaviour
{
    public GameObject BombPrefab = null;
    public float GenerateBombTime = 3.0f;
    private float unGenerateBombTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        unGenerateBombTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        unGenerateBombTime += Time.deltaTime;
        if(unGenerateBombTime > GenerateBombTime)
        {
            int generateBombNum = (int)(unGenerateBombTime / GenerateBombTime);
            for (int i = 0;i < generateBombNum; ++i)
            {
                GameObject gameObject = Instantiate(BombPrefab, new Vector3(1, 1, 1), Quaternion.identity);
                gameObject.AddComponent<Bomb>();
                unGenerateBombTime -= GenerateBombTime;
            }
        }
    }
}
