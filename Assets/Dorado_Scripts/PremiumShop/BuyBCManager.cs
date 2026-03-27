using UnityEngine;
using TMPro;

public class BuyBCManager : MonoBehaviour
{
    public static BuyBCManager main;

    [SerializeField] private int totalBcStock;
    [SerializeField] private TextMeshProUGUI bcStockText;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public bool SpendBC(int amount)
    {
        if (totalBcStock < amount)
            return false;

        totalBcStock -= amount;
        UpdateUI();
        return true;
    }

    public void AddBC(int amount)
    {
        totalBcStock += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (bcStockText != null)
            bcStockText.text = $"BC: {totalBcStock}";
    }
}