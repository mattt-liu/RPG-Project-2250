using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    Transform target;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void Follow(Inter newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .5f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    // Stop following a target
    public void StopFollowing()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

}