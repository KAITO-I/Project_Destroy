using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_instance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Instance(GameObject prefab, Transform transform)
    {
        Instance(prefab, transform);
        return prefab;
    }
}
