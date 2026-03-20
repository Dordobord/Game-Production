using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIAbility : MonoBehaviour
{
    public static UIAbility main {get; private set;}
    [SerializeField] private GameObject panel;
    [SerializeField] private Image experienceBar; 
    [SerializeField] private TextMeshProUGUI experienceValueText;
    [SerializeField] private TextMeshProUGUI abilityPointsTxt;
    [SerializeField] private TextMeshProUGUI moveSpeedTxt;
    [SerializeField] private TextMeshProUGUI efficiencyTxt;

    [SerializeField] private PlayerStats stats;

    void Awake()
    {
        main = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if(panel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        RefreshPanel();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    private void RefreshPanel()
    {
        // Experience bar
        float currentXP = PlayerStats.main.TotalExperience;
        float maxXP = PlayerStats.main.MaxExperience;
        float progress = currentXP / maxXP;

        experienceBar.fillAmount = Mathf.Clamp01(progress);

        // Experience value text
        if (experienceValueText != null) 
            experienceValueText.text = $"{currentXP} / {maxXP}";

        // Texts
        abilityPointsTxt.text = $"Points: {stats.AbilityPoints}";
        moveSpeedTxt.text = $"Move Speed: {stats.MoveSpeed}";
        efficiencyTxt.text = $"Efficiency: {stats.Efficiency}";
    }

    public void IncreaseMoveSpeed()
    {
        stats.IncreaseMoveSpeed(1f);
        RefreshPanel();
    }

    public void IncreaseEfficiency()
    {
        stats.IncreaseEfficiency(0.1f);
        RefreshPanel();
    }
}
