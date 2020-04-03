using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Level Lights")]
    public Light[] lights;
    private LightController[] lightControllers;

    [Header("Enemy")]
    public GameObject[] es;
    public ArrayList enemies = new ArrayList();

    [Header("Player")]
    public PlayerController player;
    public float targetRadius = 3f;

    [Header("Level Doors")]
    public GameObject[] doors;

    private ArrayList enemyMovements;

    private int _currentLevel;
    private bool gameOver = false;

    void Start()
    {
        _currentLevel = 1;
        player.currentLevel = _currentLevel;

        // get enemy scripts
        for (int i = 0; i < es.Length; i ++)
        {
            enemies.Add(es[i]);
        }
        enemyMovements = new ArrayList();
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject e = (GameObject)enemies[i];
            enemyMovements.Add(e.GetComponentInChildren<EnemyMovement>());
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
        for (int i = 0; i < enemies.Count; i ++)
        {
            EnemyMovement e = ((EnemyMovement)enemyMovements[i]);
            if (e.getDead())
            {
                ((GameObject)enemies[i]).SetActive(false);
                enemies.RemoveAt(i);
                enemyMovements.RemoveAt(i);
                break;
            }
        }
        if (enemies.Count == 0)
        {
            gameOver = true;
        }
        // determine player's target
        SetPlayerTarget();
        // player progress
        if (player.LevelUp)
        {
            LevelUp();
        }
        // open doors
        if (_currentLevel > 1)
        {
            for (int i = 0; i < _currentLevel - 1; i ++)
            {
                doors[i].SetActive(false);
            }
        }
        // lights
        UpdateLight();
    }
    private void SetPlayerTarget()
    {

            // target enemy is closest one to current pos
            Vector3 curpos = player.getCurpos(); // gets the clicked pos player is walking to

            // find closest target
            int targetIndex = Closest(curpos, enemies);  // gets the index 
            if (Vector3.Distance(curpos, ((GameObject)enemies[targetIndex]).transform.position) <= targetRadius && !player.targeted)
            {
                player.targeted = true;
                player.Target((EnemyMovement)(enemyMovements[targetIndex])); //can only target inside radius
            }
        
        else
        {
            player.targeted = false;
        }

    }
    private int Closest(Vector3 pos, ArrayList y)
    {
        // finds the index of closest GameObject y to pos

        int minIndex = 0;
        float min = Vector3.Distance(pos, ((GameObject)(y[minIndex])).transform.position);

        for (int i = 0; i < y.Count; i ++)
        {
            float dist = Vector3.Distance(pos, ((GameObject)(y[i])).transform.position);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRadius);
    }
}
