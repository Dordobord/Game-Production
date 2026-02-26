using UnityEngine;

public class PremiumShop : MonoBehaviour, IInteractable, IFocusable
{
    [SerializeField]private UIPremiumShop uiPremiumShop;
    public void Interact()
    {
        if (uiPremiumShop == null) return; 
        uiPremiumShop.OpenVault();
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
