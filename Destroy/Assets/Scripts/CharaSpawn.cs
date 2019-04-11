using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSpawn : MonoBehaviour
{
    public GameObject[] spawmPoint;
    public GameObject[] charas;
    ItemSet Item;
    float spawnTime;
    public float spawnSpan = 30f;
    public int Spawnnum = 5;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnSpan;
        Item = GetComponent<ItemSet>();
        //Item.SetItemData();
    }
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0f)
        {
            spawnTime = spawnSpan;
            for (int i = 0;i < Random.Range(20,Spawnnum+1); i++)
            {
                Spawn();
            }
        }
    }
    public void Spawn()
    {
        ItemData item = ItemCalc();
        if (item == null) Debug.Log("null");
        int pos = Random.Range(0, spawmPoint.Length);
        GameObject chara =  Instantiate(charas[Random.Range(0, charas.Length)], spawmPoint[pos].transform);
        chara.GetComponent<CharaData>().SetItem(item);
    }
    public void Spawn(int num)
    {
        ItemData item = ItemCalc();
        if (item == null) Debug.Log("null");
        GameObject chara = Instantiate(charas[Random.Range(0, charas.Length)], spawmPoint[num].transform);
        chara.GetComponent<CharaData>().SetItem(item);
    }
    ItemData ItemCalc()
    {
        Item = GetComponent<ItemSet>();
        int num = Random.Range(1, 101);
        Item.SetItemData();
        List <ItemData> it= Item.GetItemList();
        switch (num)
        {
            case  int i when i <= 5:
                return it.Find(x => x.Name == "DS");
            case int i when i <= 15:
                return it.Find(x => x.Name == "Switch");
            case int i when i <= 20:
                return it.Find(x => x.Name == "SmartPhone");
            case int i when i <= 35:
                return it.Find(x => x.Name == "Cake");
            case int i when i == 36:
                return it.Find(x => x.Name == "Tubo(true)");
            case int i when i <= 46:
                return it.Find(x => x.Name == "Tubo(false)");
            case int i when i <= 61:
                return it.Find(x => x.Name == "Megane");
            case int i when i <= 80:
                return it.Find(x => x.Name == "Can");
            default:
                return it.Find(x => x.Name == "None");
        }
    }
}
