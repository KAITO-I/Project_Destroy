using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ranking : MonoBehaviour
{
    public int nowScore;
    public int myscore;
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
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
       nowScore = ScoreManager.GetScore();
        if (test)
        {
            GetKariScore();
        }
        myscore = nowScore;

        GetHighScore();
        //        ScoreSet();
        ScoreCalc();
        RankSave();
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        if (Rankin)
        {
            time += Time.deltaTime * timebo;
            FlashText();
        }
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
    void ScoreCalc()
    {
        int a = 0;
        for (int i = 0; i < ranknum; i++)
        {
            if (rankingi[i] < Mathf.Abs(nowScore))
            {
                a = rankingi[i];
                rankingi[i] = nowScore;
                nowScore = a;
                if (Rankin == false)
                {
                    rankinnum = i;
                    Rankin = true;

                }
            }
        }
    }
    void RankSave()
    {
        string newRank = rankingi[0].ToString() + "," + rankingi[1].ToString() + "," + rankingi[2].ToString() + "," + rankingi[3].ToString() + "," + rankingi[4].ToString();
        PlayerPrefs.SetString("rankingS", newRank);
        PlayerPrefs.SetString("IsFirstS", IsFirst);
        PlayerPrefs.Save();
    }
    void Display()
    {
        ScoreText.text = "被害総額: " + myscore.ToString() + "円";
        for (int i = 0; i < rankingi.Length; i++)
        {
            rankingText[i].text = (i + 1) + "位 ：" + (rankingi[i]) + "円";
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
    void GetKariScore()
    {
        nowScore = kariScore;
    }
    void FlashText()
    {
        Color inText = rankingText[rankinnum].color;
        inText.a = Mathf.Sin(time) * 0.5f + 0.5f;
        rankingText[rankinnum].color = inText;
    }
    public void Totitle()
    {
        SceneController.Instance.Load("Title");
    }
}
