using UnityEngine;

[CreateAssetMenu(menuName = "Game/Restaurant Upgrade")]
public class RestaurantUpgradeSO : ScriptableObject
{
    [SerializeField] private string upgradeName;
    [SerializeField] private int maxUpgradeLevel = 3;
    [SerializeField] private int[] upgradeCosts;
    [SerializeField] private float[] values;

    private int currentLevel;
    private int pendingLevel;
    private bool unlocked;

    public string UpgradeName => upgradeName;
    public int CurrentLevel => currentLevel;
    public int PendingLevel => pendingLevel;
    public bool IsUnlocked => unlocked;

    public void UnlockUpgrade()
    {
        unlocked = true;
    }

    public int GetCurrentLevel()
    {
        return currentLevel + pendingLevel;
    }

    public bool CanUpgrade()
    {
        if (!unlocked) return false;
        return currentLevel + pendingLevel < maxUpgradeLevel;
    }

    public bool IsMaxLevel()
    {
        return GetCurrentLevel() >= maxUpgradeLevel;
    }

    public int GetCost()
    {
        if (upgradeCosts == null || upgradeCosts.Length == 0)
            return 0;

        int level = Mathf.Clamp(GetCurrentLevel(), 0, upgradeCosts.Length - 1);
        return upgradeCosts[level];
    }

    public float GetValue()
    {
        if (values == null || values.Length == 0)
            return 0;

        int level = Mathf.Clamp(currentLevel, 0, values.Length - 1);
        return values[level];
    }

    public void PurchaseUpgrade()
    {
        if (!CanUpgrade()) return;

        pendingLevel++;
    }

    public void ApplyUpgrade()
    {
        currentLevel += pendingLevel;
        pendingLevel = 0;
    }

    private void OnEnable()
    {
        currentLevel = 0;
        pendingLevel = 0;
        unlocked = false;
    }
}