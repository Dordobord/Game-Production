using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField]private SpriteRenderer highlightRenderer;
    private bool isHighlighted;

    private void Awake()
    {
        highlightRenderer.enabled = false;
    }

    public void Highlight()
    {
        if (isHighlighted) return;

        highlightRenderer.enabled = true;
        isHighlighted = true;

        if (TryGetComponent(out IFocusable focusable))
        {
            focusable.OnFocus();
        }
    }

    public void UnHighlight()
    {
        if (!isHighlighted) return;

        highlightRenderer.enabled = false;
        isHighlighted = false;

        if (TryGetComponent(out IFocusable focusable))
        {
            focusable.OnLoseFocus();
        }
    }
}