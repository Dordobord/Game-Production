using System;
using System.Collections.Generic;
using UnityEngine;

public class PantryInventory : MonoBehaviour, IInteractable
{
    [SerializeField]private int slots = 9;
    [SerializeField]private List<ItemType> allowedItems;
    [SerializeField]private ItemType spawnItem;
    private List<ItemType> items = new List<ItemType>();

    public event Action OnPantryChanged;

    private void Start()
    {
        for (int i = 0; i < slots; i++)
        {
            if (allowedItems.Contains(spawnItem))
            {
                items.Add(spawnItem);
            }
        }

        OnPantryChanged?.Invoke();
    }
    public bool AddItem(ItemType item)
    {
        if (!allowedItems.Contains(item))
        {
            Debug.Log("Item not allowed in this pantry");
            return false;
        }

        if (items.Count >= slots)
        {
            return false;
        }

        items.Add(item);

        OnPantryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(ItemType item)
    {
        if (!items.Contains(item))
            return false;
        
        items.Remove(item);

        OnPantryChanged?.Invoke();
        return true;
    }

    public ItemType GetItem(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return default;
        }

        return items[index];
    }

    public int GetItemCount()
    {
        return items.Count;
    }

    public void Interact()
    {
        UIPantryInventory.main.OpenPanel(this);
    }
}
