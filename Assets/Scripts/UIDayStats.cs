using TMPro;
using UnityEngine;

public class UIDayStats : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI dayTxt;
    [SerializeField] private TextMeshProUGUI incomeTxt;
    [SerializeField] private TextMeshProUGUI targetTxt;
    [SerializeField] private TextMeshProUGUI expTxt;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void EnablePanel()
    {
        panel.SetActive(true);

        dayTxt.text = $"Day {LevelManager.main.CurrentDay}";
        incomeTxt.text = $"Income: ${LevelManager.main.CurrentIncome}";
        targetTxt.text = $"Target: ${LevelManager.main.TargetIncome}";
        expTxt.text = $"EXP Gained: {LevelManager.main.CurrentExp}";
    }
}