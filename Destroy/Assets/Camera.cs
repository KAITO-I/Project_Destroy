using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Transparent(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        UnTransparent(other.gameObject);
    }
    void Transparent(GameObject go)
    {
        MeshRenderer mesh = go.GetComponent<MeshRenderer>();
        if(mesh != null)
        {
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0.5f);
        }
    }
    void UnTransparent(GameObject go)
    {
        MeshRenderer mesh = go.GetComponent<MeshRenderer>();
        if (mesh != null)
        {
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1f);
        }
    }
}
