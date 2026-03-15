using UnityEngine;

public class UpgradeShop : MonoBehaviour, IInteractable, IFocusable
{
    private UIUpgradeShop uiUpgradeShop;

    public void Interact()
    {
        if (uiUpgradeShop == null)
        {
            uiUpgradeShop = FindFirstObjectByType<UIUpgradeShop>();
        }
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