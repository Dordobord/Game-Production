using UnityEngine;

public class Cookbook : MonoBehaviour, IInteractable, IFocusable
{
    private AssemblyStation assemblyStation;
    private UICookbook uiCookbook;
    private void Awake()
    {
        assemblyStation = FindFirstObjectByType<AssemblyStation>();
        uiCookbook = FindFirstObjectByType<UICookbook>();
    }

    public void Interact()
    {
        if (assemblyStation == null || uiCookbook == null) return;

        uiCookbook.OpenCookbook(assemblyStation.Recipes);
    }

    public void OnFocus(){}

    public void OnLoseFocus()
    {
        if (uiCookbook != null && uiCookbook.IsOpen)
            uiCookbook.CloseCookbook();
    }
}