using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor; // デプロイ時にUnityEditorが入るとエラーになるからUNITY_EDITORで括る
#endif

public class Opening2 : MonoBehaviour
{
    [SerializeField] float animationTime;
    [SerializeField] Transform sphere;
    [SerializeField] Vector2 sphereStartPos, sphereEndPos;

    private bool skip;

    private void Awake()
    {
        this.skip = false;

        // 初期位置設定
        this.sphere.position = new Vector3(this.sphereStartPos.x, this.sphereStartPos.y, 0f);
    }

    private void Start()
    {
        StartCoroutine(PlayOpeningAnimation());
    }

    //==============================
    // Inspector拡張
    //==============================
#if UNITY_EDITOR
    [CustomEditor(typeof(Opening2))]
    class OpeningEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Opening2 opening = target as Opening2;

            //===== カスタマイズ =====
            // 制御部分
            opening.animationTime = EditorGUILayout.FloatField("各時間", opening.animationTime);

            EditorGUILayout.Space();

            // 破壊者(プレイヤー)
            EditorGUILayout.LabelField("Sphere");
            opening.sphere = EditorGUILayout.ObjectField("球体", opening.sphere, typeof(Transform), true) as Transform;
            opening.sphereStartPos = EditorGUILayout.Vector2Field("開始地点", opening.sphereStartPos);
            opening.sphereEndPos = EditorGUILayout.Vector2Field("終了地点", opening.sphereEndPos);
        }
    }
#endif

    //==============================
    // アニメーション
    //==============================
    //===== スキップ =====
    public void SkipOpeningAnimation()
    {
        this.skip = true;
    }

    //===== オープニング用 =====
    public IEnumerator PlayOpeningAnimation()
    {
        float time, speed, centerTime;

        time = 0f;
        speed = Time.deltaTime;
        centerTime = this.animationTime / 2;
        while (time <= this.animationTime)
        {
            Debug.Log(speed);
            time += Time.deltaTime;
            speed = (time < centerTime) ? Mathf.Lerp(0.0f, 1.0f, time / centerTime) : Mathf.Lerp(1.0f, 0.0f, time / this.animationTime);
            this.sphere.position = Vector3.Lerp(this.sphereStartPos, this.sphereEndPos, time / this.animationTime);
            Instantiate(this.sphere);
            yield return null;
        }
    }

    //===== メインUI表示用 =====
    public IEnumerator PlayDisplayMainUIAnimation()
    {
        yield return null;
    }
}

