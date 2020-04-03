using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Level Lights")]
    public Light[] lights;

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
        foreach (Light l in lights)
        {
            l.intensity = 0;
        }
        lights[0].intensity = LightController.defaultBrightness;
    }

    void Update()
    {
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
}
