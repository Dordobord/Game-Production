using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager main {get; private set;}

    [Header("Upgrades")]
    [SerializeField]private RestaurantUpgradeSO tables;
    [SerializeField]private RestaurantUpgradeSO chairs;
    [SerializeField]private RestaurantUpgradeSO plates;
    [SerializeField]private RestaurantUpgradeSO coffeeMachine;
    [SerializeField]private RestaurantUpgradeSO fryer;
    [SerializeField]private RestaurantUpgradeSO grill;

    public RestaurantUpgradeSO Tables => tables;
    public RestaurantUpgradeSO Chairs => chairs;
    public RestaurantUpgradeSO Plates => plates;
    public RestaurantUpgradeSO CoffeeMachine => coffeeMachine;
    public RestaurantUpgradeSO Fryer => fryer;
    public RestaurantUpgradeSO Grill => grill;

    private RestaurantUpgradeSO[] upgrades;

    void Awake()
    {
        main = this;

        upgrades = new RestaurantUpgradeSO[]
        {
            tables,
            chairs,
            plates,
            coffeeMachine,
            fryer,
            grill
        };
    }

    public void ApplyUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.ApplyUpgrade();
        }

        Debug.Log("Upgrades applied for next day.");
    }

    public void TryBuyUpgrade(RestaurantUpgradeSO upgrade)
    {
        Debug.Log("Trying to buy upgrade");
        
        if(!upgrade.CanUpgrade()) return;

        int cost = upgrade.GetCost();

        if (!LevelManager.main.TrySpendMoney(cost))
        {
            Debug.Log("not enough money");
            return;
        }

        upgrade.PurchaseUpgrade();
        Debug.Log("purchased upgrade");
    }
    
    public void UpgradeTables()
    {
        TryBuyUpgrade(tables);
    }

    public void UpgradeChairs()
    {
        TryBuyUpgrade(chairs);
    }

    public void UpgradePlates()
    {
        TryBuyUpgrade(plates);
    }

    public void UpgradeCoffeeMachine()
    {
        TryBuyUpgrade(coffeeMachine);
    }

    public void UpgradeFryer()
    {
        TryBuyUpgrade(fryer);
    }

    public void UpgradeGrill()
    {
        TryBuyUpgrade(grill);
    }
}


