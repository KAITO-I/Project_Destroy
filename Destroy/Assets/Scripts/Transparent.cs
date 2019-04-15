using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh != null && other.gameObject.layer == 10) other.gameObject.layer = 11;
    }
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh != null && other.gameObject.layer == 11) other.gameObject.layer = 10;
    }
}
