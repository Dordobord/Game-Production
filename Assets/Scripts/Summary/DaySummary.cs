using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaySummary : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject summaryPanel;

    [Header("General Summary")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI quotaText;
    [SerializeField] private TextMeshProUGUI abilityPointsText;

    [Header("Financial Summary")]
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI savingsText;
    [SerializeField] private TextMeshProUGUI utilitiesText;
    [SerializeField] private TextMeshProUGUI stockText;
    [SerializeField] private TextMeshProUGUI totalText;

    [Header("Button Reference")]
    [SerializeField] private Button proceedButton;    
    [SerializeField] private TextMeshProUGUI proceedText;
    
    [Header("Stars")]
    [SerializeField] private UIStarRating starUI; 
    private bool isDaySuccess = true;

    void Awake()
    {
        summaryPanel.SetActive(false);
    }

    public void SummarizeDay()
    {
        isDaySuccess = true;
        float utilityBill = UtilityBillHandler.main.CalculateBill(LevelManager.main.FetchCurrentLevel());
        float stockBill = MenuHandler.main.CalculateStockBill();

        summaryPanel.SetActive(true);

        // General Summary
        dayText.text = $"END OF DAY {DayManager.main.FetchCurrentDay()}";
        quotaText.text = $"{CustomerSpawner.main.GetQuotaCount()} / {CustomerSpawner.main.GetMaxQuota()}";
        abilityPointsText.text = $"{PlayerStats.main.FetchNewPoints()}";

        if(!CustomerSpawner.main.IsQuotaMet()) isDaySuccess = false;

        // Financial Summary
        incomeText.text = $"{PlayerWallet.main.CalculateIncome()}";
        savingsText.text = $"{PlayerWallet.main.FetchSavings()}";
        utilitiesText.text = $"{utilityBill}";
        stockText.text = $"{stockBill}";

        if(!PlayerWallet.main.PayBills(utilityBill, stockBill)) 
        {
            isDaySuccess = false;
            // Display player failed
            totalText.text = ":(";
        }
        else
        {
            totalText.text = $"{PlayerWallet.main.FetchDayIncome()}";
        }

        int stars = CustomerSpawner.main.CalculateRating();
        starUI.SetStarRating(stars);

        if(!isDaySuccess)
            SetUpButton(proceedButton, proceedText, "Try Again", () => LevelManager.main.RestartDay());
        else
            SetUpButton(proceedButton, proceedText, "Continue", () => LevelManager.main.NextDay());
    }

    public void ClosePanel()
    {
        summaryPanel.SetActive(false);
    }

    private void SetUpButton(Button actionButton, TMP_Text textbutton, string message, System.Action onButtonClicked)
    {
        textbutton.text = message;
        actionButton.onClick.RemoveAllListeners();

        actionButton.onClick.AddListener(() => {
            onButtonClicked?.Invoke();
            ClosePanel();
        });
    }
}