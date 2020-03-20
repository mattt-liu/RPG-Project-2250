﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{

    public LayerMask mask;
    public Camera cam;
    public PlayerMovement mover;

    public int currentLevel;

    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<PlayerMovement>();
    }

    void Update()
    {
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
}