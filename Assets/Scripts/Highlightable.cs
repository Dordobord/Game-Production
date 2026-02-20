using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private Color highlightCol;

    private SpriteRenderer _sr;
    private Color origCol;
    private bool isHighlighted;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        origCol = _sr.color;
    }

    public void Highlight()
    {
        if (isHighlighted) return;

        _sr.color = highlightCol;
        isHighlighted = true;

        if (TryGetComponent(out IFocusable focusable))
        {
            focusable.OnFocus();
        }
    }

    public void UnHighlight()
    {
        if (!isHighlighted) return;

        _sr.color = origCol;
        isHighlighted = false;

        if (TryGetComponent(out IFocusable focusable))
        {
            focusable.OnLoseFocus();
        }
    }
}