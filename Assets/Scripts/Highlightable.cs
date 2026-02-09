using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private Color highlightCol;
    private SpriteRenderer sr;
    private Color origColor;
    private bool isHighlighted;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        origColor = sr.color;
    }

    public void Highlight()
    {
        if (isHighlighted) return;

        sr.color = highlightCol;
        isHighlighted = true;
    }

    public void UnHighlight()
    {
        if (!isHighlighted) return;

        sr.color = origColor;
        isHighlighted = false;
    }
}
