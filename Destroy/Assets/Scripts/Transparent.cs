using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh != null && other.gameObject.tag == "Building") mesh.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh != null && other.gameObject.tag == "Building") mesh.enabled = true;
    }
}
