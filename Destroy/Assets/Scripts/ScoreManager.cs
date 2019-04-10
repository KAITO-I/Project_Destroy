using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]static int score;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void Scorecalc(GameObject obj)
    {
        CharaData cd = obj.GetComponent<CharaData>();
        if (cd.haveItem.Name == "None") return;
        cd.haveItem.HP--;
        if (cd.haveItem.HP == 0)
        {
            GameObject mi = cd.myItem;
            cd.myItem = null;
            mi.GetComponent<ItemFall>().GrabtyChange();
        }
    }
}
