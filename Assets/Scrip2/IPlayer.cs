using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameJamDemo
{
    public enum MoveDirection
    {
        None,
        Forward,
        BackWard,
        Left,
        Right,
        Up,
        Down,
    }

    public class BasePlayer
    {
        private MapManager mapManager = GameManager.Instance.mapManager;
        private BombManager bombManager = GameManager.Instance.bombManager;
        private GameConfig gameConfig = GameManager.Instance.gameConfig;

        private MoveDirection m_direction;
        private Vector3Int m_position;
        private GameObject m_obj;

        protected bool IsSelf;
        public BasePlayer(Vector3Int initPos, GameObject obj)
        {
            m_position = initPos;
            m_obj = GameObject.Instantiate(obj);
            m_direction = MoveDirection.Forward;
            SetPos(m_position);
        }

        public void SetDirection(MoveDirection direction)
        {
            m_direction = direction;
            //更新模型的方向

        }

        public bool Move()
        {
            bool gameOver = false;
            Vector3Int result = mapManager.TryMove(m_position, m_direction, out gameOver);
            //Debug.LogFormat("curPos {0}, dir {1}, result {2}", m_position, m_direction, result);
            SetPos(result);

            return gameOver;
        }

        /// <summary>
        /// 设置玩家位置
        /// </summary>
        /// <param name="pos"></param>
        public void SetPos(Vector3Int pos)
        {
            m_position = pos;
            var block = mapManager.GetBlock(pos);
            var blockPos = block.GetObjTransPosition();
            m_obj.transform.position = blockPos + gameConfig.PlayerPosOffset;
        }

        /// <summary>
        /// 更新玩家高度
        /// </summary>
        public void UpdateHeight()
        {

        }

        /// <summary>
        /// 释放一个炸弹
        /// </summary>
        public void SetBomb()
        {
            bombManager.AddBomb(m_position);
        }

        public void Release()
        {
            GameObject.Destroy(m_obj);
        }
    }
}


