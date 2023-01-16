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
        public BlockExplodeMgr blockExplodeMgr;
        public AudioManager audioManager;

        private bool openTwoPlayer = false;

        public GameManager()
        {
            mapManager = new MapManager();
            bombManager = new BombManager();
            blockExplodeMgr = new BlockExplodeMgr();
            audioManager = new AudioManager();
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

        public void InitGame()
        {
            audioManager.Init();
            mapManager.CreateMap(gameConfig.MapSize, gameConfig.BlockPrefab);
            CreaterPlayer();
        }

        public void StartGame()
        {
            m_start = true;
        }

        public void StopGame()
        {
            m_start = false;
            bombManager.Release();
            blockExplodeMgr.Release();
        }

        public void CreaterPlayer()
        {
            playerSelf = new PlayerSelf(new Vector3Int(0, 0, gameConfig.MapSize.z - 1), MoveDirection.Forward, gameConfig.player1Prfab);
            if (openTwoPlayer)
                playerOther = new PlayerOther(new Vector3Int(gameConfig.MapSize.x - 1, gameConfig.MapSize.y - 1, gameConfig.MapSize.z - 1), MoveDirection.BackWard, gameConfig.player2Prfab);
        }

        /// <summary>
        /// 每秒钟触发一次，推进游戏
        /// </summary>
        public void Step()
        {
            bool otherGameOver = false;
            bool selfGameOver = playerSelf.Move();
            if (openTwoPlayer)
                otherGameOver = playerOther.Move();

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
            if (openTwoPlayer)
                playerOther.SetBomb();
        }

        public void Update()
        {
            if (!m_start)
            {
                return;
            }
            bombManager.OnUpdate();
            UpdatePlayerControl();
            blockExplodeMgr.OnUpdate();

            m_timer += Time.deltaTime;
            m_bombTiemr += Time.deltaTime;
            if (m_timer >= gameConfig.PlayerJumpTimeInterval)
            {
                Step();
                //m_timer = 0;
                m_timer -= gameConfig.PlayerJumpTimeInterval;
            }
            if (m_bombTiemr >= gameConfig.PlayerReleaseBombTimeInterval)
            {
                SetBomb();
                //m_bombTiemr = 0;
                m_bombTiemr -= gameConfig.PlayerReleaseBombTimeInterval;
            }
        }

        /// <summary>
        /// 炸弹爆炸强制刷新玩家和炸弹高度
        /// </summary>
        public void UpdateAllHeight()
        {
            playerSelf.UpdateHeight();
            if (openTwoPlayer)
                playerOther.UpdateHeight();
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


