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


    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
        // If we press right mouse
        if (Input.GetMouseButtonDown(0))
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                Inter interactable = hit.collider.GetComponent<Inter>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
}
    // Set our focus to a new focus
    void SetFocus(Inter newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            mover.Follow(newFocus);   // Follow the new focus
        }

        newFocus.OnFocused(transform);
    }

    // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        mover.StopFollowing();
    }
}