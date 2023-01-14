using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public enum MoveDirection
    {
        None,
        Forward,
        BackWard,
        Left,
        Right
    }

    public class BasePlayer
    {
        private MapManager mapManager = GameManager.Instance.mapManager;
        private MoveDirection m_direction;
        private Vector3 m_position;

        void SetDirection(MoveDirection direction)
        {
            m_direction = direction;
            //更新模型的方向

        }

        public void Move()
        {
            Vector3 result = mapManager.TryMove(m_position, m_direction);
            SetPos(result);
        }

        /// <summary>
        /// 设置玩家位置
        /// </summary>
        /// <param name="pos"></param>
        public void SetPos(Vector3 pos)
        {
            //TODO
        }
    }
}


