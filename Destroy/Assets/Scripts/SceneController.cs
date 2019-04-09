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

    private AsyncOperation async;
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
        this.isFading = true;

        //だんだん暗く
        float time = 0f;
        while (time <= this.fadeInTime)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / this.fadeInTime);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切り替え
        this.async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }

        //だんだん明るく
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
