﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public LayerMask mask;
    public Camera cam;
    public PlayerMovement mover;
    public PlayerAnimation animate;

    private Vector3 _curPos;
    private Vector3 _newPos;
    private bool _moving;

    void Start()
    {
        _curPos = transform.position;
        _newPos = transform.position;
        cam = Camera.main;
        mover = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        _curPos = transform.position;

        // Moving player

        if (Input.GetMouseButtonDown(0))
            if (Input.GetMouseButtonDown(1))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, mask))
                {
                    _newPos = hit.point;
                    mover.MoveToPoint(_newPos);
                }
            }

        // Checking for movement

        _moving = (_newPos.x != _curPos.x) && (_newPos.z != _curPos.z);

    }
    void LateUpdate()
    {

        if (_moving)
        {
            animate.Move();
        }
    }
}
