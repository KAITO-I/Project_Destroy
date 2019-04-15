using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    class Menu
    {
        public GameObject frame     { private set; get; }
        public GameObject selecting { private set; get; }
        public GameObject selected  { private set; get; }

        Menu(GameObject frame, GameObject selecting, GameObject selected)
        {
            this.frame     = frame;
            this.selecting = selecting;
            this.selected  = selected;
        }
    }
    private Menu start;
    private Menu ranking;
    private Menu quit;

    public void Awaked()
    {
        
    }

    public void Updated()
    {

    }
}
