using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 2f;

    public float attackSpeed = 0.25f;

    private bool _attacking = false;
    private bool _walking = false;

    private int _health = 100; //temp

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            setWalking(true);
            agent.SetDestination(target.position);

            // enters radius
            if (distance <= agent.stoppingDistance)
            {
                // Attack the target
                FaceTarget();    // Face the target
                _attacking = true;
            }
            else
            {
                _attacking = false;
            }
        }
        else
        {
            setWalking(false);
        }
    }
    void LateUpdate()
    {
        if (_attacking)
        {
            _attacking = false;
            Debug.Log(1 / attackSpeed);
            StartCoroutine(WaitFor(1 / attackSpeed));
        }
    }


    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public int getDamage()
    {
        return 10; //temp
    }
    public void takeDamage(int dmg)
    {
        _health -= dmg;
    }
    public bool getAttacking()
    {
        return _attacking;
    }
    public void setAttacking(bool b)
    {
        _attacking = b;
    }
    public bool getWalking()
    {
        return _walking;
    }
    public void setWalking(bool b)
    {
        _walking = b;   
    }
    private IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _attacking = true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
