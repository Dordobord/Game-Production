using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DinerLayout
{
    public GameObject layoutPrefab;
    public int levelRequirement;
    public int dayRequirement;
} 

public class LevelManager : MonoBehaviour
{
    public static LevelManager main { get; private set; }
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int maxDays = 5;
    [SerializeField] private int dayCounter = 0;
    [SerializeField] private List<DinerLayout> dinerLayouts = new List<DinerLayout>();
    // [SerializeField] private int totalMoney;

    // private float currentIncome;
    // private int currentExp;

    // public float CurrentIncome => currentIncome;
    // public int TargetIncome => levels[currentLevelIndex].targetIncome;
    // public int CurrentDay => currentLevelIndex + 1;
    // public int CurrentExp => currentExp;
    // public int TotalMoney => totalMoney;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        StartDay(); // TODO: put this in level selection manager to start the game
    }

    public void StartDay()
    {

    }

    public void NextDay()
    {
        if (dayCounter > maxDays)
        {
            Debug.Log("No more days!");
            // TODO: go to next level/level selection screen
            return;
        }

        dayCounter++;

        Debug.Log("Applying upgrades for next day");
        UpgradeManager.main?.ApplyUpgrades();

        StartDay();
    }
}