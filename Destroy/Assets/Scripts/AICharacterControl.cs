using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;
        public Vector3 pos;// target to aim for
        public GameObject[] points;
        static System.Random random = new System.Random();
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            
	        agent.updateRotation = false;
	        agent.updatePosition = true;
            points = GameObject.FindGameObjectsWithTag("point");
            target = points[random.Next(0, points.Length)].transform;
            pos = target.position;

        }


        private void Update()
        {
         if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);


            if (target != null)
            {
                agent.SetDestination(target.position);
               
                if ((transform.position - target.position).magnitude <= 1.0)
                {
                    target = points[random.Next(0, points.Length)].transform;
                    pos = target.position;

                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void Null()
        {
            target = null;
        }
    }
}
