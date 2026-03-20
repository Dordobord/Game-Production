using System.Collections.Generic;
using UnityEngine;


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

    [Header("INITIALIZE LEVEL FOR TESTING")]
    [SerializeField] private int initialLevel = 1;
    [SerializeField] private int initialDay = 0;
    [SerializeField] private int initialMaxDay = 5;

    public int FetchCurrentLevel() => currentLevel;

    void Awake()
    {
        main = this;
    }
 
    void Start()
    {
        InitializeLevel(initialLevel, initialDay, initialMaxDay); // TODO: put this in level selection manager to start the game
    }

    public void InitializeLevel(int level, int day, int dayLimit)
    {
        currentLevel = level;
        dayCounter = day;
        maxDays = dayLimit;

        // Set correct diner layout base on level and day
        GameObject layoutPrefab = null;
        foreach (DinerLayout layout in dinerLayouts)
        {
            if(layout.levelRequirement == currentLevel)
            {
                if(layout.dayRequirement == dayCounter)
                {
                    layoutPrefab = layout.layoutPrefab;
                    break;
                }
            }
        }

        DayManager.main.InitializeDay(dayCounter, layoutPrefab);
    }

    public void RestartDay()
    {
        InitializeLevel(currentLevel, dayCounter, maxDays);
    }

    public void NextDay()
    {
        if (dayCounter > maxDays)
        {
            Debug.Log("No more days this level!");
            // TODO: go to next level/level selection screen
            return;
        }

        dayCounter++;

        Debug.Log("Applying upgrades for next day");
        UpgradeManager.main?.ApplyUpgrades();

        PlayerWallet.main?.ConfirmWalletChanges();
        PlayerStats.main?.ConfirmStatsChanges();

        InitializeLevel(currentLevel, dayCounter, maxDays);
    }
}