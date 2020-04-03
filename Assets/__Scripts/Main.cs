using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Level Lights")]
    public Light[] lights;
    private LightController[] lightControllers;

    [Header("Enemy")]
    public GameObject[] enemies;

    [Header("Player")]
    public PlayerController player;

    [Header("Level Doors")]
    public GameObject[] doors;

    private EnemyMovement[] enemyMovements;

    private int _currentLevel;

    void Start()
    {
        _currentLevel = 1;
        player.currentLevel = _currentLevel;

        // get enemy scripts
        enemyMovements = new EnemyMovement[enemies.Length];
        for (int i = 0; i < enemyMovements.Length; i++)
        {
            GameObject e = enemies[i];
            enemyMovements[i] = e.GetComponentInChildren<EnemyMovement>();
        }

        // set lights
        lightControllers = new LightController[lights.Length];
        for (int i = 0; i < lights.Length; i ++)
        {
            Light l = lights[i];

            lightControllers[i] = l.GetComponent<LightController>();
            lightControllers[i].on = false;
        }
        lightControllers[0].on = true;
    }

    void Update()
    {
        // update enemy
        foreach( EnemyMovement enemy in enemyMovements) {
            if (enemy.getAttacking())
            {
          
                int dmg = enemy.getDamage();
                enemy.setAttacking(false);

                player.takeDamage(dmg);
                    
                if (player.getPunched())
                {
                    enemy.takeDamage(player.getPunchDamage());
                    player.setPunched(false);
                }
                if (player.getKicked())
                {
                    enemy.takeDamage(player.getKickDamage());
                    player.setKicked(false);
                }
            }
        }
        // enemy death
        for (int i = 0; i < enemies.Length; i ++)
        {
            if (enemyMovements[i].getDead())
            {
                enemies[i].SetActive(false);
            }
        }
        // determine player's target
        if (player.WalkingToEnemy && !player.SetTarget)
        {
            SetPlayerTarget();
            player.SetTarget = true;
        }
        if (!player.WalkingToEnemy && player.SetTarget)
        {
            player.SetTarget = false;
        }
        // player progress
        if (player.LevelUp)
        {
            LevelUp();
        }

    }
    private void SetPlayerTarget()
    {
        // target enemy is closest one to clicked point
        Vector3 targetPos = player.getTarget(); // gets the clicked pos player is walking to

        // set target
        int targetIndex = Closest(targetPos, enemies);  // gets the index 
        player.Target(enemyMovements[targetIndex]);

    }
    private int Closest(Vector3 pos, GameObject[] y)
    {
        // finds the index of closest GameObject y to pos

        int minIndex = 0;
        float min = Vector3.Distance(pos, y[minIndex].transform.position);

        for (int i = 0; i < y.Length; i ++)
        {
            float dist = Vector3.Distance(pos, y[i].transform.position);
            if (dist < min)
            {
                min = dist;
                minIndex = i;
            }
        }
        return minIndex;
        
    }
    private void UpdateLight()
    {
        // change lighting
        lights[_currentLevel - 1].intensity = LightController.defaultBrightness;
    }

    private void LevelUp()
    {
        _currentLevel ++;
        player.currentLevel++;
    }
}
