using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeRow : MonoBehaviour
{
    [Header("Upgrade Station Data")]
    [SerializeField] private UpgradeSO upgrade;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private TextMeshProUGUI lockTxt;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private GameObject lockPanel;

    [Header("Upgrade Buttons")]
    [SerializeField] private Button[] upgradeBtns;
    [SerializeField] private Color lockedCol;
    [SerializeField] private Color unlockedCol;

    void Start()
    {
        RefreshUI();
    }

    void OnEnable()
    {
        if (LevelManager.main != null && DayManager.main != null)
        {
            RefreshUI();
        }
    }

    public void Purchase()
    {
        int level = LevelManager.main.FetchCurrentLevel();
        int day = DayManager.main.FetchCurrentDay();

        // Check unlock requirement
        if (level < upgrade.requiredLevel || day < upgrade.requiredDay)
        {
            Debug.Log("Requirement not met yet.");
            return;
        }

        int cost = upgrade.GetUnlockCost();

        if (!PlayerWallet.main.SpendMoney(cost))
        {
            Debug.Log("Not enough money to buy station: " + upgrade.UpgradeName);
            return;
        }

        upgrade.UnlockUpgrade();

        Debug.Log("Station purchased: " + upgrade.UpgradeName);

        MenuHandler.main.SetMainMenu(day);

        RefreshUI();
    }

    public void Upgrade()
    {
        if (upgrade == null) return;

        UpgradeManager.main.TryUpgrade(upgrade);

        RefreshUI();
    }

    public void RefreshUI()
    {
        int level = LevelManager.main.FetchCurrentLevel();
        int day = DayManager.main.FetchCurrentDay();

        titleTxt.text = upgrade.UpgradeName;

        bool requirementMet = level >= upgrade.requiredLevel && day >= upgrade.requiredDay;

        if (!requirementMet)
        {
            lockTxt.text = "Unlocks at Level " + upgrade.requiredLevel + " Day " + upgrade.requiredDay;
            lockTxt.color = Color.yellow;

            priceTxt.text = "";

            purchaseButton.gameObject.SetActive(false);

            if (lockPanel != null)
                lockPanel.SetActive(true);

            DisableUpgradeButtons();
            return;
        }

        if (!upgrade.IsUnlocked)
        {
            int unlockCost = upgrade.GetUnlockCost();

            lockTxt.text = "Buy " + upgrade.UpgradeName + " ($" + unlockCost + ")";
            priceTxt.text = "$" + unlockCost;

            purchaseButton.gameObject.SetActive(true);

            if (lockPanel != null)
                lockPanel.SetActive(true);

            DisableUpgradeButtons();
            return;
        }

        lockTxt.text = "";

        purchaseButton.gameObject.SetActive(false);

        if (lockPanel != null)
            lockPanel.SetActive(false);

        UpdateUpgradeButtons();
    }

    private void DisableUpgradeButtons()
    {
        for (int i = 0; i < upgradeBtns.Length; i++)
        {
            Button btn = upgradeBtns[i];
            btn.interactable = false;

            Image img = btn.GetComponent<Image>();
            img.color = lockedCol;
        }
    }

    private void UpdateUpgradeButtons()
    {
        if (upgrade.IsMaxLevel())
        {
            priceTxt.text = "MAX";
        }
        else
        {
            priceTxt.text = "$" + upgrade.GetUpgradeCost();
        }

        int level = upgrade.GetCurrentLevel();

        for (int i = 0; i < upgradeBtns.Length; i++)
        {
            Image img = upgradeBtns[i].GetComponent<Image>();

            if (i < level)
                img.color = unlockedCol;
            else
                img.color = lockedCol;

            Button btn = upgradeBtns[i];

            if (i == level && !upgrade.IsMaxLevel())
                btn.interactable = true;
            else
                btn.interactable = false;
        }
    }
}