using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSelf : IPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W))
        {
            base.SetDirection(MoveDirection.Forward);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            base.SetDirection(MoveDirection.BackWard);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            base.SetDirection(MoveDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            base.SetDirection(MoveDirection.Right);
        }
    }
}
