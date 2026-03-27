using UnityEngine;

public class PremiumShopManager : MonoBehaviour
{
    public static PremiumShopManager main { get; private set; }

    [SerializeField] private PremiumItemSO[] items;

    private void Awake()
    {
        main = this;
    }

    public void Buy(PremiumItemSO item)
    {
        if (!BuyBCManager.main.SpendBC(item.cost))
        {
            Debug.Log("Not enough BC");
            return;
        }

        ShopInventoryManager.main.AddOwned(item);
    }

    public void Use(PremiumItemSO item)
    {
        if (!UpgradeManager.main.IsStationUnlocked(item.requiredCooker))
        {
            Debug.Log("Station is locked, can't use item");
            return;
        }

        if (BoostManager.main.IsBoostActive(item.itemType))
        {
            Debug.Log($"{item.itemName} already active!");
            return;
        }

        if (!ShopInventoryManager.main.ConsumeItem(item))
        {
            Debug.Log("No item to use");
            return;
        }


        ApplyEffect(item);
    }

    private void ApplyEffect(PremiumItemSO item)
    {
        BoostManager.main.ActivateBoost(item.itemType, item.durationDays, item.amount);
    }
}