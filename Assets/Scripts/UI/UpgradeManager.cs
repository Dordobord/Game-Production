using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager main {get; private set;}

    [Header("Upgrades")]
    [SerializeField]private RestaurantUpgradeSO[] upgrades;

    void Awake()
    {
        main = this;
    }

    public void ApplyUpgrades()
    {
        foreach (RestaurantUpgradeSO upgrade in upgrades)
        {
            upgrade.ApplyUpgrade();
        }
    }

    public void TryUpgrade(RestaurantUpgradeSO upgrade)
    {
        if (!upgrade.CanUpgrade())
        {
            Debug.Log("Upgrade can't be purchased");
            return;
        }

        int cost = upgrade.GetCost();

        if (!PlayerWallet.main.SpendMoney(cost))
        {
            Debug.Log("Not enough money");
            return;
        }

        upgrade.PurchaseUpgrade();

        Debug.Log($"Purchased upgrade: {upgrade.name}");
    }
}


