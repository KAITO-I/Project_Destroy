using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    // [RequireComponent(typeof(NavMeshAgent))]
    public class Homing_tarako_child : MonoBehaviour
    {
        public enum EnemyState
        {
            Chase
        };

        public EnemyState state;
        AICharacterControl ai;
        public GameObject target;
        public NavMeshAgent agent;
        public Material Red, Blue;
        private new Renderer renderer = null;

        public float Speed;
        public bool Flag = false, Flag2 = false, Flag3 = true, cor_Flag;
        public GameObject tarako_prefab;
        Animator animator;


        void Start()
        {
            target = GameObject.FindWithTag("Player");
            renderer = GetComponentInChildren<Renderer>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = Speed;
            animator = GetComponent<Animator>();
            SetState("Chase");
            ai = GetComponent<AICharacterControl>();
            cor_Flag = true;
        }

        void Update()
        {
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
            if (Mode == "Chase")
            {
                state = EnemyState.Chase;
            }
        }
        public void Get()
        {
            if (state == EnemyState.Chase)
            {
                agent.SetDestination(target.transform.position);
                if (cor_Flag == true)
                {
                    StartCoroutine(Fake());
                    cor_Flag = false;
                }
            }
        }
        void Chase_Start()
        {
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
                yield return new WaitForSeconds(3.0f);
                Timer();
            }
        }

        void Timer()
        {

            //Instantiate(tarako_prefab, this.transform);
            Debug.Log("instance");
        }
    }
}