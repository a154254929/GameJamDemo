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
        public MainUI mainUI;
        public GameUI gameUI;
        public Camera mainCamera;

        private float HeartSendTime = 3.0f;
        private float LastSendHeartTime = .0f;
        private float initFOV;
        private Vector3 initCameraPostion;
        private bool readyToBegin = false;

        private int selfId;

        public GameManager()
        {
            mapManager = new MapManager();
            bombManager = new BombManager();
            blockExplodeMgr = new BlockExplodeMgr();
            audioManager = new AudioManager();
            readyToBegin = false;
        }

        private float m_timer = 0;
        private float m_bombTiemr = 0;
        private bool m_start = false;
        public bool SingleMode = false;

        public BasePlayer playerSelf;
        public BasePlayer playerOther;

        private Vector3Int[] startPos = new Vector3Int[4];
        private MoveDirection[] startDir = new MoveDirection[4];
        public void SetConfig(GameConfig config)
        {
            gameConfig = config;
        }

        public void InitGame()
        {
            audioManager.Init();
            mapManager.CreateMap(gameConfig.MapSize, gameConfig.BlockPrefab);
            mainUI = Transform.FindObjectOfType<MainUI>();
            gameUI = Transform.FindObjectOfType<GameUI>();
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            initFOV = mainCamera.fieldOfView;
            initCameraPostion = mainCamera.transform.position;

            startPos[0] = new Vector3Int(0, 0, gameConfig.MapSize.z - 1);
            startPos[1] = new Vector3Int(0, gameConfig.MapSize.y - 1, gameConfig.MapSize.z - 1);
            startPos[2] = new Vector3Int(gameConfig.MapSize.x - 1, 0, gameConfig.MapSize.z - 1);
            startPos[3] = new Vector3Int(gameConfig.MapSize.x - 1, gameConfig.MapSize.y - 1, gameConfig.MapSize.z - 1);
            startDir[0] = MoveDirection.Forward;
            startDir[1] = MoveDirection.Left;
            startDir[2] = MoveDirection.Right;
            startDir[3] = MoveDirection.BackWard;
        }

        public void JoinGame()
        {
            NetworkManager.GetInstance().SendPacket(MessageType.JoinGame);
            //m_start = true;
        }

        /// <summary>
        /// 重置整个游戏的状态，并回到主界面
        /// </summary>
        public void ResetAllGame()
        {
            SingleMode = false;
            m_start = false;
            mainUI.ResetMainUI();
            ResetCamera();
            mapManager.ResetAll();
            bombManager.Release();
            blockExplodeMgr.Release();
            if (playerSelf != null)
            {
                playerSelf.Release();
                playerSelf = null;
            }
            if (playerOther != null)
            {
                playerOther.Release();
                playerOther = null;
            }
        }

        public void SingleStartGame()
        {
            playerSelf = new PlayerSelf(startPos[0], startDir[0], gameConfig.player1Prfab);
            SingleMode = true;
            m_start = true;
        }

        public void StopGame()
        {
            m_start = false;
            bombManager.Release();
            blockExplodeMgr.Release();
        }

        public void CreaterSelfPlayer(int id)
        {
            playerSelf = new PlayerSelf(startPos[id], startDir[id], gameConfig.player1Prfab);
        }

        public void CreaterOtherPlayer(int id)
        {
            playerOther = new PlayerOther(startPos[id], startDir[id], gameConfig.player2Prfab);
        }

        /// <summary>
        /// 每秒钟触发一次，推进游戏
        /// </summary>
        public void Step()
        {
            bool otherGameOver = false;
            bool selfGameOver = playerSelf.Move();
            if (playerOther != null)
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
                audioManager.PlayCommonSE(audioManager.GetGameOverAudio());
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
            if (playerOther != null)
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
            if (readyToBegin)
            {
                Debug.LogWarning(selfId);
                mainUI.HideMainUI();
                CreaterSelfPlayer(selfId);
                CreaterOtherPlayer(1 - selfId);
                m_start = true;
                readyToBegin = false;
            }
            if (!m_start)
            {
                return;
            }

            KeyBoardInput();
            TouchInput();

            bombManager.OnUpdate();
            blockExplodeMgr.OnUpdate();

            m_timer += Time.deltaTime;
            m_bombTiemr += Time.deltaTime;

            if (m_bombTiemr >= gameConfig.PlayerReleaseBombTimeInterval)
            {
                SetBomb();
                //m_bombTiemr = 0;
                m_bombTiemr -= gameConfig.PlayerReleaseBombTimeInterval;
            }
            if (m_timer >= gameConfig.PlayerJumpTimeInterval)
            {
                Step();
                //m_timer = 0;
                m_timer -= gameConfig.PlayerJumpTimeInterval;
            }
        }

        /// <summary>
        /// 炸弹爆炸强制刷新玩家和炸弹高度
        /// </summary>
        public void UpdateAllHeight()
        {
            playerSelf.UpdateHeight();
            if (playerOther != null)
                playerOther.UpdateHeight();
            bombManager.UpdateAllBombHeight();
        }

        public void KeyBoardInput()
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

        public void TouchInput()
        {
            Vector2 touchDir = Vector2.zero;
            Vector2 startPos = Vector2.zero;

            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    startPos = Input.touches[0].position;
                }

                if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled)
                {
                    Vector2 endPos = Input.touches[0].position;

                    //判断手指移动
                    if (startPos.x > endPos.x)
                    {
                        touchDir.x = -1;
                    }
                    else
                    {
                        touchDir.x = 1;
                    }
                    if (startPos.y > endPos.y)
                    {
                        touchDir.y = -1;
                    }
                    else
                    {
                        touchDir.y = 1;
                    }
                }

                if (touchDir.x < 0 && touchDir.y > 0)
                {
                    playerSelf.SetDirection(MoveDirection.Forward);
                }
                else if (touchDir.x < 0 && touchDir.y < 0)
                {
                    playerSelf.SetDirection(MoveDirection.BackWard);
                }
                else if (touchDir.x < 0 && touchDir.y > 0)
                {
                    playerSelf.SetDirection(MoveDirection.Left);
                }
                else if (touchDir.x < 0 && touchDir.y < 0)
                {
                    playerSelf.SetDirection(MoveDirection.Right);
                }
            }
        }

        public void Zoom(int topLayer, int curLayer)
        {
            curLayer = Mathf.Clamp(curLayer, 2, topLayer);
            int level = topLayer - curLayer;
            var newFov = initFOV - level * 3;
            if (mainCamera.fieldOfView != newFov)
            {
                DOTween.To(() => mainCamera.fieldOfView, r => mainCamera.fieldOfView = r, newFov, 2f).SetEase(Ease.Linear);
                var newPos = mainCamera.transform.position + new Vector3(0, -0.2f, 0);
                DOTween.To(() => mainCamera.transform.position, r => mainCamera.transform.position = r, newPos, 2f).SetEase(Ease.Linear);
            }
        }

        public void ResetCamera()
        {
            mainCamera.fieldOfView = initFOV;
            mainCamera.transform.position = initCameraPostion;
        }

        public void OnStartGame(G2CGameBegin gameBegin)
        {
            readyToBegin = true;
        }

        public void OnFrameOperation(G2CFrameOperation frameOp)
        {
            for (int i = 0; i < frameOp.PlayerOpt.Count; ++i)
            {
                int id = frameOp.PlayerOpt[i].Id;
                int Dir = frameOp.PlayerOpt[i].Dir;
                //Debug.LogError("Player"+ id + "OtherDir" + Dir);
                if (id != selfId && Dir != 0)
                {
                    playerOther.SetDirection((MoveDirection)Dir);
                    break;
                }
            }
        }

        public void OnMove()
        {


        }

        public void OnBomb()
        {


        }

        public void OnConnectSuccess()
        {
            //mainUI.SetStartButtonActive(true);
            //自动join游戏
            JoinGame();
        }

        public void CancelFindMatch()
        {
            //取消匹配游戏
            NetworkManager.GetInstance().Release();
        }
    }
}


