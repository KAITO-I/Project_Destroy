using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    // private RectTransform myRectTfm;
    private Transform myRectTfm;
    // Start is called before the first frame update
    void Start()
    {
        // myRectTfm = GetComponent<RectTransform>();
        myRectTfm = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRectTfm.LookAt(Camera.main.transform);
    }
}
