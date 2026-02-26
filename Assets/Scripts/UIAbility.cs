using TMPro;
using UnityEngine;

public class UIAbility : MonoBehaviour
{
    [SerializeField]private GameObject panel;
    [SerializeField]private TextMeshProUGUI levelTxt;
    [SerializeField]private TextMeshProUGUI abilityPointsTxt;
    [SerializeField]private TextMeshProUGUI moveSpeedTxt;

    private PlayerStats stats;

    void Awake()
    {
        panel.SetActive(false);
        stats = Object.FindAnyObjectByType<PlayerStats>();
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
        levelTxt.text = $"Level: {stats.Level}";
        abilityPointsTxt.text = $"AP: {stats.AbilityPoints}";
        moveSpeedTxt.text = $"Move Speed: {stats.MoveSpeed}";
    }

    public void IncreaseMoveSpeed()
    {
        stats.IncreaseMoveSpeed(1f);
        RefreshPanel();
    }
}
