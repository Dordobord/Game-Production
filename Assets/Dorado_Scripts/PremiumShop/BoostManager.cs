using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveBoost
{
    public PremiumItemType boostType;
    public int remainingDays;
    public int amount;
}

public class BoostManager : MonoBehaviour
{
    public static BoostManager main;

    public Action OnBoostUpdated;

    private List<ActiveBoost> activeBoosts = new List<ActiveBoost>();

    private void Awake()
    {
        main = this;
    }

    // Activate or refresh a boost
    public void ActivateBoost(PremiumItemType type, int days, int amount)
    {
        // Remove expired boosts
        activeBoosts.RemoveAll(boost => boost.remainingDays <= 0);

        // Check if boost already exists
        ActiveBoost existingBoost = activeBoosts.Find(b => b.boostType == type);

        if (existingBoost != null)
        {
            // Refresh existing boost
            existingBoost.remainingDays = days;
            existingBoost.amount = amount;
        }
        else
        {
            // Add new boost
            activeBoosts.Add(new ActiveBoost
            {
                boostType = type,
                remainingDays = days,
                amount = amount
            });
        }

        OnBoostUpdated?.Invoke();
    }

    public bool IsBoostActive(PremiumItemType type)
    {
        foreach (ActiveBoost boost in activeBoosts)
        {
            if (boost.boostType == type && boost.remainingDays > 0)
                return true;
        }
        return false;
    }

    public void ReduceDays()
    {
        for (int i = activeBoosts.Count - 1; i >= 0; i--)
        {
            activeBoosts[i].remainingDays--;

            if (activeBoosts[i].remainingDays <= 0)
            {
                activeBoosts.RemoveAt(i);
            }
        }

        OnBoostUpdated?.Invoke();
    }

    public int GetRemainingDays(PremiumItemType type)
    {
        foreach (ActiveBoost boost in activeBoosts)
        {
            if (boost.boostType == type)
                return boost.remainingDays;
        }

        return 0;
    }

    public int GetBoostAmount(PremiumItemType type)// for patience manager
    {
        foreach (ActiveBoost boost in activeBoosts)
        {
            if (boost.boostType == type && boost.remainingDays > 0)
                return boost.amount;
        }

        return 0;
    }
}