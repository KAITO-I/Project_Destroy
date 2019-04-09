using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_offset : MonoBehaviour {
    public GameObject targetObj;
    public Vector3 Vec_T = new Vector3(0, 0, 0);
    public Quaternion Vec_R = new Quaternion(0, 0, 0 ,0);

    Vector3 targetPos;
    Vector3 targetRot;
    
    void Start()
    {
        targetObj = GameObject.FindWithTag("Player");
        targetPos = targetObj.transform.position;
        transform.position = targetPos + Vec_T;
        transform.rotation = Vec_R;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // マウスの右クリックを押している間
       /* if (Input.GetAxis("CameraX") != 0)
        {
            // マウスの移動量
            float Camera = Input.GetAxis("CameraX") * 1.5f;
            //          float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, Camera * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
        */
    }
}
