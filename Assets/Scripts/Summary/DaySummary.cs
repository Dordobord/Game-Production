using TMPro;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class DaySummary : MonoBehaviour
{
    [SerializeField] private GameObject panel;

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

    private bool isDaySuccess = true;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void SummarizeDay()
    {
        float utilityBill = UtilityBillHandler.main.CalculateBill(LevelManager.main.FetchCurrentLevel());
        float stockBill = MenuHandler.main.CalculateStockBill();
        panel.SetActive(true);

        // General Summary
        dayText.text = $"END OF DAY {DayManager.main.FetchCurrentDay()}";
        quotaText.text = $"{CustomerSpawner.main.GetQuotaCount()} / {CustomerSpawner.main.GetMaxQuota()}";
        abilityPointsText.text = $"{PlayerStats.main.AbilityPoints}";

        // Financial Summary
        incomeText.text = $"{PlayerWallet.main.CalculateIncome()}";
        savingsText.text = $"{PlayerWallet.main.FetchTotalMoney()}";
        utilitiesText.text = $"{utilityBill}";
        stockText.text = $"{stockBill}";

        if(!PlayerWallet.main.PayBills(utilityBill, stockBill)) isDaySuccess = false;

        totalText.text = $"{PlayerWallet.main.FetchTotalMoney()}";

        //if(!isDaySuccess)
            //SetUpButton(proceedButton, proceedText, "Try Again", RestartDay());
        //else
            //SetUpButton(proceedButton, proceedText, "Continue", NextDay());
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void SetUpButton(Button actionButton, TMP_Text textbutton, string message, System.Action onButtonClicked)
    {
        textbutton.text = message;
        actionButton.onClick.RemoveAllListeners();

        actionButton.onClick.AddListener(() => {
            onButtonClicked?.Invoke();
        });
    }
}