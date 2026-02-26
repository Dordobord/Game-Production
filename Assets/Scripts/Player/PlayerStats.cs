using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]private int level = 1;
    [SerializeField]private int currentExp;
    [SerializeField]private int expToLvlUp = 40;

    [SerializeField]private int abilityPoints;
    
    [SerializeField]private float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    public int Level => level;
    public int AbilityPoints => abilityPoints;

    public void AddExp(int amount)
    {
        currentExp += amount;
        IncreaseLevelChecker();
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
}
