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
        else 
        {
            animator.SetBool("isPunching", false);
        }
        if (playerController.getKicking())
        {
            animator.SetBool("isKicking", true);
        }
        else
        {
            animator.SetBool("isKicking", false);
        }
    }
    public void StopPunching()
    {
        animator.SetBool("isPunching", false);
        playerController.setPunching(false);
    }
    public void StopWalking()
    {
        animator.SetBool("isWalking", false);
        playerController.setWalking(false);
    }
    public void StopJumping()
    {
        animator.SetBool("isJumping", false);
        playerController.setJumping(false);
    }
    public void StopKicking()
    {
        animator.SetBool("isKicking", false);
        playerController.setKicking(false);
    }
}
