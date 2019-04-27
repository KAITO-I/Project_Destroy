﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
   // [RequireComponent(typeof(NavMeshAgent))]
    public class Homing : MonoBehaviour
    {
        public enum EnemyState
        {
            Nomal,
            Chase
        };

        public EnemyState state;
        AICharacterControl ai;
        CharaData data;
        public GameObject target;
        public NavMeshAgent agent;
         public Material Red, Blue;
        private new Renderer renderer = null;

        public float Speed;
        public bool Flag = false, Flag2 = false, Flag3 = true;
        Animator animator;
        

        void Start()
        {
            data = GetComponent<CharaData>();
            target = GameObject.FindWithTag("Player")
;           renderer = GetComponentInChildren<Renderer>();
            target = GameObject.FindWithTag("Player");
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 1;
            animator = GetComponent<Animator>();
            SetState("Nomal");
            ai = GetComponent<AICharacterControl>();   
            if(data.myItem.name != "None(Clone)")
            {
                animator.SetBool("Have", true);
            }
        }

        void Update()
        {
          
            //if (state == EnemyState.Nomal) renderer.material = Blue;

            if (Flag == true && Flag2 == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && state == EnemyState.Nomal)
                {
                    Chase();
                }
            }
            if(Flag == true && Flag3 == true)
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
            animator.SetTrigger("Flag");
            SetState("Chase");
            agent.SetDestination(this.transform.position);
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
    }
}
