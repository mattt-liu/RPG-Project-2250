using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;   //NOT FindGameObjectWithTag 
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
    }
}
