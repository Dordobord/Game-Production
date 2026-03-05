using System.Collections.Generic;
using UnityEngine;

public class MenuList : MonoBehaviour
{
    public static List<ItemMenuData> mainMenu = new List<ItemMenuData>();
    [SerializeField] private List<ItemMenuData> menuDayOne;
    [SerializeField] private List<ItemMenuData> menuDayFive;
    [SerializeField] private List<ItemMenuData> menuDayEight;
    [SerializeField] private List<ItemMenuData> menuDayEleven;

    void Start()
    {
        // Start of the game, only the first menu is available
        mainMenu = menuDayOne;
    }

    void Update()
    {
        // Update the menu based on the current day
        int currentDay = LevelManager.main.CurrentDay;

        if (currentDay >= 1 && currentDay < 5)
        {
            mainMenu = menuDayOne;
        }
        else if (currentDay >= 5 && currentDay < 8) // TODO: add kitchen station availability condition here
        {
            //mainMenu = menuDayFive;
        }
        else if (currentDay >= 8 && currentDay < 11)
        {
            //mainMenu = menuDayEight;
        }
        else if (currentDay >= 11)
        {
            //mainMenu = menuDayEleven;
        }
    }
}
