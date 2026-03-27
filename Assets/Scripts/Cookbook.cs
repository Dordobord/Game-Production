using UnityEngine;

public class Cookbook : MonoBehaviour, IInteractable, IFocusable
{
    private UICookbook uiCookbook;
    private void Awake()
    {
        uiCookbook = FindFirstObjectByType<UICookbook>();
    }

    public void Interact()
    {
        if (uiCookbook == null) return;

        if (uiCookbook.IsOpen)
        {
            uiCookbook.CloseCookbook();
        }
        else
        {
            uiCookbook.OpenCookbook();
 
        }
    }

    public void OnFocus(){}

    public void OnLoseFocus()
    {
        if (uiCookbook != null && uiCookbook.IsOpen)
            uiCookbook.CloseCookbook();
    }
}