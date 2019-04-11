using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Animation
    [SerializeField] Animation openingAnim;

    // Button(UI)
    private int selectedMenuNum;
    private Dictionary<int, GameObject> menuDic;
    private bool selected;

    // Audio
    private AudioSource audio;
    [SerializeField] AudioClip menuMoveSE;
    [SerializeField] AudioClip pushStartButtonSE;
    [SerializeField] AudioClip pushButtonSE;

    // 隠しコマンド
    [SerializeField] bool hiddenCommand;
    private KeyCode[] command;
    private int successLength;

    //==============================
    // 初期化
    //==============================
    private void Awake()
    {
        Cursor.visible = false;

        this.selectedMenuNum = 0;
        this.menuDic = new Dictionary<int, GameObject>()
        {
            { 0, GameObject.Find("Canvas/UI/StartButton") },
            { 1, GameObject.Find("Canvas/UI/RankingButton") },
            { 2, GameObject.Find("Canvas/UI/EndButton") }
        };
        this.selected = false;

        this.audio = GetComponent<AudioSource>();

        this.command = new KeyCode[]
        {
            KeyCode.UpArrow, KeyCode.UpArrow,
            KeyCode.DownArrow, KeyCode.DownArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.B, KeyCode.A,
        };
        this.successLength = -1;
    }

    void Update()
    {
        if (!this.selected)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && this.selectedMenuNum < this.menuDic.Count - 1)
            {
                this.selectedMenuNum++;
                UpdateMenu();
            }
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && this.selectedMenuNum > 0)
            {
                this.selectedMenuNum--;
                UpdateMenu();
            }

            if (Input.GetKeyDown(KeyCode.Space)) SelectMenu();

            for (int i = 0; i < this.command.Length; i++)
            {
                if (Input.GetKeyDown(this.command[i]))
                {
                    Command();
                    break;
                }
            }
        }
    }

    //==============================
    // メニュー更新
    //==============================
    private void UpdateMenu()
    {
        for (int i = 0; i < this.menuDic.Count; i++)
        {
            if (i == this.selectedMenuNum)
                this.menuDic[i].GetComponent<TitleUIManager>().SetSelecting(true);
            else
                this.menuDic[i].GetComponent<TitleUIManager>().SetSelecting(false);
        }
        PlaySound(this.menuMoveSE);
    }

    //==============================
    // メニュー選択
    //==============================
    private IEnumerator SelectMenu()
    {
        this.selected = true;
        TitleUIManager menuUI = this.menuDic[this.selectedMenuNum].GetComponent<TitleUIManager>();
        menuUI.SetSelecting(false);
        menuUI.Selected();
        PlaySound(this.selectedMenuNum == 0 ? this.pushStartButtonSE : this.pushButtonSE);

        yield return new WaitForSeconds(0.5f);

        switch (this.selectedMenuNum)
        {
            case 0:
                SceneController.Instance.Load("jam");
                break;

            case 1:
                SceneController.Instance.Load("Ranking");
                break;

            case 2:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
                UnityEngine.Application.Quit();
#endif
                break;
        }
    }

    //==============================
    // 再生
    //==============================
    private void PlaySound(AudioClip sound)
    {
        this.audio.clip = sound;
        this.audio.Play();
    }

    //==============================
    // 隠しコマンド
    //==============================
    private void Command()
    {
        for (int i = 0; i < this.command.Length; i++)
        {
            if (this.successLength != i - 1) continue;

            if (Input.GetKeyDown(command[i]))
            {
                this.successLength = i;
                //if (this.successLength == this.command.Length - 1) SceneController.Instance.Load("TarakoTitle");
                if (this.successLength == this.command.Length - 1) Debug.Log("読込");
                Debug.Log("OK");
            }
            else
            {
                this.successLength = -1;
                if (Input.GetKeyDown(command[0])) this.successLength = 0;
                Debug.Log("NG");
            }
            break;
        }
    }
}
