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

    public int FetchCurrentLevel() => currentLevel;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        InitializeLevel(1, 0); // TODO: put this in level selection manager to start the game
    }

    public void InitializeLevel(int level, int day)
    {
        currentLevel = level;
        dayCounter = day;

        DayManager.main.InitializeDay(dayCounter, null); // TODO: insert diner layout if there is a layout prefab
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

        DayManager.main.InitializeDay(dayCounter, null);
    }
}