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
                player.takeDamage(dmg);
                enemy.setAttacking(false);
            }
        }
    }
    private void UpdateLight()
    {
        // change lighting
        lights[_currentLevel - 1].intensity = LightController.defaultBrightness;
    }
}
