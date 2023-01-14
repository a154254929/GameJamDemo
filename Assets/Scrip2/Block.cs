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
        public bool IsActive
        {
            set
            {
                if (m_obj != null)
                {
                    m_obj.SetActive(value);
                }
            }
            get
            {
                return m_obj.active;
            }
        }

        private GameObject m_obj;

        public Block(int x, int y, int z, GameObject obj)
        {
            Debug.LogFormat("x={0}, y={1}, z={2}", x, y, z);
            if (obj != null)
            {
                m_obj = GameObject.Instantiate(obj);
            }
        }

        public Vector3 GetObjPosition()
        {
            return m_obj.transform.position;
        }
    }
}


