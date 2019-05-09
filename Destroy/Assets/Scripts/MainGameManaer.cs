using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Mode
{
    Nomal,
    Tarako
}
public class MainGameManaer : MonoBehaviour
{
    CameraMove cm;
    [SerializeField]
    Animator startMsgAnim;
    static Mode NowMode;
    [SerializeField] GameObject gameoverText;
    [SerializeField] GameObject player;
    public bool started { private set; get; }
    private void Awake()
    {
        this.cm = GetComponent<CameraMove>();
        GetComponent<ScoreManager>().ScoreSet(0);
        gameoverText.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(OpeningAndStartGame());
        this.startMsgAnim.enabled = false;
        this.player.GetComponent<Unityちゃん_Move>().enabled = false;
        this.started = false;
    }

    private IEnumerator OpeningAndStartGame()
    {
        //カメラワーク
        var startCamera = this.cm.StartCamera();
        yield return StartCoroutine(startCamera);

        //ゲーム開始
        this.startMsgAnim.enabled = true;
        this.startMsgAnim.Play(0);
        this.player.GetComponent<Unityちゃん_Move>().enabled = true;
        this.started = true;
    }

    public void GameSet() {
        StartCoroutine(EndGameCorutine());
    }

    private IEnumerator EndGameCorutine()
    {
        gameoverText.SetActive(true);
        yield return new WaitForSeconds(5f);
        if(NowMode == Mode.Nomal)SceneController.Instance.Load("Result");
        else SceneController.Instance.Load("Title");
    }
    public static void ModeChange(Mode m)
    {
        NowMode = m;
    }
    public static Mode GetMode()
    {
        return NowMode;
    }
}