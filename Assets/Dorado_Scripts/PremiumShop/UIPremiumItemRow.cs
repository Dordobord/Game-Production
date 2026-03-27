using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPremiumItemRow : MonoBehaviour
{
    [SerializeField] private PremiumItemSO item;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI costTxt;
    [SerializeField] private TextMeshProUGUI ownedTxt;

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    public void Buy()
    {
        PremiumShopManager.main.Buy(item);
        RefreshUI();
    }

    public void Use()
    {
        PremiumShopManager.main.Use(item);
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (item == null) return;

        titleTxt.text = item.itemName;
        costTxt.text = $"Cost: {item.cost}";

        int owned = ShopInventoryManager.main.GetOwned(item);
        ownedTxt.text = $"Owned: {owned}";
    }
}