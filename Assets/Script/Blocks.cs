using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private bool IsDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        IsDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBombExplod()
    {
        IsDestroy = true;
        this.gameObject.SetActive(false);
    }

    public bool GetIsDestroy()
    {
        return IsDestroy;
    }
}
