﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{

    public LayerMask mask;
    public Camera cam;
    public PlayerMovement mover;


    public Inter focus;

    public GameObject canvas;
    public int currentLevel;

    private Vector3 _curPos;
    private Vector3 _newPos;
    private bool _moving;
    private bool _jumping;


    void Start()
    {
        _curPos = transform.position;
        _newPos = transform.position;
        _jumping = false;

        cam = Camera.main;
        mover = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        // Moving player

        _curPos = transform.position;

            if (Input.GetMouseButtonDown(0))
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

        // UI
        canvas.GetComponent<SliderJoint2D>();
        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumping = true;
        }

    }
    void LateUpdate()
    {

        // If we press right mouse
        if (Input.GetMouseButtonDown(0))

        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                mover.MoveToPoint(hit.point);
                // Move player

                // Stop focusing
            }
        }
    }

    public bool isJumping()
    {
        return _jumping;
    }
}

