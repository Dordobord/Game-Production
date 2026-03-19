using UnityEngine;

[CreateAssetMenu(fileName = "ItemMenuData", menuName = "Menu/ItemMenuData")]
public class ItemMenuData : ScriptableObject
{
[Header("Item Menu Data")]
    public ItemType dishItemType;

    [Header("Sprites (Temporary)")]
    public Sprite dishSprite;
    public Sprite cookedSprite;
    public Sprite rawSprite;

    [Header("Item Menu Information")]
    public ItemCategory category;
    public float price;
    public float restockPrice;

    [Header("Cooking Information")]
    public ItemCooker cooker;
    public float cookingTime;

    [Header("Item States")]
    public ItemType cookedItem;
    public ItemType rawItem;

    [Header("Unlockable Settings")]
    public int dayRequirement;
}

public enum ItemCategory
{
    Main,
    Side,
    Drink,
    Dessert,
}

public enum ItemCooker
{
    Grill,
    Fryer,
    Blender,
    CoffeeMachine,
    SodaMachine,
}
