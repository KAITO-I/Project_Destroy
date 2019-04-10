using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingGet : MonoBehaviour
{
    public int nowScore;
    //    private int HighScore = 0;
    public int kariScore = 0;
    string rankings;
    int ranknum = 5;
    int[] rankingi = { 1, 1, 1, 1, 1 };
    string[] rank = { "" };
    string IsFirst;
    bool Rankin;
    int rankinnum;
    float time = 0.0f;
    public float timebo = 0.5f;
    public bool test = false;
    public Text[] rankingText;
    private void Start()
    {
        GetHighScore();
        Display();
    }
    void GetHighScore()//スコアアタックモード
    {
        IsFirst = PlayerPrefs.GetString("IsFirstS");
        if (IsFirst != "true")
        {
            ScoreSet();
            IsFirst = "true";
        }
        else
        {
            //         HighScore = PlayerPrefs.GetInt("scoreS");
            rankings = PlayerPrefs.GetString("rankingS", "1");
            string[] rank = rankings.Split(","[0]);
            if (rankings.Length > 0)
            {
                for (int i = 0; i < rank.Length && i < ranknum; i++)
                {
                    rankingi[i] = System.Convert.ToInt32(rank[i]);
                }
            }
        }
    }
    void ScoreSet()
    {
        //        HighScore = 0;
        for (int i = 0; i < ranknum; i++)
        {
            rankingi[i] = 0;
        }
    }
    void Display()
    {
        for (int i = 0; i < rankingi.Length; i++)
        {
            rankingText[i].text = (i + 1) + "位 ：" + (rankingi[i]) + "円";
        }
    }
}
