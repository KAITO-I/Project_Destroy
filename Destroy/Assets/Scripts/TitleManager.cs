using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TitleStatus
{
    None,
    Opening,
    Title
}

public class TitleManager : MonoBehaviour
{
    public TitleStatus status { private get; set; }
    private Opening opening;

    private void Awake()
    {
        Cursor.visible = false;

        this.opening = GetComponent<Opening>();
    }

    private void Start()
    {
        this.status = TitleStatus.Opening;
        StartCoroutine(this.opening.PlayOpeningAnimation());
    }

    private void Update()
    {
        switch (this.status)
        {
            case TitleStatus.Opening:
                this.opening.Updated();
                break;
        }
    }
}
