using UnityEngine;
using System.Collections;

// 必要なコンポーネントの列記
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(Rigidbody))]

public class Unityちゃん_Move: MonoBehaviour
{

 //   public float animSpeed = 1.5f;              // アニメーション再生速度設定
 //   public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
//    public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
                                                // このスイッチが入っていないとカーブは使われない
 ///   public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

    // 以下キャラクターコントローラ用パラメタ
    // 前進速度
    public float Speed = 0f;
    public float Sp;
    private float h;
    private float v;
    // 
    private float input = 0;
    private float input_h = 0;   //ヤケクソ気味に変数を増やしてみただけです、たぶんもっとうまくできる
    private float input_v = 0;
    private float input_X = 0;
    private float input_Y = 0;

    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    //private Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 movement;
    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;

    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照

    private GameObject cameraObject;    // メインカメラへの参照
    private Transform camera_pos;
    private  Vector3 cameraForward;
    public Vector3 moveForward;
    private Vector3 P_pos; //プレイヤーのポジション
    private Vector3 S_pos; //プレイヤーのポジション

    [SerializeField] MainGameManaer gameManaer;
    private Vector3 diff;
    private Vector3 new_diff;
    private float pos_y;
    public bool Move_Flag = true;
    // 初期化
    void Start()
    {
        Sp = Speed;
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
        col = GetComponent<CapsuleCollider>();

        //rb = GetComponent<Rigidbody>();
        //メインカメラを取得する
        cameraObject = GameObject.FindWithTag("MainCamera");

        // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
        orgColHight = col.height;
        orgVectColCenter = col.center;
        P_pos = GetComponent<Transform>().position;
        S_pos = transform.position;

        if (Camera.main != null)
        {
            camera_pos = Camera.main.transform;
        }
    }


    // 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義
        input_X = h * Sp;
        input_Y = v * Sp;
        if (h != 0.0 || v != 0.0)
        {
            if (h > 0)
            {
                input_h = h;
            }
            else if (h < 0)
            {
                input_h = -h;
            }
            else if (v > 0)
            {
                input_v = v;
            }
            else if (v < 0)
            {
                input_v = -v;
            }
            else
            {
                input_h = 0;
                input_v = 0;
            }
        }

        input = input_h + input_v;
        if (input > 1.0) input = 1.0f;
        if (input_h > 1.0) input_h = 1.0f;
        if (input_v > 1.0) input_v = 1.0f;
        if (Move_Flag == true)
        {
            Move(input_X, input_Y);
        }
        Vector3 D_Pos = transform.position - S_pos;
        float D_mag;
        D_mag = Mathf.Clamp(D_Pos.magnitude, 0.0f,1.0f) / 10.0f ;

       anim.SetFloat("Speed",  D_mag / Time.deltaTime);

        S_pos = transform.position;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Stop", true);
            Move_Flag = false;
        }
    }

    void Move(float h, float v)
    {
        
        if (camera_pos != null)
        {
            cameraForward = Vector3.Scale(camera_pos.forward, new Vector3(1, 1, 1)).normalized;

            moveForward = v* 2 * cameraForward + h * 1.5f  * camera_pos.right;

            moveForward.y = transform.position.y;
        }
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.position= new Vector3(transform.position.x + moveForward.x,moveForward.y,transform.position.z + moveForward.z);
            diff.z = (transform.position.z - P_pos.z); //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得
            diff.x = (transform.position.x - P_pos.x);
            diff.y = 0;
            pos_y = transform.position.y;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
            input_h = 0;
            input_v = 0;
            diff.z = 0;
            diff.x = 0;
            diff.y = 0;
        }
 
       moveForward.y = pos_y;
        Vector3 new_diff = Vector3.RotateTowards(transform.forward, diff, 10f * Time.deltaTime, 0f);
        if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {

            transform.rotation = Quaternion.LookRotation(new_diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる

        }
        P_pos = transform.position; //プレイヤーの位置を更新
    }

    void Restart()
    {
        Move_Flag = true;
        anim.SetBool("Stop", false)
;    }

    void Motion_End()
    {
        Restart();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gameManaer.GameSet();
        }
    }
}


