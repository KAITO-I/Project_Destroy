using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData : MonoBehaviour
{
    public ItemData haveItem;
    public GameObject myItem;
    public GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Canvas").GetComponent<ScoreManager>().Scorecalc(gameObject,haveItem);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetItem(ItemData Item)
    {
        haveItem = Item;
        if(Item.obj != null)myItem = Instantiate(Item.obj, pos.transform);
    }
}
