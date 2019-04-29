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
    public  IEnumerator StartCamera()
    {
        Debug.Log(MainGameManaer.GetMode());
        Camera_offset Co = camera.GetComponent<Camera_offset>();
        Co.enabled = false;
        camdefpos = camera.transform.position;
        move = GameObject.FindWithTag("Player").transform.position + Co.Vec_T;
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
        for(int i = 0;i < 37; i++)
        {
            camera.transform.Rotate(new Vector3(-1,0,0));
            yield return  null;
        }
        yield return new WaitForSeconds(0.05f);
        Co.enabled = true;
        //スポーン
        CharaSpawn cp = GetComponent<CharaSpawn>();
        cp.SetFlag(true);
        int spv = cp.firstSpawnP.Length;
        cp.nowchara = 0;
        if(MainGameManaer.GetMode() != Mode.Tarako)
        {
            Debug.Log("spawn");
            for (int i = 0; i < 50; i++)
            {
                cp.Spawn(i % spv);
                cp.nowchara++;
            }
        }
        else
        {
            cp.SpawnTarako(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
