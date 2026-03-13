using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameHUD : MonoBehaviour
{
     public static UIGameHUD main { get; private set; }

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI dayCounterText;
    [SerializeField] private TextMeshProUGUI quotaCounterText;
    [SerializeField] private TextMeshProUGUI currentIncomeText;

    [Header("Experience Bar")]
    [SerializeField] private Image experienceFillImage; 
    [SerializeField] private TextMeshProUGUI experienceValueText;

    private void Awake() => main = this;
    
    public void UpdateExperience(float currentXP, float maxXP)
    {
        float progress = currentXP / maxXP;

        experienceFillImage.fillAmount = Mathf.Clamp01(progress);

        if (experienceValueText != null) 
            experienceValueText.text = $"{currentXP} / {maxXP}";
    }

    public void UpdateIncome(float currentIncome)
    {
        currentIncomeText.text = $"$ {currentIncome:F2}";
    }

    public void UpdateDay(int dayCount)
    {
        dayCounterText.text = dayCount.ToString();
    }

    public void UpdateQuota(int current, int totalQuota)
    {
        quotaCounterText.text = $"{current} / {totalQuota}";
    }
}