using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarako_Original : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void Anim_Start()
    {
        anim.SetBool("Flag", true);
    }
}
