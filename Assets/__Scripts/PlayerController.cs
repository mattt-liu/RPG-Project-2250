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
