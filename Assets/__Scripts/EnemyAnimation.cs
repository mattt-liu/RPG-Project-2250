using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public GameObject enemy;

    Animator animator;
    EnemyMovement enemyController;

    void Start()
    {
        enemyController = enemy.GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (enemyController.getWalking())
        {
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (enemyController.getAttacking())
        {
            animator.SetBool("isAttacking", true);

        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    public void StopWalking()
    {
        animator.SetBool("isWalking", false);
        enemyController.setWalking(false);
    }
    public void StopAttacking()
    {
        animator.SetBool("isAttacking", false);
        enemyController.setAttacking(false);
    }
}
