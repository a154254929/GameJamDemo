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
        Right,
        Up,
        Down,
    }

    public class BasePlayer
    {
        private MapManager mapManager = GameManager.Instance.mapManager;
        private MoveDirection m_direction;
        private Vector3 m_position;
        private GameObject m_obj;

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
            var block = mapManager.GetBlock((int)pos.x, (int)pos.y, (int)pos.z);
            var blockPos = block.GetObjPosition();
            //TODO 玩家模型到脚下的方块模型位置的偏移，后续使用配置
            Vector3 posOffset = Vector3.zero;
            m_obj.transform.position = blockPos + posOffset;
        }

        /// <summary>
        /// 更新玩家高度
        /// </summary>
        public void UpdateHeight()
        {

        }
    }
}


