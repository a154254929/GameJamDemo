using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public List<GameObject> gameBg;
    public GameObject winObj;
    public GameObject loseObj;

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
    }
}
