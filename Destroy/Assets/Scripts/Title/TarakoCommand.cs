using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarakoCommand : MonoBehaviour
{
    [SerializeField] KeyCode[] commands;
    private int completed;

    public void Awaked()
    {
        this.completed = -1;
    }

    public void Updated()
    {
        if (!Input.anyKeyDown) return;

        foreach (KeyCode input in commands)
        {
            if (Input.GetKeyDown(input))
            {
                Command();
                break;
            }
        }
    }

    private void Command()
    {
        if (Input.GetKeyDown(this.commands[this.completed + 1]))
        {
            this.completed++;
            if (this.completed == this.commands.Length - 1) SceneController.Instance.Load("TarakoTitle");
        }
        else
        {
            this.completed = -1;
            if (Input.GetKeyDown(this.commands[0])) this.completed++;
        }
    }
}
