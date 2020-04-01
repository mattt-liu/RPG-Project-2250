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
    }
    public void StopPunching()
    {
        animator.SetBool("isPunching", false);
        //enemyController.setPunching(false);
    }
    public void StopWalking()
    {
        animator.SetBool("isWalking", false);
        enemyController.setWalking(false);
    }
    public void StopJumping()
    {
        animator.SetBool("isJumping", false);
        //enemyController.setJumping(false);
    }
    public void StopKicking()
    {
        animator.SetBool("isKicking", false);
        //enemyController.setKicking(false);
    }
}
