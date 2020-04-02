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
    public CharacterController controller;

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
    private Vector3 _jumpPos;
    private bool _walking;
    private bool _jumping;
    private bool _jumped;
    private bool _punching;
    private bool _kicking;


    void Start()
    {
        // physics
        _curPos = transform.position;
        _newPos = transform.position;
        _jumping = false;
        _jumped = false;
        _punching = false;

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
        if (Input.GetMouseButtonDown(0) && !_jumping)
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
        if (Input.GetKeyDown(KeyCode.Space) && !_punching && !_kicking)
        {
            Stop();
            _jumping = true;
        }
        // punching
        if (Input.GetKeyDown(KeyCode.Q) && !_jumping && !_kicking)
        {
            Stop();
            _punching = true;
        }
        // kicking
        if (Input.GetKeyDown(KeyCode.W) && !_jumping && !_punching) 
        {
            Stop();
            _kicking = true;
        }

        UpdateHealth();
    }
    void FixedUpdate()
    {
        if (_jumping) 
        {
            if (_jumped)
            {
                _jumped = false;
                _jumping = false;

                mover.setAgent(true);
                Stop();
                _newPos = _curPos = transform.position;

            }
            else
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, mask))
                {
                    _jumpPos = hit.point;
                }

                _jumped = true;

                mover.setAgent(false);

                float maxDist = 5f;

                float dist = Mathf.Sqrt(Mathf.Pow((_jumpPos.x - transform.position.x), 2f) + Mathf.Pow((_jumpPos.z - transform.position.z), 2f));

                dist = dist > maxDist ? maxDist : dist;

                Vector3 direction = (_jumpPos - transform.position).normalized * dist;

                transform.position += direction;

                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);

                //Vector3 jump = new Vector3(0f, 0f, 0f);
                //rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            }
        }
    }
    void LateUpdate()
    {

        // ---- Checking for movement -----
        if (!_jumping)
        {
            _walking = (_newPos.x != _curPos.x) && (_newPos.z != _curPos.z);
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
    public int getDamage()
    {
        return 10; // temp
    }
    public void takeDamage(int x)
    {
        _health -= x;
    }
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
        int hp = (int)((100) * _health / _maxHealth);
        healthSlider.value = hp < 0 ? 0 : hp;
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
    private void OnDrawGizmos()
    {


    }
}
