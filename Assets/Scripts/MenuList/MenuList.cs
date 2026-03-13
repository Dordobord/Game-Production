using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuList : MonoBehaviour
{
    public static MenuList main { get; private set; }

    public List<ItemMenuData> mainMenu = new List<ItemMenuData>();
    public List<ItemMenuData> listOfItems = new List<ItemMenuData>();

    private void Awake() => main = this;

    public void SetMainMenu(int day)
    {
        if(listOfItems == null) return;

        foreach(ItemMenuData item in listOfItems)
        {
            if(item.dayRequirement <= day)
            {
                // if(){} <= add kitchen station condition here
                mainMenu.Add(item);
            }
        }
    }
}
