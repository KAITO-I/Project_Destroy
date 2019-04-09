using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;

    public static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SceneController) FindObjectOfType(typeof(SceneController));
                if (instance == null) Debug.LogError(typeof(SceneController) + "がシーン上にありません！");
            }
            return instance;
        }
    }

    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    private float fadeAlpha = 0f;
    private bool isFading = false;
    private Color fadeColor = Color.black;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnGUI()
    {
        //フェード
        if (this.isFading)
        {
            //色と透明度を更新して白テクスチャを描画
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    public void Load(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    private IEnumerator LoadScene(string scene)
    {
        //フェード開始
        this.isFading = true;

        //徐々に暗転
        float time = 0f;
        while (time <= this.fadeInTime)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / this.fadeInTime);
            time += Time.deltaTime;
            yield return 0;
        }

        //Loadingシーン
        SceneManager.LoadScene("Loading");
        yield return new WaitForSeconds(1f / 60f);

        //明転
        this.fadeAlpha = 0f;

        //呼び出し
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        while (async.progress < 0.9f) yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);

        //暗転
        this.fadeAlpha = 1f;
        async.allowSceneActivation = true;

        //徐々に明転
        time = 0f;
        while (time <= this.fadeOutTime)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / this.fadeOutTime);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}