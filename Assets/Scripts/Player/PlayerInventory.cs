using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory main;

    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 4;

    public List<ItemType> inventory = new List<ItemType>();
    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    private void Start()
    {
        OnInventoryChanged?.Invoke();
    }
    public bool AddItem(ItemType item)
    {
        if (inventory.Count >= maxSlots)
            return false;

        inventory.Add(item);
        Debug.Log("Added item: " + item);

        OnInventoryChanged?.Invoke();
        return true;
    }
    public bool RemoveItem(ItemType item)
    {
        if (!inventory.Contains(item))
            return false;

        inventory.Remove(item);
        Debug.Log("Removed item: " + item);

        OnInventoryChanged?.Invoke();
        return true;
    }
    public bool HasItem(ItemType item)
    {
        return inventory.Contains(item);
    }
}
