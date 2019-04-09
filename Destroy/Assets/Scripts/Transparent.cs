using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if(mesh != null)mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0.5f);
    }
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh != null) mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1f);
    }
}
