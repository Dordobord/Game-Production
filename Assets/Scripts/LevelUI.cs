using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI dayTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI targetIncomeTxt;
    [SerializeField] private TextMeshProUGUI currentIncomeTxt;
    [SerializeField] private TextMeshProUGUI currentExpTxt;

    [Header("References")]
    [SerializeField] private DayTimer dayManager;

    void Update()
    {
        if (LevelManager.main == null || dayManager == null) return;

        dayTxt.text = $"Day: {LevelManager.main.CurrentDay}";
        timerTxt.text = $"Time: {Mathf.Ceil(dayManager.GetRemainingTime())}";

        targetIncomeTxt.text = $"Target: ${LevelManager.main.TargetIncome}";

        currentIncomeTxt.text = $"Income: ${LevelManager.main.CurrentIncome}";

        currentExpTxt.text = $"EXP: {LevelManager.main.CurrentExp}";
    }
}