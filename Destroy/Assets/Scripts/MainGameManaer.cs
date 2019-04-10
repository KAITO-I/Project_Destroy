using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManaer : MonoBehaviour
{
    CameraMove cm;
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
        cm = GetComponent<CameraMove>();
        StartCoroutine(cm.StartCamera());
    }

    private IEnumerator Opening()
    {
        yield return null;
    }

    private void Update()
    {
        
    }
}
