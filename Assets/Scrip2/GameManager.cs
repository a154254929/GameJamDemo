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
        private float m_bombTiemr = 0;
        private bool m_start = false;

        public MapManager mapManager = new MapManager();
        public BombManager bombManager = new BombManager();
        public GameConfig config;

        public BasePlayer playerSelf;
        public BasePlayer playerOther;

        public void Awake()
        {
            mapManager.CreateMap(config.MapSize, config.BlockPrefab);
        }

        public void CreaterPlayer()
        {
            playerSelf = new PlayerSelf(Vector3Int.zero, config.player1Prfab);
            playerOther = new PlayerOther(Vector3Int.zero, config.player2Prfab);
        }

        /// <summary>
        /// 每秒钟触发一次，推进游戏
        /// </summary>
        public void Step()
        {
            playerSelf.Move();
            playerOther.Move();
        }

        /// <summary>
        /// 释放炸弹
        /// </summary>
        public void SetBomb()
        {
            playerSelf.SetBomb();
            playerOther.SetBomb();
        }

        private void Update()
        {
            if (!m_start)
            {
                return;
            }
            bombManager.OnUpdate();
            UpdatePlayerControl();

            m_timer += Time.deltaTime;
            m_bombTiemr += Time.deltaTime;
            if (m_timer >= 1)
            {
                Step();
                m_timer = 0;
            }
            if (m_bombTiemr >= 3)
            {
                SetBomb();
                m_bombTiemr = 0;
            }
        }

        /// <summary>
        /// 炸弹爆炸强制刷新玩家高度位置
        /// </summary>
        public void UpdatePlayerUpdateHeight()
        {
            playerSelf.UpdateHeight();
            playerOther.UpdateHeight();
        }

        public void UpdatePlayerControl()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h > 0)
                playerSelf.SetDirection(MoveDirection.Right);
            else if (h < 0)
                playerSelf.SetDirection(MoveDirection.Left);

            if (v > 0)
                playerSelf.SetDirection(MoveDirection.Forward);
            else if (v < 0)
                playerSelf.SetDirection(MoveDirection.BackWard);

        }
    }
}


