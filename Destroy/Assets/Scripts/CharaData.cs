﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

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
        if (MainGameManaer.GetMode() == Mode.Tarako || Item == null) return;
        haveItem = Item;
        if (Item.obj != null && pos != null) myItem = Instantiate(Item.obj, pos.transform);
        GetComponent<Homing>().itemS = Item.Item;
    }
}
