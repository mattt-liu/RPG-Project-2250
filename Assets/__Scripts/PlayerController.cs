using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public LayerMask mask;
    public LayerMask enemyMask;
    public LayerMask pause;
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

    [Header("Player Stats")]
    public int maxHealth = 100;
    public int kickDamage = 15;
    public int punchDamage = 10;
    public float kickSpeed = 0.25f;
    public float punchSpeed = 0.75f;

    [Header("Cooldown Bars")]
    public GameObject punchCD;
    public GameObject kickCD;

    //stats
    private int _maxHealth;
    private int _health;
    private int _xp;
    Slider healthSlider;
    Slider xpSlider;

    // movement
    private Vector3 _curPos;
    private Vector3 _newPos;
    private Vector3 _dashPos;
    private bool _walking = false;
    private bool _walkingToEnemy = false;
    private bool _jumping = false;
    private bool _dashing = false;
    private bool _dashed = false;
    private bool _punching = false;
    private bool _kicking = false;
    private EnemyMovement _target;
    private bool _setTarget = false;

    // ability cooldowns
    private bool _punched = false;
    private bool _punchedEnemy = false; // fix refernce in setter and getter
    private bool _canPunch = true;
    private bool _kicked = false;
    private bool _kickedEnemy = false;
    private bool _canKick = true;
    private CooldownController _punchCD;
    private CooldownController _kickCD;
      

    void Start()
    {
        // physics
        _curPos = transform.position;
        _newPos = transform.position;

        // camera
        cam = Camera.main;

        // script components
        mover = GetComponent<PlayerMovement>();

        // health and stats
        _xp = 0;
        _maxHealth = _health = maxHealth;

        healthSlider = healthBar.GetComponent<Slider>();
        xpSlider = xpBar.GetComponent<Slider>();

        healthSlider.value = 100;
        xpSlider.value = 0;

        // ability cooldowns
        _punchCD = punchCD.GetComponent<CooldownController>();
        _kickCD = kickCD.GetComponent<CooldownController>();

        _punchCD.cooldown = 1f / punchSpeed;
        _kickCD.cooldown = 1f / kickSpeed;
    }

    void Update()
    {
    // ---- Moving player -----
        _curPos = transform.position;
        
        // only allow for 1 action at a time
        if (!_dashing && !_jumping && !_kicking && !_punching)
        {
            // walking
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, mask)) // clicked on floor
                {
                    _newPos = hit.point;
                    mover.MoveToPoint(_newPos);
                    _target = null;
                }
                if (Physics.Raycast(ray, out hit, 100, enemyMask)) // clicked on enemy
                {
                    _walkingToEnemy = true;
                    _newPos = hit.point;
                    mover.MoveToPoint(_newPos);
                }
            }
            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopWalking();
                _jumping = true;
            }
            // punching
            if (_canPunch && Input.GetKeyDown(KeyCode.Q))
            {
                StopWalking();
                _punching = true;
                _punched = true;
            }
            // kicking
            if (_canKick && Input.GetKeyDown(KeyCode.W))
            {
                StopWalking();
                _kicking = true;
                _kicked = true;
            }
            //dashing
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopWalking();
                _dashing = true;
            }
        }
    // ---- Check stats ----
        UpdateHealth();
    }
    void FixedUpdate()
    {
        if (_dashing) 
        {
            if (_dashed)
            {
                _dashed = false;
                _dashing = false;

                mover.setAgent(true);
                StopWalking();
                _newPos = _curPos = transform.position;

            }
            else
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, mask))
                {
                    _dashPos = hit.point;
                }

                _dashed = true;

                mover.setAgent(false);

                float maxDist = 5f;

                float dist = Mathf.Sqrt(Mathf.Pow((_dashPos.x - transform.position.x), 2f) + Mathf.Pow((_dashPos.z - transform.position.z), 2f));

                dist = dist > maxDist ? maxDist : dist;

                Vector3 direction = (_dashPos - transform.position).normalized * dist;

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
        if (!_dashing)
        {
            _walking = (_newPos.x != _curPos.x) && (_newPos.z != _curPos.z);
        }

        // ---- ability cooldowns
        if (_punched)
        {
            _canPunch = false;
            StartCoroutine(punchCooldown(1 / punchSpeed));
            _punched = false;
            if (_target != null)
            {
                _punchedEnemy = true;
            }

            // ui
            _punchCD.StartCooldown();
        }
        if (_kicked)
        {
            _canKick = false;
            StartCoroutine(kickCooldown(1 / kickSpeed));
            _kicked = false;
            if (_target != null)
            {
                _kickedEnemy = true;
            }
            // ui
            _kickCD.StartCooldown();
        }
        // ---- target enemy ----
        if (_walkingToEnemy && _setTarget && _target != null)
        {
            CheckTarget();
        }
    }

// ------- INTERACTIONS ------------
    public bool WalkingToEnemy
    {
        set { _walkingToEnemy = value; }
        get { return _walkingToEnemy; }
    }
    public bool SetTarget
    {
        get { return _setTarget; }
        set { _setTarget = value;  }
    }
    private void CheckTarget()
    {
        float dist = Vector3.Distance(_target.transform.position, transform.position);
        if (dist <= _target.lookRadius)
        {
            StopWalking();
            _walkingToEnemy = false;
        }
    }
    public void Target(EnemyMovement enemy)
    {
        _target = enemy;
    }
    public int getPunchDamage()
    {
        return punchDamage;
    }
    public int getKickDamage()
    {
        return kickDamage;
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
    // ------ MOVEMENT ---------
    void StopWalking()
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
    public Vector3 getTarget()
    {
        return _newPos;
    }
    // -------- COMBAT --------
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
    public bool getPunched()
    {
        return _punched;
    }
    public void setPunched(bool b)
    {
        _punched = b;
    }
    public bool getKicked()
    {
        return _kicked;
    }
    public void setKicked(bool b)
    {
        _kicked = b;
    }
    private IEnumerator punchCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _canPunch = true;
    }
    private IEnumerator kickCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _canKick = true;
    }
}
