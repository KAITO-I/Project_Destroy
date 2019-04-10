using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Homing : MonoBehaviour
{
    public enum EnemyState
    {
        Nomal,
        Chase
    };
    [SerializeField]

    public EnemyState state;

    public GameObject target;
    public NavMeshAgent agent;

   // public Material Red, Blue;
    private new Renderer renderer = null;

    public float Speed;
    bool Flag = false , Flag2 = false;
    Animator animator;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        target = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
        animator = GetComponent<Animator>();
        SetState("Nomal");
    }

    void Update()
    {
        //if (state == EnemyState.Chase) renderer.material = Red;
        //if (state == EnemyState.Nomal) renderer.material = Blue;

        if (Flag == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && state == EnemyState.Nomal)
            {
                Chase();
            }
        }
        else
        {
            SetState("Nomal");
        }
        Get();
    }
    void Chase()
    {
        SetState("Chase");
        animator.SetBool("Flag", true);

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
        else
        {
            agent.SetDestination(transform.position);
        }
    }
    void Chase_Start()
    {
        Flag2 = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crusher")
        {
            Flag = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crusher" && state != EnemyState.Chase && Flag2 != false)
        {
            Flag = false;
        }
    }
}
