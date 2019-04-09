using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;

        GameObject.Find("Canvas/MainUI/StartButton").GetComponent<Button>().Select();
    }

    public void OnClickStart()
    {
        SceneController.Instance.Load("MainGame");
    }

    public void OnClickRanking()
    {
        SceneController.Instance.Load("Ranking");
    }

    public void OnClickEnd()
    {
        UnityEngine.Application.Quit();
    }
}
