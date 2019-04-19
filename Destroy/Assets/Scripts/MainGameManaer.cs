using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGameManaer : MonoBehaviour
{
    CameraMove cm;
    [SerializeField]
    Animator startMsgAnim;

    [SerializeField] GameObject gameoverText;
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
    }

    private IEnumerator OpeningAndStartGame()
    {
        //カメラワーク
        var startCamera = this.cm.StartCamera();
        yield return StartCoroutine(startCamera);

        //ゲーム開始
        this.startMsgAnim.enabled = true;
        this.startMsgAnim.Play(0);
    }

    public void GameSet() {
        StartCoroutine(EndGameCorutine());
    }

    private IEnumerator EndGameCorutine()
    {
        gameoverText.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneController.Instance.Load("Result");
    }
}