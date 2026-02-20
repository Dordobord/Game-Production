using UnityEngine;

[System.Serializable]
public class Recipe
{
    public ItemType[] requiredItems;
    public ItemType resultItem;
}

public class AssemblyStation : MonoBehaviour, IInteractable
{
    [SerializeField] private Recipe[] recipes;

    public Recipe[] Recipes => recipes;
    private PlayerInventory playerInventory;

    private PlayerInventory GetInventory()
    {
        if (playerInventory == null)
            playerInventory = PlayerInventory.main;

        return playerInventory;
    }

    public void Interact()
    {
        PlayerInventory inv = GetInventory();

        if (inv == null)
        {
            Debug.LogError("PlayerInventory not found");
            return;
        }

        foreach (Recipe recipe in recipes)
        {
            if (CanCraft(recipe, inv))
            {
                Craft(recipe, inv);
                return;
            }
        }

        Debug.Log("No matching recipe.");
    }

    private bool CanCraft(Recipe recipe, PlayerInventory inv)
    {
        if (recipe == null || recipe.requiredItems == null)
            return false;

        foreach (ItemType item in recipe.requiredItems)
        {
            if (!inv.HasItem(item))
                return false;
        }

        return true;
    }

    private void Craft(Recipe recipe, PlayerInventory inv)
    {
        foreach (ItemType item in recipe.requiredItems)
            inv.RemoveItem(item);

        if (inv.AddItem(recipe.resultItem))
            Debug.Log("Crafted: " + recipe.resultItem);
        else
            Debug.Log("Inventory full!");
    }
}
