using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TownGenerate : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;

    //マップ全体のサイズ
    int MapSizeX = 31;
    int MapSizeY = 31;

    //マップチップのサイズ
    public int objectscale = 1;

    //大通りのサイズ
    public int StreetSize = 3;

    //マップを構成する二次配列map
    int[,] map;
    int[,] town_L, town_R;

    // Start is called before the first frame update
    void Start()
    {
        //map配列の大きさを決定
        map = new int[MapSizeX, MapSizeY];

        //初期化
        for (int i = 0; i < MapSizeY; i++)
        {
            for (int j = 0; j < MapSizeX; j++)
            {
                map[i, j] = 0;
            }
        }

        //中央と外周の大通りを作成
        for (int i = 0; i < MapSizeY; i++)
        {
            for (int j = 0; j < MapSizeX; j++)
            {
                if (i < StreetSize || j < StreetSize) map[i, j] = 0;
                else if (i > MapSizeY - StreetSize - 1 || j > MapSizeX - StreetSize - 1) map[i, j] = 0;
                else if (i >= (int)Mathf.Ceil(MapSizeX / 2) - Mathf.Ceil(StreetSize / 2) && i <= (int)Mathf.Ceil(MapSizeX / 2) + Mathf.Ceil(StreetSize / 2)) map[i, j] = 0;
                else map[i, j] = 1;
            }
        }

        //
        town_L = TownMake(town_L);
        town_R = TownMake(town_R);
        
        //マップチップの配置
        for (int i = 0; i < MapSizeY; i++)
        {
            for (int j = 0; j < MapSizeX; j++)
            {
                if (map[i, j] == 0) Instantiate(Floor, new Vector3(j * objectscale, 0, i * objectscale), Quaternion.Euler(0, 90, 0)).transform.localScale = new Vector3(objectscale, objectscale, objectscale);
                if (map[i, j] == 1) Instantiate(Wall, new Vector3(j * objectscale, 0, i * objectscale), Quaternion.Euler(0, 90, 0)).transform.localScale = new Vector3(objectscale, objectscale, objectscale);
            }
        }

    }

    //左右の街並みを生成
    int[,] TownMake(int[,] map)
    {
        int TownSizeX, TownSizeY;
        TownSizeX = (int)Mathf.Ceil(MapSizeX) - (int)Mathf.Ceil(StreetSize / 2) - StreetSize;
        TownSizeY= MapSizeY - StreetSize * 2;
        map = new int[TownSizeX, TownSizeY];

        //0なら横方向に直線の道を伸ばす
        if(Random.Range(0,2) == 0)
        {
            int pos = Random.Range(TownSizeY / 2 - 3, TownSizeY / 2 + 3);
            for (int i = 0; i < TownSizeX; i++) map[i, pos] = 0;
        }
        //1なら縦方向に直線の道を伸ばす
        else
        {
            int pos = Random.Range(TownSizeX / 2 - 3, TownSizeX / 2 + 3);
            for (int i = 0; i < TownSizeY; i++) map[pos, i] = 0;
        }

        return map;
    }
}