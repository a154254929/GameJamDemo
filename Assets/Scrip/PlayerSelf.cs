using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public class PlayerSelf : BasePlayer
    {
        public PlayerSelf(Vector3Int initPos, MoveDirection initDir, GameObject obj) : base(initPos, initDir, obj)
        {
            IsSelf = true;
        }
    }
}


