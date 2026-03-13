using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats main { get; private set; }
    [SerializeField]private int level = 1;
    [SerializeField]private int currentExp;
    [SerializeField]private int expToLvlUp = 40;

    [SerializeField]private int abilityPoints;
    
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float efficiency = 1f;
    
    public int Level => level;
    public int AbilityPoints => abilityPoints;
    public float MoveSpeed => moveSpeed;
    public float Efficiency => efficiency;

    void Start()
    {
        main = this;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        IncreaseLevelChecker();

        UIGameHUD.main.UpdateExperience(currentExp, expToLvlUp);
    }

    private void IncreaseLevelChecker()
    {
        if (currentExp < expToLvlUp) return;

        currentExp -= expToLvlUp;
        level++;
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
