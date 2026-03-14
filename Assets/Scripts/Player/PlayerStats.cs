using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats main { get; private set; }
    
    [Header("Experience Settings")]
    [SerializeField] private int totalExperience;
    [SerializeField] private int dayExperience = 0;
    [SerializeField] private int maxExperience = 1000;

    [Header("Ability Settings")]
    [SerializeField] private int abilityPoints;
    [SerializeField] private int newPoints = 0;       
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float efficiency = 1f;
    
    public int FetchNewPoints() => newPoints;
    public int AbilityPoints => abilityPoints;
    public float MoveSpeed => moveSpeed;
    public float Efficiency => efficiency;

    void Awake() => main = this;
    

    public void AddExp(int amount)
    {
        dayExperience += amount;
        CheckDayExperience();

        UIGameHUD.main.UpdateExperience(dayExperience, maxExperience);
    }

    public void CheckDayExperience()
    {
        if (dayExperience < maxExperience) return;

        dayExperience -= maxExperience;
        newPoints++;
    }

    public void ConfirmStatsChanges()
    {
        totalExperience = dayExperience;
        abilityPoints = newPoints;
    }

    public void ResetNewExperienceAndPoints()
    {
        dayExperience = totalExperience;
        newPoints = abilityPoints;

        UIGameHUD.main.UpdateExperience(dayExperience, maxExperience);
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
