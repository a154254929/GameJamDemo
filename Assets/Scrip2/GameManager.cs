using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 管理游戏的总流程
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_instance;
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameManager();
                }
                return m_instance;
            }
        }

        private float m_timer = 0;
        private bool m_start = false;

        public MapManager mapManager = new MapManager();

        public BasePlayer playerSelf = new PlayerSelf();
        public BasePlayer playerOther = new PlayerOther();

        public void Awake()
        {
            mapManager.CreateMap(2, 2, 2, null);
        }

        /// <summary>
        /// 每秒钟触发一次，推进游戏
        /// </summary>
        public void Step()
        {
            playerSelf.Move();
            playerOther.Move();
        }

        private void Update()
        {
            if (!m_start)
            {
                return;
            }

            m_timer += Time.deltaTime;
            if (m_timer >= 1)
            {
                Step();
                m_timer = 0;
            }
        }
    }
}


