using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemMenuData", menuName = "Menu/ItemMenuData")]
public class ItemMenuData : ScriptableObject
{
    [Header("Item Menu Data")]
    public ItemType dishItem;
    public Sprite dishSprite;

    [Header("Item Menu Information")]
    public ItemCategory category;
    public float price;
    public float restockPrice;

    [Header("Cooking Information")]
    public ItemCooker cooker;
    public float cookingTime;

    [Header("Item Requirements")]
    public List<ItemType> ingredients;

    [Header("Unlockable Settings")]
    public int levelRequirement = 1;
    public int dayRequirement = 1;
}

public enum ItemCategory
{
    Main,
    Side,
    Drink,
    Dessert
}

public enum ItemCooker
{
    Grill,
    Fryer,
    Blender,
    CoffeeMachine,
    SodaMachine
}