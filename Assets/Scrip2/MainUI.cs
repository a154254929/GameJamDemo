using NetWorkFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamDemo
{
    public class MainUI : MonoBehaviour
    {
        public Button singleStartButton;
        public Button multiStartButton;

        public GameObject mainUI;
        public GameObject connecttingObj;
        private AudioManager audioManager = GameManager.Instance.audioManager;

        void Start()
        {
            singleStartButton.onClick.AddListener(OnClickSingleStart);
            multiStartButton.onClick.AddListener(OnClickMultiStart);
        }

        void OnClickSingleStart()
        {
            HideMainUI();
            GameManager.Instance.SingleStartGame();
        }

        void OnClickMultiStart()
        {
            //点击多人游戏
            //连接服务器，显示连接中，成功后 变更为进入游戏
            singleStartButton.gameObject.SetActive(false);
            multiStartButton.gameObject.SetActive(false);
            connecttingObj.gameObject.SetActive(true);
            NetworkManager.GetInstance().StartGame();
        }

        /// <summary>
        /// 进入游戏后，隐藏主界面UI
        /// </summary>
        public void HideMainUI()
        {
            mainUI.SetActive(false);
            audioManager.PlayBGM(1);
        }

        public void ResetMainUI()
        {
            mainUI.SetActive(true);
            audioManager.PlayBGM(0);
            singleStartButton.gameObject.SetActive(true);
            multiStartButton.gameObject.SetActive(true);
            connecttingObj.gameObject.SetActive(false);
        }
    }
}

