using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    Rigidbody rb;
    int addGrab = 3;
    bool grab;
    GameObject maj;
    SoundPlayer Sp;
    AudioClip ac;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maj = GameObject.Find("MainGameManager");
        Sp = maj.GetComponent<SoundPlayer>();
    }
    public void GrabtyChange(AudioClip cl)
    {
        gameObject.transform.parent = null;
        rb.useGravity = true;
        grab = true;
        ac = cl;
        StartCoroutine(Break(gameObject));
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
            GameObject go =  Instantiate(Resources.Load<GameObject>("CFX_Hit_C White"),gameObject.transform);
            go.transform.parent = null;
            Sp.PlaySE(ac);
            Destroy(gameObject);
        }
    }
    IEnumerator Break(GameObject g)
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Break");
        GameObject go = Instantiate(Resources.Load<GameObject>("CFX_Hit_C White"), gameObject.transform);
        go.transform.parent = null;
        Sp.PlaySE(ac);
        Destroy(gameObject);
    }
}
