using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private int selectNum;
    private Dictionary<int, GameObject> buttonDic;

    void Start()
    {
        Cursor.visible = false;

        this.selectNum = 0;
        this.buttonDic = new Dictionary<int, GameObject>();
        this.buttonDic.Add(0, GameObject.Find("Canvas/TitleOp/MainUI/StartButton"));
        this.buttonDic.Add(1, GameObject.Find("Canvas/TitleOp/MainUI/RankingButton"));
        this.buttonDic.Add(2, GameObject.Find("Canvas/TitleOp/MainUI/EndButton"));

        this.buttonDic[0].GetComponent<TitleUIManager>().SetSelecting(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (this.selectNum < 2)
            {
                this.selectNum++;
                this.buttonDic[selectNum].GetComponent<TitleUIManager>().SetSelecting(true);
                this.buttonDic[selectNum - 1].GetComponent<TitleUIManager>().SetSelecting(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (this.selectNum > 0)
            {
                this.selectNum--;
                this.buttonDic[selectNum].GetComponent<TitleUIManager>().SetSelecting(true);
                this.buttonDic[selectNum + 1].GetComponent<TitleUIManager>().SetSelecting(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (this.selectNum)
            {
                case 0:
                    StartCoroutine(OnClickStart());
                    break;

                case 1:
                    StartCoroutine(OnClickRanking());
                    break;

                case 2:
                    StartCoroutine(OnClickEnd());
                    break;
            }
        }
    }

    private IEnumerator OnClickStart()
    {
        TitleUIManager button = this.buttonDic[0].GetComponent<TitleUIManager>();
        button.SetSelecting(false);
        button.Selected();
        yield return new WaitForSeconds(0.5f);
        SceneController.Instance.Load("MainGame");
    }

    public IEnumerator OnClickRanking()
    {
        TitleUIManager button = this.buttonDic[1].GetComponent<TitleUIManager>();
        button.SetSelecting(false);
        button.Selected();
        yield return new WaitForSeconds(0.5f);
        SceneController.Instance.Load("Ranking");
    }

    public IEnumerator OnClickEnd()
    {
        TitleUIManager button = this.buttonDic[2].GetComponent<TitleUIManager>();
        button.SetSelecting(false);
        button.Selected();
        yield return new WaitForSeconds(0.5f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }
}
