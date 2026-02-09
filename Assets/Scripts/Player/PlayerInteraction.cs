using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float distance = 1.2f;
    [SerializeField] LayerMask interactMask;

    Vector2 lastDirection = Vector2.down;
    Highlightable currentHighlight;

    void Update()
    {
        FaceDirection();
        HighLightObject();

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void HighLightObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, distance, interactMask);

        if (!hit)
        {
            ClearHighLight();
            return;
        }

        if (!hit.collider.TryGetComponent(out Highlightable highlight))
        {
            ClearHighLight();
            return;
        }

        if (currentHighlight == highlight) return;

        ClearHighLight();
        currentHighlight = highlight;
        currentHighlight.Highlight();
    }

    private void ClearHighLight()
    {
        if (currentHighlight != null)
        {
            currentHighlight.UnHighlight();
            currentHighlight = null;
        }
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, distance, interactMask);

        if (!hit) return;

        if (hit.collider.TryGetComponent(out IInteractable interactable))
            interactable.Interact();
    }

    private void FaceDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x == 0 && y == 0) return;
        
        bool facingUp = Mathf.Abs(y) > Mathf.Abs(x);

        if (facingUp)
        {
            if (y > 0)
                lastDirection = Vector2.up;
            else
                lastDirection = Vector2.down;
        }
        else
        {
            if (x > 0)
                lastDirection = Vector2.right;
            else
                lastDirection = Vector2.left;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3)(lastDirection * distance);

        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(endPos, .05f);
    }
}
