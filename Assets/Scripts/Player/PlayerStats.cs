using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats main { get; private set; }
    
    [Header("Experience Settings")]
    [SerializeField] private int totalExperience;
    [SerializeField] private int dayExperience = 0;
    [SerializeField] private int maxExp = 40;

    [Header("Ability Settings")]
    [SerializeField] private int abilityPoints;    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float efficiency = 1f;
    
    public int FetchExperience() => dayExperience;
    public void ResetDayExperience() => dayExperience = 0;
    public int AbilityPoints => abilityPoints;
    public float MoveSpeed => moveSpeed;
    public float Efficiency => efficiency;

    void Start()
    {
        main = this;
    }

    public void AddExp(int amount)
    {
        totalExperience += amount;
        dayExperience += amount;
        CheckCurrentExperience();

        UIGameHUD.main.UpdateExperience(totalExperience, maxExp);
    }

    private void CheckCurrentExperience()
    {
        if (totalExperience < maxExp) return;

        totalExperience -= maxExp;
        abilityPoints++;
    }

    public bool CanSpendPoints()
    {
        return abilityPoints > 0;
    }

    public void IncreaseMoveSpeed(float amount)
    {
        if (!CanSpendPoints()) return;

        moveSpeed += amount;
        abilityPoints--;
    }

    public void IncreaseEfficiency(float amount)
    {
        if (!CanSpendPoints()) return;

        efficiency += amount;
        abilityPoints--;
    }
}
