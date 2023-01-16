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
        private GameObject m_obj;
        private Vector3Int m_position = Vector3Int.zero;

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

        public bool IsTransparent
        {
            get
            {
                return m_isTransparent;
            }
            set
            {
                m_obj.SetActive(!value);
                m_isTransparent = value;
            }
        }

        private bool m_isTransparent = false;//是否是透明的


        public Block(int x, int y, int z, GameObject obj, Transform parent)
        {
            m_position.Set(x, y, z);
            Vector3 createPos = new Vector3(x, z, y);
            if (obj != null)
            {
                m_obj = GameObject.Instantiate(obj, createPos, Quaternion.identity, parent);
            }
            m_isActive = true;
            m_isTransparent = false;
        }

        public Vector3 GetObjTransPosition()
        {
            return m_obj.transform.position;
        }
    }
}


