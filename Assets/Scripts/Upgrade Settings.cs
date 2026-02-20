using UnityEngine;

[CreateAssetMenu(fileName = "RestaurantUpgradeSettings", menuName = "Game/Restaurant Upgrade Settings")]
public class RestaurantUpgradeSettings : ScriptableObject
{
    [Header("Player Upgrades")]
    public float moveSpeedMultiplier = 1f;

    [Header("Kitchen Upgrades")]
    public float cookSpeedMultiplier = 1f;

    [Header("Economy")]
    public float incomeMultiplier = 1f;
}

