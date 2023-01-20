using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public class PlayerOther : BasePlayer
    {
        public PlayerOther(Vector3Int initPos, MoveDirection initDir, GameObject obj) : base(initPos, initDir, obj)
        {
            IsSelf = false;
        }
    }
}


