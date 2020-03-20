 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public GameObject player;

    Animator animator;
    PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerController.getWalking())
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (playerController.getJumping())
        {
            animator.SetBool("isJumping", true);

        } 
        else
        {
            animator.SetBool("isJumping", false);
        }
        if (playerController.getPunching())
        {
            animator.SetBool("isPunching", true);
        }
    }
    void StopPunching()
    {
        animator.SetBool("isPunching", false);
        playerController.setPunching(false);
    }
    void StopWalking()
    {
        animator.SetBool("isWalking", false);
        playerController.setPunching(false);
    }
}
