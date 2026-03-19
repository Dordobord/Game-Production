using UnityEngine;

public class Clipboard : MonoBehaviour, IInteractable, IFocusable
{
    private UIAbility clipboardPanel;

    void Awake()
    {
        clipboardPanel = UIAbility.main;
    }

    public void Interact()
    {
        if(!clipboardPanel.gameObject.activeSelf && clipboardPanel != null)
            clipboardPanel.OpenPanel();
    }

    public void OnFocus()
    {
        
    }

    public void OnLoseFocus()
    {
        if (clipboardPanel != null && clipboardPanel.gameObject.activeSelf)
            clipboardPanel.ClosePanel();
    }
}
