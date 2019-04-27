using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField]static int score;
    [SerializeField] Text scoreTxt;
    public 
    // Start is called before the first frame update
    void Update()
    {
        scoreTxt.text = score.ToString() + "円";
    }
    //スコアの初期設定
    public void ScoreSet(int sc)
    {
        score = sc;
    }
    //スコアの取得
    public static int GetScore()
    {
        return score;
    }
    public void Scorecalc(GameObject obj,ItemData id)
    {
        CharaData cd = obj.GetComponent<CharaData>();
        score += id.Price;
        GameObject mi = cd.myItem;
        cd.myItem = null;
        if (id.audio == null) return;
        mi.GetComponent<ItemFall>().GrabtyChange(id.audio);
    }
}
