using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet : MonoBehaviour {
    ItemData Item;
    public List<ItemData> Items;
    // Use this for initialization
    void Start()
    {
        Item = ScriptableObject.CreateInstance<ItemData>();
		Items = new List<ItemData>();
		SetItemData();
    }
    void SetItemData()
    {
		ItemData[] it = Resources.LoadAll<ItemData>("Item");
        for(int i = 0;i < it.Length; i++)
		{
			ItemData t = Instantiate(it[i]);
			Items.Add(t);
		}
    }
    public List<ItemData> GetItemList()
    {
        return Items;
    }
    public ItemData GetItemData(int ind)
    {
        return Instantiate(Items[ind]);
    }

}
