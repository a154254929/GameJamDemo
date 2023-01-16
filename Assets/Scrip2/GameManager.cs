using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 管理游戏的总流程
    /// </summary>
    public class GameManager
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

        public MapManager mapManager;
        public BombManager bombManager;
        public GameConfig gameConfig;

        public GameManager()
        {
            mapManager = new MapManager();
            bombManager = new BombManager();
        }

        private float m_timer = 0;
        private float m_bombTiemr = 0;
        private bool m_start = false;

        public BasePlayer playerSelf;
        public BasePlayer playerOther;

        public void SetConfig(GameConfig config)
        {
            gameConfig = config;
        }

        public void StartGame()
        {
            mapManager.CreateMap(gameConfig.MapSize, gameConfig.BlockPrefab);
            CreaterPlayer();
            m_start = true;
        }

        public void StopGame()
        {
            m_start = false;
            bombManager.ClearAllBomb();
        }

        public void CreaterPlayer()
        {
            playerSelf = new PlayerSelf(new Vector3Int(0, 0, gameConfig.MapSize.z - 1), MoveDirection.Forward, gameConfig.player1Prfab);
            //playerOther = new PlayerOther(new Vector3Int(gameConfig.MapSize.x - 1, gameConfig.MapSize.y - 1, gameConfig.MapSize.z - 1), MoveDirection.BackWard, gameConfig.player2Prfab);
        }

        /// <summary>
        /// 每秒钟触发一次，推进游戏
        /// </summary>
        public void Step()
        {
            bool selfGameOver = playerSelf.Move();
            //bool otherGameOver = playerOther.Move();
            bool otherGameOver = false;

            if (selfGameOver || otherGameOver)
            {
                StopGame();
            }

            if (selfGameOver && !otherGameOver)
            {
                //失败
                Debug.Log("失败");
            }
            else if (!selfGameOver && otherGameOver)
            {
                //胜利
                Debug.Log("胜利");
            }
            else if (selfGameOver && otherGameOver)
            {
                //平局
                Debug.Log("平局");
            }
        }

        /// <summary>
        /// 释放炸弹
        /// </summary>
        public void SetBomb()
        {
            playerSelf.SetBomb();
            //playerOther.SetBomb();
        }

        public void Update()
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
                //m_timer = 0;
                m_timer -= 1;
            }
            if (m_bombTiemr >= 3)
            {
                SetBomb();
                //m_bombTiemr = 0;
                m_bombTiemr -= 3;
            }
        }

        /// <summary>
        /// 炸弹爆炸强制刷新玩家和炸弹高度
        /// </summary>
        public void UpdateAllHeight()
        {
            playerSelf.UpdateHeight();
            //playerOther.UpdateHeight();
            bombManager.UpdateAllBombHeight();
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


