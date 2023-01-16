using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamDemo
{
    public class MainUI : MonoBehaviour
    {
        public Button startButton;
        public GameObject mainUI;
        public GameObject connecttingObj;
        private AudioManager audioManager = GameManager.Instance.audioManager;

        void Start()
        {
            startButton.onClick.AddListener(OnClickStart);
            SetStartButtonActive(false);
        }

        void OnClickStart()
        {
            mainUI.SetActive(false);
            GameManager.Instance.StartGame();
            audioManager.PlayBGM(1);
        }

        public void SetStartButtonActive(bool active)
        {
            startButton.gameObject.SetActive(active);
            connecttingObj.SetActive(!active);
        }
    }
}

