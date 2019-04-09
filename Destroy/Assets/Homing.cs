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

    public Material Red, Blue;
    private new Renderer renderer = null;

    public float Speed;
    bool Flag = false;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
        SetState("Nomal");
    }

    void Update()
    {
        if (state == EnemyState.Chase) renderer.material = Red;
        if (state == EnemyState.Nomal) renderer.material = Blue;

        if (Flag == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && state == EnemyState.Nomal)
            {
                SetState("Chase");
            }
        }
        else
        {
            SetState("Nomal");
        }
        Get();
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
        if (state == EnemyState.Chase)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Flag = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && state != EnemyState.Chase)
        {
            Flag = false;
        }
    }
}
