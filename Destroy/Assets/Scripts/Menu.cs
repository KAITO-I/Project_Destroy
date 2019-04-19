using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MenuWindow;
    void Start()
    {
        if(MenuWindow.activeInHierarchy == true)MenuWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Cancelを押すことで画面が止まりMapが表示される
        if (Input.GetButtonDown("Cancel") && MenuWindow.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            MenuWindow.SetActive(true);
        }
        //もう一度押すと解除
        else if (Input.GetButtonDown("Cancel") && MenuWindow.activeInHierarchy == true)
        {
            Time.timeScale = 1;
            MenuWindow.SetActive(false);
        }
    }
}
