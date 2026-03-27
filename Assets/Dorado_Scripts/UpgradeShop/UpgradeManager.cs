using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager main {get; private set;}

    [Header("Upgrades")]
    [SerializeField]private UpgradeSO[] upgrades;

    void Awake()
    {
        main = this;
        ResetAllUpgrades();
    }

    public void ApplyUpgrades()
    {
        foreach (UpgradeSO upgrade in upgrades)
        {
            upgrade.ApplyUpgrade();
        }
    }

    public void TryUpgrade(UpgradeSO upgrade)
    {
        if (!upgrade.CanUpgrade())
        {
            Debug.Log("Upgrade can't be purchased");
            return;
        }

        int cost = upgrade.GetUpgradeCost();

        if (!PlayerWallet.main.SpendMoney(cost))
        {
            Debug.Log("Not enough money");
            return;
        }

        upgrade.PurchaseUpgrade();

        Debug.Log($"Purchased upgrade: {upgrade.name}");
    }

    public void ResetAllUpgrades()
    {
        foreach (UpgradeSO upgrade in upgrades)
        {
            upgrade.ResetUpgrade();

            if (upgrade.unlockedByDefault)
            {
                upgrade.UnlockUpgrade();
            }
        }

        Debug.Log("All upgrades reset");
    }

    public bool IsStationUnlocked(ItemCooker cooker)
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade == null) continue;

            if (upgrade.upgradeType != UpgradeType.Station)
                continue;
            
            if (upgrade.cookerType == cooker)
                return upgrade.IsUnlocked;
        }

        return false;
    }
}


