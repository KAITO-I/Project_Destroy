using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData",fileName ="Item")]
public class ItemData : ScriptableObject {
    public string Name;
    public int HP;
    public int dropPar;
    public int Price;
    public GameObject obj;
    public AudioClip audio;
}
