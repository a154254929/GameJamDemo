using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 炸弹类，负责炸弹的模型，创建or销毁
    /// </summary>
    public class Bomb
    {
        private GameObject m_obj;
        private float m_timer = 0;
        public Bomb(GameObject sourceObj)
        {
            m_obj = GameObject.Instantiate(sourceObj);
        }

        /// <summary>
        /// 是否超时
        /// </summary>
        /// <returns></returns>
        public bool IsExpire()
        {
            m_timer += Time.deltaTime;
            return m_timer >= 3;
        }

        /// <summary>
        /// 执行爆炸
        /// </summary>
        public void Explode()
        {

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
    }
}


