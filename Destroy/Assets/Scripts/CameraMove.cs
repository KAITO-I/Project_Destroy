using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraMove : MonoBehaviour
{
    [SerializeField] float moveTime = 5f;
    public GameObject camera;
    Vector3 move;
    Vector3 camdefpos ;
    [SerializeField] GameObject movePos;
    [SerializeField] Text startText;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartCamera());
    }
    public IEnumerator StartCamera()
    {
        camdefpos = camera.transform.position;
        move = movePos.transform.position;
        float time = 0;
        do
        {
            time += Time.deltaTime;
            float tm = time / moveTime;
            tm = Mathf.Clamp01(tm);
            camera.transform.position = Vector3.Lerp(camdefpos, move, tm);
            yield return null;
        }
        while (moveTime >= time);
        for(int i = 0;i < 45; i++)
        {
            camera.transform.Rotate(new Vector3(-1,0,0));
            yield return  null;
        }
        //yield return new WaitForSeconds(0.05f);
        //startText.gameObject.SetActive(true);
        //yield return new WaitForSeconds(3f);
        //startText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
