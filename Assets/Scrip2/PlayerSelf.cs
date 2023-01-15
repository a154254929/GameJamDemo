using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public class PlayerSelf : BasePlayer
    {
        public PlayerSelf(Vector3Int initPos, GameObject obj) : base(initPos, obj)
        {
            IsSelf = true;
        }
    }
}


