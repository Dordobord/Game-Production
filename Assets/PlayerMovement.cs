using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public GameObject targetGameobject;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(movement * speed * Time.deltaTime);

        if (transform.position.x > 0.1f)
        {
            SpriteRenderer target = targetGameobject.GetComponent<SpriteRenderer>();
            transform.Rotate(0, 0, -90);
        }
    }
}
