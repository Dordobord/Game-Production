using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler main { get; private set; }

    [Header("Main Menu and Menu Items")]    
    [SerializeField] private List<ItemMenuData> mainMenu = new List<ItemMenuData>();
    [SerializeField] private List<ItemMenuData> listOfItems = new List<ItemMenuData>();

    public List<ItemMenuData> GetTodayMenu() => mainMenu;


    private void Awake() => main = this;

    public void SetMainMenu(int day)
    {
        if(listOfItems == null || listOfItems.Count == 0) return;

        mainMenu.Clear();

        int currentLevel = LevelManager.main .FetchCurrentLevel();

        foreach(ItemMenuData item in listOfItems)
        {
            bool unlockByDay = item.dayRequirement <= day;
            bool unlockByLevel = item.levelRequirement <= currentLevel;
            bool stationUnlocked = UpgradeManager.main.IsStationUnlocked(item.cooker);

            if (unlockByDay && unlockByLevel && stationUnlocked)
            {
                mainMenu.Add(item);
            }
        }

        Debug.Log($"Menu for Level {currentLevel} - Day {day}");

        foreach (var item in mainMenu)
        {
            Debug.Log(item.name);
        }
    }

    public float CalculateStockBill()
    {
        if (mainMenu == null || mainMenu.Count == 0) return 0f;

        float stockBill = 0f;

        foreach(ItemMenuData item in mainMenu)
        {
            stockBill += item.restockPrice;
        }

        return stockBill;
    }
}
