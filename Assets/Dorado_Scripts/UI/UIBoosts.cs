using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBoosts : MonoBehaviour
{
    [SerializeField] private PremiumItemType boostType;
    [SerializeField] private TextMeshProUGUI daysText;
    [SerializeField] private Image icon;

    private void OnEnable()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        while (BoostManager.main == null)
            yield return null;

        BoostManager.main.OnBoostUpdated += UpdateBoostUI;

        yield return null;

        UpdateBoostUI();
    }

    private void OnDisable()
    {
        if (BoostManager.main != null)
            BoostManager.main.OnBoostUpdated -= UpdateBoostUI;
    }

    private void UpdateBoostUI()
    {
        int remainingDays = BoostManager.main.GetRemainingDays(boostType);

        bool isActive = remainingDays > 0;

        icon.gameObject.SetActive(true);
        daysText.text = isActive ? remainingDays.ToString() : "0";
    }
}