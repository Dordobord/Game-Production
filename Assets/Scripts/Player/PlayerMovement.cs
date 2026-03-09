using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool moving;
    private Vector2 input;
    private float x;
    private float y;
    private float idleTimer;
    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        WalkAnimation();
        PlayerInput();
        IdleAnimation();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = input * stats.MoveSpeed;
    }

    private void WalkAnimation()
    {
        if (input.magnitude > 0.1f || input.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            _anim.SetFloat("x", x);
            _anim.SetFloat("y", y);
        }

        _anim.SetBool("Moving", moving);
    }

    private void IdleAnimation()
    {
        if (input.magnitude < 0.1f)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0f;
        }

        _anim.SetFloat("Idletimer", idleTimer);
    }

    private void PlayerInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }

    public void AllowMovement(bool canMove)
    {
        enabled = canMove;
        _rb.linearVelocity = Vector2.zero;
    }

}
