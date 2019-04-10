using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    Rigidbody rb;
    int addGrab = 3;
    bool grab;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void GrabtyChange()
    {
        gameObject.transform.parent = null;
        rb.useGravity = true;
        grab = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (grab)
        {
            Debug.Log("Break");
            Destroy(gameObject);
        }
    }
}
