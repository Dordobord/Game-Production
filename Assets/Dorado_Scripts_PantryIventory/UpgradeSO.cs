using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Restaurant/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    [Header("Category")]
    public UpgradeType upgradeType;

    [Header("Station Type")]
    public ItemCooker cookerType;// disregard cookertype if the category is Furniture

    [Header("Upgrade Info")]
    public string UpgradeName;

    [Header("Unlock Requirements")]
    public int requiredLevel;
    public int requiredDay;

    [Header("Unlock Cost")]
    [SerializeField] private int unlockCost = 5;

    [Header("Upgrade Levels")]
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int maxLevel = 3;

    [Header("Upgrade Costs")]
    [SerializeField] private int[] upgradeCosts;

    [Header("Upgrade Values")]
    [SerializeField] private float[] upgradeValues;

    [Header("State")]
    public bool unlockedByDefault;

    public bool IsUnlocked = false;
    public event Action OnUpgradeUnlocked;

    public int GetUnlockCost()
    {
        return unlockCost;
    }

    public int GetUpgradeCost()
    {
        if (upgradeCosts == null || upgradeCosts.Length == 0)
            return 0;
            
        if (currentLevel >= upgradeCosts.Length)
            return 0;

        return upgradeCosts[currentLevel];
    }

    // Current upgrade level
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Can the upgrade still level up
    public bool CanUpgrade()
    {
        return currentLevel < maxLevel;
    }

    // Check if already maxed
    public bool IsMaxLevel()
    {
        return currentLevel >= maxLevel;
    }

    // Unlock the station
    public void UnlockUpgrade()
    {
        if (IsUnlocked) return;

        IsUnlocked = true;
        OnUpgradeUnlocked?.Invoke();
    }

    // Increase upgrade level
    public void PurchaseUpgrade()
    {
        if (CanUpgrade())
        {
            currentLevel++;
        }
    }

    // Value used by machines (like cooking speed reduction)
    public float GetValue()
    {
       if (!IsUnlocked || currentLevel <= 0)
        return 0f;

        if (upgradeValues == null || upgradeValues.Length == 0)
            return 0f;

        int index = Mathf.Clamp(currentLevel - 1, 0, upgradeValues.Length - 1);

        return upgradeValues[index];
    }

    // Called every day when upgrades are applied
    public void ApplyUpgrade()
    {
        if (!IsUnlocked)
            return;

        Debug.Log("Applying upgrade: " + UpgradeName);
    }

    // Optional: reset upgrade (useful for new game)
    public void ResetUpgrade()
    {
        currentLevel = 0;
        IsUnlocked = unlockedByDefault;
    }
}

public enum UpgradeType
{
    Station,
    Furniture
}