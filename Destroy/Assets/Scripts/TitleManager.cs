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
        FadeManager.Instance.LoadScene("MainGame", 1f);
    }

    public void OnClickRanking()
    {

    }
}
