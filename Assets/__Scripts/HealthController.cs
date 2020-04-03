using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public void TakeDamage(int amount)
    {
        //spawn health bar
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            TakeDamage(Random.Range(10, 25));
        }
    }
}
