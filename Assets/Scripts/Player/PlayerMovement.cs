using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D _rb;
  
    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y);
        _rb.MovePosition(_rb.position + movement * speed * Time.deltaTime);
    }

}
