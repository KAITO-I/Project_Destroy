using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TitleStatus
{
    None,
    Opening,
    MainMenu
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] bool tarakoEdition;

    public TitleStatus status { private get; set; }
    private Opening     opening;
    private MainMenu    menu;
    private HideCommand command;

    private void Awake()
    {
        Cursor.visible = false;

        (this.menu = GetComponent<MainMenu>()).Awaked();
        (this.opening = GetComponent<Opening>()).Awaked();
        if (!this.tarakoEdition) (this.command = GetComponent<HideCommand>()).Awaked();
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

            case TitleStatus.MainMenu:
                this.menu.Updated();
                if (!this.tarakoEdition) this.command.Updated();
                break;

        }
    }
}
