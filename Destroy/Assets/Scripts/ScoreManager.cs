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
        //scoreTxt.text = score.ToString();
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
        id.HP--;
        if (id.HP == 0)
        {
            GameObject mi = cd.myItem;
            cd.myItem = null;
            mi.GetComponent<ItemFall>().GrabtyChange();
        }
    }
}
