using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 2f;

    [Header("Stats")]
    public float attackSpeed = 0.25f;
    public int _health = 100;
    public int _damage = 10;

    private bool _attacked = false;
    private bool _attacking = false;
    private bool _walking = false;


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
                if (!_attacked)
                {
                    StartCoroutine(SetAttackTrue(1 / attackSpeed));
                    _attacked = true;
                }
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

    // ----------------
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public int getDamage()
    {
        return _damage;
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
    private IEnumerator SetAttackTrue(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _attacking = true;
        _attacked = false;
    }
    private IEnumerator SetAttackFalse(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _attacking = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
