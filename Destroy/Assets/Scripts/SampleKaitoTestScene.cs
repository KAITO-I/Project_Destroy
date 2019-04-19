using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleKaitoTestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.Load("Title");
    }
}
