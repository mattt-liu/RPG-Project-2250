using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 2f;

    [Header("Stats")]
    public float attackSpeed = 0.25f;
    public int maxHealth = 100;
    public int damage = 10;

    [Header("HealthBar")]
    public GameObject healthBar;

    private int _health;

    private float _maxHealthBarSize;
    private float _hpBarX;
    private float _hpBarY;
    private float _hpBarZ;

    private bool _attacked = false;
    private bool _attacking = false;
    private bool _walking = false;

    private bool _dead = false;
    private bool _dying = false;


    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        _health = maxHealth;
        _hpBarX = _maxHealthBarSize = healthBar.transform.localScale.x;
        _hpBarY = healthBar.transform.localScale.y;
        _hpBarZ = healthBar.transform.localScale.z;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // check player dist
        if (distance <= lookRadius)
        {
            setWalking(true);
            agent.SetDestination(target.position);

            // enters radius
            if (distance <= agent.stoppingDistance)
            {
                // Attack the target
                setWalking(false);
                FaceTarget();    // Face the target
                if (!_attacked)
                {
                    StartCoroutine(SetAttackTrue(1 / attackSpeed));
                    _attacked = true;
                }
            }
            else
            {
                setWalking(true);
                _attacking = false;
                _attacked = false;
            }
        }
        else
        {
                setWalking(false);
        }

        // health bar
        RotateHealthBar();
        UpdateHealthBar();
    }

    // ----------------
    void UpdateHealthBar()
    {
        _hpBarX = _maxHealthBarSize * _health / maxHealth;
        healthBar.transform.localScale = new Vector3(_hpBarX, _hpBarY, _hpBarZ);
    }
    void RotateHealthBar()
    {
        float x = transform.rotation.x;
        float z = transform.rotation.z;
        Quaternion barRotation = Quaternion.LookRotation(new Vector3(x, 0.01f, z));
        healthBar.transform.rotation = barRotation;

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public int getDamage()
    {
        return damage;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
