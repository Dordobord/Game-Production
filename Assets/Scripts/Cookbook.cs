using UnityEngine;

public class Cookbook : MonoBehaviour, IInteractable, IFocusable
{
    [SerializeField]private AssemblyStation assemblyStation;
    [SerializeField]private UICookbook uiCookbook;

    public void Interact()
    {
        if (assemblyStation == null || uiCookbook == null) return;

        uiCookbook.OpenCookbook(assemblyStation.Recipes);
    }

    public void OnFocus()
    {
        
    }

    public void OnLoseFocus()
    {
        if (uiCookbook != null && uiCookbook.IsOpen)
            uiCookbook.CloseCookbook();
    }
}