using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munu : MonoBehaviour
{

    public GameObject MenuWindow;
    // Start is called before the first frame update
    void Start()
    {
        if(MenuWindow.activeInHierarchy == true)MenuWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel") && MenuWindow.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            MenuWindow.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") && MenuWindow.activeInHierarchy == true)
        {
            Time.timeScale = 1;
            MenuWindow.SetActive(false);
        }
    }
}
