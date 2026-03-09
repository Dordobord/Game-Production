using UnityEngine;

[CreateAssetMenu(fileName = "ItemMenuData", menuName = "Menu/ItemMenuData")]
public class ItemMenuData : ScriptableObject
{
    [Header("Item Menu Data")]
    public ItemType dishItemType;
    public Sprite dishSprite;

    [Header("Item Menu Information")]
    public ItemCategory category;
    public float price;

    [Header("Cooking Information")]
    public ItemCooker cooker;
    public float cookingTime;

    [Header("Item States")]
    public ItemType cookedItem;
    public Sprite cookedSprite;
    public ItemType rawItem;
    public Sprite rawSprite;
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
