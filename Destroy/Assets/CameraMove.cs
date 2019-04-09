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
        camdefpos = camera.transform.position;
        move = movePos.transform.position;
        StartCoroutine(StartCamera());
    }
    IEnumerator StartCamera()
    {
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
        yield return new WaitForSeconds(0.05f);
        startText.gameObject.SetActive(true);
        Color cl;
        for (int i = 0;i < 255; i++)
        {
            cl = startText.color;
            cl.a = 1f-((1f/255)*i);
            startText.color = cl;
            yield return null;
        }
        startText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
