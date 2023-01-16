using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 格子类，负责格子的模型，创建or销毁
    /// </summary>
    public class Block
    {
        private MapManager manager = GameManager.Instance.mapManager;
        private BlockExplodeMgr explodeMgr = GameManager.Instance.blockExplodeMgr;
        private GameObject m_obj;
        private Vector3Int m_position = Vector3Int.zero;
        private MeshRenderer m_meshRender;
        private MaterialPropertyBlock prop = new MaterialPropertyBlock();
        public float m_explodeTimer = 0;

        /// <summary>
        /// 被炸掉
        /// </summary>
        public void SetExploded(bool delay)
        {
            if (delay)
            {
                explodeMgr.AddBlock(this);
                m_explodeTimer = 0;
            }
            else
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// 是否查出计时爆炸时间
        /// </summary>
        /// <returns></returns>
        public bool ExpireExlopdeTime()
        {
            m_explodeTimer += Time.deltaTime;
            return m_explodeTimer > 0.5f;
        }


        public bool IsActive
        {
            set
            {
                if (m_obj != null)
                {
                    m_isActive = value;
                    m_obj.SetActive(value);
                }
            }
            get
            {
                return m_isActive;
            }
        }
        private bool m_isActive = true;

        public void SetTransparent(float alpha)
        {
            m_meshRender.GetPropertyBlock(prop);
            prop.SetFloat("_Alpha", alpha);
            m_meshRender.SetPropertyBlock(prop);
        }

        public void SetReadToBomb1()
        {
            m_meshRender.GetPropertyBlock(prop);
            prop.SetInt("_ExplodState1", 1);
            m_meshRender.SetPropertyBlock(prop);
        }

        public void SetReadToBomb2()
        {
            m_meshRender.GetPropertyBlock(prop);
            prop.SetInt("_ExplodState2", 1);
            m_meshRender.SetPropertyBlock(prop);
        }

        public Block(int x, int y, int z, GameObject obj, Transform parent)
        {
            m_position.Set(x, y, z);
            Vector3 createPos = new Vector3(x, z, y);
            if (obj != null)
            {
                m_obj = GameObject.Instantiate(obj, createPos, Quaternion.identity, parent);
                //shader参数
                m_meshRender = m_obj.transform.Find("Block").GetComponent<MeshRenderer>();
                m_meshRender.GetPropertyBlock(prop);
                GameManager gameMgr = GameManager.Instance;
                prop.SetFloat("_Top", gameMgr.gameConfig.StepColors.Length);
                prop.SetVector("_StepColor", (Vector4)gameMgr.gameConfig.StepColors[z]);
                prop.SetInt("_ExplodState1", 0);
                prop.SetInt("_ExplodState2", 0);
                m_meshRender.SetPropertyBlock(prop);
            }
            m_isActive = true;
        }

        public Vector3 GetObjTransPosition()
        {
            return m_obj.transform.position;
        }
    }

    /// <summary>
    /// 管理一些延迟爆炸的格子
    /// </summary>
    public class BlockExplodeMgr
    {
        public List<Block> m_blockList = new List<Block>();

        public void AddBlock(Block block)
        {
            if (!m_blockList.Contains(block))
            {
                m_blockList.Add(block);
            }
        }

        public void OnUpdate()
        {
            for (int i = m_blockList.Count - 1; i >= 0; i--)
            {
                if (m_blockList[i].ExpireExlopdeTime())
                {
                    m_blockList[i].SetExploded(false);
                    m_blockList.RemoveAt(i);
                    GameManager.Instance.UpdateAllHeight();
                }
            }
        }

        public void Release()
        {
            m_blockList.Clear();
        }
    }
}


