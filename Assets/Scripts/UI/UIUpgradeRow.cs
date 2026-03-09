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

    void Start()
    {
        RefreshUI();
    }

    public void Upgrade()
    {
        if (upgrade == null) return;

        UpgradeManager.main.TryBuyUpgrade(upgrade);

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (upgrade == null) return;

        titleTxt.text = upgrade.UpgradeName;

        if (upgrade.IsMaxLevel())
        {
            priceTxt.text = "MAX";
        }
        else
        {
            priceTxt.text = "$" + upgrade.GetCost();
        }
        
        int level = upgrade.GetCurrentLevel();

        for (int i = 0; i < upgradeBtns.Length; i++)
        {
            Image img = upgradeBtns[i].GetComponent<Image>();

            if (i < level)
            {
                img.color = unlockedCol;
            }
            else
            {
                img.color = lockedCol;
            }

            Button btn = upgradeBtns[i];

            if (i == level)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
        }
    }
}