using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    // [RequireComponent(typeof(NavMeshAgent))]
    public class Homing_tarako : MonoBehaviour
    {
        public enum EnemyState
        {
            Nomal,
            Chase
        };
        public float SpownTime;
        public EnemyState state;
        AICharacterControl ai;
        public GameObject target,Prefab;
        public NavMeshAgent agent;
        public Material Red, Blue;
        private new Renderer renderer = null;
        public new Transform transform;
        public Vector3 vector3;
        public float Speed;
        public bool Flag = false, Flag2 = false, Flag3 = true , cor_Flag;
        public Animator animator;
        GameObject tarako_prefab;
        public Tarako_Original tarako;

        void Start()
        {
            target = GameObject.FindWithTag("Player");
            renderer = GetComponentInChildren<Renderer>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 1;
            animator = GetComponent<Animator>();
            SetState("Nomal");
            ai = GetComponent<AICharacterControl>();
            cor_Flag = true;
        }

        void Update()
        {
            transform = this.gameObject.transform;
            if (state == EnemyState.Nomal) renderer.material = Blue;

            if (Flag == true && Flag2 == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && state == EnemyState.Nomal)
                {
                    Chase();
                }
            }
            if (Flag == true && Flag3 == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Flag3 = false;
                    GameObject.Find("MainGameManager").GetComponent<ScoreManager>().Scorecalc(gameObject, gameObject.GetComponent<CharaData>().haveItem);
                }
            }
            Get();
        }
        void Chase()
        {
            SetState("Chase");
            agent.speed = 0;
            animator.SetBool("Flag", true);
            SetState("Chase");
            agent.SetDestination(this.transform.position);
            Invoke("Chase_Start", 2.0f);
        }
        public void SetState(string Mode)
        {
            if (Mode == "Nomal")
            {
                state = EnemyState.Nomal;

            }
            if (Mode == "Chase")
            {
                state = EnemyState.Chase;
            }
        }
        public void Get()
        {
            if (state == EnemyState.Chase && Flag2 == true)
            {
                agent.SetDestination(target.transform.position);
                if(tarako != null) tarako.Anim_Start();
                if (cor_Flag == true)
                {
                    StartCoroutine(Fake());
                    cor_Flag = false;
                }
            }
        }
        void Chase_Start()
        {
            Flag2 = true;
            Flag = false;
            agent.speed = Speed;
            ai.Null();
            Get();
            this.tag = "Enemy";
            if (state == EnemyState.Chase) renderer.material = Red;

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Crusher")
            {
                Flag = true;
            }
        }
        public void OnTriggerExit(Collider other)
        {
            Flag = false;
        }
        IEnumerator Fake()
        {
            while (true)
            {
                yield return new WaitForSeconds(SpownTime);
                Timer();
            }
        }

        void Timer()
        {
            
            tarako_prefab =  Instantiate(Prefab,this.transform);
            tarako_prefab.transform.parent = null;
            Debug.Log("instance");
        }
    }
}
