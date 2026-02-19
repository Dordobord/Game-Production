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

    private PlayerInventory playerInventory;

    [System.Obsolete]
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        foreach (Recipe recipe in recipes)
        {
            if (CanCraft(recipe))
            {
                Craft(recipe);
                return;
            }
        }

        Debug.Log("No matching recipe.");
    }

    private bool CanCraft(Recipe recipe)
    {
        foreach (ItemType item in recipe.requiredItems)
        {
            if (!playerInventory.HasItem(item))
                return false;
        }
        return true;
    }

    private void Craft(Recipe recipe)
    {
        foreach (ItemType item in recipe.requiredItems)
        {
            playerInventory.RemoveItem(item);
        }

        bool added = playerInventory.AddItem(recipe.resultItem);

        if (added)
            Debug.Log("Crafted " + recipe.resultItem);
        else
            Debug.Log("Inventory full! Could not craft.");
    }
}
