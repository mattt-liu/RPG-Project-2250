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
    public void setAgent(bool b)
    {
        agent.enabled = b;
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.isStopped = false;
        agent.SetDestination(point);
    }
    public void StopMoving()
    {
        agent.isStopped = true;
    }
    public void Follow(Inter newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .5f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }
    public void StopFollowing()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

}