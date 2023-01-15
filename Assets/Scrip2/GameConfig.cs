using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 负责游戏中所有配置的数据存储
    /// </summary>
    [CreateAssetMenu]
    [Serializable]
    public class GameConfig : ScriptableObject
    {
        //服务器ip，端口号
        public string IP;
        public int port;

        //地图长宽高
        public Vector3Int MapSize = new Vector3Int(8, 8, 8);
        //地图方块模型
        public GameObject BlockPrefab;
        //玩家地图方块上的位置偏移
        public Vector3 PlayerPosOffset;
        //玩家1模型
        public GameObject player1Prfab;
        //玩家2模型
        public GameObject player2Prfab;
    }
}


