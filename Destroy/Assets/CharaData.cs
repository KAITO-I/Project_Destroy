using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData : MonoBehaviour
{
    static int score;
    public static int GetScore()
    {
        return score;
    }
    //スコアのセット
    public void SetScore(int num)
    {
        score = num;
    }
    //スコアの加算
    public void AddScore(int num)
    {
        score += num;
    }
}
