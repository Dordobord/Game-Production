using UnityEngine;

[CreateAssetMenu(menuName = "Game/CustomerData")]
public class CustomerData : ScriptableObject
{
    [Header("Customer Information")]
    public CustomerType customerType;
    public float moveSpeed = 5f;
    public float orderAgainChance = 0.6f;

    [Header("Patience Settings")]
    public float minPatience;
    public float maxPatience;
    public float patienceDecreaseRate = 0.25f;

    [Header("Timer Settings")]
    public float thinkingTimer = 5f;
    public float eatingTimer = 10f;

    [Header("Tip Settings")]
    public float tipChance = 0.5f;
    public float tipMultiplier = 1f;

    [Header("Rewards and Penalties")]
    public int expReward = 50;
    public int expPenalty = 20;
}

public enum CustomerType
{
    Basic,
    Homeless,
    FoodCritic,
}
