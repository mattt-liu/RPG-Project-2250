using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health2 : MonoBehaviour
{
    public int health;

    public Text damageText;
    public Text healthText;
    public Slider healthBar;

    Animator damageAnim;

    // Start is called before the first frame update
    void Start()
    {

        damageAnim = damageText.GetComponent<Animator>();

        healthBar.value = health;
        healthBar.maxValue = health;

        healthText.text = health.ToString() + "%";

        InvokeRepeating("TakeDamage",1f, 5f);
        


        //TakeDamage();    <--- works once
    }




    void TakeDamage()
    {
        Debug.Log("take damage executed");
        int num = Random.Range(5, 20);
        health -= num;


        damageText.text = "-" + num.ToString();
        damageAnim.SetTrigger("Show");

        healthBar.value = health;
        healthText.text = health.ToString();
    }
}

