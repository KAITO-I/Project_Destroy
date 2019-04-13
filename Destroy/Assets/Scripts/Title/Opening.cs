using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Opening : MonoBehaviour
{
    [SerializeField] float animationSpeed;
    [SerializeField] int waitFrame;
    [SerializeField] AudioClip entrySE;
    private Animation destroyer;
    private Animation citizen;
    private Animation title;

    [SerializeField] int flashMaxFrame;
    [SerializeField] AudioClip titleBGM;
    private Image panel;
    private GameObject openingAnimation;
    private GameObject mainUI;

    private bool skip;

    //==============================
    // inspector拡張
    //==============================
    [CustomEditor(typeof(Opening))]
    public class OpeningEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Opening opening = target as Opening;

            //===== カスタマイズ =====
            // オープニングアニメーション
            EditorGUILayout.LabelField("オープニングアニメーション");
            opening.animationSpeed = EditorGUILayout.FloatField("速度", opening.animationSpeed);
            opening.waitFrame      = EditorGUILayout.IntField("待機間隔フレーム", opening.waitFrame);
            opening.entrySE        = EditorGUILayout.ObjectField("SE", opening.entrySE, typeof(AudioClip), true) as AudioClip;

            EditorGUILayout.Space();

            // メインUI表示アニメーション
            EditorGUILayout.LabelField("メインUI表示アニメーション");
            opening.flashMaxFrame = EditorGUILayout.IntField("フラッシュ最大フレーム", opening.flashMaxFrame);
            opening.titleBGM      = EditorGUILayout.ObjectField("タイトルBGM", opening.titleBGM, typeof(AudioClip), true) as AudioClip;
        }
    }

    private void Awake()
    {
        this.destroyer = GameObject.Find("Canvas/OpeningAnimation/Destroyer").GetComponent<Animation>();
        this.citizen   = GameObject.Find("Canvas/OpeningAnimation/Citizen").GetComponent<Animation>();
        this.title     = GameObject.Find("Canvas/OpeningAnimation/Title").GetComponent<Animation>();

        this.panel            = GameObject.Find("Canvas/Panel").GetComponent<Image>();
        this.panel.color      = new Color(255.0f, 255.0f, 255.0f, 0f);
        (this.openingAnimation = GameObject.Find("Canvas/OpeningAnimation")).SetActive(true);
        (this.mainUI           = GameObject.Find("Canvas/Main")).SetActive(false);

        this.skip = false;
    }

    public void Updated()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.skip = true;
            StopCoroutine(PlayOpeningAnimation());
            StartCoroutine(DisplayMainUI());
        }
    }

    //==============================
    // オープニングアニメーション
    //==============================
    public IEnumerator PlayOpeningAnimation()
    {
        // 破壊者の登場
        this.destroyer["Opening_Destroyer"].speed = this.animationSpeed;
        this.destroyer.Play();
        SoundManager.Instance.PlaySE(this.entrySE);
        while (this.destroyer.isPlaying) yield return null;
        yield return new WaitForSeconds(Time.deltaTime * waitFrame);

        // 市民の登場
        if (!this.skip)
        {
            this.citizen["Opening_Citizen"].speed = this.animationSpeed;
            this.citizen.Play();
            SoundManager.Instance.PlaySE(this.entrySE);
            while (this.citizen.isPlaying) yield return null;
            yield return new WaitForSeconds(Time.deltaTime * waitFrame);
        }

        // タイトルの表示
        if (!this.skip)
        {
            this.title["Opening_Title"].speed = this.animationSpeed;
            this.title.Play();
            SoundManager.Instance.PlaySE(this.entrySE);
            while (this.title.isPlaying) yield return null;
            yield return new WaitForSeconds(Time.deltaTime * waitFrame);
        }

        // メインUIに切替
        if (!this.skip)
            StartCoroutine(DisplayMainUI());
    }

    //==============================
    // メインUI表示
    //==============================
    public IEnumerator DisplayMainUI()
    {
        GetComponent<TitleManager>().status = TitleStatus.None;

        float time = 0f;
        while (time <= Time.deltaTime * flashMaxFrame)
        {
            time += Time.deltaTime;
            this.panel.color = new Color(255.0f, 255.0f, 255.0f, Mathf.Lerp(0.0f, 1.0f, time / (Time.deltaTime * flashMaxFrame)));
            yield return null;
        }

        SoundManager.Instance.PlayBGM(this.titleBGM);
        this.openingAnimation.SetActive(false);
        this.mainUI.SetActive(true);

        time = 0f;
        while (time <= Time.deltaTime * flashMaxFrame) {
            time += Time.deltaTime;
            this.panel.color = new Color(255.0f, 255.0f, 255.0f, Mathf.Lerp(1.0f, 0.0f, time / (Time.deltaTime * flashMaxFrame)));
            yield return null;
        }

        GetComponent<TitleManager>().status = TitleStatus.Title;
    }
}

