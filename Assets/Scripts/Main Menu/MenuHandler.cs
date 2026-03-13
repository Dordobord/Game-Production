using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler main { get; private set; }

    [Header("Main Menu and Menu Items")]
    public List<ItemMenuData> mainMenu = new List<ItemMenuData>();
    public List<ItemMenuData> listOfItems = new List<ItemMenuData>();

    private void Awake() => main = this;

    public void SetMainMenu(int day)
    {
        if(listOfItems == null || listOfItems.Count == 0) return;

        foreach(ItemMenuData item in listOfItems)
        {
            if(item.dayRequirement <= day)
            {
                // if(){} <= add kitchen station condition here
                mainMenu.Add(item);
            }
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
