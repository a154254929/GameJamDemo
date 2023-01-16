using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

namespace GameJamDemo
{
    /// <summary>
    /// 炸弹类，负责炸弹的模型，创建or销毁
    /// </summary>
    public class Bomb
    {
        private MapManager mapManager = GameManager.Instance.mapManager;
        private GameConfig gameConfig = GameManager.Instance.gameConfig;

        //private GameObject m_obj;
        private float m_timer = 0;
        private Vector3Int m_position;
        public Bomb(Vector3Int position, GameObject sourceObj)
        {
            //m_obj = GameObject.Instantiate(sourceObj);
            SetPos(position);
        }

        public void UpdateHeight()
        {
            bool haveBlock;
            var targetPos = mapManager.GetActiveBlockPos(m_position, out haveBlock);
            SetPos(targetPos);
        }

        public void SetPos(Vector3Int position)
        {
            m_position = position;
            var block = mapManager.GetBlock(m_position);
            var blockPos = block.GetObjTransPosition();
            //m_obj.transform.position = blockPos + gameConfig.PlayerPosOffset;
        }

        /// <summary>
        /// 是否超时
        /// </summary>
        /// <returns></returns>
        public bool IsExpire()
        {
            m_timer += Time.deltaTime;
            return m_timer >= gameConfig.BombExplodTime;
        }

        /// <summary>
        /// 执行爆炸
        /// </summary>
        public void Explode()
        {
            mapManager.Explode(m_position);
            Release();
        }

        /// <summary>
        /// 销毁炸弹
        /// </summary>
        public void Release()
        {
            m_timer = 0;
            //GameObject.Destroy(m_obj);
            //m_obj = null;
        }
    }

    /// <summary>
    /// 炸弹管理类，管理炸弹的爆炸
    /// </summary>
    public class BombManager
    {
        private List<Bomb> m_bombList = new List<Bomb>();

        public void OnUpdate()
        {
            for (int i = m_bombList.Count - 1; i >= 0; i--)
            {
                if (m_bombList[i].IsExpire())
                {
                    m_bombList[i].Explode();
                    m_bombList.RemoveAt(i);
                }
            }
        }

        public void AddBomb(Vector3Int pos)
        {
            m_bombList.Add(new Bomb(pos, GameManager.Instance.gameConfig.BombPrefab));
        }

        public void Release()
        {
            for (int i = m_bombList.Count - 1; i >= 0; i--)
            {
                m_bombList[i].Release();
                m_bombList.RemoveAt(i);
            }
            m_bombList.Clear();
        }

        public void UpdateAllBombHeight()
        {
            for (int i = m_bombList.Count - 1; i >= 0; i--)
            {
                m_bombList[i].UpdateHeight();
            }
        }
    }
}


