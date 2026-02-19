using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory main;

    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 4;

    public List<ItemType> inventory = new List<ItemType>();

    // Event fired when inventory changes
    public event Action OnInventoryChanged;

    private void Awake()
    {
        // Singleton setup
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    private void Start()
    {
        // Force initial UI sync
        OnInventoryChanged?.Invoke();
    }

    // Add item safely
    public bool AddItem(ItemType item)
    {
        if (inventory.Count >= maxSlots)
            return false;

        inventory.Add(item);
        Debug.Log("Added item: " + item);

        OnInventoryChanged?.Invoke();
        return true;
    }

    // Remove item safely
    public bool RemoveItem(ItemType item)
    {
        if (!inventory.Contains(item))
            return false;

        inventory.Remove(item);
        Debug.Log("Removed item: " + item);

        OnInventoryChanged?.Invoke();
        return true;
    }

    // Check item
    public bool HasItem(ItemType item)
    {
        return inventory.Contains(item);
    }
}
