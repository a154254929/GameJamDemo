using GameJamDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public class GameStart : MonoBehaviour
    {
        public GameConfig config;

        void Start()
        {
            GameManager.Instance.SetConfig(config);
            GameManager.Instance.StartGame();
        }

        void Update()
        {
            GameManager.Instance.Update();
        }
    }
}

