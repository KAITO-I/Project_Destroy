using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData : MonoBehaviour
{
    [SerializeField] ItemData haveItem;
    [SerializeField] GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItem(ItemData Item)
    {
        haveItem = Item;
        if(Item.obj != null)Instantiate(Item.obj, pos.transform);
    }
    public ItemData GetItem()
    {
        return haveItem;
    }
}
