using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Game/Restaurant Upgrade")]
public class RestaurantUpgradeSO : ScriptableObject
{
    [SerializeField] private string upgradeName;
    [SerializeField] private int maxUpgradeLevel = 3;
    [SerializeField] private int[] upgradeCosts;
    [SerializeField] private float[] values;

    private int currentLevel;
    private int pendingLevel;

    public string UpgradeName => upgradeName;
    public int CurrentLevel => currentLevel;
    public int PendingLevel => pendingLevel;

    public int GetCurrentLevel()
    {
        return currentLevel + pendingLevel;
    }

    public bool CanUpgrade()
    {
        return currentLevel + pendingLevel < maxUpgradeLevel;
    }

    public bool IsMaxLevel()
    {
        return GetCurrentLevel() >= maxUpgradeLevel;
    }
    public int GetCost()
    {
        int level = GetCurrentLevel();

        if (level >= upgradeCosts.Length)
            return 0;

        return upgradeCosts[level];
    }

    public float GetValue()
    {
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

    private void OnEnable()//Reset upgrade purchase when play mode (temporary).
    {
        currentLevel = 0;
        pendingLevel = 0;
    }
}