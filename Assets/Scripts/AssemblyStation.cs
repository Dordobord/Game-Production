using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AssemblyStation : MonoBehaviour, IInteractable
{
    [SerializeField] private List<ItemMenuData> menuOfTheDay = new List<ItemMenuData>();

    public List<ItemMenuData>  Recipes => menuOfTheDay;
    private PlayerInventory playerInventory;

    private void Start()
    {
        menuOfTheDay = MenuHandler.main.GetTodayMenu();
    }

    private PlayerInventory GetInventory()
    {
        if (playerInventory == null)
            playerInventory = PlayerInventory.main;

        return playerInventory;
    }

    public void Interact()
    {
        Debug.Log("Player attempting to assemble.");
        PlayerInventory inv = GetInventory();

        if (inv == null)
        {
            Debug.LogError("PlayerInventory not found");
            return;
        }

        foreach (ItemMenuData menuItem in menuOfTheDay)
        {
            if (CanAssemble(menuItem, inv))
            {
                Assemble(menuItem, inv);
                return;
            }
        }

        Debug.Log("No matching recipe.");
    }

    private bool CanAssemble(ItemMenuData menuItem, PlayerInventory inv)
    {
        if (menuItem == null || menuItem.ingredients.Count < 1)
            return false;

        foreach (ItemType item in menuItem.ingredients)
        {
            if (!inv.HasItem(item))
                return false;
        }

        return true;
    }

    private void Assemble(ItemMenuData menuItem, PlayerInventory inv)
    {
        foreach (ItemType item in menuItem.ingredients)
            inv.RemoveItem(item);

        if (inv.AddItem(menuItem.dishItem))
        {
            Debug.Log("Crafted: " + menuItem.dishItem);
            TutorialManager.main?.OnFoodAssembled();
        }
        else
            Debug.Log("Inventory full!");
    }
}
