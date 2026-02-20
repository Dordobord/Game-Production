using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool moving;
    private Vector2 input;
    private float x;
    private float y;
  
    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        WalkAnimation();
        PlayerInput();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = input * speed;
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

    private void PlayerInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }

}
