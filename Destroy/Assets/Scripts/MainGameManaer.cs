using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManaer : MonoBehaviour
{
    private enum GameStatus
    {
        Opening
    }

    private GameStatus status;

    private void Awake()
    {
        
    }

    private void Start()
    {
        this.status = GameStatus.Opening;
        
    }

    private IEnumerator Opening()
    {
        
    }

    private void Update()
    {
        
    }
}
