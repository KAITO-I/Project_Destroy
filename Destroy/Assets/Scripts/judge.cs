using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    public List<GameObject> Char;

    public void OnTriggerEnter(Collider col)
    {
      //  Debug.Log("Test");
        if(col.tag == "Enemy") Char.Add(col.gameObject);
    }

    public void OnTriggerExit(Collider col)
    {
       
    }
}
