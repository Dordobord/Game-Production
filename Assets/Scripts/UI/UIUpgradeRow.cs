using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeRow : MonoBehaviour
{
    [Header("Upgrade Station Data")]
    [SerializeField]private RestaurantUpgradeSO upgrade;

    [Header("UI")]
    [SerializeField]private TextMeshProUGUI titleTxt;
    [SerializeField]private TextMeshProUGUI priceTxt;
    [SerializeField]private Button[] upgradeBtns;
    [SerializeField]private Color lockedCol;
    [SerializeField]private Color unlockedCol;

    private Image[] buttonImages;


    void Awake()
    {
        buttonImages = new Image[upgradeBtns.Length];

        for (int i = 0; i < upgradeBtns.Length; i++)
        {
            buttonImages[i] = upgradeBtns[i].GetComponent<Image>();
        }
    }
    void Start()
    {
        RefreshUI();
    }

    public void Upgrade()
    {
        if (upgrade == null) return;

        UpgradeManager.main.TryUpgrade(upgrade);

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (upgrade == null) return;

        UpdateTexts();
        UpdateButtons();
    }

    private void UpdateTexts()
    {
        titleTxt.text = upgrade.UpgradeName;

        if (upgrade.IsMaxLevel())
        {
            priceTxt.text = "Max Upgrade";
        }
        else
        {
            priceTxt.text = "$" + upgrade.GetCost();
        }
    }

    private void UpdateButtons()
{
    int level = upgrade.GetCurrentLevel();

    for (int i = 0; i < upgradeBtns.Length; i++)
    {
        bool isUnlocked = i < level;
        bool isNextUpgrade = i == level;

        if (isUnlocked)
        {
            buttonImages[i].color = unlockedCol;
        }
        else
        {
            buttonImages[i].color = lockedCol;
        }

        if (isNextUpgrade)
        {
            upgradeBtns[i].interactable = true;
        }
        else
        {
            upgradeBtns[i].interactable = false;
        }
    }
}
}