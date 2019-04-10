using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSpawn : MonoBehaviour
{
    public GameObject[] spawmPoint;
    public GameObject[] charas;
    ItemSet Item;
    
    // Start is called before the first frame update
    void Start()
    {
        Item = GetComponent<ItemSet>();
        Spawn();
    }
    public void Spawn()
    {
        int item = Random.Range(0,Item.GetItemList().Count);
        int pos = Random.Range(0, spawmPoint.Length);
        GameObject chara =  Instantiate(charas[Random.Range(0, charas.Length)], spawmPoint[pos].transform);
        chara.GetComponent<CharaData>().SetItem(Item.GetItemData(item));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
