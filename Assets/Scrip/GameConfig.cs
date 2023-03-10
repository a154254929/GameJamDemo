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
        //玩家地图方块上的位置偏移
        public Vector3 PlayerPosOffset;
        //地图颜色阶数
        public Color[] StepColors;

        //地图方块模型
        public GameObject BlockPrefab;
        //炸弹模型
        public GameObject BombPrefab;
        //玩家1模型
        public GameObject player1Prfab;
        //玩家2模型
        public GameObject player2Prfab;

        //玩家跳跃移动间隔
        public float PlayerJumpTimeInterval;
        //玩家释放炸弹间隔
        public float PlayerReleaseBombTimeInterval;
        //炸弹爆炸延时
        public float BombExplodTime;
        //下层格子保护机制时间
        public float BlockProtectTime;
    }
}


