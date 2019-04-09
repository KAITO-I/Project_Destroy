using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] GameObject loading;
    private GameObject[] loadingArray;

    private float time;
    [SerializeField] float interval;

    private int charNum;
    private int maxCharNum;

    void Start()
    {
        List<GameObject> loadingList = new List<GameObject>();
        foreach (GameObject loadingChar in this.loading.transform) loadingList.Add(loadingChar);
        this.loadingArray = loadingList.ToArray();

        this.time = 0f;
        this.charNum = 0;
        this.maxCharNum = this.loadingArray.Length;
    }

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time >= this.interval)
        {
            GameObject charObj = this.loadingArray[this.charNum];
            charObj.SetActive(!charObj.active);
        }
    }
}
