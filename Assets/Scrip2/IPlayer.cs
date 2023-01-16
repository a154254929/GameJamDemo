using DG.Tweening;
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
        ForwardLeft,
        ForwardRight,
        BackWardLeft,
        BackWardRight,
    }

    public class BasePlayer
    {
        private MapManager mapManager = GameManager.Instance.mapManager;
        private BombManager bombManager = GameManager.Instance.bombManager;
        private GameConfig gameConfig = GameManager.Instance.gameConfig;

        private MoveDirection m_direction;
        private Vector3Int m_position;
        private GameObject m_obj;
        private Animator m_animator;


        protected bool IsSelf;
        public BasePlayer(Vector3Int initPos, MoveDirection initDir, GameObject obj)
        {
            m_position = initPos;
            m_obj = GameObject.Instantiate(obj);
            m_animator = m_obj.GetComponent<Animator>();
            m_direction = initDir;
            SetPos(m_position);
        }

        public void SetDirection(MoveDirection direction)
        {
            m_direction = direction;
            //更新模型的方向
            switch (direction)
            {
                case MoveDirection.Forward:
                    m_obj.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case MoveDirection.BackWard:
                    m_obj.transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
                case MoveDirection.Left:
                    m_obj.transform.eulerAngles = new Vector3(0, 270, 0);
                    break;
                case MoveDirection.Right:
                    m_obj.transform.eulerAngles = new Vector3(0, 90, 0);
                    break;
            }
        }

        public bool Move()
        {
            bool gameOver = false;
            Vector3Int result = mapManager.TryMove(m_position, m_direction, out gameOver);
            if (m_position != result)
            {
                m_animator.SetTrigger("Jump");
            }

            SetPos(result);
            //Debug.LogFormat("curPos {0}, dir {1}, result {2}", m_position, m_direction, result);
            return gameOver;
        }

        /// <summary>
        /// 设置玩家位置
        /// </summary>
        /// <param name="pos"></param>
        public void SetPos(Vector3Int pos)
        {
            m_position = pos; 
            mapManager.HideUpLayer(pos.z);
            var block = mapManager.GetBlock(pos);
            var blockPos = block.GetObjTransPosition();

            var targetTransPos = blockPos + gameConfig.PlayerPosOffset;
            if (Vector3.Distance(m_obj.transform.position, targetTransPos) > 0.2f)
            {
                DOTween.To(() => m_obj.transform.position, r => m_obj.transform.position = r, targetTransPos, 0.4f);
            }
            
            //m_obj.transform.position = targetTransPos;
        }

        public Vector3 GetTargetTransPos(Vector3Int pos)
        {
            var block = mapManager.GetBlock(pos);
            var blockPos = block.GetObjTransPosition();
            return blockPos + gameConfig.PlayerPosOffset;
        }

        /// <summary>
        /// 更新玩家高度
        /// </summary>
        public void UpdateHeight()
        {
            bool haveBlock;
            var targetPos = mapManager.GetActiveBlockPos(m_position, out haveBlock);
            SetPos(targetPos);
        }

        /// <summary>
        /// 释放一个炸弹
        /// </summary>
        public void SetBomb()
        {
            bombManager.AddBomb(m_position);
            //mapManager.ChangeExplodeColor(m_position);
            //Debug.LogFormat("释放炸弹{0}", m_position);
        }

        public void Release()
        {
            GameObject.Destroy(m_obj);
        }
    }
}


