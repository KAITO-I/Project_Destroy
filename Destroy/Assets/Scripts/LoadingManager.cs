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
        
        foreach (Transform loadingChar in this.loading.GetComponentInChildren<Transform>()) loadingList.Add(loadingChar.gameObject);
        this.loadingArray = loadingList.ToArray();

        this.time = 0f;
        this.charNum = 0;
        this.maxCharNum = this.loadingArray.Length;
    }

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.loadingArray[this.maxCharNum - 1].GetComponent<Animation>().isPlaying) return;
        if (this.time >= this.interval)
        {
            this.time = 0f;
            this.loadingArray[this.charNum].GetComponent<Animation>().Play();

            this.charNum++;
            if (this.charNum == this.maxCharNum) this.charNum = 0;
        }
    }
}