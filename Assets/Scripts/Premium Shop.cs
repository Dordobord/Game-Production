using UnityEngine;

public class PremiumShop : MonoBehaviour, IInteractable, IFocusable
{
    private UIPremiumShop uiPremiumShop;

    void Awake()
    {
        uiPremiumShop = FindFirstObjectByType<UIPremiumShop>();
    }
    public void Interact()
    {
        if (uiPremiumShop == null) return; 
        
        if (uiPremiumShop.IsOpen)
        {
            uiPremiumShop.CloseVault();
        }
        else
        {
            uiPremiumShop.OpenVault();
        }
    }

    public void OnFocus()
    {
        
    }

    public void OnLoseFocus()
    {
        if (uiPremiumShop != null && uiPremiumShop.IsOpen)
            uiPremiumShop.CloseVault();
    }
}
