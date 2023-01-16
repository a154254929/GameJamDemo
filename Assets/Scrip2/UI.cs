using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamDemo
{
    public class UI : MonoBehaviour
    {
        public Button startButton;
        public GameObject mainUI;
        private AudioManager audioManager = GameManager.Instance.audioManager;

        void Start()
        {
            startButton.onClick.AddListener(OnClickStart);
        }

        void OnClickStart()
        {
            mainUI.SetActive(false);
            GameManager.Instance.StartGame();
            audioManager.PlayBGM(1);
        }
    }
}

