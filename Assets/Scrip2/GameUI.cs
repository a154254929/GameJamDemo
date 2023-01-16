using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamDemo
{
    public class GameUI : MonoBehaviour
    {
        public List<GameObject> gameBg;
        public GameObject winObj;
        public GameObject loseObj;
        public Button goBackBtn;

        private void Start()
        {
            goBackBtn.onClick.AddListener(OnClickGoBack);
        }

        public void ShowGameBg(int index)
        {
            for (int i = 0; i < gameBg.Count; i++)
            {
                gameBg[i].SetActive(false);
                if (i == index)
                {
                    gameBg[i].SetActive(true);
                }
            }
        }

        public void ShowResult(bool win)
        {
            winObj.SetActive(win);
            loseObj.SetActive(!win);
            goBackBtn.gameObject.SetActive(true);
        }

        public void OnClickGoBack()
        {
            GameManager.Instance.ResetAllGame();
            winObj.SetActive(false);
            loseObj.SetActive(false);
            goBackBtn.gameObject.SetActive(false);
        }
    }
}

