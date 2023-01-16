using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;
using NetWorkFrame;

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
        public SingletonManager singletonMgr;
        public BlockExplodeMgr blockExplodeMgr;
        public AudioManager audioManager;
        public GameUI gameUI;
        public Camera mainCamera;

        private float HeartSendTime = 3.0f;
        private float LastSendHeartTime = .0f;
        private bool openTwoPlayer = false;
        private float initFOV;

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
            gameUI = Transform.FindObjectOfType<GameUI>();
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            initFOV = mainCamera.fieldOfView;
            NetworkManager.GetInstance().StartGame();
        }

        public void StartGame()
        {
            NetworkManager.GetInstance().SendPacket(MessageType.JoinGame);
            //m_start = true;
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
                //Debug.Log("失败");
                gameUI.ShowResult(false);
                audioManager.PlayCommonSE(audioManager.GetExplodeAudio());
            }
            else if (!selfGameOver && otherGameOver)
            {
                //胜利
                //Debug.Log("胜利");
                gameUI.ShowResult(true);
                audioManager.PlayCommonSE(audioManager.GetExplodeAudio());
            }
            else if (selfGameOver && otherGameOver)
            {
                //平局
                gameUI.ShowResult(true);
                audioManager.PlayCommonSE(audioManager.GetExplodeAudio());
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
            if (NetworkManager.GetInstance().IsConncted)
            {
                LastSendHeartTime += Time.deltaTime;
                if (LastSendHeartTime >= HeartSendTime)
                {
                    NetworkManager.GetInstance().SendPacket(MessageType.Heart);
                    LastSendHeartTime -= HeartSendTime;
                }
            }
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

        public void Zoom(int topLayer, int curLayer)
        {
            curLayer = Mathf.Clamp(curLayer, 2, topLayer);
            int level = topLayer - curLayer;
            var newFov = initFOV - level * 3;
            if (mainCamera.fieldOfView != newFov)
            {
                //mainCamera.fieldOfView = newFov;
                //mainCamera.transform.Translate(new Vector3(0, -0.2f, 0));

                DOTween.To(() => mainCamera.fieldOfView, r => mainCamera.fieldOfView = r, newFov, 2f).SetEase(Ease.Linear);
                var newPos = mainCamera.transform.position + new Vector3(0, -0.2f, 0);
                DOTween.To(() => mainCamera.transform.position, r => mainCamera.transform.position = r, newPos, 2f).SetEase(Ease.Linear);
            }
        public void OnStartGame(G2CGameBegin gameBegin)
        {
            m_start = true;
        }

        public void OnFrameOperation(G2CFrameOperation frameOp)
        {


        }

        public void OnMove()
        {


        }

        public void OnBomb()
        {


        }
    }
}


