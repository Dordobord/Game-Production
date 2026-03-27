using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public PremiumItemSO item;
    public int owned;
}

public class ShopInventoryManager : MonoBehaviour
{
    public static ShopInventoryManager main { get; private set; }

    [SerializeField] private InventoryItem[] ownedItems;

    private void Awake()
    {
        main = this;
    }

    public void AddOwned(PremiumItemSO item)
    {
        foreach (var data in ownedItems)
        {
            if (data.item == item)
            {
                data.owned++;
                return;
            }
        }

        Debug.Log("Item not found in OwnedItems list!");
    }

    public bool Use(PremiumItemSO item)
    {
        foreach (var data in ownedItems)
        {
            if (data.item == item && data.owned > 0)
            {
                data.owned--;
                return true;
            }
        }

        return false;
    }

    public int GetOwned(PremiumItemSO item)
    {
        foreach (var data in ownedItems)
        {
            if (data.item == item)
                return data.owned;
        }

        return 0;
    }
}