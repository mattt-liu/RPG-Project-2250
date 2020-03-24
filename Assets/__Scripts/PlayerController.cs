using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public LayerMask mask;
    public Camera cam;
    public PlayerMovement mover;
    public Rigidbody rb;

    [Header("Selection")]
    public Inter focus;

    [Header("Game Content")]
    public GameObject canvas;
    public int currentLevel;

    private Vector3 _curPos;
    private Vector3 _newPos;
    private bool _walking;
    private bool _jumping;
    private bool _punching;


    void Start()
    {
        _curPos = transform.position;
        _newPos = transform.position;
        _jumping = false;
        _punching = false;
        rb = GetComponent<Rigidbody>();

        cam = Camera.main;
        mover = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // ---- Moving player ----

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

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && _punching == false)
        {
            Stop();
            _jumping = true;
            Debug.Log("Jumped");
        }

        // punching
        if (Input.GetKeyDown(KeyCode.Q) && _jumping == false)
        {
            Stop();
            _punching = true;
        }

        // UI
        //canvas.GetComponent<SliderJoint2D>();

    }
    void LateUpdate()
    {

        // ---- Checking for movement -----

        _walking = (_newPos.x != _curPos.x) && (_newPos.z != _curPos.z);

        if (_jumping) // **still not working
        {
            rb.AddForce(Vector3.up * 15f);
        }

        // If we press left mouse
        /**
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

        }*/
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
    void Stop()
    {
        _newPos = _curPos;
        _walking = false;
        mover.StopMoving();
    }

    public bool getWalking()
    {
        return _walking;
    }
    public void setWalking(bool b)
    {
        _walking = b;
    }
    public bool getJumping()
    {
        return _jumping;
    }
    public bool getPunching()
    {
        return _punching;
    }
    public void setPunching(bool b)
    {
        _punching = b;
    }
}
