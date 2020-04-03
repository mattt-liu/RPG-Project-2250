using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{

    public Image attackCooldown;
    public float cooldown;
    public bool isCooldown = false;
    public GameObject attackTimerUI;


    private void Start()
    {
        attackCooldown.fillAmount = 0f;
    }
    void Update()
    {
        Timer();
    }

    //Controls the punch timer fill
    void Timer()
    {
        if (isCooldown)
        {
            attackCooldown.fillAmount +=( 1f / cooldown) * Time.deltaTime;

            if (attackCooldown.fillAmount >= 1f)
            {
                attackCooldown.fillAmount = 0f;
                ResetCooldown();
            }
        }
    }

    //When character can't attack this deactivates timer and cooldown
    public void StartCooldown()
    {
        attackTimerUI.SetActive(true);
        isCooldown = true;
    }

    //When the character can attack this deactivates the timer and cooldown
    void ResetCooldown()
    {
        attackTimerUI.SetActive(false);
        isCooldown = false;
    }
}

