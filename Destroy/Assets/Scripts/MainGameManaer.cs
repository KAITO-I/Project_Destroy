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
    [SerializeField] float ToRedTime;
    [SerializeField] Light light;
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
        started = false;
        this.light.color = new Color(255.0f / 255.0f, 244.0f / 255.0f, 214.0f / 255.0f, 1.0f);
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
        if (NowMode == Mode.Nomal) started = true;
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

    public void StartRun()
    {
        started = true;
        if (NowMode == Mode.Tarako) StartCoroutine(LightColorToRed());
    }

    private IEnumerator LightColorToRed()
    {
        //ライトを赤く
        float time = 0f;
        while (time <= this.ToRedTime)
        {
            time += Time.deltaTime;
            float r = Mathf.Lerp(255.0f / 255.0f, 161.0f / 255.0f, time / this.ToRedTime);
            float g = Mathf.Lerp(244.0f / 255.0f, 12.0f / 255.0f, time / this.ToRedTime);
            float b = Mathf.Lerp(214.0f / 255.0f, 5.0f / 255.0f, time / this.ToRedTime);
            this.light.color = new Color(r, g, b, 1.0f);
            yield return null;
        }
    }
}