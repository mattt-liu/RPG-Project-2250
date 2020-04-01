using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject healthBar;
    public GameObject xpBar;
    public int currentLevel;

    //stats
    private int _maxHealth;
    private int _health;
    private int _xp;
    Slider healthSlider;
    Slider xpSlider;

    // movement
    private Vector3 _curPos;
    private Vector3 _newPos;
    private bool _walking;
    private bool _jumping;
    private bool _punching;
    private bool _kicking;


    void Start()
    {
        // physics
        _curPos = transform.position;
        _newPos = transform.position;
        _jumping = false;
        _punching = false;
        rb = GetComponent<Rigidbody>();

        // camera
        cam = Camera.main;

        // script components
        mover = GetComponent<PlayerMovement>();

        // health and stats
        _xp = 0;
        _maxHealth = _health = 100;// temp?

        healthSlider = healthBar.GetComponent<Slider>();
        xpSlider = xpBar.GetComponent<Slider>();

        healthSlider.value = 100;
        xpSlider.value = 0;
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
        if (Input.GetKeyDown(KeyCode.Space) && _punching == false && _kicking == false)
        {
            Stop();
            _jumping = true;
        }

        // punching
        if (Input.GetKeyDown(KeyCode.Q) && _jumping == false && _kicking == false)
        {
            Stop();
            _punching = true;
        }
        // kicking
        if (Input.GetKeyDown(KeyCode.W) && _jumping == false && _punching == false) 
        {
            Stop();
            _kicking = true;
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
    
    // ------- Interactions ------------
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
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        mover.StopFollowing();
    }

    void UpdateHealth()
    {

    }

    // ------ Movement ---------
    void Stop()
    {
        _newPos = _curPos;

        _walking = false;

        mover.MoveToPoint(_curPos);
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
    public bool getKicking()
    {
        return _kicking;
    }
    public void setKicking(bool b)
    {
        _kicking = b;
    }
    public void setJumping(bool b) {
        _jumping = b;
    }
}
