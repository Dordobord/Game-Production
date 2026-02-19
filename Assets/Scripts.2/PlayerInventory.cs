using UnityEngine;
using System.Collections.Generic;


public class PlayerInventory : MonoBehaviour
{

    private int maxSlots = 4;
    public List<ItemType> inventory = new List<ItemType>();
    
    public bool AddItem(ItemType item)
    {
        if (inventory.Count < maxSlots)
        {
            inventory.Add(item);
            return true;
        }
        return false;
    }

    public void RemoveItem(ItemType item){
        inventory.Remove(item);
    }
    
    public bool HasItem(ItemType item)
    {
        return inventory.Contains(item);
    }
    public void TestInventorty()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(ItemType.RawBeef);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {       
            AddItem(ItemType.Bun);
        } 
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveItem(ItemType.RawBeef);
        }

    }

    void Start()
    {
        
    }


    void Update()
    {
    //    TestInventorty();
    }
}
