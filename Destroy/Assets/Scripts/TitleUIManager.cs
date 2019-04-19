using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject selecting;
    [SerializeField] GameObject selected;

    private void Start()
    {
        selecting.SetActive(false);
        selected.SetActive(false);
    }

    public void SetSelecting(bool b)
    {
        selecting.SetActive(b);
    }

    public void Selected()
    {
        selecting.SetActive(false);
        selected.SetActive(true);
    }
}
