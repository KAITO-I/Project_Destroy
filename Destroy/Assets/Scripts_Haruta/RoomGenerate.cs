using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[DefaultExecutionOrder(-103)]
public class RoomGenerate : MonoBehaviour {

    public GameObject cube;
    public GameObject plane;
    public GameObject grass;
    public GameObject spownPoint;
    public GameObject Player;
    public GameObject Gate;
    public GameObject ScoreBox;

    public int objectScale = 3;

    //マップ全体のサイズ
    [Range(10, 100)]
    public int MapSizeX = 50;
    [Range(10, 100)]
    public int MapSizeZ = 50;

    //一部屋の最小サイズ
    [Range(5, 100)]
    public int RoomSizeMinX = 5;
    [Range(5, 100)]
    public int RoomSizeMinZ = 5;

    //各方向に作る部屋の総数
    [Range(1, 10)]
    public int RoomValueX = 2;
    [Range(1, 10)]
    public int RoomValueZ = 2;

    //消す部屋の数
    [Range(0,10)]
    public int DeleteRoom = 2;

    //敵を出現させるオブジェクトの生成数
    public int SpownPoint = 2;

    public int MakeBox = 10;

	// Use this for initialization
	void Awake () {

        //マップを描く配列を作成
        //0…壁
        //1…床
        //2…敵スポーンポイント
        //3…プレイヤースポーンポイント
        //4…出口
        //5…宝箱
        int[,] Map = new int[MapSizeX, MapSizeZ];

        //部屋数とどこにどんな部屋を作るかを決める配列を作成
        //0…作成しない
        //1…通常の部屋(部屋以外何も作らない)
        //2…部屋+敵スポーンポイント
        //3…部屋+プレイヤースポーンポイント
        int[,] RandomRoomMake = new int[RoomValueX, RoomValueZ];

        //一部屋の最大サイズを決定
        int RoomSizeMaxX = Mathf.FloorToInt (MapSizeX / RoomValueX - 2);
        int RoomSizeMaxZ = Mathf.FloorToInt (MapSizeZ / RoomValueZ - 2);

        //一部屋の最小サイズが最大サイズを超えている場合に仮の値を作る
        if (RoomSizeMinX > RoomSizeMaxX) RoomSizeMinX = Mathf.FloorToInt (RoomSizeMaxX / 2);
        if (RoomSizeMinZ > RoomSizeMaxZ) RoomSizeMinZ = Mathf.FloorToInt (RoomSizeMaxZ / 2);

        //消す部屋の数が生成する部屋数以上の場合に仮の値を作る
        if (DeleteRoom >= RoomValueX * RoomValueZ) DeleteRoom = 2;


        //Map配列の初期化
        for (int z = 0; z < MapSizeZ; z++)
        {
            for (int x = 0; x < MapSizeX; x++) Map[x, z] = 0;
        }

        //RandomRoomMake配列の初期化
        for (int z = 0; z < RoomValueZ; z++)
        {
            for (int x = 0; x < RoomValueX; x++) RandomRoomMake[x, z] = 1;
        }

        if (DeleteRoom > 0)
        {
            int i = 0;
            do
            {
                int x = 0;
                int z = 0;

                x = Random.Range(0, RoomValueX);
                z = Random.Range(0, RoomValueZ);

                //RandomRoomMake配列から1つを選んで無効化する(無効化された場所は部屋が作られない)
                if (RandomRoomMake[x, z] == 1)
                {
                    RandomRoomMake[x, z] = 0;
                    i++;
                }
            } while (i < DeleteRoom);
        }

        if(SpownPoint > 0)
        {
            int i = 0;
            do
            {
                int x = Random.Range(0, RoomValueX);
                int z = Random.Range(0, RoomValueZ);

                if(RandomRoomMake[x,z] == 1)
                {
                    RandomRoomMake[x, z] = 2;
                    i++;
                }
            } while (i < SpownPoint);
        }

        bool playerSpown = false;
        do
        {
            int x = Random.Range(0, RoomValueX);
            int z = Random.Range(0, RoomValueZ);

            if (RandomRoomMake[x, z] == 1)
            {
                RandomRoomMake[x, z] = 3;
                playerSpown = true;
            }
        } while (playerSpown == false);


        //マップ作成開始
        for (int z = 0;z < RoomValueZ; z++)
        {
            for (int x = 0; x < RoomValueX; x++)
            {
                int room = 0;

                if (x == 0) room++;
                else if (RandomRoomMake[x - 1, z] == 0) room++;
                if (x == RoomValueX - 1) room++;
                else if (RandomRoomMake[x + 1, z] == 0) room++;
                if (z == 0) room++;
                else if (RandomRoomMake[x, z - 1] == 0) room++;
                if (z == RoomValueZ - 1) room++;
                else if (RandomRoomMake[x, z + 1] == 0) room++;

                if (room >= 4) RandomRoomMake[x, z] = 0;


                //もし部屋を作る場所なら作成する
                if (RandomRoomMake[x, z] != 0)
                {
                    //部屋のサイズを決定する
                    int roomSizeX = Random.Range(RoomSizeMinX, RoomSizeMaxX);
                    int roomSizeZ = Random.Range(RoomSizeMinZ, RoomSizeMaxZ);

                    //部屋作成の開始位置を決定する
                    int startPosX = Random.Range((MapSizeX / RoomValueX) * x + 2, (MapSizeX / RoomValueX) * (x + 1) - roomSizeX - 2);
                    int startPosZ = Random.Range((MapSizeZ / RoomValueZ) * z + 2, (MapSizeZ / RoomValueZ) * (z + 1) - roomSizeZ - 2);

                    //部屋を作る
                    for (int i = 0; i < roomSizeZ; i++)
                    {
                        for (int j = 0; j < roomSizeX; j++)Map[startPosX + j, startPosZ + i] = 1;
                    }

                    if (RandomRoomMake[x, z] == 2)
                    {
                        bool spownMake = false;
                        do
                        {
                        
                            int spownX = Random.Range(startPosX, startPosX + roomSizeX);
                            int spownZ = Random.Range(startPosZ, startPosZ + roomSizeZ);
                            int count = 0;
                            for(int i = spownZ - 2; i <= spownZ + 2; i++)
                            {
                                for (int j = spownX - 2; j <= spownX + 2; j++)
                                {
                                    if (Map[j, i] == 0) count++;
                                }
                            }

                            if (count < 5)
                            {
                                Map[spownX, spownZ] = 2;
                                spownMake = true;
                            }
                        } while (spownMake != true);
                        
                    }

                    if (RandomRoomMake[x, z] == 3)
                    {
                        bool playerSpownPoint = false;
                        do
                        {
                            int spownX = Random.Range(startPosX, startPosX + roomSizeX);
                            int spownZ = Random.Range(startPosZ, startPosZ + roomSizeZ);
                            int count = 0;
                            for (int i = spownZ - 2; i <= spownZ + 2; i++)
                            {
                                for (int j = spownX - 2; j <= spownX + 2; j++)
                                {
                                    if (Map[j, i] == 0) count++;
                                }
                            }

                            if (count < 5)
                            {
                                Map[spownX, spownZ] = 3;
                                playerSpownPoint = true;
                            }
                        } while (playerSpownPoint != true);
                    }


                    //部屋が左端でなく左側に部屋があるなら左側(-x側)へ道を伸ばす
                    if (x != 0)
                    {
                        if (RandomRoomMake[x - 1, z] != 0)
                        {
                            int randomRoad = Random.Range(startPosZ, startPosZ + roomSizeZ);
                            for (int a = MapSizeX / RoomValueX * x; a < startPosX; a++)
                            {
                                Map[a, randomRoad] = 1;
                            }
                        }
                    }

                    //部屋が右端でなく右側に部屋があるなら右側(+x側)へ道を伸ばす
                    if (x != RoomValueX - 1)
                    {
                        if (RandomRoomMake[x + 1, z] != 0)
                        {
                            int randomRoad = Random.Range(startPosZ, startPosZ + roomSizeZ);
                            for (int a = startPosX + roomSizeX; a < (MapSizeX / RoomValueX) * (x + 1) + 1; a++)
                            {
                                Map[a, randomRoad] = 1;
                            }
                        }
                    }

                    //下側(-z側)へ道を伸ばす
                    if (z != 0)
                    {
                        if (RandomRoomMake[x, z - 1] != 0)
                        {
                            int randomRoad = Random.Range(startPosX, startPosX + roomSizeX);
                            for (int a = MapSizeZ / RoomValueZ * z; a < startPosZ; a++)
                            {
                                Map[randomRoad, a] = 1;
                            }
                        }
                    }

                    //上側(+z側)へ道を伸ばす
                    if (z != RoomValueZ - 1)
                    {
                        if (RandomRoomMake[x, z + 1] != 0)
                        {
                            int randomRoad = Random.Range(startPosX, startPosX + roomSizeX);
                            for (int a = startPosZ + roomSizeZ; a < (MapSizeZ / RoomValueZ) * (z + 1) + 1; a++)
                            {
                                Map[randomRoad, a] = 1;
                            }
                        }
                    }
                }
            }
        }


        //道を伸ばして繋げる
        for (int z = 0; z < RoomValueZ; z++)
        {
            for (int x = 0; x < RoomValueX; x++)
            {
                //道の元となる箇所を数える変数
                int roadCont = 0;

                //区切った線を走査し、道の元があればカウントする
                for (int a = MapSizeX / RoomValueX * x; a < MapSizeX / RoomValueX * (x + 1); a++)
                {
                    if (Map[a, MapSizeZ / RoomValueZ * z] == 1) roadCont++;
                }

                //道の作成を許可するフラグ
                bool RoadMake = false;

                //道の元が2つあればそれをつなげる
                if (roadCont == 2)
                {
                    for (int a = MapSizeX / RoomValueX * x; a < MapSizeX / RoomValueX * (x + 1); a++)
                    {
                        if (Map[a, MapSizeZ / RoomValueZ * z] == 1 && RoadMake == false) RoadMake = true;
                        else if (Map[a, MapSizeZ / RoomValueZ * z] == 1 && RoadMake == true) RoadMake = false;
                        if (Map[a, MapSizeZ / RoomValueZ * z] == 0 && RoadMake == true) Map[a, MapSizeZ / RoomValueZ * z] = 1;
                    }
                }

                roadCont = 0;

                for (int a = MapSizeZ / RoomValueZ * z; a < MapSizeZ / RoomValueZ * (z + 1); a++)
                {
                    if (Map[MapSizeX / RoomValueX * x, a] == 1) roadCont++;
                }

                RoadMake = false;

                if (roadCont == 2)
                {
                    for (int a = MapSizeZ / RoomValueZ * z; a < MapSizeZ / RoomValueZ * (z + 1); a++)
                    {
                        if (Map[MapSizeX / RoomValueX * x, a] == 1 && RoadMake == false) RoadMake = true;
                        else if (Map[MapSizeX / RoomValueX * x, a] == 1 && RoadMake == true) RoadMake = false;
                        if (Map[MapSizeX / RoomValueX * x, a] == 0 && RoadMake == true) Map[MapSizeX / RoomValueX * x, a] = 1;
                    }
                }
            }
        }


        //出口を作る
        bool exit = false;
        do {
            int roadcount = 0;
            int exitX = Random.Range(1, MapSizeX - 1);
            int exitZ = Random.Range(1, MapSizeZ - 1);

            if (Map[exitX, exitZ] == 0)
            {
                if (Map[exitX - 1, exitZ] == 0) roadcount++;
                if (Map[exitX + 1, exitZ] == 0) roadcount++;
                if (Map[exitX, exitZ + 1] == 0) roadcount++;
                if (Map[exitX, exitZ - 1] == 0) roadcount++;
            }

            if (roadcount == 3)
            {
                exit = true;
                Map[exitX, exitZ] = 4;
            }
        } while (exit == false);
/*
        int MakeingBox = 0;
        do
        {
            int BoxPosX = Random.Range(1,MapSizeX - 1);
            int BoxPosZ = Random.Range(1,MapSizeZ - 1);
            int count = 0;
            for (int i = BoxPosX - 1; i <= BoxPosX + 1; i++)
            {
                for (int j = BoxPosZ - 1; j <= BoxPosZ + 1; j++)
                {
                    if (Map[j, i] != 0 || Map[j, i] != 1)
                    {
                        count = 9;
                        break;
                    }
                    else if (Map[j, i] == 0) count++;
                }
            }

        if (count < 6)
        {
            Map[BoxPosX, BoxPosZ] = 5;
            MakeingBox++;
            }
        } while (MakeingBox <MakeBox );
*/

        //マップチップの配置
        for (int z = 0; z < MapSizeZ; z++)
        {
            for (int x = 0; x < MapSizeX; x++)
            {
                if (Map[x, z] == 0) Instantiate(cube, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale), Quaternion.identity).transform.localScale = new Vector3(objectScale,1,objectScale);
                if (Map[x, z] != 0)
                {
                    Instantiate(plane, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale), Quaternion.identity).transform.localScale = new Vector3(objectScale,1,objectScale);
                    if (Map[x, z] == 2) Instantiate(spownPoint, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale), Quaternion.identity);
                    else if (Map[x, z] == 3) Player.transform.position = new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale);
                    else if (Map[x, z] == 4) Instantiate(Gate, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale), Quaternion.identity);
                    else if(Map[x,z]==5)Instantiate(ScoreBox, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale), Quaternion.identity);
                    //マップの境界線となるパーツを配置
                    if (x != 0 && Map[x - 1, z] == 0) Instantiate(grass, new Vector3(x * 3 * objectScale - (objectScale * 3 / 2.0f), 0, z * 3 * objectScale), Quaternion.Euler(0, 90, 0)).transform.localScale = new Vector3(objectScale,objectScale * 0.3f,objectScale);
                    if (x != MapSizeX - 1 && Map[x + 1, z] == 0) Instantiate(grass, new Vector3(x * 3 * objectScale + (objectScale * 3 / 2.0f), 0, z * 3 * objectScale), Quaternion.Euler(0, 90, 0)).transform.localScale = new Vector3(objectScale, objectScale * 0.3f, objectScale);
                    if (z != 0 && Map[x, z - 1] == 0) Instantiate(grass, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale - (objectScale * 3 / 2.0f)), Quaternion.identity).transform.localScale = new Vector3(objectScale, objectScale * 0.3f, objectScale);
                    if (x != MapSizeZ - 1 && Map[x, z + 1] == 0) Instantiate(grass, new Vector3(x * 3 * objectScale, 0, z * 3 * objectScale + (objectScale * 3 / 2.0f)), Quaternion.identity).transform.localScale = new Vector3(objectScale, objectScale * 0.3f, objectScale);
                }
            }
        }
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}