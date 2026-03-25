using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public int FetchCurrentLevel() => currentLevel;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        if (LevelSelection.selectedLevel.levelID != 0)
        {
            InitializeLevel(
                LevelSelection.selectedLevel.levelID,
                LevelSelection.selectedLevel.startingDay,
                LevelSelection.selectedLevel.maxDays
            );
        }
        else
        {
            Debug.LogWarning("No level selected! Defaulting to Level 1.");
            InitializeLevel(1, 1, 5);
        }
        
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
        if (dayCounter >= maxDays)
        {
            Debug.Log("No more days!");
            SceneManager.LoadScene("LevelSelection");
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