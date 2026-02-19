using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI dayTxt;
    [SerializeField]private TextMeshProUGUI targetIncomeTxt;
    [SerializeField]private TextMeshProUGUI currentIncomeTxt;
    [SerializeField]private TextMeshProUGUI currentExpTxt;

    void Update()
    {
        dayTxt.text = $"Day: {LevelManager.main.CurrentDay}";
        targetIncomeTxt.text = $"Target: ${LevelManager.main.TargetIncome}";
        currentIncomeTxt.text = $"Income: ${LevelManager.main.CurrentIncome}";
        currentExpTxt.text = $"EXP: {LevelManager.main.CurrentExp}";
    }
}
