using UnityEngine;

public class UpgradeShop : MonoBehaviour, IInteractable, IFocusable
{
    [SerializeField]private UIUpgradeShop uiUpgradeShop;
    public void Interact()
    {
        if (uiUpgradeShop == null) return; 
        uiUpgradeShop.OpenVault();
    }

    public void OnFocus()
    {
        
    }

    public void OnLoseFocus()
    {
        if (uiUpgradeShop != null && uiUpgradeShop.IsOpen)
            uiUpgradeShop.CloseVault();
    }
}