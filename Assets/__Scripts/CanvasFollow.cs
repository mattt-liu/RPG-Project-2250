using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public GameObject target;
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _target = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_target.position);
    }
}
